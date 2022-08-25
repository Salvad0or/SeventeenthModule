using Microsoft.Data.SqlClient;
using SeventeenthModule.EntityObjects;
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
        public void DeleteClient(int id)
        {
            try
            {
          
                using (Context context = new Context())
                {

                    var client = context.Clients.FirstOrDefault(p => p.Id == id);

                    if (client is null) return;
                    
                    context.Clients.Remove(client);

                    Show?.Invoke("Клиент был удален");

                    context.SaveChanges();

                }
            }
            catch (Exception e)
            {

                Show?.Invoke(e.Message);
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
