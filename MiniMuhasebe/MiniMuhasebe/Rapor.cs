using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MiniMuhasebe
{
    public partial class Rapor : Form
    {
        public Rapor()
        {
            InitializeComponent();
        }
        SqlConnection con =new SqlConnection("Data Source = HALIT\\SQLEXPRESS; Initial Catalog = MiniMuhasebe; Integrated Security = True; Encrypt=False");
        private void Rapor_Load(object sender, EventArgs e)
        {
            //Gelir Toplamı
            con.Open();
            SqlCommand komut1 = new SqlCommand("Select sum(fiyat) from Tbl_muhasebe where durum = 1",con);
            SqlDataReader k1 = komut1.ExecuteReader();
            while (k1.Read())
            {
                label10.Text = k1[0].ToString();
            }
            con.Close();

            //Gider Toplamı
            con.Open();
            SqlCommand komut2 = new SqlCommand("Select sum(fiyat) from Tbl_muhasebe where durum = 0", con);
            SqlDataReader k2 = komut2.ExecuteReader();
            while (k2.Read())
            {
                label9.Text = k2[0].ToString();
            }
            con.Close();

            //Gelir-Gider Dengesi
            int gelir,gider;
            gelir = Convert.ToInt32(label10.Text);
            gider = Convert.ToInt32(label9.Text);
            label8.Text = (gelir - gider).ToString();

            //EN YÜKSEK GELİR
            con.Open();
            SqlCommand komut3 = new SqlCommand("SELECT TOP 1 adi FROM Tbl_muhasebe WHERE durum = 1 ORDER BY fiyat DESC;", con);
            SqlDataReader k3 = komut3.ExecuteReader();
            while (k3.Read())
            {
                label7.Text = k3[0].ToString();
            }
            con.Close();

            //EN YÜKSEK GİDER
            //EN YÜKSEK GELİR
            con.Open();
            SqlCommand komut4 = new SqlCommand("SELECT TOP 1 adi FROM Tbl_muhasebe WHERE durum = 0 ORDER BY fiyat DESC;", con);
            SqlDataReader k4 = komut4.ExecuteReader();
            while (k4.Read())
            {
                label6.Text = k4[0].ToString();
            }
            con.Close();

        }
    }
}
