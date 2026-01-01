using System;
using System.Data;
using System.Data.SqlClient;
using OtoGaleriUygulamasi.Helpers;

namespace OtoGaleriUygulamasi.DAL
{
    public class FotografDAL
    {
        // İlanın tüm fotoğraflarını getir
        public static DataTable IlanFotograflariniGetir(int ilanID)
        {
            string query = @"
                SELECT FotografID, FotografYolu, Sira
                FROM IlanFotograf
                WHERE IlanID = @IlanID
                ORDER BY Sira";

            return DatabaseHelper.ExecuteQuery(query, new[] {
                new SqlParameter("@IlanID", ilanID)
            });
        }

        // Fotoğraf ekle
        public static bool FotografEkle(int ilanID, string fotografYolu, int sira)
        {
            string query = @"
                INSERT INTO IlanFotograf (IlanID, FotografYolu, Sira)
                VALUES (@IlanID, @FotografYolu, @Sira)";

            SqlParameter[] parameters = {
                new SqlParameter("@IlanID", ilanID),
                new SqlParameter("@FotografYolu", fotografYolu),
                new SqlParameter("@Sira", sira)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Fotoğraf sil
        public static bool FotografSil(int fotografID)
        {
            string query = "DELETE FROM IlanFotograf WHERE FotografID = @FotografID";
            return DatabaseHelper.ExecuteNonQuery(query, new[] {
                new SqlParameter("@FotografID", fotografID)
            }) > 0;
        }

        // Vitrin fotoğrafını getir (Sira = 1)
        public static string VitrinFotografiGetir(int ilanID)
        {
            string query = @"
                SELECT TOP 1 FotografYolu
                FROM IlanFotograf
                WHERE IlanID = @IlanID
                ORDER BY Sira";

            object result = DatabaseHelper.ExecuteScalar(query, new[] {
                new SqlParameter("@IlanID", ilanID)
            });

            return result?.ToString();
        }
    }
}
