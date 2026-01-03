using OtoGaleriUygulamasi.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtoGaleriUygulamasi.Forms
{
    public partial class FormIlanDetay : Form
    {
        private int _ilanID;
        private DataTable _fotograflar;
        private int _mevcutFotoIndex = 0;
        private DataRow _ilanBilgisi;

        public FormIlanDetay(int ilanID)
        {
            InitializeComponent();
            _ilanID = ilanID;
        }

        private void FormIlanDetay_Load(object sender, EventArgs e)
        {
            IlanBilgileriniYukle();
            FotograflariYukle();
            FotografiGoster();
        }

        private void IlanBilgileriniYukle()
        {
            DataTable dt = IlanDAL.IlanDetayGetir(_ilanID);

            if (dt.Rows.Count > 0)
            {
                _ilanBilgisi = dt.Rows[0];

                // Form başlığı
                this.Text = $"İlan Detay - {_ilanBilgisi["MarkaAdi"]} {_ilanBilgisi["ModelAdi"]}";

                // Bilgileri label'lara doldur
                lblMarkaModel.Text = $"{_ilanBilgisi["MarkaAdi"]} {_ilanBilgisi["ModelAdi"]}";
                lblFiyat.Text = Convert.ToDecimal(_ilanBilgisi["Fiyat"]).ToString("C2");
                lblYil.Text = _ilanBilgisi["Yil"].ToString();
                lblKilometre.Text = Convert.ToInt32(_ilanBilgisi["Kilometre"]).ToString("N0") + " km";
                lblYakitTipi.Text = _ilanBilgisi["YakitTipiAdi"].ToString();
                lblVitesTipi.Text = _ilanBilgisi["VitesTipiAdi"].ToString();
                lblKasaTipi.Text = _ilanBilgisi["KasaTipiAdi"].ToString();
                lblRenk.Text = _ilanBilgisi["RenkAdi"].ToString();
                lblAgirHasar.Text = Convert.ToBoolean(_ilanBilgisi["AgirHasarKayitli"]) ? "Evet" : "Hayır";
                lblIlanTarihi.Text = Convert.ToDateTime(_ilanBilgisi["IlanTarihi"]).ToString("dd.MM.yyyy");

                bool durum = Convert.ToBoolean(_ilanBilgisi["Durum"]);
                lblDurum.Text = durum ? "Satışta" : "Satıldı";
                lblDurum.ForeColor = durum ? Color.Green : Color.Red;

                // Açıklama
                txtAciklama.Text = _ilanBilgisi["Aciklama"].ToString();

                // Eğer satıldıysa satış butonunu devre dışı bırak
                btnSatisYap.Enabled = durum;
            }
        }

        private void FotograflariYukle()
        {
            _fotograflar = FotografDAL.IlanFotograflariniGetir(_ilanID);

            if (_fotograflar.Rows.Count == 0)
            {
                // Fotoğraf yoksa placeholder göster
                picFotograf.Image = null;
                lblFotoSayisi.Text = "Fotoğraf yok";
                btnOnceki.Enabled = false;
                btnSonraki.Enabled = false;
            }
            else
            {
                _mevcutFotoIndex = 0;
                btnOnceki.Enabled = _fotograflar.Rows.Count > 1;
                btnSonraki.Enabled = _fotograflar.Rows.Count > 1;
            }
        }

        private void FotografiGoster()
        {
            if (_fotograflar.Rows.Count == 0)
            {
                picFotograf.Image = null;
                return;
            }

            try
            {
                string fotografYolu = _fotograflar.Rows[_mevcutFotoIndex]["FotografYolu"].ToString();

                if (File.Exists(fotografYolu))
                {
                    // Mevcut resmi bellekten temizle
                    if (picFotograf.Image != null)
                    {
                        picFotograf.Image.Dispose();
                    }

                    // Yeni resmi yükle
                    using (var fs = new FileStream(fotografYolu, FileMode.Open, FileAccess.Read))
                    {
                        picFotograf.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    picFotograf.Image = null;
                    MessageBox.Show("Fotoğraf dosyası bulunamadı!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Fotoğraf sayısını göster
                lblFotoSayisi.Text = $"Fotoğraf {_mevcutFotoIndex + 1} / {_fotograflar.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fotoğraf yüklenirken hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOnceki_Click(object sender, EventArgs e)
        {
            if (_fotograflar.Rows.Count == 0) return;

            _mevcutFotoIndex--;
            if (_mevcutFotoIndex < 0)
                _mevcutFotoIndex = _fotograflar.Rows.Count - 1; // Son fotoğrafa git

            FotografiGoster();
        }

        private void btnSonraki_Click(object sender, EventArgs e)
        {
            if (_fotograflar.Rows.Count == 0) return;

            _mevcutFotoIndex++;
            if (_mevcutFotoIndex >= _fotograflar.Rows.Count)
                _mevcutFotoIndex = 0; // İlk fotoğrafa dön

            FotografiGoster();
        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            // Satış fiyatı al
            decimal ilanFiyati = Convert.ToDecimal(_ilanBilgisi["Fiyat"]);

            string fiyatGirdisi = Microsoft.VisualBasic.Interaction.InputBox(
                $"İlan Fiyatı: {ilanFiyati:C2}\n\nLütfen satış fiyatını giriniz:",
                "Satış Fiyatı",
                ilanFiyati.ToString());

            if (string.IsNullOrWhiteSpace(fiyatGirdisi))
                return;

            if (!decimal.TryParse(fiyatGirdisi, out decimal satisFiyati) || satisFiyati <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"İlan Fiyatı: {ilanFiyati:C2}\nSatış Fiyatı: {satisFiyati:C2}\n\n" +
                $"Fark: {(ilanFiyati - satisFiyati):C2}\n\n" +
                "Satışı onaylıyor musunuz?",
                "Satış Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Fiyatı güncelle ve satıldı olarak işaretle
                if (IlanDAL.IlanSatisFiyatiGuncelle(_ilanID, satisFiyati))
                {
                    MessageBox.Show("Satış başarıyla gerçekleştirildi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormIlanDetay_FormClosing(object sender, FormClosingEventArgs e)
        {
            // PictureBox'taki resmi temizle
            if (picFotograf.Image != null)
            {
                picFotograf.Image.Dispose();
                picFotograf.Image = null;
            }
        }

        private void btnOnceki_Click_1(object sender, EventArgs e)
        {
            if (_fotograflar == null || _fotograflar.Rows.Count == 0)
                return;

            _mevcutFotoIndex--;

            // Eğer ilk fotoğraftaysa, son fotoğrafa git
            if (_mevcutFotoIndex < 0)
                _mevcutFotoIndex = _fotograflar.Rows.Count - 1;

            FotografiGoster();
        }

        private void btnSonraki_Click_1(object sender, EventArgs e)
        {
            if (_fotograflar == null || _fotograflar.Rows.Count == 0)
                return;

            _mevcutFotoIndex++;

            // Eğer son fotoğraftaysa, ilk fotoğrafa dön
            if (_mevcutFotoIndex >= _fotograflar.Rows.Count)
                _mevcutFotoIndex = 0;

            FotografiGoster();
        }

        private void btnSatisYap_Click_1(object sender, EventArgs e)
        {
            // Satış fiyatı al
            decimal ilanFiyati = Convert.ToDecimal(_ilanBilgisi["Fiyat"]);

            // InputBox ile fiyat girişi al
            string fiyatGirdisi = Microsoft.VisualBasic.Interaction.InputBox(
                $"İlan Fiyatı: {ilanFiyati:C2}\n\nLütfen satış fiyatını giriniz:",
                "Satış Fiyatı",
                ilanFiyati.ToString());

            // İptal edildi mi kontrol et
            if (string.IsNullOrWhiteSpace(fiyatGirdisi))
                return;

            // Geçerli bir sayı mı kontrol et
            if (!decimal.TryParse(fiyatGirdisi, out decimal satisFiyati) || satisFiyati <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Onay mesajı
            decimal fark = ilanFiyati - satisFiyati;
            string farkMesaji = fark >= 0
                ? $"Fark: {fark:C2} (İndirim)"
                : $"Fark: {Math.Abs(fark):C2} (Fazla)";

            DialogResult result = MessageBox.Show(
                $"İlan Fiyatı: {ilanFiyati:C2}\n" +
                $"Satış Fiyatı: {satisFiyati:C2}\n" +
                $"{farkMesaji}\n\n" +
                "Satışı onaylıyor musunuz?",
                "Satış Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Fiyatı güncelle ve satıldı olarak işaretle
                if (IlanDAL.IlanSatisFiyatiGuncelle(_ilanID, satisFiyati))
                {
                    MessageBox.Show(
                        $"Satış başarıyla gerçekleştirildi!\n\n" +
                        $"Araç: {_ilanBilgisi["MarkaAdi"]} {_ilanBilgisi["ModelAdi"]}\n" +
                        $"Satış Fiyatı: {satisFiyati:C2}",
                        "Başarılı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Satış işlemi sırasında bir hata oluştu!", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnKapat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormIlanDetay_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            // PictureBox'taki resmi bellekten temizle
            if (picFotograf.Image != null)
            {
                picFotograf.Image.Dispose();
                picFotograf.Image = null;
            }
        }
    }
}
