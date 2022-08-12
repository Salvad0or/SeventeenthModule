using Microsoft.Data.SqlClient;
using SeventeenthModule.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        /// <summary>
        /// Запрос поиска клиента по ID
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client SearchClientsById(Client client, int id)
        {

            string SelectCommand = $"SELECT * FROM [Clients] WHERE ID = {id}";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.ToString()))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand(SelectCommand, sqlConnection);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        client.Fname = dataReader.GetValue(1).ToString() ?? String.Empty;
                        client.Lname = dataReader.GetValue(2).ToString() ?? String.Empty;
                        client.Pname = dataReader.GetValue(3).ToString() ?? String.Empty;
                        client.Phone = dataReader.GetValue(4).ToString() ?? String.Empty;
                        client.Emai = dataReader.GetValue(5).ToString() ?? String.Empty;
                    }

                    return client;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

            return null;
        }
    }
}
