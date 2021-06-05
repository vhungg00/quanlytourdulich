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
    public partial class HoaDon : Form
    {
        string ketnoi = @"Data Source=DESKTOP-238CB0R\SQLEXPRESS02;Initial Catalog=Tour;Integrated Security=True;";
        public HoaDon()
        {
            InitializeComponent();
        }
        public void HienThi()
        {
            SqlConnection con = new SqlConnection(ketnoi);
            SqlCommand cmd = new SqlCommand("select * from HoaDon order by MaHD asc", con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet set = new DataSet();
            adapter.Fill(set, "S");
            dataGridView1.DataSource = set.Tables["S"];

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            HienThi();
        }
    }
}
