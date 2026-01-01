using System;
using System.Data.SqlClient;
using OtoGaleriUygulamasi.Helpers; // DatabaseHelper'a erişmek için

namespace OtoGaleriUygulamasi.DAL
{
    public class ModelDAL
    {
        /// Veritabanında model ismini arar, varsa ID'sini döndürür. 
        /// Yoksa yeni modeli ekleyip yeni oluşan ID'yi döndürür.
        public static int GetirVeyaEkleModel(int markaID, string modelAdi)
        {
            // 1. Model zaten var mı kontrol et
            string kontrolSorgu = "SELECT ModelID FROM Model WHERE MarkaID = @mID AND ModelAdi = @mName";
            SqlParameter[] p1 = {
                new SqlParameter("@mID", markaID),
                new SqlParameter("@mName", modelAdi.Trim())
            };

            object sonuc = DatabaseHelper.ExecuteScalar(kontrolSorgu, p1);

            if (sonuc != null)
            {
                // Model bulundu, mevcut ID'yi gönder
                return Convert.ToInt32(sonuc);
            }
            else
            {
                // 2. Model bulunamadı, yeni ekle ve ID'sini al
                string ekleSorgu = "INSERT INTO Model (MarkaID, ModelAdi, Aktif) VALUES (@mID, @mName, 1); SELECT SCOPE_IDENTITY();";
                SqlParameter[] p2 = {
                    new SqlParameter("@mID", markaID),
                    new SqlParameter("@mName", modelAdi.Trim())
                };

                // SCOPE_IDENTITY() sayesinde yeni oluşan ID'yi alıyoruz
                return Convert.ToInt32(DatabaseHelper.ExecuteScalar(ekleSorgu, p2));
            }
        }
    }
}