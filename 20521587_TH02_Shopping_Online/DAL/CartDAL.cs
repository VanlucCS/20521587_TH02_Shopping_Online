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
    class CartDAL
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
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM CART", cnn))
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
        public bool Insert(ProductBLL p)
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);

            try
            {
                //SQL Query to insert product into database
                String sql = "INSERT INTO CART (MASP,SOLUONG) VALUES (@MASP,@SOLUONG)";

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passign the values through parameters
                cmd.Parameters.AddWithValue("@MASP", p.MASP);
                cmd.Parameters.AddWithValue("@SOLUONG", p.SOLUONG);

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
        #region Merge row the same ID
        public void MergeItem()
        {

            // Kết nối database
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);
            try
            {
                // sql qr to merge item
                String sql = @"Drop table CART_1
                                SELECT*
                                INTO CART_1
                                FROM CART
                                Truncate table CART
                                INSERT INTO CART(MASP, SOLUONG)
                                SELECT MASP, SUM(SOLUONG) AS SOLUONG FROM CART_1 GROUP BY MASP";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion
        #region update sản phẩm
        public bool update(string masp, int soLuong)
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);

            try
            {
                //SQL Query to insert product into database
                String sql = @"UPDATE CART SET SOLUONG = @SOLUONG WHERE MASP = @MAMUA";

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
        public bool delete ()
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);

            try
            {
                //SQL Query to insert product into database
                String sql = @"DELETE FROM CART WHERE SOLUONG <= 0";

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);
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
        public bool sell(string masp)
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);

            try
            {
                //SQL Query to insert product into database
                String sql = @"DELETE FROM CART WHERE MASP = @MASP;";

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passign the values through parameters
                cmd.Parameters.AddWithValue("@MASP", masp);
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
    }
}
