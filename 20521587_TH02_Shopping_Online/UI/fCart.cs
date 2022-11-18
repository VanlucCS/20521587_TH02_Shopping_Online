using System;
using System.Collections.Generic;
using _20521587_TH02_Shopping_Online.DAL;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace _20521587_TH02_Shopping_Online.UI
{
    public partial class fCart : Form
    {
        DataTable dt = new DataTable();
        DataTable dt_cart = new DataTable();
        CartDAL cartdal = new CartDAL();

        ResourceManager rm = new ResourceManager("_20521587_TH02_Shopping_Online.Product",
            Assembly.GetExecutingAssembly());
        static float Cost = 0;
        //static float[] Costperitem= { };
        List<int> Costperitem = new List<int>();
        List<string> ListItem = new List<string>();
        public fCart()
        {
            InitializeComponent();
            dt_cart = cartdal.Select();
            ShoppingDAL spdal = new ShoppingDAL();
            dt = spdal.Select();

            //listView load item in cart database
            var imageList = new ImageList();
            imageList.ImageSize = new Size(124, 124);
            //listView1.View = View.LargeIcon;
            //listView1.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            //foreach (DataRow row in dt_cart.Rows)
            //{
                
            //    imageList.Images.Add(row[0].ToString(), (Image)rm.GetObject(row[0].ToString()));
            //}
            //listView1.LargeImageList = imageList;
            //for (int i = 0; i < dt_cart.Rows.Count; i++)
            //{
            //    this.listView1.Items.Add(dt_cart.Rows[i][1].ToString());
            //    listView1.Items[listView1.Items.Count - 1].ImageKey = dt_cart.Rows[i][0].ToString();
            //}
            //end

            DataGridViewCheckBoxColumn GvCheckBox = new DataGridViewCheckBoxColumn();
            GvCheckBox.HeaderText = "Chọn Sản Phẩm";
            DataGridViewImageColumn GvImage = new DataGridViewImageColumn();
            GvImage.ImageLayout = DataGridViewImageCellLayout.Zoom;
            GvImage.HeaderText = "Sản Phẩm";
            this.dataGridView1.Columns.Add(GvCheckBox);
            this.dataGridView1.Columns.Add(GvImage);
            this.dataGridView1.Columns.Add("Tên sản phẩm", "Tên sản phẩm");
            this.dataGridView1.Columns.Add("Số lượng","Số lượng");
            this.dataGridView1.Columns.Add("Tiền","Tiền");
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.RowTemplate.Height = 124;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.GridColor = Color.White;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Columns[2].ReadOnly = true;
            this.dataGridView1.Columns[4].ReadOnly = true;


            foreach (DataRow row in dt_cart.Rows)
            {
                //imageList.Images.Add(row[0].ToString(), (Image)rm.GetObject(row[0].ToString()));
                //Image img = (Image)rm.GetObject(row[0].ToString());
                DataRow dr = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("MASP") == row[0].ToString());
                int d = int.Parse(dr[3].ToString()) * int.Parse(row[1].ToString());
                //Cost += d;
                Costperitem.Add(d);
                ListItem.Add(row[0].ToString());
                //dataGridView1.Rows.Add(false, (Image)rm.GetObject(row[0].ToString()), dr[1].ToString()+"\nSố lượng :"+ row[1].ToString());
                dataGridView1.Rows.Add(false, (Image)rm.GetObject(row[0].ToString()), dr[1].ToString(), row[1].ToString(), string.Format("{0:C}", d).Substring(1, string.Format("{0:C}", d).Length - 4) + " đ");
            }
            
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // tổng giá trị các sản phẩm được chọn
            //label3.Text = string.Format("{0:C}", Cost).Substring(1, string.Format("{0:C}", Cost).Length-4);
        }
        
        private void fCart_Load(object sender, EventArgs e)
        {
                

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Cells[0].Value = true;
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Cells[0].Value = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // cac san pham chon mua
            List<int> chiphitt = new List<int>();

            //float Cost_Purchse = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() == "True")
                {
                    chiphitt.Add( Costperitem[row.Index]);

                }

            }

            //MessageBox.Show(Cost_Purchse.ToString());
            //MessageBox.Show(ListItem.ToArray().ToString());
            fPurchase f = new fPurchase(this.dataGridView1, Cost, ListItem, chiphitt);
            f.ShowDialog();
            Cost = 0;
            label3.Text = string.Format("{0:C}", Cost).Substring(1, string.Format("{0:C}", Cost).Length - 4);

            Costperitem.Clear();
            dt_cart = cartdal.Select();
            dataGridView1.Rows.Clear();
            // cập nhập sản phẩm sau khi mua
            foreach (DataRow row in dt_cart.Rows)
            {
                DataRow dr = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("MASP") == row[0].ToString());
                int d = int.Parse(dr[3].ToString()) * int.Parse(row[1].ToString());
                Costperitem.Add(d);
                ListItem.Add(row[0].ToString());
                dataGridView1.Rows.Add(false, (Image)rm.GetObject(row[0].ToString()), dr[1].ToString(), row[1].ToString(), string.Format("{0:N0}", d) + " đ");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int pre_value = int.Parse(dt_cart.Rows[e.RowIndex][1].ToString());
                DataGridViewRow dr = dataGridView1.Rows[e.RowIndex];
                int soLuong = int.Parse(dr.Cells[3].Value.ToString());
                CartDAL cartdal = new CartDAL();
                cartdal.update(dt_cart.Rows[e.RowIndex][0].ToString(), soLuong);
                cartdal.delete();


                ShoppingDAL shoppingDAL = new ShoppingDAL();
                shoppingDAL.update(dt_cart.Rows[e.RowIndex][0].ToString(), soLuong-pre_value);
                DataRow dr_update = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("MASP") == dt_cart.Rows[e.RowIndex][0].ToString());
                int d = int.Parse(dr_update[3].ToString()) *soLuong;
                dataGridView1.Rows[e.RowIndex].Cells[4].Value = string.Format("{0:N0}", d) + " đ";
            }
            if(e.ColumnIndex == 0)
            {
                DataGridViewRow dr = dataGridView1.Rows[e.RowIndex];
                string check = dr.Cells[0].Value.ToString();
                //MessageBox.Show(check);

                // cập nhập số tiền khi chọn sản phẩm mua
                if(check == "True")
                {
                    Cost += Costperitem[e.RowIndex];
                    label3.Text = string.Format("{0:C}", Cost).Substring(1, string.Format("{0:C}", Cost).Length - 4);

                }
                else
                {
                    Cost -= Costperitem[e.RowIndex];
                    label3.Text = string.Format("{0:C}", Cost).Substring(1, string.Format("{0:C}", Cost).Length - 4);

                }
            }
        }
    }
}
