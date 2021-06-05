using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
        }
        string ketnoi = @"Data Source=DESKTOP-238CB0R\SQLEXPRESS02;Initial Catalog=Tour;
                    Integrated Security=True;";

        public void HienThi()
        {
            SqlConnection con = new SqlConnection(ketnoi);
            SqlCommand cmd = new SqlCommand("select * from KhachHang order by MaKH asc", con);
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

        private void NhanVien_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void KhachHang_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void themNV_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ketnoi);
            try
            {
                con.Open();
                if (textBox1.Text != "" && textBox8.Text != "" && textBox2.Text != "" && textBox10.Text != ""
                    && textBox11.Text != "" && textBox3.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("insert into KhachHang values(" +
                        "'" + textBox1.Text + "', '" + textBox8.Text + "', '" + textBox2.Text + "'," +
                        " '" + textBox10.Text + "', '" + textBox11.Text + "', '" + textBox3.Text + "')", con);
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

        private void suaNV_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ketnoi);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update KhachHang set TenKH ='" + textBox2.Text + "'," +
                    "SDT ='" + textBox10.Text + "'," +
                    "HoChieu ='" + textBox11.Text + "',MaTour ='" + textBox8.Text + "',MaNV ='" + textBox3.Text + "' " +
                    "where MaKH ='" + textBox1.Text + "'", con);
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

        private void xoaNv_Click_1(object sender, EventArgs e)
        {
            DialogResult tb;
            tb = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection(ketnoi);
                SqlCommand cmd = new SqlCommand("delete from KhachHang where MaKH='" + textBox1.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                HienThi();
                con.Close();
            }
        }

        private void timNv_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ketnoi);
            SqlCommand cmd = new SqlCommand("select * from KhachHang where MaKH=@manv", con);
            con.Open();
            cmd.Parameters.AddWithValue("manv", textBox7.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox11.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            if (!String.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[6].Value.ToString()))
            {
                byte[] data = (byte[])dataGridView1.CurrentRow.Cells[6].Value;
                MemoryStream ms = new MemoryStream(data);
                pictureBox1.Image = Image.FromStream(ms);
            }
            else
            {
                //MessageBox.Show(System.IO.Directory.GetCurrentDirectory());
                pictureBox1.Image = pictureBox1.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "/user-3331257__340.png");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        public void insert(byte[] image)
        {
            SqlConnection con = new SqlConnection(ketnoi);
            con.Open();
            SqlCommand cmd = new SqlCommand("update KhachHang set Anh=@image  where MaKH ='" + textBox1.Text + "'", con);
            //SqlCommand cmd = new SqlCommand("Insert into NhanVien(MaNV,TenNV,DiaChi,NgaySinh,Luong) values (@ma,@ten,@diachi,@ngaysinh,@luong,@image)", con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@ma", textBox1.Text);
            //cmd.Parameters.AddWithValue("@ten", textBox8.Text);
            //cmd.Parameters.AddWithValue("@diachi", textBox2.Text);
            //cmd.Parameters.AddWithValue("@ngaysinh", textBox10.Text);
            //cmd.Parameters.AddWithValue("@luong", textBox11.Text);

            cmd.Parameters.AddWithValue("@image", image);
            cmd.ExecuteNonQuery();
        }

        byte[] ConvertImageToBytes(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public Image ConvertByteArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg)|*.jpg;*.Jpeg", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    insert(ConvertImageToBytes(pictureBox1.Image));
                    HienThi();
                }
            }
        }
    }
}
