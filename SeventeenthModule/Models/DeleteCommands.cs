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
        public List<EntityClient> DeleteClient(int id, List<EntityClient> ClientsFromBase)
        {
            try
            {
          
                using (Context context = new Context())
                {

                    var client = context.Clients.FirstOrDefault(p => p.Id == id);

                    if (client is null) return ClientsFromBase;
                    
                    context.Clients.Remove(client);

                    Show?.Invoke("Клиент был удален");

                    context.SaveChanges();

                    List<EntityClient> Clients = context.Clients.ToList();

                    return Clients;

                }
            }
            catch (Exception e)
            {

                Show?.Invoke(e.Message);
                return ClientsFromBase;
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

                using (Context context = new Context())
                {
                    context.Remove(nameof(name));

                    context.SaveChanges();

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
