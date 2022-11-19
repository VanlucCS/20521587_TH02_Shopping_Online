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
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace _20521587_TH02_Shopping_Online.UI
{
    public partial class fHis : Form
    {

        VideoCaptureDevice capdv;
        DataTable dt = new DataTable();

        public fHis()
        {
            InitializeComponent();
            PurchasedDAL pdal = new PurchasedDAL();
            dt = pdal.Select();
            dt.Columns["SOLUONG"].ColumnName = "SL";
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["MAKH"].Visible = false;
            dataGridView1.Columns["TENKH"].Visible = false;
            dataGridView1.Columns["TENNV"].Visible = false;
            dataGridView1.Columns["MASP"].Visible = false;
            dataGridView1.Columns["SOHD"].Visible = false;
            dataGridView1.Columns["GIAMGIA"].Visible = false;
            dataGridView1.Columns["SL"].Width= 40;
            dataGridView1.Columns["TENSP"].Width= 200;
            dataGridView1.Columns["TT"].Width= 80;
            dataGridView1.Columns["GIAMGIA"].Width= 80;
            dataGridView1.Columns["MAHD"].Width= 50;
            dataGridView1.Columns["VAT"].Width= 40;
            dataGridView1.Columns["TONGTHANHTOAN"].Width= 120;
            dataGridView1.Columns["PHIVANCHUYEN"].Width= 120;
            dataGridView1.Columns["DIACHI"].Width= 200;
            dataGridView1.Columns["TT"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["TIENHANG"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["TONGTHANHTOAN"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["TONGTHANHTOAN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["SL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["TIENHANG"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["PHIVANCHUYEN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["GIAMGIA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["VAT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //GroupByGrid gr = new GroupByGrid();
            DataGridViewButtonColumn GvCheckBox = new DataGridViewButtonColumn();
            GvCheckBox.HeaderText = "ĐÁNH GIÁ SP";
            GvCheckBox.Name = "DANHGIASP";
            GvCheckBox.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(GvCheckBox);
            dataGridView1.Columns["DANHGIASP"].DisplayIndex = 0 ;
            dataGridView1.Columns["DANHGIASP"].Width = 50;


            dt.Columns["TENSP"].ColumnName = "Tên SP";
            dt.Columns["DIACHI"].ColumnName = "Địa Chỉ";
            dt.Columns["VANCHUYEN"].ColumnName = "Vận Chuyển";
            dt.Columns["TIENHANG"].ColumnName = "TIỀN HÀNG";
            dt.Columns["TONGTHANHTOAN"].ColumnName = "Tổng Thanh Toán";
            dt.Columns["PHIVANCHUYEN"].ColumnName = "Phí vận chuyển";
            dt.Columns["THOIGIANMUA"].ColumnName = "Thời gian mua";

            //dataGridView1.Columns["DANHGIASP"].ven = 50;
            //dataGridView1.Columns["DANHGIASP"]. = 50;

            //dataGridView1.Columns["DANHGIASP"].DefaultCellStyle.NullValue = "abc";
            //foreach (DataGridViewRow c in dataGridView1.Rows)
            //{
            //    c.Cells["DANHGIASP"].Value = "a";
            //}



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs args)
        {

            args.AdvancedBorderStyle.Bottom =DataGridViewAdvancedCellBorderStyle.None;

            if (args.RowIndex < 1 || args.ColumnIndex < 0)
                return;
            if (IsRepeatedCellValue(args.RowIndex, args.ColumnIndex))
            {
                args.AdvancedBorderStyle.Top =DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                args.AdvancedBorderStyle.Top = dataGridView1.AdvancedCellBorderStyle.Top;
            }

        }
    
        private bool IsRepeatedCellValue(int rowIndex, int colIndex)
        {
            DataGridViewCell currCell = dataGridView1.Rows[rowIndex].Cells[colIndex];
            DataGridViewCell prevCell = dataGridView1.Rows[rowIndex - 1].Cells[colIndex];
            if ((currCell.Value == prevCell.Value) ||
               (currCell.Value != null && prevCell.Value != null &&
               currCell.Value.ToString() == prevCell.Value.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs args)
        {
            if (args.RowIndex == 0)
                return;
            if (IsRepeatedCellValue(args.RowIndex, args.ColumnIndex))
            {
                args.Value = string.Empty;
                args.FormattingApplied = true;
            }
        }

        private void cButton1_Click(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = string.Format("MAHD LIKE '%{0}%'", textBox3.Text);
        }

        private void fHis_Load(object sender, EventArgs e)
        {

        }

        private void cButton2_Click(object sender, EventArgs e)
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            capdv = new VideoCaptureDevice(videoDevices[0].MonikerString);
            capdv.NewFrame += Capdv_Newframe;
            capdv.Start();
            timer1.Start();
        }
        private void Capdv_Newframe(object sender,NewFrameEventArgs e)
        {
            pictureBox3.Image = (Bitmap)e.Frame.Clone();
        }

        private void fHis_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (capdv.IsRunning)
                    capdv.Stop();
            }
            catch (Exception)
            {
               return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pictureBox3.Image != null)
            {
                BarcodeReader barcode = new BarcodeReader();
                Result result = barcode.Decode((Bitmap)pictureBox3.Image);
                if(result != null)
                {
                    textBox1.Text = result.ToString();
                    dt.DefaultView.RowFilter = string.Format("MAHD LIKE '%{0}%'", textBox1.Text);
                    timer1.Stop();
                    if (capdv.IsRunning)
                        capdv.Stop();
                }
            }
        }
        private void Search_Mahd(string mahd)
        {

        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //e.Row.Cells["DANHGIASP"].Value = "VOTE";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==0)
            {

                fReview f = new fReview(dataGridView1.Rows[e.RowIndex].Cells["MASP"].Value.ToString());
                f.ShowDialog();
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor =Color.White ;
            }   
        }
    }
}
