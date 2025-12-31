using System;
using System.Windows.Forms;

namespace OtoGaleriUygulamasi
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAnaEkran()); // Form1 yerine FormAnaEkran
        }
    }
}
