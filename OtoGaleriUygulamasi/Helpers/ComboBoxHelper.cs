using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OtoGaleriUygulamasi.Helpers
{
    public class ComboBoxHelper
    {
        // Markaları ComboBox'a yükle
        public static void MarkalariYukle(ComboBox comboBox)
        {
            string query = "SELECT MarkaID, MarkaAdi FROM Marka WHERE Aktif = 1 ORDER BY MarkaAdi";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            comboBox.DataSource = dt;
            comboBox.DisplayMember = "MarkaAdi";
            comboBox.ValueMember = "MarkaID";
            comboBox.SelectedIndex = -1;
        }

        // Modelleri ComboBox'a yükle (Markaya göre)
        public static void ModelleriYukle(ComboBox comboBox, int markaID)
        {
            string query = "SELECT ModelID, ModelAdi FROM Model WHERE MarkaID = @MarkaID AND Aktif = 1 ORDER BY ModelAdi";
            SqlParameter[] parameters = {
                new SqlParameter("@MarkaID", markaID)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            comboBox.DataSource = dt;
            comboBox.DisplayMember = "ModelAdi";
            comboBox.ValueMember = "ModelID";
            comboBox.SelectedIndex = -1;
        }

        // Yakıt tiplerini yükle
        public static void YakitTipleriniYukle(ComboBox comboBox)
        {
            string query = "SELECT YakitTipiID, YakitTipiAdi FROM YakitTipi WHERE Aktif = 1 ORDER BY YakitTipiAdi";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            comboBox.DataSource = dt;
            comboBox.DisplayMember = "YakitTipiAdi";
            comboBox.ValueMember = "YakitTipiID";
            comboBox.SelectedIndex = -1;
        }

        // Vites tiplerini yükle
        public static void VitesTipleriniYukle(ComboBox comboBox)
        {
            string query = "SELECT VitesTipiID, VitesTipiAdi FROM VitesTipi WHERE Aktif = 1 ORDER BY VitesTipiAdi";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            comboBox.DataSource = dt;
            comboBox.DisplayMember = "VitesTipiAdi";
            comboBox.ValueMember = "VitesTipiID";
            comboBox.SelectedIndex = -1;
        }

        // Kasa tiplerini yükle
        public static void KasaTipleriniYukle(ComboBox comboBox)
        {
            string query = "SELECT KasaTipiID, KasaTipiAdi FROM KasaTipi WHERE Aktif = 1 ORDER BY KasaTipiAdi";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            comboBox.DataSource = dt;
            comboBox.DisplayMember = "KasaTipiAdi";
            comboBox.ValueMember = "KasaTipiID";
            comboBox.SelectedIndex = -1;
        }

        // Renkleri yükle
        public static void RenkleriYukle(ComboBox comboBox)
        {
            string query = "SELECT RenkID, RenkAdi FROM Renk WHERE Aktif = 1 ORDER BY RenkAdi";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            comboBox.DataSource = dt;
            comboBox.DisplayMember = "RenkAdi";
            comboBox.ValueMember = "RenkID";
            comboBox.SelectedIndex = -1;
        }
    }
}