using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WebMarket.ETL
{
    public static class SqlHelper
    {
        public static void Execute(Func<SqlConnection> connection, Func<SqlCommand> command, Action<SqlDataReader> borrowReader)
        {
            using (SqlConnection cn = connection())
            {
                cn.Open();
                using (SqlCommand cmd = command())
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        borrowReader(reader);
                    }
                }
            }
        }
    }
}
