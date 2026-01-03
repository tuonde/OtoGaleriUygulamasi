using OtoGaleriUygulamasi.DAL;
using OtoGaleriUygulamasi.Forms;
using OtoGaleriUygulamasi.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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

            // Kolon başlıklarını Türkçeleştir - NULL kontrolü ile
            if (dataGridView1.Columns.Count > 0)
            {
                // IlanID
                if (dataGridView1.Columns.Contains("IlanID"))
                    dataGridView1.Columns["IlanID"].HeaderText = "İlan No";

                // Marka
                if (dataGridView1.Columns.Contains("MarkaAdi"))
                    dataGridView1.Columns["MarkaAdi"].HeaderText = "Marka";

                // Model
                if (dataGridView1.Columns.Contains("ModelAdi"))
                    dataGridView1.Columns["ModelAdi"].HeaderText = "Model";

                // Araç (Marka + Model birleşik)
                if (dataGridView1.Columns.Contains("Arac"))
                    dataGridView1.Columns["Arac"].HeaderText = "Araç";

                // Fiyat
                if (dataGridView1.Columns.Contains("Fiyat"))
                {
                    dataGridView1.Columns["Fiyat"].HeaderText = "Fiyat";
                    dataGridView1.Columns["Fiyat"].DefaultCellStyle.Format = "C2";
                }

                // Satış Fiyatı
                if (dataGridView1.Columns.Contains("SatisFiyati"))
                {
                    dataGridView1.Columns["SatisFiyati"].HeaderText = "Satış Fiyatı";
                    dataGridView1.Columns["SatisFiyati"].DefaultCellStyle.Format = "C2";
                }

                // Yıl
                if (dataGridView1.Columns.Contains("Yil"))
                    dataGridView1.Columns["Yil"].HeaderText = "Yıl";

                // Yakıt Tipi
                if (dataGridView1.Columns.Contains("YakitTipiAdi"))
                    dataGridView1.Columns["YakitTipiAdi"].HeaderText = "Yakıt";

                // Vites Tipi
                if (dataGridView1.Columns.Contains("VitesTipiAdi"))
                    dataGridView1.Columns["VitesTipiAdi"].HeaderText = "Vites";

                // Kilometre
                if (dataGridView1.Columns.Contains("Kilometre"))
                {
                    dataGridView1.Columns["Kilometre"].HeaderText = "KM";
                    dataGridView1.Columns["Kilometre"].DefaultCellStyle.Format = "N0";
                }

                // Kasa Tipi
                if (dataGridView1.Columns.Contains("KasaTipiAdi"))
                    dataGridView1.Columns["KasaTipiAdi"].HeaderText = "Kasa";

                // Renk
                if (dataGridView1.Columns.Contains("RenkAdi"))
                    dataGridView1.Columns["RenkAdi"].HeaderText = "Renk";

                // Ağır Hasar
                if (dataGridView1.Columns.Contains("AgirHasar"))
                    dataGridView1.Columns["AgirHasar"].HeaderText = "Ağır Hasar";

                // Durum
                if (dataGridView1.Columns.Contains("Durum"))
                    dataGridView1.Columns["Durum"].HeaderText = "Durum";

                // İlan Tarihi
                if (dataGridView1.Columns.Contains("IlanTarihi"))
                {
                    dataGridView1.Columns["IlanTarihi"].HeaderText = "İlan Tarihi";
                    dataGridView1.Columns["IlanTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                }

                // Satış Tarihi
                if (dataGridView1.Columns.Contains("SatisTarihi"))
                {
                    dataGridView1.Columns["SatisTarihi"].HeaderText = "Satış Tarihi";
                    dataGridView1.Columns["SatisTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                }

                // Satılma Gün Sayısı
                if (dataGridView1.Columns.Contains("SatilmaGunSayisi"))
                    dataGridView1.Columns["SatilmaGunSayisi"].HeaderText = "Satılma Süresi (Gün)";

                // Marka Adı (Raporlar için)
                if (dataGridView1.Columns.Contains("MarkaAdi"))
                    dataGridView1.Columns["MarkaAdi"].HeaderText = "Marka";

                // Toplam Adet
                if (dataGridView1.Columns.Contains("ToplamAdet"))
                    dataGridView1.Columns["ToplamAdet"].HeaderText = "Toplam";

                // Satıştaki Adet
                if (dataGridView1.Columns.Contains("SatistakiAdet"))
                    dataGridView1.Columns["SatistakiAdet"].HeaderText = "Satışta";

                // Satılan Adet
                if (dataGridView1.Columns.Contains("SatilanAdet"))
                    dataGridView1.Columns["SatilanAdet"].HeaderText = "Satılan";

                // Ortalama Fiyat
                if (dataGridView1.Columns.Contains("OrtalamFiyat"))
                {
                    dataGridView1.Columns["OrtalamFiyat"].HeaderText = "Ort. Fiyat";
                    dataGridView1.Columns["OrtalamFiyat"].DefaultCellStyle.Format = "C2";
                }
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
            FormIlanEkle formEkle = new FormIlanEkle();
            if (formEkle.ShowDialog() == DialogResult.OK)
            {
                IlanlariYukle(); // Listeyi yenile
            }
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

            FormIlanDuzenle formDuzenle = new FormIlanDuzenle(ilanID);
            if (formDuzenle.ShowDialog() == DialogResult.OK)
            {
                IlanlariYukle(); // Listeyi yenile
            }
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Başlık satırına tıklanmışsa çık

            int ilanID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["IlanID"].Value);

            FormIlanDetay formDetay = new FormIlanDetay(ilanID);
            if (formDetay.ShowDialog() == DialogResult.OK)
            {
                IlanlariYukle(); // Satış yapıldıysa listeyi yenile
            }
        }

        private void yeniİlanCtrlNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnYeniIlan_Click(sender, e);
        }

        private void çıkışAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?",
        "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void tümİlanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = IlanDAL.TumIlanlariGetir();
            DataGridViewAyarla();
        }

        private void satıştaOlanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = IlanDAL.SatistaOlanIlanlariGetir();
            DataGridViewAyarla();
        }

        private void satılanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT 
            i.IlanID,
            m.MarkaAdi + ' ' + md.ModelAdi AS Arac,
            i.Fiyat AS SatisFiyati,
            i.Yil,
            i.Kilometre,
            i.IlanTarihi,
            i.SatisTarihi,
            DATEDIFF(DAY, i.IlanTarihi, i.SatisTarihi) AS SatilmaGunSayisi
        FROM Ilan i
        INNER JOIN Marka m ON i.MarkaID = m.MarkaID
        INNER JOIN Model md ON i.ModelID = md.ModelID
        WHERE i.Durum = 0 AND i.Aktif = 1
        ORDER BY i.SatisTarihi DESC";

            dataGridView1.DataSource = DatabaseHelper.ExecuteQuery(query);
            DataGridViewAyarla();
        }

        private void gelişmişAramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // İleride FormArama açılacak
            MessageBox.Show("Gelişmiş arama formu yakında eklenecek!", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void istatistiklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = IlanDAL.IstatistikleriGetir();

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                string mesaj = $"📊 GENEL İSTATİSTİKLER\n\n" +
                              $"Toplam İlan: {row["ToplamIlan"]}\n" +
                              $"Satışta: {row["Satista"]}\n" +
                              $"Satılan: {row["Satilan"]}\n\n" +
                              $"Ortalama Fiyat: {Convert.ToDecimal(row["OrtalamFiyat"]):C2}\n" +
                              $"En Düşük Fiyat: {Convert.ToDecimal(row["MinFiyat"]):C2}\n" +
                              $"En Yüksek Fiyat: {Convert.ToDecimal(row["MaxFiyat"]):C2}\n\n" +
                              $"Son 7 Gün: {row["SonBirHafta"]} ilan\n" +
                              $"Son 30 Gün: {row["SonBirAy"]} ilan";

                MessageBox.Show(mesaj, "İstatistikler",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void aylıkSatışRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int yil = DateTime.Now.Year;
            int ay = DateTime.Now.Month;

            string query = @"
        SELECT 
            m.MarkaAdi + ' ' + md.ModelAdi AS Arac,
            i.Fiyat AS SatisFiyati,
            i.SatisTarihi
        FROM Ilan i
        INNER JOIN Marka m ON i.MarkaID = m.MarkaID
        INNER JOIN Model md ON i.ModelID = md.ModelID
        WHERE i.Durum = 0 
            AND i.Aktif = 1
            AND YEAR(i.SatisTarihi) = @Yil
            AND MONTH(i.SatisTarihi) = @Ay
        ORDER BY i.SatisTarihi DESC";

            SqlParameter[] parameters = {
        new SqlParameter("@Yil", yil),
        new SqlParameter("@Ay", ay)
    };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Bu ay hiç satış yapılmamış!", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal toplamCiro = 0;
            foreach (DataRow row in dt.Rows)
            {
                toplamCiro += Convert.ToDecimal(row["SatisFiyati"]);
            }

            string mesaj = $"📅 {DateTime.Now:MMMM yyyy} SATIŞ RAPORU\n\n" +
                          $"Satış Adedi: {dt.Rows.Count}\n" +
                          $"Toplam Ciro: {toplamCiro:C2}\n" +
                          $"Ortalama: {(toplamCiro / dt.Rows.Count):C2}";

            MessageBox.Show(mesaj, "Aylık Satış Raporu",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            dataGridView1.DataSource = dt;
            DataGridViewAyarla();
        }

        private void stokDurumuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT 
            m.MarkaAdi,
            COUNT(i.IlanID) AS ToplamAdet,
            SUM(CASE WHEN i.Durum = 1 THEN 1 ELSE 0 END) AS SatistakiAdet,
            SUM(CASE WHEN i.Durum = 0 THEN 1 ELSE 0 END) AS SatilanAdet,
            AVG(i.Fiyat) AS OrtalamFiyat
        FROM Marka m
        LEFT JOIN Ilan i ON m.MarkaID = i.MarkaID AND i.Aktif = 1
        GROUP BY m.MarkaAdi
        HAVING COUNT(i.IlanID) > 0
        ORDER BY ToplamAdet DESC";

            dataGridView1.DataSource = DatabaseHelper.ExecuteQuery(query);
            DataGridViewAyarla();
        }

        private void exceleAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Aktarılacak veri yok!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Dosyası|*.csv";
                sfd.FileName = $"OtoGaleri_Rapor_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    ExportHelper.DataTableToCSV(dt, sfd.FileName);
                }
            }
        }
    }
}