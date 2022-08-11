using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenthModule.Models
{
    internal class SelectCommands : DataWorker
    {

        /// <summary>
        /// Задаем все три таблицы SELECT в данном конструкторе
        /// </summary>
        public SelectCommands()
        {          
            string[] commands =
                {
                "SELECT * FROM Clients",
                "SELECT * FROM Orders",
                "SELECT * FROM Products"
                };

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.ToString()))
            {

                sqlConnection.Open();

                DataSet = new DataSet();

                DataSet.Tables.Add("Clients");
                DataSet.Tables.Add("Orders");
                DataSet.Tables.Add("Products");

                for (int i = 0; i < 3; i++)
                {
                    SqlDataAdapter DataAdapter = new SqlDataAdapter(commands[i], sqlConnection);
                    DataAdapter.Fill(DataSet.Tables[i]);
                }
        }   }
    }
}
