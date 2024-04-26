using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace XinViec
{
    class ConnectionTK
    {
        private static string stringConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=XinViec;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConn);
        }
    }
}
