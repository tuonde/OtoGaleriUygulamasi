using OtoGaleriUygulamasi.DAL;
using OtoGaleriUygulamasi.Helpers;
using OtoGaleriUygulamasi.Models;
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
    public partial class FormIlanEkle : Form
    {
        // Seçilen fotoğrafların yollarını tutacak liste
        private List<string> seciliFotograflar = new List<string>();

        public FormIlanEkle()
        {
            InitializeComponent();
        }

        
        private void FormIlanEkle_Load(object sender, EventArgs e)
        {
            // ComboBox'ları doldur
            ComboBoxHelper.MarkalariYukle(cmbMarka);
            ComboBoxHelper.YakitTipleriniYukle(cmbYakitTipi);
            ComboBoxHelper.VitesTipleriniYukle(cmbVitesTipi);
            ComboBoxHelper.KasaTipleriniYukle(cmbKasaTipi);
            ComboBoxHelper.RenkleriYukle(cmbRenk);

            // Yıl için NumericUpDown ayarları
            nudYil.Minimum = 1950;
            nudYil.Maximum = DateTime.Now.Year + 1;
            nudYil.Value = DateTime.Now.Year;

            // Kilometre için sadece sayı girişi
            txtKilometre.KeyPress += SadeceRakam_KeyPress;
            txtFiyat.KeyPress += SadeceRakam_KeyPress;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Marka seçilince modelleri yükle

            if (cmbMarka.SelectedValue != null && !(cmbMarka.SelectedValue is System.Data.DataRowView))
            {
                int markaID = Convert.ToInt32(cmbMarka.SelectedValue);
                ComboBoxHelper.ModelleriYukle(cmbModel, markaID);
            }
        }

        // 📷 FOTOĞRAF SEÇİMİ
        private void btnFotografSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Multiselect = true; // Çoklu seçim
                ofd.Title = "Fotoğraf Seçin";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string dosyaYolu in ofd.FileNames)
                    {
                        // Aynı fotoğraf tekrar eklenmesin
                        if (!seciliFotograflar.Contains(dosyaYolu))
                        {
                            seciliFotograflar.Add(dosyaYolu);

                            // ListBox'a sadece dosya adını göster
                            string dosyaAdi = Path.GetFileName(dosyaYolu);
                            lstFotograflar.Items.Add(dosyaAdi);
                        }
                    }

                    MessageBox.Show($"{ofd.FileNames.Length} fotoğraf seçildi!", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // 📷 FOTOĞRAF KALDIRMA
        private void btnFotografKaldir_Click(object sender, EventArgs e)
        {
            if (lstFotograflar.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen kaldırılacak fotoğrafı seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int secilenIndex = lstFotograflar.SelectedIndex;

            // Listeden ve koleksiyondan kaldır
            seciliFotograflar.RemoveAt(secilenIndex);
            lstFotograflar.Items.RemoveAt(secilenIndex);

            MessageBox.Show("Fotoğraf kaldırıldı!", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (!ValidasyonKontrol())
                return;

            // Ilan nesnesi oluştur
            Ilan yeniIlan = new Ilan
            {
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
                Durum = true // Yeni ilan satışta
            };

            // Veritabanına ekle
            int yeniIlanID = IlanDAL.IlanEkle(yeniIlan);

            if (yeniIlanID > 0)
            {
                // 📷 FOTOĞRAFLARI KAYDET
                if (seciliFotograflar.Count > 0)
                {
                    FotograflariKaydet(yeniIlanID);
                }

                MessageBox.Show("İlan başarıyla eklendi!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("İlan eklenirken bir hata oluştu!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 📷 FOTOĞRAFLARI KLASÖRE VE VERİTABANINA KAYDET
        private void FotograflariKaydet(int ilanID)
        {
            try
            {
                // Fotograflar klasörünü oluştur
                string klasorYolu = Path.Combine(Application.StartupPath, "Fotograflar");
                if (!Directory.Exists(klasorYolu))
                    Directory.CreateDirectory(klasorYolu);

                int sira = 1;
                foreach (string kaynakDosya in seciliFotograflar)
                {
                    // Yeni dosya adı oluştur (ilan_1_guid.jpg formatında)
                    string uzanti = Path.GetExtension(kaynakDosya);
                    string yeniDosyaAdi = $"ilan_{ilanID}_{Guid.NewGuid()}{uzanti}";
                    string hedefYol = Path.Combine(klasorYolu, yeniDosyaAdi);

                    // Dosyayı kopyala
                    File.Copy(kaynakDosya, hedefYol, true);

                    // Veritabanına kaydet
                    FotografDAL.FotografEkle(ilanID, hedefYol, sira);
                    sira++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fotoğraflar kaydedilirken hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidasyonKontrol()
        {
            if (cmbMarka.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen marka seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMarka.Focus();
                return false;
            }

            if (cmbModel.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen model seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbModel.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFiyat.Text) || Convert.ToDecimal(txtFiyat.Text) <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFiyat.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtKilometre.Text) || Convert.ToInt32(txtKilometre.Text) < 0)
            {
                MessageBox.Show("Lütfen geçerli bir kilometre giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKilometre.Focus();
                return false;
            }

            if (cmbYakitTipi.SelectedIndex == -1 || cmbVitesTipi.SelectedIndex == -1 ||
                cmbKasaTipi.SelectedIndex == -1 || cmbRenk.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Sadece rakam girişine izin ver
        private void SadeceRakam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
