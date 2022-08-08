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
    internal  class SqlCommands : DataWorker
    {


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
            
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
    }
}
