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

        public static DataSet JoinTables { get; private set; }

        #region Конструкторы
        public JoinCommands(ShowTextMessage show)
        {
            try
            {
                string command =
                    "SELECT c.Id Id, Fname, Lname, Pname , Phone, Emai Email, o.Id OrderId, [Date], ProductId, Clientid FROM CLIENTS c " +
                    "JOIN [Orders] o " +
                    "ON c.Id = o.Clientid";

                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();

                    JoinTables = new DataSet();

                    JoinTables.Tables.Add("JoinAllOrders");
                    JoinTables.Tables.Add("JoinById");

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connection);

                    dataAdapter.Fill(JoinTables.Tables["JoinAllOrders"]);

                    Show = show;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        public DataTable JoinTableByName(int JoinId)
        {
            DataTable returnTable = new DataTable();

            try
            {
                string command =
                    "SELECT * FROM [Clients] c " +
                    "JOIN [Orders] o " +
                    "ON c.Id = o.Clientid " +
                    $"WHERE c.Id = {JoinId}";

                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();              

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connection);

                    dataAdapter.Fill(JoinTables.Tables["JoinById"]);

                    returnTable = JoinTables.Tables["JoinById"];

                    return returnTable;
                }
            }
            catch (Exception e)
            {

                Show?.Invoke(e.Message);

                return null;
            }

            
        }
    }
}
