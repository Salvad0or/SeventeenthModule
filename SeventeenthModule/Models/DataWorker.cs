using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenthModule.Models
{
    internal class DataWorker
    {
        SqlConnectionStringBuilder ConnectionString { get; init; }

        SqlDataAdapter _dataAdapter;

        private DataSet _dataSet;

        public DataSet DataSet
        {
            get { return _dataSet; }
            private set { _dataSet = value; }
        }

        public SqlDataAdapter DataAdapter
        {
            get => _dataAdapter;
            private set => _dataAdapter = value;
        }

       

        public DataWorker()
        {
            ConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "ITVDN2db",
                Pooling = true
            };

            string command = "SELECT * FROM Clients";

            string[] commands =
                {
                "SELECT * FROM Clients",
                "SELECT * FROM Orders",
                "SELECT * FROM Products"
                };
            

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.ToString()))
            {
                DataSet = new DataSet();

                DataSet.Tables.Add("Clients");
                DataSet.Tables.Add("Orders");
                DataSet.Tables.Add("Products");

                for (int i = 0; i < 3; i++)
                {
                    DataAdapter = new SqlDataAdapter(commands[i], sqlConnection);
                    DataAdapter.Fill(DataSet.Tables[i]);                  
                }       
            }
        }
        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Саша\ITVDN2db.mdf;Integrated Security=True;Connect Timeout=30
    }
}
