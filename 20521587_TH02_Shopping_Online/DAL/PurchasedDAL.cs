using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _20521587_TH02_Shopping_Online.BLL;

namespace _20521587_TH02_Shopping_Online.DAL
{
    class PurchasedDAL
    {
        #region Select method for Product Module
        public DataTable Select()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString))
                {
                    if (cnn.State == ConnectionState.Closed)
                    {
                        cnn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM PURCHASED", cnn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("error");
                throw;
            }

            return dt;
        }
        #endregion
        #region Method to Insert Product in database
        public bool Insert(PurchasedBLL p)
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);

            try
            {
                //SQL Query to insert product into database
                String sql = @"INSERT INTO PURCHASED (MAKH,TENKH,TENNV,MASP,MAHD,SOHD,TENSP,SOLUONG,TT,DIACHI,VANCHUYEN,MESS,VOURCHER,TIENHANG,PHIVANCHUYEN,GIAMGIA,VAT,TONGTHANHTOAN,THOIGIANMUA) " +
                                "VALUES (@MAKH,@TENKH,@TENNV,@MASP,@MAHD,@SOHD,@TENSP,@SOLUONG,@TT,@DIACHI,@VANCHUYEN,@MESS,@VOURCHER,@TIENHANG,@PHIVANCHUYEN,@GIAMGIA,@VAT,@TONGTHANHTOAN,@THOIGIANMUA)";

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passign the values through parameters
                cmd.Parameters.AddWithValue("@MAKH", p.MAKH);
                cmd.Parameters.AddWithValue("@TENKH", p.TENKH);
                cmd.Parameters.AddWithValue("@TENNV", p.TENNV);
                cmd.Parameters.AddWithValue("@MASP", p.MASP);
                cmd.Parameters.AddWithValue("@MAHD", p.MAHD);
                cmd.Parameters.AddWithValue("@SOHD", p.SOHD);
                cmd.Parameters.AddWithValue("@TENSP", p.TENSP);
                cmd.Parameters.AddWithValue("@SOLUONG", p.SOLUONG);
                cmd.Parameters.AddWithValue("@TT", p.TT);
                cmd.Parameters.AddWithValue("@DIACHI", p.DIACHI);
                cmd.Parameters.AddWithValue("@VANCHUYEN", p.VANCHUYEN);
                cmd.Parameters.AddWithValue("@MESS", p.MESS);
                cmd.Parameters.AddWithValue("@VOURCHER", p.VOURCHER);
                cmd.Parameters.AddWithValue("@TIENHANG", p.TIENHANG);
                cmd.Parameters.AddWithValue("@PHIVANCHUYEN", p.PHIVANCHUYEN);
                cmd.Parameters.AddWithValue("@GIAMGIA", p.GIAMGIA);
                cmd.Parameters.AddWithValue("@VAT", p.VAT);
                cmd.Parameters.AddWithValue("@TONGTHANHTOAN", p.TONGTHANHTOAN);
                cmd.Parameters.AddWithValue("@THOIGIANMUA", p.THOIGIANMUA);

                //Opening the Database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the query is executed successfully then the value of rows will be greater than 0 else it will be less than 0
                if (rows > 0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //FAiled to Execute Query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
    }
}
