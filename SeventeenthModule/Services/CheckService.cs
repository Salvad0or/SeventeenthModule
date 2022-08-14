using Microsoft.Data.SqlClient;
using SeventeenthModule.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenthModule.Services
{
    /// <summary>
    /// Класс предоставляющий разного рода проверки для выполнения команд
    /// </summary>
    internal class CheckService : DataWorker
    {
       /// <summary>
       /// Метод проверяющий есть ли клиент в базе, для выведения его на экран редактирования
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>

        public bool CheckClientId(int id)
        {

            string sellectCommand = $"SELECT Fname FROM [Clients] WHERE Id = {id}";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sellectCommand, connection);

                    DataTable dt = new DataTable();

                    dataAdapter.Fill(dt);

                    var result = dt.Rows;

                    if (result.Count == 0) return false;

                    return true;

                }
                
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool CheckOnEmpty(Client client)
        {
            Type myType = typeof(Client);

            var nameProp = myType.GetProperty("Fname");
            var lnameProp = myType.GetProperty("Lname");
            var pnameProp = myType.GetProperty("Pname");
            var phoneProp = myType.GetProperty("Phone");
            var emaiProp = myType.GetProperty("Emai");

            string name = Convert.ToString(nameProp.GetValue(client) ?? String.Empty);
            string lname = Convert.ToString(lnameProp.GetValue(client) ?? String.Empty);
            string pname = Convert.ToString(pnameProp.GetValue(client) ?? String.Empty);
            string phone = Convert.ToString(phoneProp.GetValue(client) ?? String.Empty);
            string emai = Convert.ToString(emaiProp.GetValue(client) ?? String.Empty);

            string[] properties = { name, lname, pname, phone, emai };

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i] == String.Empty) return false;

            }

            return true;
        }
    }
}
