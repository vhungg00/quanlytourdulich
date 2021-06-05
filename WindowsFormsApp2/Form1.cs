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
    public partial class Form1 : Form
    {
        int panelWidth;
        bool Hidden;


        public Form1()
        {
            InitializeComponent();
            panelWidth = panel4.Width;
            Hidden = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.Show();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {
                panel4.Width = panel4.Width + 10;
                if (panel4.Width >= panelWidth)
                {
                    timer1.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                panel4.Width = panel4.Width - 10;
                if (panel4.Width <= 50)
                {
                    timer1.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Tour t = new Tour();
            t.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HoaDon dh = new HoaDon();
            dh.Show();
        }
    }
}

