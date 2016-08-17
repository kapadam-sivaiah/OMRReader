using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMRManagement.DAL
{
    public class LoginDAL
    {
        public static bool GetUser(string userName,string password)
        {
            bool user = false;
            Connection conn = new Connection();
            using (conn.SqlConn = new SqlConnection(conn.ConnectionString))
            {
                conn.OpenConnection();
                try
                {
                    using (conn.SqlCmd = new SqlCommand("CheckUser", conn.SqlConn))
                    {

                        conn.SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.SqlCmd.Parameters.AddWithValue("@UserName", userName);
                        conn.SqlCmd.Parameters.AddWithValue("@Password", password);
                        conn.SqlDr = conn.SqlCmd.ExecuteReader();
                        if (conn.SqlDr.HasRows)
                        {

                           
                               user=true;
                            
                        }
                        
                    }
                    conn.SqlConn.Close();

                }
                catch (SqlException ex)
                {

                }

            }
            return user;
        }
    }
}
