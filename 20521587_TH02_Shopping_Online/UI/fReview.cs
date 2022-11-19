using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _20521587_TH02_Shopping_Online.BLL;
using _20521587_TH02_Shopping_Online.DAL;

namespace _20521587_TH02_Shopping_Online.UI
{
    public partial class fReview : Form
    {
        string MASP;
        int vote = 0;
        ResourceManager rm = new ResourceManager("_20521587_TH02_Shopping_Online.Product",
            Assembly.GetExecutingAssembly());
        public fReview(string masp)
        {
            InitializeComponent();
            pictureBox8.Image = (Image)rm.GetObject(masp);
            MASP = masp;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;

        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            vote = 1;
            pictureBox5.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            pictureBox4.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            pictureBox3.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            pictureBox2.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;

            pictureBox1.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            vote = 2;

            pictureBox5.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            pictureBox4.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            pictureBox3.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;

            pictureBox2.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            pictureBox1.Image = pictureBox2.Image;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            vote = 3;

            pictureBox5.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            pictureBox4.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;

            pictureBox3.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            pictureBox2.Image = pictureBox3.Image;
            pictureBox1.Image = pictureBox2.Image;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            vote = 4;

            pictureBox5.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            pictureBox4.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            pictureBox3.Image = pictureBox4.Image;
            pictureBox2.Image = pictureBox3.Image;
            pictureBox1.Image = pictureBox2.Image;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            vote = 5;

            pictureBox5.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            pictureBox4.Image = pictureBox5.Image;
            pictureBox3.Image = pictureBox4.Image;
            pictureBox2.Image = pictureBox3.Image;
            pictureBox1.Image = pictureBox2.Image;
        }

        private void cButton1_Click(object sender, EventArgs e)
        {
            ReviewBLL rbll = new ReviewBLL();
            rbll.MASP = MASP;
            rbll.VOTE = vote;
            rbll.DANHGIA = textBox1.Text.ToString();

            ReviewDAL rdal = new ReviewDAL();
            rdal.Insert(rbll);
            this.Close();
        }
    }
}
