using System;
using _20521587_TH02_Shopping_Online.DAL;
using _20521587_TH02_Shopping_Online.BLL;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Resources;
using System.Reflection;

namespace _20521587_TH02_Shopping_Online.UI
{
    public partial class fShopping : Form
    {
        string Pload = "all";
        DataTable dt = new DataTable();
        DataTable dt_cart = new DataTable();

        ResourceManager rm = new ResourceManager("_20521587_TH02_Shopping_Online.Product",
            Assembly.GetExecutingAssembly());
        public fShopping()
        {
            InitializeComponent();
            listView2.GridLines = false;
            // kết nối với cartdal giỏ hàng
            CartDAL cartdal = new CartDAL();
            dt_cart = cartdal.Select();
            cButton2.Text = dt_cart.Rows.Count.ToString();
            listView1.ItemMouseHover += new ListViewItemMouseHoverEventHandler(item_hover);
            
            // lấy danh saacsh sản phẩm từ database
            ShoppingDAL sdal = new ShoppingDAL();
            dt = sdal.Select();

            Load_Product();
            Load_HisProduct();
            // sửa kích thước control
            listView1.Size = new Size(760, (dt.Rows.Count) / 4 * 240);
            listView2.Top = listView1.Top + listView1.Height + 20;
            label6.Top = listView2.Top - 25;
        }
        public void Load_Product()
        {
            listView1.Clear();
            int count = 0;

            var imageList = new ImageList();
            imageList.ImageSize = new Size(124, 124);
            listView1.View = View.LargeIcon;
            listView1.LabelWrap = true;
            listView1.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            // lấy ảnh từ source
            foreach (DataRow row in dt.Rows)
                imageList.Images.Add(row[0].ToString(), (Image)rm.GetObject(row[0].ToString()));

            listView1.LargeImageList = imageList;

            // thêm sản phẩm
            if (Pload != "all")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][2].ToString() == Pload)
                    {
                        string cost = dt.Rows[i][3].ToString();
                        this.listView1.Items.Add(dt.Rows[i][1].ToString() + "\n " + string.Format("{0:N0}", int.Parse(cost)) + " đ");
                        listView1.Items[listView1.Items.Count - 1].ImageKey = dt.Rows[i][0].ToString();
                        listView1.Items[listView1.Items.Count - 1].Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        count++;
                    }
                }
            }   
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        string cost = dt.Rows[i][3].ToString();
                        this.listView1.Items.Add(dt.Rows[i][1].ToString() + "\n " + string.Format("{0:N0}", int.Parse(cost)) + " đ");
                        listView1.Items[listView1.Items.Count - 1].ImageKey = dt.Rows[i][0].ToString();
                        listView1.Items[listView1.Items.Count - 1].Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        count++;
                }
            }

            // thay đổi kích thước list viwe theo số lượng sản phẩm

            int lvHeight = (count + 2) / 4;
            if (lvHeight < 3)
                lvHeight = 3;
            listView1.Size = new Size(760, lvHeight * 240);
            listView2.Top = listView1.Top + listView1.Height+20;
            label6.Top = listView2.Top - 25;
            Load_HisProduct();
            // scroll in tow tab
            // in update
        }
        // load sản phẩm xem gần đây
        public void Load_HisProduct()
        {
            listView2.Clear();
            // datatable lưu lịch sử mua sắm
            DataTable hisDTable = new DataTable();
            #region Connect to Sql
            ViewedProductsDAL ViedHis = new ViewedProductsDAL();
            hisDTable = ViedHis.SelectDistinct();
            #endregion


            var imageList = new ImageList();
            imageList.ImageSize = new Size(124, 124);
            listView2.View = View.LargeIcon;
            listView2.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            foreach (DataRow row in dt.Rows)
                imageList.Images.Add(row[0].ToString(), (Image)rm.GetObject(row[0].ToString()));

            listView2.LargeImageList = imageList;

            // thêm sản phẩm vào lịch sử xem
            for (int i = 0; i < hisDTable.Rows.Count; i++)
            {
                DataRow dr = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("MASP") == hisDTable.Rows[i][0].ToString());
                string cost = dr[3].ToString();
                this.listView2.Items.Add(dr[1].ToString() + "\n " + string.Format("{0:N0}", int.Parse(cost)) + " đ");
                listView2.Items[listView2.Items.Count - 1].ImageKey = dr[0].ToString();
                listView2.Items[listView2.Items.Count - 1].Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Load_Product();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Pload = "Sach";
            //label2.ForeColor = Color.Black;
            //label2.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            label3.ForeColor = Color.Blue;
            label3.Font = new Font("Tahoma", 14, FontStyle.Bold);
            label4.ForeColor = Color.Black;
            label4.Font = new Font("Tahoma", 14, FontStyle.Regular);
            Load_Product();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //Pload = "Nha";
            //label2.ForeColor = Color.Blue;
            //label2.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
            //label3.ForeColor = Color.Black;
            //label3.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            //label4.ForeColor = Color.Black;
            //label4.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            //Load_Product();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Pload = "Thuc An";
            //label2.ForeColor = Color.Black;
            //label2.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            label3.ForeColor = Color.Black;
            label3.Font = new Font("Tahoma", 14, FontStyle.Regular);
            label4.ForeColor = Color.Blue;
            label4.Font = new Font("Tahoma", 14, FontStyle.Bold);
            Load_Product();
        }
        ListViewItemMouseHoverEventArgs f = null;
        private void item_hover(object s, ListViewItemMouseHoverEventArgs e)
        {
            if (f != null)
            {
                f.Item.ForeColor = Color.Black;
                f.Item.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

            }
            e.Item.ForeColor = Color.Blue;
            e.Item.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular); 
            f = e;
        }

        private void fShopping_Load(object sender, EventArgs e)
        {

        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            ViewedProductsDAL vdal = new ViewedProductsDAL();
            ViewedProductsBLL vbll = new ViewedProductsBLL();
            ListViewItem item = listView1.SelectedItems[0];
            vbll.MASP = item.ImageKey;
            vbll.THOIGIAN = DateTime.Now;
            vdal.Insert(vbll);
            fProduct f = new fProduct(item.ImageKey);
            f.ShowDialog();
            Load_HisProduct();
            CartDAL cartdal = new CartDAL();
            dt_cart = cartdal.Select();
            cButton2.Text = dt_cart.Rows.Count.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {
            textBox1.Select();
           textBox1.Text = "";
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            //if (textBox1.Text == "")
            //    textBox1.Text = "Tìm sản phẩm";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "Tìm sản phẩm")
                textBox1.Text = "";

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            fCart f = new fCart();
            f.ShowDialog();
        }

        private void cButton2_Click(object sender, EventArgs e)
        {

        }

        private void cButton1_Click(object sender, EventArgs e)
        {
            Load_Product();
            foreach (ListViewItem item in listView1.Items)
            {
                if (!item.Text.ToLower().Contains(textBox1.Text.ToLower()))
                    listView1.Items.Remove(item);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                Load_Product();
            //// tìm sản phẩm theo tên
            //    Load_Product();
            //if (textBox1.Text != "" && textBox1.Text != "Tìm sản phẩm")
            //{
            //    foreach (ListViewItem item in listView1.Items)
            //    {
            //        if (!item.Text.ToLower().Contains(textBox1.Text.ToLower()))
            //            listView1.Items.Remove(item);
            //    }
            //}
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = listView2.SelectedItems[0];
            fProduct f = new fProduct(item.ImageKey);
            f.ShowDialog();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            //cButton1_Click(null, null);
            //MessageBox.Show("anc");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cButton1_Click(null, null);

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            fHis f = new fHis();
            f.ShowDialog();
        }
    }
}

