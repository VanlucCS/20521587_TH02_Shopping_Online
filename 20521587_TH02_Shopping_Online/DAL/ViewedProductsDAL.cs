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
    class ViewedProductsDAL
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
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM VIEWEDPRODUCTS", cnn))
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
        public bool Insert(ViewedProductsBLL p)
        {
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);

            try
            {
                //SQL Query to insert product into database
                String sql = "INSERT INTO VIEWEDPRODUCTS (MASP,THOIGIAN) VALUES (@MASP,@THOIGIAN)";

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passign the values through parameters
                cmd.Parameters.AddWithValue("@MASP", p.MASP);
                cmd.Parameters.AddWithValue("@THOIGIAN", p.THOIGIAN);

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
        #region select distinct top 6
        public DataTable SelectDistinct()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString))
                {
                    if (cnn.State == ConnectionState.Closed)
                    {
                        cnn.Open();
                        using (SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT TOP 5 MASP, MAX(THOIGIAN) 
                                                                FROM VIEWEDPRODUCTS 
                                                                GROUP BY MASP 
                                                                ORDER BY MAX(THOIGIAN) DESC, MASP", cnn))
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
    }
}
