using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SqlTesting.classes
{

    public class SystemDataSqlClient
    {
        //public SqlConnection conn { get; set; } = new SqlConnection();
        private string ConnectionString = string.Empty;

        public SystemDataSqlClient()
        {

        }
        public string Connect(string _constring)
        {
            ConnectionString = _constring;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                };
                return "good";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
