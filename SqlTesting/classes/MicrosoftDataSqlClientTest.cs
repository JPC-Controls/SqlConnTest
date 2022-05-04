using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Data.SqlClient;

namespace SqlTesting.classes
{
    public class MicrosoftDataSqlClientTest
    {
        public SqlConnection Conn { get; set; } = new SqlConnection();
        private string ConnectionString = string.Empty;

        public MicrosoftDataSqlClientTest()
        {

        }
        public string Connect(string _constring)
        {
            ConnectionString = _constring;
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                return "good";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
