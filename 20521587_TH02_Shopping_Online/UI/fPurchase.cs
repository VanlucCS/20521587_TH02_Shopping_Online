using System;
using _20521587_TH02_Shopping_Online.DAL;
using _20521587_TH02_Shopping_Online.BLL;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20521587_TH02_Shopping_Online.UI
{
    public partial class fPurchase : Form
    {
        List<string> SanPhamID = new List<string>();
        List<int> CostPerItem = new List<int>();
        int  phiVanChuyen = 0;
        float tongThanhToan = 0;
        public fPurchase(DataGridView grv,float Cost,List<string> ListItem,List<int> costperitem)
        {
            InitializeComponent();
            //foreach (DataGridViewRow item in grv.Rows)
            //{
            //    if (item.Cells[0].Value.ToString() == "false")
            //        grv.Rows.RemoveAt(item.Index);
            //}
            label13.Text = string.Format("{0:N0}", int.Parse(Cost.ToString()));
            label14.Text = string.Format("{0:N0}", phiVanChuyen);
            List<string> toRemove = new List<string>();
            CostPerItem = costperitem;
            SanPhamID = ListItem;
            try
            {
                if (dataGridView1.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn dgvc in grv.Columns)
                    {
                        dataGridView1.Columns.Add(dgvc.Clone() as DataGridViewColumn);
                    }
                }

                DataGridViewRow row = new DataGridViewRow();
                //MessageBox.Show(grv.Rows.Count.ToString());
                for (int i = 0; i < grv.Rows.Count; i++)
                {
                    row = (DataGridViewRow)grv.Rows[i].Clone();
                    int intColIndex = 0;
                    //MessageBox.Show(i.ToString());
                    //MessageBox.Show(grv.Rows[i].Cells[0].Value.ToString());
                    //MessageBox.Show(SanPhamID[i]);

                    if (grv.Rows[i].Cells[0].Value.ToString() == "True")
                    {
                        //MessageBox.Show(i.ToString());

                        foreach (DataGridViewCell cell in grv.Rows[i].Cells)
                        {
                            row.Cells[intColIndex].Value = cell.Value;
                            intColIndex++;
                        }
                        dataGridView1.Rows.Add(row);

                    }
                    else
                    {
                        try
                        {
                            toRemove.Add(ListItem[i]);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            throw;
                        }
                    }
                }
                SanPhamID = SanPhamID.Except(toRemove).ToList();
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Refresh();
                dataGridView1.RowTemplate.Height = 64;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.GridColor = Color.White;

            }
            catch (Exception ex)
            {
                //cf.ShowExceptionErrorMsg("Copy DataGridViw", ex);
            }
            label3.Text = string.Format("{0:C}", Cost).Substring(1, string.Format("{0:C}", Cost).Length - 4);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[3].ReadOnly = true ;
            //dataGridView1.RowTemplate.Height = 50;

            float tongThanhToan = Cost + float.Parse(phiVanChuyen.ToString()) + float.Parse((Cost * 0.05).ToString());
            label3.Text = string.Format("{0:N0}", int.Parse( tongThanhToan.ToString()));

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.Text = "Nhập địa chỉ giao hàng";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            //if (textBox2.Text == "")
            //    textBox2.Text = "Nhập địa chỉ giao hàng";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // xóa sản phẩm trong giỏ hàng sau khi mua

            CartDAL cdal = new CartDAL();

            // lưu hóa dơn
            PurchasedDAL pDAL = new PurchasedDAL();
            PurchasedBLL pBLL = new PurchasedBLL();
            DataTable dt = new DataTable();
            dt = pDAL.Select();
            int maxsohd = 1;
            try
            {
                maxsohd = Convert.ToInt32(dt.Compute("max([SOHD])", string.Empty));
            }
            catch (Exception)
            {
                
            }
            pBLL.SOHD = maxsohd + 1;
            if (maxsohd + 1 < 10)
            {
                pBLL.MAHD = "HD0" + pBLL.SOHD.ToString();
                pBLL.MAKH = "KH0" + pBLL.SOHD.ToString();

            }    
            else
            {
                pBLL.MAKH = "KH" + pBLL.SOHD.ToString();
                pBLL.MAHD = "HD" + pBLL.SOHD.ToString();
            }
            pBLL.TENKH = textBox4.Text;
            pBLL.TENNV = "Văn Lực";
            pBLL.DIACHI = textBox1.Text;
            pBLL.VANCHUYEN = comboBox1.Text;
            pBLL.MESS = textBox2.Text;
            pBLL.VOURCHER = comboBox2.Text;
            pBLL.TIENHANG = float.Parse( label13.Text.Replace(",", ""));
            pBLL.PHIVANCHUYEN = float.Parse(phiVanChuyen.ToString());
            pBLL.GIAMGIA = float.Parse(label19.Text);
            pBLL.VAT = float.Parse(label21.Text);
            pBLL.TONGTHANHTOAN = float.Parse(label3.Text.Replace(",", ""));
            pBLL.THOIGIANMUA = DateTime.Now;
            //foreach (string ID in SanPhamID)
            //{
            //    cdal.sell(ID);
            //    pBLL.MASP = ID;
            //    pDAL.Insert(pBLL);
            //}
            for (int i = 0; i < SanPhamID.Count; i++)
            {
                cdal.sell(SanPhamID[i]);
                pBLL.MASP = SanPhamID[i];
                pBLL.TENSP = dataGridView1[2, i].Value.ToString();
                pBLL.SOLUONG = int.Parse(dataGridView1[3, i].Value.ToString());
                pBLL.TT = CostPerItem[i];
                pDAL.Insert(pBLL);
            }
            // xóa sản phẩm khỏi giỏ hàng
            MessageBox.Show("mua sản phẩm thành công");
            fReportViewer f = new fReportViewer(pBLL.MAHD);
            f.ShowDialog();
            this.Close();
            //  tùy chọn in hóa đơn 

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Thanh toán khi nhận hàng")
                textBox3.Visible = false;
            else
                textBox3.Visible = true;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Miễn phí vận chuyển")
                phiVanChuyen = 0;
            else
                phiVanChuyen = 35000;
            label14.Text = string.Format("{0:N0}", phiVanChuyen);
            float tongThanhToan = float.Parse(label13.Text.Replace(",", "")) + float.Parse(phiVanChuyen.ToString()) + float.Parse((float.Parse(label13.Text.Replace(",", "")) * 0.05).ToString());
            label3.Text = string.Format("{0:N0}", int.Parse(tongThanhToan.ToString()));
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
