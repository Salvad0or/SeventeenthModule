using Microsoft.Data.SqlClient;
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
        public void EditClientData(Client client, int id, DataTable ClientsTable)
        {
            try
            {
                string SelectCommand = "SELECT * FROM Clients";
                string UpdateCommand = "UPDATE [Clients] SET Fname = @fname, Lname = @lname, Pname = @pname, Phone = @phone, Emai = @emai " +
                    "WHERE Id = @id";

                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(UpdateCommand, connection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SelectCommand, connection);

                    sqlCommand.Parameters.AddWithValue("@fname", client.Fname);
                    sqlCommand.Parameters.AddWithValue("@lname", client.Lname);
                    sqlCommand.Parameters.AddWithValue("@pname", client.Pname);
                    sqlCommand.Parameters.AddWithValue("@phone", client.Phone);
                    sqlCommand.Parameters.AddWithValue("@emai", client.Emai);
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    sqlCommand.ExecuteNonQuery();

                    ClientsTable.Clear();
                    sqlDataAdapter.Fill(ClientsTable);

                    Show?.Invoke("Клиент успешно добавлен");
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
