using Microsoft.Data.SqlClient;
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

        /// <summary>
        /// Таблица хранит все сущности JOIN'ов
        /// </summary>
        public DataSet JoinTables { get; private set; }

        #endregion

        #region Конструкторы

        public JoinCommands()
        {

            JoinTables = new DataSet();

            JoinTables.Tables.Add("JoinAllOrders");
            JoinTables.Tables.Add("JoinById");

            InitializeJoinTableByAllOrders();

        }

        #endregion

        #region Методы

        /// <summary>
        /// Находим клиента по ID и вывыводим в таблицы
        /// </summary>
        /// <param name="JoinId"></param>
        public void JoinTableById(int JoinId)
        {
            DataTable returnTable = new DataTable();

            try
            {
                string command =
                    "SELECT Fname, Lname, Pname, Phone, Emai, o.Id OrderId,[Date],ProductId,Clientid FROM [Clients] c " +
                    "JOIN [Orders] o " +
                    "ON c.Id = o.Clientid " +
                    $"WHERE c.Id = {JoinId}";

                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();              

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connection);

                    JoinTables.Tables["JoinById"].Clear();

                    dataAdapter.Fill(JoinTables.Tables["JoinById"]);
                }
            }
            catch (Exception e)
            {

                Show?.Invoke(e.Message);
               
            }

            
        }

        /// <summary>
        /// Заполнение таблицы для всех заказов
        /// </summary>
        public void InitializeJoinTableByAllOrders ()
        {
            string command =
                    "SELECT c.Id Id, Fname, Lname, Pname , Phone, Emai Email, o.Id OrderId, [Date], ProductId, Clientid FROM CLIENTS c " +
                    "JOIN [Orders] o " +
                    "ON c.Id = o.Clientid";

            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connection);

                    JoinTables.Tables["JoinAllOrders"].Clear();

                    dataAdapter.Fill(JoinTables.Tables["JoinAllOrders"]);

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion
    }
}
