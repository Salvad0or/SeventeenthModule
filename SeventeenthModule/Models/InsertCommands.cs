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
                string command =
                    "INSERT INTO [Orders] " +
                    "(ProductId, Clientid) " +
                    "VALUES" +
                    "(@product, @client)";

                string selectCommand = "SELECT * FROM Orders";

                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();

                    for (int i = 0; i < 5; i++)
                    {
                        if (mass[i] == 0) continue;

                        SqlCommand insertCommand = new SqlCommand(command, connection);

                        insertCommand.Parameters.AddWithValue("@product", mass[i]);
                        insertCommand.Parameters.AddWithValue("@client", id);

                        insertCommand.ExecuteNonQuery();
                        
                    }

                    JoinTable.InitializeJoinTableByAllOrders();

                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
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
        public void IstertNewClient(string fname, string lname, string pname, string phone, string emai, DataTable ClientsTable)
        {

            string InsertCommand = "INSERT INTO [Clients] (Fname,Lname,Pname,Phone,Emai) VALUES (@fname, @lname, @pname, @phone, @emai)";
            string SelectCommand = "SELECT * FROM Clients";


            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.ToString()))
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(InsertCommand, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SelectCommand, sqlConnection);

                    command.Parameters.AddWithValue("@fname", fname);
                    command.Parameters.AddWithValue("@lname", lname);
                    command.Parameters.AddWithValue("@pname", pname);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@emai", emai);

                    command.ExecuteNonQuery();

                    ClientsTable.Clear();

                    sqlDataAdapter.Fill(ClientsTable);

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
