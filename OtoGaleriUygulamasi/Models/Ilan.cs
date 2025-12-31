using System;

namespace OtoGaleriUygulamasi.Models
{
    public class Ilan
    {
        public int IlanID { get; set; }
        public int MarkaID { get; set; }
        public int ModelID { get; set; }
        public decimal Fiyat { get; set; }
        public int Yil { get; set; }
        public int YakitTipiID { get; set; }
        public int VitesTipiID { get; set; }
        public int Kilometre { get; set; }
        public int KasaTipiID { get; set; }
        public int RenkID { get; set; }
        public bool AgirHasarKayitli { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; } // true: Satışta, false: Satıldı
        public DateTime IlanTarihi { get; set; }

        // Ek özellikler (JOIN sonucu için)
        public string MarkaAdi { get; set; }
        public string ModelAdi { get; set; }
        public string YakitTipiAdi { get; set; }
        public string VitesTipiAdi { get; set; }
        public string KasaTipiAdi { get; set; }
        public string RenkAdi { get; set; }
    }
}