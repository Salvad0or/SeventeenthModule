using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenthModule.Models
{
    /// <summary>
    /// Базовый класс. Хранит свойства для работы
    /// </summary>
    internal class DataWorker
    {
        #region Поля и свойства

        protected static readonly SqlConnectionStringBuilder ConnectionString;

        public delegate void ShowTextMessage(string Message);

        protected static ShowTextMessage Show;

          
        public DataSet DataSet { get; protected set; }
      

        #endregion

        #region Конструкторы
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

        #endregion

    }
}
