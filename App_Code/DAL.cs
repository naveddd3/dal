using System.Data;
using System.Data.SqlClient;

namespace DAL_PRACTICE.App_Code
{
    public class DAL : IDAL
    {
        private readonly string ConnectionString;
        public DAL(ConnectionHelper connectionhelper1)
        {
            ConnectionString = connectionhelper1.Default;
        }
        public int Execute(string sqlquery, SqlParameter[] param, CommandType commandtype = CommandType.Text)
        {
            int i = 0;
            try
            {
                using(SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sqlquery, con);
                    cmd.CommandType = commandtype;
                    foreach (var p in param)
                    {
                        cmd.Parameters.Add(p);
                    }
                    con.Open();
                    i=cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception ex)
            {
 
            }
            return i;
        }

        public DataTable Get(string sqlquery, CommandType commandtype = CommandType.Text)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.CommandType = commandtype;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            return dt;
        }

        public DataTable Get(string sqlquery, SqlParameter[] param, CommandType commandtype = CommandType.Text)
        {
            DataTable dt = new DataTable();
            try
            {
                using(SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sqlquery,con);
                    cmd.CommandType = commandtype;
                    foreach (var p in param)

                    {
                        cmd.Parameters.Add(p);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {

              
            }
            return dt;
        }
    }
}
