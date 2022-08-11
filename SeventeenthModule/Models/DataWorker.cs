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
        protected SqlConnectionStringBuilder ConnectionString { get; init; }
     
        private DataSet _dataSet;

        public delegate void ShowTextMessage(string Message);
        protected ShowTextMessage Show;

        public DataSet DataSet
        {
            get { return _dataSet; }
            protected set { _dataSet = value; }
        }

        public DataWorker(ShowTextMessage show)
        {
            ConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "ITVDN2db",
                Pooling = true
            };

            Show = show;
        }

        public DataWorker() { }

        
    }
}
