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
        string PID = "SP01";
        int soluong = 1;
        DataTable dt = new DataTable(); 
        DataTable dt_cart = new DataTable();
        ResourceManager rm = new ResourceManager("_20521587_TH02_Shopping_Online.Product",
            Assembly.GetExecutingAssembly());
        
        ShoppingDAL sdal = new ShoppingDAL();
        CartDAL cartdal = new CartDAL();
        ShoppingDAL shoppingdal = new ShoppingDAL();

        public fProduct(string ProductID)
        {
            InitializeComponent();
            PID = ProductID;

            dt = sdal.Select();
            dt_cart = cartdal.Select();
            cButton1.Text = dt_cart.Rows.Count.ToString();
            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == PID)
                {
                    lname.Text = row[1].ToString();
                    lcost.Text = string.Format("{0:N0}",int.Parse( row[3].ToString()));
                    lcostmax.Text = string.Format("{0:N0}", int.Parse(row[3].ToString()) * 2);
                    List<string> d = new List<string>();
                    d = row[8].ToString().Split(',').ToList();
                    foreach (string item in d)
                    {
                        richTextBox2.AppendText(item +"\n");

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
            if(int.Parse(tbsoluong.Text) > int.Parse(lsoluong.Text))
            {
                MessageBox.Show("số lượng bạn chọn lớn hơn số hàng tồn tại trong kho hiện tại","Cháy hàng",MessageBoxButtons.OK);
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
            if(soluong+1 <= int.Parse(lsoluong.Text))
                soluong = soluong + 1;
            tbsoluong.Text = soluong.ToString();
        }

        private void bSub_Click(object sender, EventArgs e)
        {
            if (soluong-1> 0)
                soluong= soluong -1;
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
    }
}
