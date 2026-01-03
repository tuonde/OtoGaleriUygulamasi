using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace OtoGaleriUygulamasi.Helpers
{
    public class ExportHelper
    {
        public static void DataTableToCSV(DataTable dt, string dosyaYolu)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(dosyaYolu, false, System.Text.Encoding.UTF8))
                {
                    // Başlıklar
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sw.Write(dt.Columns[i].ColumnName);
                        if (i < dt.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();

                    // Veriler
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            sw.Write(row[i].ToString().Replace(",", ";"));
                            if (i < dt.Columns.Count - 1)
                                sw.Write(",");
                        }
                        sw.WriteLine();
                    }
                }

                MessageBox.Show("Dosya başarıyla kaydedildi: " + dosyaYolu, "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya kaydedilemedi: " + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
