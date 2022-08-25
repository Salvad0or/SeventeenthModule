using Microsoft.Data.SqlClient;
using SeventeenthModule.EntityObjects;
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
    internal class InsertCommands : DataWorker
    {
        /// <summary>
        /// Метод добавления нового заказа
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="id"></param>
        /// <param name="JoinTable"></param>
        public void InsertDataInOrders(int[] mass, int id, JoinCommands JoinTable)
        {
            try
            {
               
                using (Context context = new Context())
                {
                    List<Order> orders = new List<Order>();

                    for (int i = 0; i < 5; i++)
                    {
                        if (mass[i] == 0) continue;

                        orders.Add(new Order()
                        {
                            ProductId = mass[i],
                            Clientid = id
                        });

                    }

                    context.SaveChanges();

                }
            }
            catch (Exception e)
            {
                
                Show?.Invoke(e.Message);
            }
        }

        /// <summary>
        /// Метод добавления нового клиента
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="pname"></param>
        /// <param name="phone"></param>
        /// <param name="emai"></param>
        /// <param name="ClientsTable"></param>
        public void IstertNewClient(EntityClient NewClient)
        {

            try
            {
                using (Context context = new Context())
                {
                    context.Clients.Add(NewClient);

                    context.SaveChanges();

                    Show?.Invoke("Клиент успешно добавлен");
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

    }
}
