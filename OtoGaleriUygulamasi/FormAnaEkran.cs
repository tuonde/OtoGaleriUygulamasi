using System;
using System.Data;
using System.Windows.Forms;
using OtoGaleriUygulamasi.DAL;
using OtoGaleriUygulamasi.Helpers;

namespace OtoGaleriUygulamasi
{
    public partial class FormAnaEkran : Form
    {
        public FormAnaEkran()
        {
            InitializeComponent();
        }

        private void FormAnaEkran_Load(object sender, EventArgs e)
        {
            // Veritabanı bağlantısını test et
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show("Veritabanına bağlanılamadı! Lütfen bağlantı ayarlarını kontrol edin.",
                    "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IlanlariYukle();
            DataGridViewAyarla();
        }

        private void IlanlariYukle()
        {
            DataTable dt = IlanDAL.TumIlanlariGetir();
            dataGridView1.DataSource = dt;
        }

        private void DataGridViewAyarla()
        {
            // DataGridView görünüm ayarları
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            // Kolon başlıklarını Türkçeleştir
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["IlanID"].HeaderText = "İlan No";
                dataGridView1.Columns["MarkaAdi"].HeaderText = "Marka";
                dataGridView1.Columns["ModelAdi"].HeaderText = "Model";
                dataGridView1.Columns["Fiyat"].HeaderText = "Fiyat";
                dataGridView1.Columns["Fiyat"].DefaultCellStyle.Format = "C2"; // Para formatı
                dataGridView1.Columns["Yil"].HeaderText = "Yıl";
                dataGridView1.Columns["YakitTipiAdi"].HeaderText = "Yakıt";
                dataGridView1.Columns["VitesTipiAdi"].HeaderText = "Vites";
                dataGridView1.Columns["Kilometre"].HeaderText = "KM";
                dataGridView1.Columns["Kilometre"].DefaultCellStyle.Format = "N0"; // Binlik ayraç
                dataGridView1.Columns["KasaTipiAdi"].HeaderText = "Kasa";
                dataGridView1.Columns["RenkAdi"].HeaderText = "Renk";
                dataGridView1.Columns["AgirHasar"].HeaderText = "Ağır Hasar Kaydı";
                dataGridView1.Columns["Durum"].HeaderText = "Durum";
                dataGridView1.Columns["IlanTarihi"].HeaderText = "İlan Tarihi";
                dataGridView1.Columns["IlanTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
            }
        }

        
        private void btnYeniIlan_Click(object sender, EventArgs e)
        {
            // İleride FormIlanEkle açılacak
            MessageBox.Show("İlan ekleme formu yakında eklenecek!");
        }

        // Düzenle butonu
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlenecek ilanı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IlanID"].Value);
            MessageBox.Show("İlan düzenleme formu yakında eklenecek! İlan ID: " + ilanID);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek ilanı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IlanID"].Value);

            DialogResult result = MessageBox.Show("İlanı silmek istediğinize emin misiniz?",
                "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (IlanDAL.IlanSil(ilanID))
                {
                    MessageBox.Show("İlan başarıyla silindi.", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IlanlariYukle();
                }
            }
        }

        private void btnSatildi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen satıldı olarak işaretlenecek ilanı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IlanID"].Value);

            DialogResult result = MessageBox.Show("İlan satıldı olarak işaretlensin mi?",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (IlanDAL.IlanSatildi(ilanID))
                {
                    MessageBox.Show("İlan satıldı olarak işaretlendi.", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IlanlariYukle();
                }
            }
        }
        
        private void btnYeniIlan_Click_1(object sender, EventArgs e)
        {
            // İleride FormIlanEkle açılacak
            MessageBox.Show("İlan ekleme formu yakında eklenecek!");
        }

        private void btnDuzenle_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlenecek ilanı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IlanID"].Value);
            MessageBox.Show("İlan düzenleme formu yakında eklenecek! İlan ID: " + ilanID);
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek ilanı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IlanID"].Value);

            DialogResult result = MessageBox.Show("İlanı silmek istediğinize emin misiniz?",
                "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (IlanDAL.IlanSil(ilanID))
                {
                    MessageBox.Show("İlan başarıyla silindi.", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IlanlariYukle();
                }
            }
        }

        private void btnSatildi_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen satıldı olarak işaretlenecek ilanı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IlanID"].Value);

            DialogResult result = MessageBox.Show("İlan satıldı olarak işaretlensin mi?",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (IlanDAL.IlanSatildi(ilanID))
                {
                    MessageBox.Show("İlan satıldı olarak işaretlendi.", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IlanlariYukle();
                }
            }
        }
    }
}