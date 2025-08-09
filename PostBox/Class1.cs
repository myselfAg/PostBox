using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PostBox
{
    public class Class1
    {
        public static SqlConnection cnn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nova\source\repos\PostBox\PostBox\App_Data\Database1.mdf;Integrated Security=True");

        public bool save(string str)
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {

                return false;

            }
        }
        public static DataSet fetch(string str)
        {
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter(str, cnn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                cnn.Close();
            }
        }

    }
}