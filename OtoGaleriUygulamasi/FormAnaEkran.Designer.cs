namespace OtoGaleriUygulamasi
{
    partial class FormAnaEkran
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSatildi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnYeniIlan = new System.Windows.Forms.Button();
            this.btnDuzenle = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dosyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilanlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raporlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelDGW = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.yeniİlanCtrlNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çıkışAltF4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tümİlanlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satıştaOlanlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satılanlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gelişmişAramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.istatistiklerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aylıkSatışRaporuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stokDurumuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exceleAktarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelDGW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.btnSatildi);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSil);
            this.panel1.Controls.Add(this.btnYeniIlan);
            this.panel1.Controls.Add(this.btnDuzenle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 89);
            this.panel1.TabIndex = 0;
            // 
            // btnSatildi
            // 
            this.btnSatildi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSatildi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSatildi.ForeColor = System.Drawing.Color.White;
            this.btnSatildi.Location = new System.Drawing.Point(246, 3);
            this.btnSatildi.Name = "btnSatildi";
            this.btnSatildi.Size = new System.Drawing.Size(75, 40);
            this.btnSatildi.TabIndex = 4;
            this.btnSatildi.Text = "Satıldı";
            this.btnSatildi.UseVisualStyleBackColor = true;
            this.btnSatildi.Click += new System.EventHandler(this.btnSatildi_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "🚗 OTO GALERİ YÖNETİM SİSTEMİ";
            // 
            // btnSil
            // 
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSil.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Location = new System.Drawing.Point(165, 3);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 40);
            this.btnSil.TabIndex = 3;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click_1);
            // 
            // btnYeniIlan
            // 
            this.btnYeniIlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(142)))), ((int)(((byte)(150)))));
            this.btnYeniIlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYeniIlan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYeniIlan.ForeColor = System.Drawing.Color.White;
            this.btnYeniIlan.Location = new System.Drawing.Point(3, 3);
            this.btnYeniIlan.Name = "btnYeniIlan";
            this.btnYeniIlan.Size = new System.Drawing.Size(75, 40);
            this.btnYeniIlan.TabIndex = 1;
            this.btnYeniIlan.Text = "Yeni İlan";
            this.btnYeniIlan.UseVisualStyleBackColor = false;
            this.btnYeniIlan.Click += new System.EventHandler(this.btnYeniIlan_Click_1);
            // 
            // btnDuzenle
            // 
            this.btnDuzenle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuzenle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDuzenle.ForeColor = System.Drawing.Color.White;
            this.btnDuzenle.Location = new System.Drawing.Point(84, 3);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(75, 40);
            this.btnDuzenle.TabIndex = 2;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.UseVisualStyleBackColor = true;
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyaToolStripMenuItem,
            this.ilanlarToolStripMenuItem,
            this.raporlarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yeniİlanCtrlNToolStripMenuItem,
            this.çıkışAltF4ToolStripMenuItem});
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.dosyaToolStripMenuItem.Text = "Dosya";
            // 
            // ilanlarToolStripMenuItem
            // 
            this.ilanlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tümİlanlarToolStripMenuItem,
            this.satıştaOlanlarToolStripMenuItem,
            this.satılanlarToolStripMenuItem,
            this.gelişmişAramaToolStripMenuItem});
            this.ilanlarToolStripMenuItem.Name = "ilanlarToolStripMenuItem";
            this.ilanlarToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.ilanlarToolStripMenuItem.Text = "İlanlar";
            // 
            // raporlarToolStripMenuItem
            // 
            this.raporlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.istatistiklerToolStripMenuItem,
            this.aylıkSatışRaporuToolStripMenuItem,
            this.stokDurumuToolStripMenuItem,
            this.exceleAktarToolStripMenuItem});
            this.raporlarToolStripMenuItem.Name = "raporlarToolStripMenuItem";
            this.raporlarToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.raporlarToolStripMenuItem.Text = "Raporlar";
            // 
            // panelDGW
            // 
            this.panelDGW.Controls.Add(this.dataGridView1);
            this.panelDGW.Location = new System.Drawing.Point(0, 109);
            this.panelDGW.Name = "panelDGW";
            this.panelDGW.Size = new System.Drawing.Size(984, 304);
            this.panelDGW.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(984, 304);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // yeniİlanCtrlNToolStripMenuItem
            // 
            this.yeniİlanCtrlNToolStripMenuItem.Name = "yeniİlanCtrlNToolStripMenuItem";
            this.yeniİlanCtrlNToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.yeniİlanCtrlNToolStripMenuItem.Text = "Yeni İlan (Ctrl+N)";
            this.yeniİlanCtrlNToolStripMenuItem.Click += new System.EventHandler(this.yeniİlanCtrlNToolStripMenuItem_Click);
            // 
            // çıkışAltF4ToolStripMenuItem
            // 
            this.çıkışAltF4ToolStripMenuItem.Name = "çıkışAltF4ToolStripMenuItem";
            this.çıkışAltF4ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.çıkışAltF4ToolStripMenuItem.Text = "Çıkış (Alt+F4)";
            this.çıkışAltF4ToolStripMenuItem.Click += new System.EventHandler(this.çıkışAltF4ToolStripMenuItem_Click);
            // 
            // tümİlanlarToolStripMenuItem
            // 
            this.tümİlanlarToolStripMenuItem.Name = "tümİlanlarToolStripMenuItem";
            this.tümİlanlarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tümİlanlarToolStripMenuItem.Text = "Tüm İlanlar";
            this.tümİlanlarToolStripMenuItem.Click += new System.EventHandler(this.tümİlanlarToolStripMenuItem_Click);
            // 
            // satıştaOlanlarToolStripMenuItem
            // 
            this.satıştaOlanlarToolStripMenuItem.Name = "satıştaOlanlarToolStripMenuItem";
            this.satıştaOlanlarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.satıştaOlanlarToolStripMenuItem.Text = "Satışta Olanlar";
            this.satıştaOlanlarToolStripMenuItem.Click += new System.EventHandler(this.satıştaOlanlarToolStripMenuItem_Click);
            // 
            // satılanlarToolStripMenuItem
            // 
            this.satılanlarToolStripMenuItem.Name = "satılanlarToolStripMenuItem";
            this.satılanlarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.satılanlarToolStripMenuItem.Text = "Satılanlar";
            this.satılanlarToolStripMenuItem.Click += new System.EventHandler(this.satılanlarToolStripMenuItem_Click);
            // 
            // gelişmişAramaToolStripMenuItem
            // 
            this.gelişmişAramaToolStripMenuItem.Name = "gelişmişAramaToolStripMenuItem";
            this.gelişmişAramaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gelişmişAramaToolStripMenuItem.Text = "Gelişmiş Arama";
            this.gelişmişAramaToolStripMenuItem.Click += new System.EventHandler(this.gelişmişAramaToolStripMenuItem_Click);
            // 
            // istatistiklerToolStripMenuItem
            // 
            this.istatistiklerToolStripMenuItem.Name = "istatistiklerToolStripMenuItem";
            this.istatistiklerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.istatistiklerToolStripMenuItem.Text = "İstatistikler";
            this.istatistiklerToolStripMenuItem.Click += new System.EventHandler(this.istatistiklerToolStripMenuItem_Click);
            // 
            // aylıkSatışRaporuToolStripMenuItem
            // 
            this.aylıkSatışRaporuToolStripMenuItem.Name = "aylıkSatışRaporuToolStripMenuItem";
            this.aylıkSatışRaporuToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aylıkSatışRaporuToolStripMenuItem.Text = "Aylık Satış Raporu";
            this.aylıkSatışRaporuToolStripMenuItem.Click += new System.EventHandler(this.aylıkSatışRaporuToolStripMenuItem_Click);
            // 
            // stokDurumuToolStripMenuItem
            // 
            this.stokDurumuToolStripMenuItem.Name = "stokDurumuToolStripMenuItem";
            this.stokDurumuToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stokDurumuToolStripMenuItem.Text = "Stok Durumu";
            this.stokDurumuToolStripMenuItem.Click += new System.EventHandler(this.stokDurumuToolStripMenuItem_Click);
            // 
            // exceleAktarToolStripMenuItem
            // 
            this.exceleAktarToolStripMenuItem.Name = "exceleAktarToolStripMenuItem";
            this.exceleAktarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exceleAktarToolStripMenuItem.Text = "Excel\'e Aktar";
            this.exceleAktarToolStripMenuItem.Click += new System.EventHandler(this.exceleAktarToolStripMenuItem_Click);
            // 
            // FormAnaEkran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(984, 361);
            this.Controls.Add(this.panelDGW);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormAnaEkran";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OTO GALERİ YÖNETİM SİSTEMİ";
            this.Load += new System.EventHandler(this.FormAnaEkran_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelDGW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSatildi;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnDuzenle;
        private System.Windows.Forms.Button btnYeniIlan;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ilanlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raporlarToolStripMenuItem;
        private System.Windows.Forms.Panel panelDGW;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem yeniİlanCtrlNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çıkışAltF4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tümİlanlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satıştaOlanlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satılanlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gelişmişAramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem istatistiklerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aylıkSatışRaporuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stokDurumuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exceleAktarToolStripMenuItem;
    }
}

