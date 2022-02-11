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

namespace TriggerTetikleyici
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-493DFJA\SQLEXPRESS;Initial Catalog=VeriYedeklOlusturma;Integrated Security=True");
        void list()
        {
            SqlDataAdapter dt = new SqlDataAdapter("select * from TBLKITAPLAR", con);
            DataTable da = new DataTable();
            dt.Fill(da);
            dataGridView1.DataSource = da;
        }
        void listSTOKSUZ()
        {
            SqlDataAdapter dt = new SqlDataAdapter("select * from TBLKITAPYEDEK", con);
            DataTable da = new DataTable();
            dt.Fill(da);
            dataGridView2.DataSource = da;
        }
       

        void sayac()
        {
            con.Open();
            SqlCommand kmt = new SqlCommand("select * from TBLSAYAÇ", con);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                labelADET.Text = dr[0].ToString();
            }
            con.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBoxKİTAPLİSTESİ.Visible = true;
             groupBoxSTOĞUBİTEN.Visible = false;
            list();
            sayac();
            listSTOKSUZ();
          
         }

        private void buttonEKLE_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand kmt = new SqlCommand("insert into TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVİ,TUR,STOK,FOTOGRAF,FİYAT) values (@p1,@p2,@p3,@p4,@p5,@P6,@p7,@p8)", con);
            kmt.Parameters.AddWithValue("@p1", textBoxAD.Text);
            kmt.Parameters.AddWithValue("@p2", textBoxYAZAR.Text);
            kmt.Parameters.AddWithValue("@p3", textBoxSAYFA.Text);
            kmt.Parameters.AddWithValue("@p4", textBoxYAYINEVİ.Text);
            kmt.Parameters.AddWithValue("@p5", textBoxTÜR.Text);
            kmt.Parameters.AddWithValue("@p6", textBoxSTOK.Text);
            kmt.Parameters.AddWithValue("@p7", textBoxfoto.Text);
            kmt.Parameters.AddWithValue("@p8", textBoxfiyat.Text);
            kmt.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kitap Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            list();
            sayac();
            

        }
     


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            textBoxID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBoxAD.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBoxYAZAR.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBoxSAYFA.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBoxYAYINEVİ.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textBoxTÜR.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            textBoxSTOK.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            textBoxfoto.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            textBoxfiyat.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            pictureBox1.ImageLocation = textBoxfoto.Text;


        }

        
        int stok;
        bool DURUM;
    
        private void buttonSİL_Click(object sender, EventArgs e)
        {
            
            
           
                con.Open();
                SqlCommand kmt1 = new SqlCommand("update TBLKITAPLAR SET STOK=STOK-1 WHERE @P1=ID", con);
                kmt1.Parameters.AddWithValue("@P1", textBoxID.Text);
                kmt1.ExecuteNonQuery();
                con.Close();
            
                MessageBox.Show("Kitap satıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                 
            con.Open();
            SqlCommand kmt = new SqlCommand("select * from TBLKITAPLAR ", con);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                stok = Convert.ToInt32(dr[6]);
                if (stok == 0)
                {
                    DURUM = true;
                }
                
            }
            con.Close();
            con.Open();
            if (DURUM==true)
                   {
                
                SqlCommand kmt2 = new SqlCommand(" DELETE FROM TBLKITAPLAR WHERE @P1=ID", con);
                kmt2.Parameters.AddWithValue("@P1", textBoxID.Text);
                kmt2.ExecuteNonQuery();
                
                MessageBox.Show("Kitap Tükendiği için sipariş edilecek Kitaplar bölümüne eklenmiştir" +
                    "", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);



            }
           
            con.Close();
            list();
            listSTOKSUZ();
            sayac();






        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            textBoxfoto.Text = openFileDialog1.FileName;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            textBoxID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            textBoxAD.Text = dataGridView2.Rows[secilen].Cells[1].Value.ToString();
            textBoxYAZAR.Text = dataGridView2.Rows[secilen].Cells[2].Value.ToString();
            textBoxSAYFA.Text = dataGridView2.Rows[secilen].Cells[3].Value.ToString();
            textBoxYAYINEVİ.Text = dataGridView2.Rows[secilen].Cells[4].Value.ToString();
            textBoxTÜR.Text = dataGridView2.Rows[secilen].Cells[5].Value.ToString();
            textBoxSTOK.Text = dataGridView2.Rows[secilen].Cells[6].Value.ToString();
            textBoxfoto.Text = dataGridView2.Rows[secilen].Cells[7].Value.ToString();
            pictureBox1.ImageLocation = textBoxfoto.Text;
        }

        private void buttonSTOKEKLE_Click(object sender, EventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            textBoxID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            textBoxAD.Text = dataGridView2.Rows[secilen].Cells[1].Value.ToString();
            textBoxYAZAR.Text = dataGridView2.Rows[secilen].Cells[2].Value.ToString();
            textBoxSAYFA.Text = dataGridView2.Rows[secilen].Cells[3].Value.ToString();
            textBoxYAYINEVİ.Text = dataGridView2.Rows[secilen].Cells[4].Value.ToString();
            textBoxTÜR.Text = dataGridView2.Rows[secilen].Cells[5].Value.ToString();
            textBoxfoto.Text = dataGridView2.Rows[secilen].Cells[7].Value.ToString();

            pictureBox1.ImageLocation = textBoxfoto.Text;

            con.Open();
            SqlCommand kmt = new SqlCommand("UPDATE TBLKITAPYEDEK SET STOK=@P6 WHERE ID=@P1", con);
            kmt.Parameters.AddWithValue("@p1", textBoxID.Text);
            kmt.Parameters.AddWithValue("@p6", textBoxSTOK.Text);
            kmt.ExecuteNonQuery();
            MessageBox.Show("STOK GÜNCELLERNDİ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            
            MessageBox.Show("Kitap Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Open();
            SqlCommand kmt2 = new SqlCommand("DELETE FROM TBLKITAPYEDEK WHERE @P1=ID", con);
            kmt2.Parameters.AddWithValue("@P1", textBoxID.Text);
            kmt2.ExecuteNonQuery();
            con.Close();
            list();
            listSTOKSUZ();
            sayac();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand kmt1 = new SqlCommand("update TBLKITAPLAR SET AD=@P1,YAZAR=@P2,SAYFA=@P3,YAYINEVİ=@P4,TUR=@P5,STOK=@P6,FOTOGRAF=@P7,FİYAT=@P8" +
                " WHERE ID=@P9", con);
            
            kmt1.Parameters.AddWithValue("@P1", textBoxAD.Text);
            kmt1.Parameters.AddWithValue("@P2", textBoxYAZAR.Text);
            kmt1.Parameters.AddWithValue("@P3", textBoxSAYFA.Text);
            kmt1.Parameters.AddWithValue("@P4", textBoxYAYINEVİ.Text);
            kmt1.Parameters.AddWithValue("@P5", textBoxTÜR.Text);
            kmt1.Parameters.AddWithValue("@P6", textBoxSTOK.Text);
            kmt1.Parameters.AddWithValue("@P7", textBoxfoto.Text);
            kmt1.Parameters.AddWithValue("@P8", textBoxfiyat.Text);
            kmt1.Parameters.AddWithValue("@P9", textBoxID.Text);
            kmt1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("GÜNCELLENDİ");
            list();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBoxKİTAPLİSTESİ.Visible = true;
            
            groupBoxSTOĞUBİTEN.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBoxKİTAPLİSTESİ.Visible = false;
            
            groupBoxSTOĞUBİTEN.Visible = true;
        }

       
    }
}
