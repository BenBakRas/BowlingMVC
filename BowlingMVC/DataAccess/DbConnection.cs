using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class DbConnection
    {
        private static SqlConnectionStringBuilder conStr = new SqlConnectionStringBuilder()
        {
            DataSource = "localhost",
            InitialCatalog = "BowlingDB",
            UserID = "sa",
            Encrypt = false,
            Password = "Sommer2022"
        };

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(conStr.ConnectionString);
        }

        public static SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            return conStr;
        }
    }
}
