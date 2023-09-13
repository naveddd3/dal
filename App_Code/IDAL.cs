using System.Data;
using System.Data.SqlClient;

namespace DAL_PRACTICE.App_Code
{
    public interface IDAL
    {
        int Execute(string sqlquery, SqlParameter[] param, CommandType commandtype = CommandType.Text);
        DataTable Get(string sqlquery, CommandType commandtype = CommandType.Text);

        DataTable Get(string sqlquery, SqlParameter[] param, CommandType commandtype = CommandType.Text);

    }
}
