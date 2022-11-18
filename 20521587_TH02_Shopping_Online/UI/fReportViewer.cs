using Microsoft.Reporting.WinForms;
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
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using ZXing.QrCode.Internal;
using System.Drawing.Imaging;

namespace _20521587_TH02_Shopping_Online
{
    public partial class fReportViewer : Form
    {
        PurchasedDAL pdal = new PurchasedDAL();
        DataTable data = new DataTable();
        ShoppingDAL sdal = new ShoppingDAL();
        DataTable shop_data = new DataTable();
        int index;
        string SOHD = "HD01";
        public fReportViewer(string sohd)
        {
            InitializeComponent();
            SOHD = sohd;
            data = pdal.Select();
            shop_data = sdal.Select();
            LoadReportTest();
        }

        private void fReportViewer_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();

        }
        public void LoadReportTest()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(string));
            dt.Columns.Add("TenSP", typeof(string));
            dt.Columns.Add("SL", typeof(string));
            dt.Columns.Add("Gia", typeof(string));
            dt.Columns.Add("TT", typeof(string));
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][4].ToString() == SOHD)
                {
                    index = i;
                    DataRow dr = shop_data.AsEnumerable().SingleOrDefault(r => r.Field<string>("MASP") == data.Rows[i][3].ToString());
                    dt.Rows.Add((dt.Rows.Count + 1).ToString(), dr[1].ToString(), data.Rows[i][7].ToString(), string.Format("{0:N0}", int.Parse(dr[3].ToString())), string.Format("{0:N0}", int.Parse( data.Rows[i][8].ToString()))+" đ");
                }
                
            }

            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = "../../Report.rdlc"; //để file report trong Debug của project

            ReportDataSource dts = new ReportDataSource();

            dts.Name = "DataSet1"; //Đặt đúng tên khi đặt trong report -- có tên mặc định là DataSet1
            dts.Value = dt;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(dts);
            //data.Rows[i][4]
            ReportParameter para1 = new ReportParameter();
            para1.Name = "TENNV"; //Đặt đúng tên khi đặt trong report
            para1.Values.Add(data.Rows[index][2].ToString());
            ReportParameter para2 = new ReportParameter();
            para2.Name = "TENKH";
            para2.Values.Add(data.Rows[index][1].ToString());
            ReportParameter para3 = new ReportParameter();
            para3.Name = "MAHD";
            para3.Values.Add(data.Rows[index][4].ToString());

            // tao qr code 
            BarcodeWriter barcodeWriter = new BarcodeWriter();

            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Renderer = new BitmapRenderer();

            EncodingOptions encodingOptions = new EncodingOptions();
            encodingOptions.Width = 500;
            encodingOptions.Height = 500;
            encodingOptions.Margin = 0;
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L);

            barcodeWriter.Options = encodingOptions;

            Bitmap bm =  barcodeWriter.Write(data.Rows[index][4].ToString());
            bm.Save(@"./qr.png", ImageFormat.Png);

            ReportParameter para4 = new ReportParameter();
            para4.Name = "THOIGIAN";
            para4.Values.Add(data.Rows[index][18].ToString());
            ReportParameter para5 = new ReportParameter();
            para5.Name = "GIAMGIA";
            para5.Values.Add(data.Rows[index][15].ToString());
            ReportParameter para6 = new ReportParameter();
            para6.Name = "VAT";
            para6.Values.Add(data.Rows[index][16].ToString());
            ReportParameter para7 = new ReportParameter();
            para7.Name = "TONG";
            para7.Values.Add(string.Format("{0:N0}", int.Parse(data.Rows[index][17].ToString())) + " đ" );
            string workingDirectory = Environment.CurrentDirectory;
            ReportParameter para8 = new ReportParameter("IMG", "file:///" + workingDirectory + "/qr.png");
            //para8.Name = "IMG";
            //para8.Values.Add("./qr.png");
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { para1, para2, para3, para4, para5, para6, para7 ,para8});

        }
        private void reportViewer2_Load(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
