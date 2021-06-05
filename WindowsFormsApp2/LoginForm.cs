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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-238CB0R\SQLEXPRESS02;
             Initial Catalog=tour;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("select * from NguoiDung where " +
                "TaiKhoan=@taiKhoan and MatKhau=@matKhau", con);
            con.Open();
            cmd.Parameters.AddWithValue("taiKhoan", textBox1.Text);
            cmd.Parameters.AddWithValue("matKhau", textBox2.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Ban da nhap sai!");
            con.Close();
            if (DialogResult == DialogResult.OK)
            {
                Form1 ht = new Form1();
                ht.ShowDialog();
            }

            con.Close();
        }
    }
}
