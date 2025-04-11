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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con =new SqlConnection("Data Source = HALIT\\SQLEXPRESS; Initial Catalog = MiniMuhasebe; Integrated Security = True; Encrypt=False");
        Boolean Durum ;
        void temizle(){
            tx_ad.Text = "";
            tx_fiyat.Text = "";
            tx_id.Text = "";
            tx_islem.Text = "";
            cb_kate.Text = "";
            rb_gelir.Checked = false;
            rb_gider.Checked = false;
            }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'miniMuhasebeDataSet.Tbl_muhasebe' table. You can move, or remove it, as needed.
            this.tbl_muhasebeTableAdapter.Fill(this.miniMuhasebeDataSet.Tbl_muhasebe);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.tbl_muhasebeTableAdapter.Fill(this.miniMuhasebeDataSet.Tbl_muhasebe);
            temizle();
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand islemgun = new SqlCommand("UPDATE Tbl_muhasebe Set adi=@g1,fiyat=@g2,yer=@g3,durum=@g4,kategori=@g5,tarih=@g6 Where id=@g7", con);
            islemgun.Parameters.AddWithValue("@g1", tx_ad.Text);
            islemgun.Parameters.AddWithValue("@g2", tx_fiyat.Text);
            islemgun.Parameters.AddWithValue("@g3", tx_islem.Text);
            islemgun.Parameters.AddWithValue("@g4", Durum);
            islemgun.Parameters.AddWithValue("@g5", cb_kate.Text);
            islemgun.Parameters.AddWithValue("@g6", dt_date.Value);
            islemgun.Parameters.AddWithValue("@g7", tx_id.Text);
            islemgun.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Personel Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand islem1 = new SqlCommand("INSERT INTO Tbl_muhasebe (adi, fiyat, yer, durum, kategori, tarih) VALUES (@k1,@k2,@k3,@k4,@k5,@k6)",con);
            islem1.Parameters.AddWithValue("@k1",tx_ad.Text);
            islem1.Parameters.AddWithValue("@k2", tx_fiyat.Text);
            islem1.Parameters.AddWithValue("@k3", tx_islem.Text);
            islem1.Parameters.AddWithValue("@k4", Durum);
            islem1.Parameters.AddWithValue("@k5", cb_kate.Text);
            islem1.Parameters.AddWithValue("@k6", dt_date.Value);
            islem1.ExecuteNonQuery();
            con.Close();
            temizle();
            MessageBox.Show("İşlem Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void rb_gelir_CheckedChanged(object sender, EventArgs e)
        {
            Durum = true;
        }

        private void rb_gider_CheckedChanged(object sender, EventArgs e)
        {
            Durum = false;
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand islemsil = new SqlCommand("delete from Tbl_muhasebe where id=@s1",con);
            islemsil.Parameters.AddWithValue("@s1",tx_id.Text);
            islemsil.ExecuteNonQuery();
            MessageBox.Show("İşlem Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            con.Close();
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilendeger=dataGridView1.SelectedCells[0].RowIndex;
            tx_id.Text = dataGridView1.Rows[secilendeger].Cells[0].Value.ToString();
            tx_ad.Text = dataGridView1.Rows[secilendeger].Cells[1].Value.ToString();
            tx_fiyat.Text = dataGridView1.Rows[secilendeger].Cells[2].Value.ToString();
            tx_islem.Text = dataGridView1.Rows[secilendeger].Cells[3].Value.ToString();
            Durum = Convert.ToBoolean(dataGridView1.Rows[secilendeger].Cells[4].Value);
            if (Durum==true)
            {
                rb_gelir.Checked = true;
            }
            else { 
                rb_gider.Checked = true;
            }
                
            cb_kate.Text = dataGridView1.Rows[secilendeger].Cells[5].Value.ToString();
            dataGridView1.Text = dataGridView1.Rows[secilendeger].Cells[6].Value.ToString();
        }

        private void btn_rapor_Click(object sender, EventArgs e)
        {
            Rapor rapor = new Rapor();
            rapor.Show();
            this.Hide();
        }
    }
}
