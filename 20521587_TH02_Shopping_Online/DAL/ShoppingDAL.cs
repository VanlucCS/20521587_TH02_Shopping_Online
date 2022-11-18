using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20521587_TH02_Shopping_Online.DAL
{
    class ShoppingDAL
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
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM SANPHAM", cnn))
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
        #region update sản phẩm
        public bool update(string masp,int soLuong)
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);

            try
            {
                //SQL Query to insert product into database
                String sql = @"UPDATE SANPHAM SET SOLUONG = SOLUONG - @SOLUONG WHERE MASP = @MAMUA";

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passign the values through parameters
                cmd.Parameters.AddWithValue("@MAMUA", masp);
                cmd.Parameters.AddWithValue("@SOLUONG", soLuong);
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
