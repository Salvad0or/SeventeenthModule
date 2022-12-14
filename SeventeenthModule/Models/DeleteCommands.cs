using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeventeenthModule.Models
{
    internal class DeleteCommands : DataWorker
    {
        /// <summary>
        /// Метод удаления клиента из базы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ClientsTable"></param>
        public void DeleteClient(int id, DataTable ClientsTable)
        {
            try
            {
                string SelectCommand = "SELECT * FROM [Clients]";
                string DeleteCommand = "DELETE [Clients] WHERE Id = @id";


                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(DeleteCommand, connection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SelectCommand, connection);

                    sqlCommand.Parameters.AddWithValue("@id", id);

                    sqlCommand.ExecuteNonQuery();

                    ClientsTable.Clear();

                    sqlDataAdapter.Fill(ClientsTable);

                    Show?.Invoke("Клиент был удален");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Метод очистки данных в таблицах
        /// </summary>
        /// <param name="name"></param>
        public void DeleteTable(string name)
        {
            try
            {
                string command = $"DELETE {name}";

                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(command,connection);

                   // sqlCommand.Parameters.AddWithValue("@name", name);

                    sqlCommand.ExecuteNonQuery();

                    Show?.Invoke($"Данные были удалены из таблицы {name}");

                }
            }

            catch (Exception e)
            {

                Show?.Invoke(e.Message);
            }
        }
    }
}
