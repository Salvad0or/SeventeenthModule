using Microsoft.Data.SqlClient;
using SeventeenthModule.EntityObjects;
using SeventeenthModule.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenthModule.Models
{
    internal class UpdateCommands : DataWorker
    { 
        
        /// <summary>
      /// Изменение данных клиента по ID
      /// </summary>
      /// <param name="client"></param>
      /// <param name="id"></param>
      /// <param name="ClientsTable"></param>
        public List<EntityClient> EditClientData(int id, EntityClient entityClient, List<EntityClient> ClientsFromBase)
        {
            try
            {
                using (Context context = new Context())
                {
                    entityClient.Id = id;

                    context.Update(entityClient);

                    context.SaveChanges();

                    List<EntityClient> Clients = context.Clients.ToList();

                    Show?.Invoke($"Данные клиента {entityClient.Fname} были изменены успешно");

                    return Clients;

                }

            }
            catch (Exception e)
            {

                Show?.Invoke(e.Message);
                return ClientsFromBase;
            }
        }
    }
}
