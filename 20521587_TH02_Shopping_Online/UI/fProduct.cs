using System;
using System.Collections.Generic;
using _20521587_TH02_Shopping_Online.DAL;
using _20521587_TH02_Shopping_Online.BLL;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20521587_TH02_Shopping_Online.UI
{
    public partial class fProduct : Form
    {
        string PID = "SP05";
        int soluong = 1;
        DataTable dt = new DataTable();
        DataTable dt_cart = new DataTable();
        ResourceManager rm = new ResourceManager("_20521587_TH02_Shopping_Online.Product",
            Assembly.GetExecutingAssembly());

        ShoppingDAL sdal = new ShoppingDAL();
        CartDAL cartdal = new CartDAL();
        ShoppingDAL shoppingdal = new ShoppingDAL();
        DataTable dt_review = new DataTable();
        ReviewDAL rdal = new ReviewDAL();

        public fProduct(string ProductID)
        {
            InitializeComponent();
            setimage(1,1,1,0,0);
            dt_review = rdal.Select();
            float advote = 5;
            int numpp = 1;
            for (int i = 0; i < dt_review.Rows.Count; i++)
            {
                if (dt_review.Rows[i][0].ToString() == ProductID)
                {
                    richTextBox1.AppendText("\n USER: " + numpp.ToString() + "\n" + "\t" + dt_review.Rows[i][2].ToString());
                    advote = (advote + float.Parse(dt_review.Rows[i][1].ToString())) / 2;
                    numpp++;
                }
            }
            PID = ProductID;
            //MessageBox.Show(advote.ToString());
            int v = (int)advote;
            //MessageBox.Show(v.ToString());
            if (v == 1)
                setimage(0, 0, 0, 0, 0);
            else if (v == 1)
                setimage(1, 0, 0, 0, 0);

            else
                    if (v == 2)
                setimage(1, 1, 0, 0, 0);

            else
                        if (v == 3)
                setimage(1, 1, 1, 0, 0);

            else
                             if (v == 4)
                setimage(1, 1, 1, 1, 0);

            else
                setimage(1, 1, 1, 1,1);


            dt = sdal.Select();
            dt_cart = cartdal.Select();
            cButton1.Text = dt_cart.Rows.Count.ToString();
            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == PID)
                {
                    lname.Text = row[1].ToString();
                    lcost.Text = string.Format("{0:N0}", int.Parse(row[3].ToString()));
                    lcostmax.Text = string.Format("{0:N0}", int.Parse(row[3].ToString()) * 2);
                    List<string> d = new List<string>();
                    d = row[8].ToString().Split(',').ToList();
                    foreach (string item in d)
                    {
                        richTextBox2.AppendText(item + "\n");

                    }

                    //lcost.Text = row[3].ToString();
                    xuatxu.Text = row[4].ToString();
                    hsd.Text = row[5].ToString();
                    nsx.Text = row[6].ToString();
                    lsoluong.Text = row[7].ToString();
                    break;
                }
            }
            pictureBox1.Image = (Image)rm.GetObject(PID);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btCart_Click(object sender, EventArgs e)
        {
            // trường hợp chọn số lượng chọn nhiều hơn hiện có
            if (int.Parse(tbsoluong.Text) > int.Parse(lsoluong.Text))
            {
                MessageBox.Show("số lượng bạn chọn lớn hơn số hàng tồn tại trong kho hiện tại", "Cháy hàng", MessageBoxButtons.OK);
                return;
            }
            ProductBLL p = new ProductBLL();
            p.MASP = PID;
            p.SOLUONG = soluong;
            bool success = cartdal.Insert(p);
            //if the product is added successfully then the value of success will be true else it will be false
            if (success == true)
            {
                dt_cart = cartdal.Select();
                cButton1.Text = dt_cart.Rows.Count.ToString();
            }
            cartdal.MergeItem();
            dt_cart = cartdal.Select();
            cButton1.Text = dt_cart.Rows.Count.ToString();
            // update số lượng có trong kho
            shoppingdal.update(PID, soluong);


            // update số lượng sau kkhi mua
            dt = sdal.Select();
            DataRow dr = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("MASP") == PID);
            lsoluong.Text = dr[7].ToString();

        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (soluong + 1 <= int.Parse(lsoluong.Text))
                soluong = soluong + 1;
            tbsoluong.Text = soluong.ToString();
        }

        private void bSub_Click(object sender, EventArgs e)
        {
            if (soluong - 1 > 0)
                soluong = soluong - 1;
            tbsoluong.Text = soluong.ToString();

        }

        private void cButton1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            fCart f = new fCart();
            f.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void setimage(int a, int b, int c, int d, int e)
        {
            pictureBox9.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            pictureBox10.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            pictureBox11.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            pictureBox12.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            pictureBox13.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__2_;
            if (e == 0)
                pictureBox9.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            if (d == 0)
                pictureBox10.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            if (c == 0)

                pictureBox11.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            if (b == 0)

                pictureBox12.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;
            if (a == 0)
                pictureBox13.Image = _20521587_TH02_Shopping_Online.Properties.Resources.star__3_;


            pictureBox14.Image = pictureBox9.Image;
            pictureBox15.Image = pictureBox10.Image;
            pictureBox16.Image = pictureBox11.Image;
            pictureBox17.Image = pictureBox12.Image;
            pictureBox18.Image = pictureBox13.Image;
        }
    }
}
