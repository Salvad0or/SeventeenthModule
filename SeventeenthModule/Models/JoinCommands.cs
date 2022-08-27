using Microsoft.Data.SqlClient;
using SeventeenthModule.EntityObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenthModule.Models
{
    internal class JoinCommands : DataWorker
    {
        #region Свойства

        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        public string Pname { get; set; }

        public string Emai { get; set; }
        public string Phone { get; set; }

        public int OrderId { get; set; }

        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public int Clientid { get; set; }

        public  List<JoinCommands> [] JoinTables { get; set; }

        #endregion

        #region Конструкторы

        public JoinCommands()
        {
            JoinTables = new List<JoinCommands>[2];
        }

        #endregion

        #region Методы

        /// <summary>
        /// Находим клиента по ID и вывыводим в таблицы
        /// </summary>
        /// <param name="JoinId"></param>
        public List<JoinCommands> JoinTableById(int JoinId)
        {
            
            try
            {
                string command =
                    "SELECT Fname, Lname, Pname, Phone, Emai, o.Id OrderId,[Date],ProductId,Clientid FROM [Clients] c " +
                    "JOIN [Orders] o " +
                    "ON c.Id = o.Clientid " +
                    $"WHERE c.Id = {JoinId}";

                using (Context context = new Context())

                {
                    List <JoinCommands> j = (from c in context.Clients
                                     join o in context.Orders
                                     on c.Id equals o.Clientid
                                     where c.Id == JoinId

                                     select new JoinCommands
                                     {
                                         Id = c.Id,
                                         Fname = c.Fname,
                                         Lname = c.Lname,
                                         Pname = c.Pname,
                                         Emai = c.Emai,
                                         Phone = c.Phone ?? String.Empty,
                                         OrderId = o.Id,
                                         Date = o.Date ?? DateTime.Now,
                                         ProductId = o.ProductId ?? default,
                                         Clientid = o.Clientid ?? default
                                     }).ToList();    
                    return j;
                }
            }
            catch (Exception e)
            {

                Show?.Invoke(e.Message);
                return null;

            }

            
        }

        /// <summary>
        /// Заполнение таблицы для всех заказов
        /// </summary>
        public void InitializeJoinTableByAllOrders ()
        {
            try
            {

                using (Context context = new Context())
                {
                  
                    JoinTables[0] = (from c in context.Clients
                                     join o in context.Orders
                                     on c.Id equals o.Id
                                     select new JoinCommands
                                     {
                                         Id = c.Id,
                                         Fname = c.Fname,
                                         Lname = c.Lname,
                                         Pname = c.Pname,
                                         Emai = c.Emai,
                                         OrderId = o.Id,
                                         Date = o.Date ?? DateTime.Now,
                                         ProductId = o.ProductId ?? default,
                                         Clientid = o.Clientid ?? default
                                     }).ToList();
                }
            }
            catch (Exception e)
            {

                Show?.Invoke(e.Message);
            }

        }

        #endregion
    }
}
