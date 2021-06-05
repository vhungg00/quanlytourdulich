using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Tour : Form
    {
        public Tour()
        {
            InitializeComponent();
        }
        string ketnoi = @"Data Source=DESKTOP-238CB0R\SQLEXPRESS02;Initial Catalog=Tour;Integrated Security=True;";

        public void HienThi()
        {
            SqlConnection con = new SqlConnection(ketnoi);
            SqlCommand cmd = new SqlCommand("select * from Tour order by MaTour asc", con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet set = new DataSet();
            adapter.Fill(set, "S");
            dataGridView1.DataSource = set.Tables["S"];

        }


        private void themNV_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ketnoi);
            try
            {
                con.Open();
                if (textBox1.Text != "" && textBox8.Text != ""  && textBox10.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("insert into Tour values('" + textBox1.Text + "', '" + textBox8.Text + "',  '" + textBox10.Text + "')", con);
                    int kq = (int)cmd.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Them thanh cong!");
                        HienThi();
                    }
                    else
                        MessageBox.Show("Them that bai!");
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi ket noi!" + ex.Message);
            }
        }

        private void suaNV_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ketnoi);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Tour set TenTour ='" + textBox8.Text + "',Gia ='" + textBox10.Text + "' where MaTour ='" + textBox1.Text + "'", con);
                int kq = (int)cmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Sua thanh cong!");
                    HienThi();
                }
                else
                    MessageBox.Show("Sua that bai!");
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi ket noi!" + ex.Message);
            }
        }

        private void xoaNv_Click(object sender, EventArgs e)
        {
            DialogResult tb;
            tb = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection(ketnoi);
                SqlCommand cmd = new SqlCommand("delete from Tour where MaTour='" + textBox1.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                HienThi();
                con.Close();
            }

        }

        private void timNv_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ketnoi);
            SqlCommand cmd = new SqlCommand("select * from Tour where MaTour=@manv", con);
            con.Open();
            cmd.Parameters.AddWithValue("manv", textBox7.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Tour_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HienThi();
        }
    }
}
