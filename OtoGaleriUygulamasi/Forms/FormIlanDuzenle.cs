using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OtoGaleriUygulamasi.Models;
using OtoGaleriUygulamasi.DAL;
using OtoGaleriUygulamasi.Helpers;

namespace OtoGaleriUygulamasi.Forms
{
    public partial class FormIlanDuzenle : Form
    {
        private int _ilanID;
        private Ilan _mevcutIlan;

        public FormIlanDuzenle(int ilanID)
        {
            InitializeComponent();
            _ilanID = ilanID;
        }

        

        private void FormIlanDuzenle_Load(object sender, EventArgs e)
        {
            // ComboBox'ları doldur
            ComboBoxHelper.MarkalariYukle(cmbMarka);
            ComboBoxHelper.YakitTipleriniYukle(cmbYakitTipi);
            ComboBoxHelper.VitesTipleriniYukle(cmbVitesTipi);
            ComboBoxHelper.KasaTipleriniYukle(cmbKasaTipi);
            ComboBoxHelper.RenkleriYukle(cmbRenk);

            // Yıl ayarları
            nudYil.Minimum = 1950;
            nudYil.Maximum = DateTime.Now.Year + 1;

            // İlan bilgilerini yükle
            IlanBilgileriniYukle();

            // Fotoğrafları yükle
            FotograflariYukle();
        }

        private void IlanBilgileriniYukle()
        {
            DataTable dt = IlanDAL.IlanDetayGetir(_ilanID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                // ComboBox'ları doldur
                cmbMarka.SelectedValue = row["MarkaID"];

                // Model yükle ve seç
                int markaID = Convert.ToInt32(row["MarkaID"]);
                ComboBoxHelper.ModelleriYukle(cmbModel, markaID);
                cmbModel.SelectedValue = row["ModelID"];

                // Diğer alanları doldur
                txtFiyat.Text = row["Fiyat"].ToString();
                nudYil.Value = Convert.ToInt32(row["Yil"]);
                cmbYakitTipi.SelectedValue = row["YakitTipiID"];
                cmbVitesTipi.SelectedValue = row["VitesTipiID"];
                txtKilometre.Text = row["Kilometre"].ToString();
                cmbKasaTipi.SelectedValue = row["KasaTipiID"];
                cmbRenk.SelectedValue = row["RenkID"];
                chkAgirHasar.Checked = Convert.ToBoolean(row["AgirHasarKayitli"]);
                txtAciklama.Text = row["Aciklama"].ToString();
            }
        }

        private void FotograflariYukle()
        {
            lstFotograflar.Items.Clear();
            DataTable dt = FotografDAL.IlanFotograflariniGetir(_ilanID);

            foreach (DataRow row in dt.Rows)
            {
                string yol = row["FotografYolu"].ToString();
                string dosyaAdi = Path.GetFileName(yol);
                lstFotograflar.Items.Add(new
                {
                    ID = row["FotografID"],
                    Yol = yol,
                    Ad = dosyaAdi
                });
            }

            lstFotograflar.DisplayMember = "Ad";
        }

        private void cmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarka.SelectedValue != null)
            {
                int markaID = Convert.ToInt32(cmbMarka.SelectedValue);
                ComboBoxHelper.ModelleriYukle(cmbModel, markaID);
            }
        }

        private void btnFotografSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Multiselect = true;
                ofd.Title = "Fotoğraf Seçin";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Fotoğraf klasörü oluştur
                    string klasorYolu = Path.Combine(Application.StartupPath, "Fotograflar");
                    if (!Directory.Exists(klasorYolu))
                        Directory.CreateDirectory(klasorYolu);

                    foreach (string dosya in ofd.FileNames)
                    {
                        // Dosyayı kopyala
                        string yeniDosyaAdi = $"ilan_{_ilanID}_{Guid.NewGuid()}{Path.GetExtension(dosya)}";
                        string hedefYol = Path.Combine(klasorYolu, yeniDosyaAdi);
                        File.Copy(dosya, hedefYol, true);

                        // Veritabanına kaydet
                        int sira = lstFotograflar.Items.Count + 1;
                        FotografDAL.FotografEkle(_ilanID, hedefYol, sira);
                    }

                    FotograflariYukle();
                    MessageBox.Show("Fotoğraflar başarıyla eklendi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnFotografKaldir_Click(object sender, EventArgs e)
        {
            if (lstFotograflar.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silinecek fotoğrafı seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dynamic secili = lstFotograflar.SelectedItem;
            int fotografID = secili.ID;

            DialogResult result = MessageBox.Show("Fotoğrafı silmek istediğinize emin misiniz?",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (FotografDAL.FotografSil(fotografID))
                {
                    // Dosyayı da sil
                    try
                    {
                        if (File.Exists(secili.Yol))
                            File.Delete(secili.Yol);
                    }
                    catch { }

                    FotograflariYukle();
                    MessageBox.Show("Fotoğraf silindi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (!ValidasyonKontrol())
                return;

            Ilan guncelIlan = new Ilan
            {
                IlanID = _ilanID,
                MarkaID = Convert.ToInt32(cmbMarka.SelectedValue),
                ModelID = Convert.ToInt32(cmbModel.SelectedValue),
                Fiyat = Convert.ToDecimal(txtFiyat.Text),
                Yil = Convert.ToInt32(nudYil.Value),
                YakitTipiID = Convert.ToInt32(cmbYakitTipi.SelectedValue),
                VitesTipiID = Convert.ToInt32(cmbVitesTipi.SelectedValue),
                Kilometre = Convert.ToInt32(txtKilometre.Text),
                KasaTipiID = Convert.ToInt32(cmbKasaTipi.SelectedValue),
                RenkID = Convert.ToInt32(cmbRenk.SelectedValue),
                AgirHasarKayitli = chkAgirHasar.Checked,
                Aciklama = txtAciklama.Text,
                Durum = true
            };

            if (IlanDAL.IlanGuncelle(guncelIlan))
            {
                MessageBox.Show("İlan başarıyla güncellendi!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidasyonKontrol()
        {
            // FormIlanEkle ile aynı validasyon
            if (cmbMarka.SelectedIndex == -1 || cmbModel.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen marka ve model seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFiyat.Text) || Convert.ToDecimal(txtFiyat.Text) <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
