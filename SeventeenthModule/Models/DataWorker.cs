using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenthModule.Models
{
    internal class DataWorker
    {
        SqlConnectionStringBuilder ConnectionString { get; set; }

        public DataWorker()
        {
            ConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "ITVDN2db",
                Pooling = true
            };

            SqlConnection sql = new SqlConnection(ConnectionString.ToString());

            sql.Open();

            sql.Close();
        }
        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Саша\ITVDN2db.mdf;Integrated Security=True;Connect Timeout=30
    }
}
