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
    }
}
