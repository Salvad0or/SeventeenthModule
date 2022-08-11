using Microsoft.Data.SqlClient;
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
    internal class SecondWindowWorker : DataWorker
    {
        /// <summary>
        /// Запрос на добавление клиента
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="pname"></param>
        /// <param name="phone"></param>
        /// <param name="emai"></param>
        /// <param name="ClientsTable"></param>
        public void Istert(string fname, string lname, string pname, string phone, string emai, DataTable ClientsTable)
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

        /// <summary>
        /// Запрос поиска клиента по ID
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client SearchClientsById(Client client, int id)
        {

            string SelectCommand = $"SELECT * FROM [Clients] WHERE ID = {id}";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.ToString()))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand(SelectCommand, sqlConnection);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        client.Fname = dataReader.GetValue(1).ToString() ?? String.Empty;
                        client.Lname = dataReader.GetValue(2).ToString() ?? String.Empty;
                        client.Pname = dataReader.GetValue(3).ToString() ?? String.Empty;
                        client.Phone = dataReader.GetValue(4).ToString() ?? String.Empty;
                        client.Emai = dataReader.GetValue(5).ToString() ?? String.Empty;
                    }

                    return client;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

            return null;
        }

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

        /// <summary>
        /// Метод удаления клиента из базы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ClientsTable"></param>
        public void DeleteClient(int id, DataTable ClientsTable)
        {
            try
            {
                string SelectCommand = "SELECT * FROM [Clients]";
                string DeleteCommand = "DELETE [Clients] WHERE Id = @id";
                

                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(DeleteCommand, connection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SelectCommand, connection);

                    sqlCommand.Parameters.AddWithValue("@id", id);

                    sqlCommand.ExecuteNonQuery();

                    ClientsTable.Clear();

                    sqlDataAdapter.Fill(ClientsTable);

                    Show?.Invoke("Клиент был удален");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
