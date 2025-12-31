using OtoGaleriUygulamasi.DAL;
using OtoGaleriUygulamasi.Helpers;
using OtoGaleriUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OtoGaleriUygulamasi.DAL
{
    public class IlanDAL
    {
        // Tüm ilanları getir
        public static DataTable TumIlanlariGetir()
        {
            string query = @"
                SELECT 
                    i.IlanID,
                    m.MarkaAdi,
                    md.ModelAdi,
                    i.Fiyat,
                    i.Yil,
                    y.YakitTipiAdi,
                    v.VitesTipiAdi,
                    i.Kilometre,
                    k.KasaTipiAdi,
                    r.RenkAdi,
                    CASE WHEN i.AgirHasarKayitli = 1 THEN 'Evet' ELSE 'Hayır' END AS AgirHasar,
                    CASE WHEN i.Durum = 1 THEN 'Satışta' ELSE 'Satıldı' END AS Durum,
                    i.IlanTarihi
                FROM Ilan i
                INNER JOIN Marka m ON i.MarkaID = m.MarkaID
                INNER JOIN Model md ON i.ModelID = md.ModelID
                INNER JOIN YakitTipi y ON i.YakitTipiID = y.YakitTipiID
                INNER JOIN VitesTipi v ON i.VitesTipiID = v.VitesTipiID
                INNER JOIN KasaTipi k ON i.KasaTipiID = k.KasaTipiID
                INNER JOIN Renk r ON i.RenkID = r.RenkID
                WHERE i.Aktif = 1
                ORDER BY i.IlanTarihi DESC";
            
            return DatabaseHelper.ExecuteQuery(query);
        }
        
        // Sadece satışta olan ilanları getir
        public static DataTable SatistaOlanIlanlariGetir()
        {
            string query = @"
                SELECT 
                    i.IlanID,
                    m.MarkaAdi + ' ' + md.ModelAdi AS Arac,
                    i.Fiyat,
                    i.Yil,
                    i.Kilometre,
                    i.IlanTarihi
                FROM Ilan i
                INNER JOIN Marka m ON i.MarkaID = m.MarkaID
                INNER JOIN Model md ON i.ModelID = md.ModelID
                WHERE i.Durum = 1 AND i.Aktif = 1
                ORDER BY i.IlanTarihi DESC";
            
            return DatabaseHelper.ExecuteQuery(query);
        }
        
        // İlan detayını getir
        public static DataTable IlanDetayGetir(int ilanID)
        {
            string query = @"
                SELECT 
                    i.*,
                    m.MarkaAdi,
                    md.ModelAdi,
                    y.YakitTipiAdi,
                    v.VitesTipiAdi,
                    k.KasaTipiAdi,
                    r.RenkAdi
                FROM Ilan i
                INNER JOIN Marka m ON i.MarkaID = m.MarkaID
                INNER JOIN Model md ON i.ModelID = md.ModelID
                INNER JOIN YakitTipi y ON i.YakitTipiID = y.YakitTipiID
                INNER JOIN VitesTipi v ON i.VitesTipiID = v.VitesTipiID
                INNER JOIN KasaTipi k ON i.KasaTipiID = k.KasaTipiID
                INNER JOIN Renk r ON i.RenkID = r.RenkID
                WHERE i.IlanID = @IlanID";
            
            SqlParameter[] parameters = {
                new SqlParameter("@IlanID", ilanID)
            };
            
            return DatabaseHelper.ExecuteQuery(query, parameters);
        }
        
        // Yeni ilan ekle (Basitleştirilmiş - fotoğrafsız)
        public static int IlanEkle(Ilan ilan)
        {
            string query = @"
                INSERT INTO Ilan (MarkaID, ModelID, Fiyat, Yil, YakitTipiID, VitesTipiID, 
                                  Kilometre, KasaTipiID, RenkID, AgirHasarKayitli, Aciklama, Durum)
                VALUES (@MarkaID, @ModelID, @Fiyat, @Yil, @YakitTipiID, @VitesTipiID, 
                        @Kilometre, @KasaTipiID, @RenkID, @AgirHasarKayitli, @Aciklama, @Durum);
                SELECT SCOPE_IDENTITY();";
            
            SqlParameter[] parameters = {
                new SqlParameter("@MarkaID", ilan.MarkaID),
                new SqlParameter("@ModelID", ilan.ModelID),
                new SqlParameter("@Fiyat", ilan.Fiyat),
                new SqlParameter("@Yil", ilan.Yil),
                new SqlParameter("@YakitTipiID", ilan.YakitTipiID),
                new SqlParameter("@VitesTipiID", ilan.VitesTipiID),
                new SqlParameter("@Kilometre", ilan.Kilometre),
                new SqlParameter("@KasaTipiID", ilan.KasaTipiID),
                new SqlParameter("@RenkID", ilan.RenkID),
                new SqlParameter("@AgirHasarKayitli", ilan.AgirHasarKayitli),
                new SqlParameter("@Aciklama", ilan.Aciklama ?? (object)DBNull.Value),
                new SqlParameter("@Durum", ilan.Durum)
            };
            
            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }
        
        // İlan güncelle
        public static bool IlanGuncelle(Ilan ilan)
        {
            string query = @"
                UPDATE Ilan 
                SET MarkaID = @MarkaID,
                    ModelID = @ModelID,
                    Fiyat = @Fiyat,
                    Yil = @Yil,
                    YakitTipiID = @YakitTipiID,
                    VitesTipiID = @VitesTipiID,
                    Kilometre = @Kilometre,
                    KasaTipiID = @KasaTipiID,
                    RenkID = @RenkID,
                    AgirHasarKayitli = @AgirHasarKayitli,
                    Aciklama = @Aciklama,
                    Durum = @Durum,
                    GuncellemeTarihi = GETDATE()
                WHERE IlanID = @IlanID";
            
            SqlParameter[] parameters = {
                new SqlParameter("@IlanID", ilan.IlanID),
                new SqlParameter("@MarkaID", ilan.MarkaID),
                new SqlParameter("@ModelID", ilan.ModelID),
                new SqlParameter("@Fiyat", ilan.Fiyat),
                new SqlParameter("@Yil", ilan.Yil),
                new SqlParameter("@YakitTipiID", ilan.YakitTipiID),
                new SqlParameter("@VitesTipiID", ilan.VitesTipiID),
                new SqlParameter("@Kilometre", ilan.Kilometre),
                new SqlParameter("@KasaTipiID", ilan.KasaTipiID),
                new SqlParameter("@RenkID", ilan.RenkID),
                new SqlParameter("@AgirHasarKayitli", ilan.AgirHasarKayitli),
                new SqlParameter("@Aciklama", ilan.Aciklama ?? (object)DBNull.Value),
                new SqlParameter("@Durum", ilan.Durum)
            };
            
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        
        // İlanı satıldı olarak işaretle
        public static bool IlanSatildi(int ilanID)
        {
            string query = @"
                UPDATE Ilan 
                SET Durum = 0,
                    SatisTarihi = GETDATE(),
                    GuncellemeTarihi = GETDATE()
                WHERE IlanID = @IlanID";
            
            SqlParameter[] parameters = {
                new SqlParameter("@IlanID", ilanID)
            };
            
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        
        // İlan sil (Soft delete)
        public static bool IlanSil(int ilanID)
        {
            string query = @"
                UPDATE Ilan 
                SET Aktif = 0,
                    GuncellemeTarihi = GETDATE()
                WHERE IlanID = @IlanID";
            
            SqlParameter[] parameters = {
                new SqlParameter("@IlanID", ilanID)
            };
            
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        
        // Arama yap
        public static DataTable IlanAra(int? markaID, int? modelID, decimal? minFiyat, 
                                        decimal? maxFiyat, int? minYil, int? maxYil)
        {
            string query = @"
                SELECT 
                    i.IlanID,
                    m.MarkaAdi + ' ' + md.ModelAdi AS Arac,
                    i.Fiyat,
                    i.Yil,
                    i.Kilometre
                FROM Ilan i
                INNER JOIN Marka m ON i.MarkaID = m.MarkaID
                INNER JOIN Model md ON i.ModelID = md.ModelID
                WHERE i.Durum = 1 AND i.Aktif = 1
                    AND (@MarkaID IS NULL OR i.MarkaID = @MarkaID)
                    AND (@ModelID IS NULL OR i.ModelID = @ModelID)
                    AND (@MinFiyat IS NULL OR i.Fiyat >= @MinFiyat)
                    AND (@MaxFiyat IS NULL OR i.Fiyat <= @MaxFiyat)
                    AND (@MinYil IS NULL OR i.Yil >= @MinYil)
                    AND (@MaxYil IS NULL OR i.Yil <= @MaxYil)
                ORDER BY i.IlanTarihi DESC";
            
            SqlParameter[] parameters = {
                new SqlParameter("@MarkaID", (object)markaID ?? DBNull.Value),
                new SqlParameter("@ModelID", (object)modelID ?? DBNull.Value),
                new SqlParameter("@MinFiyat", (object)minFiyat ?? DBNull.Value),
                new SqlParameter("@MaxFiyat", (object)maxFiyat ?? DBNull.Value),
                new SqlParameter("@MinYil", (object)minYil ?? DBNull.Value),
                new SqlParameter("@MaxYil", (object)maxYil ?? DBNull.Value)
            };
            
            return DatabaseHelper.ExecuteQuery(query, parameters);
        }
    }
}

