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
        protected static readonly SqlConnectionStringBuilder ConnectionString;
        

        private DataSet _dataSet;
        public delegate void ShowTextMessage(string Message);
        protected static ShowTextMessage Show;


        public DataSet DataSet
        {
            get { return _dataSet; }
            protected set { _dataSet = value; }
        }

        public DataWorker(ShowTextMessage show)
        {             

            Show = show;
        }

        static DataWorker()
        {
            ConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "ITVDN2db",
                Pooling = true
            };
        }

        public DataWorker() { }

        
    }
}
