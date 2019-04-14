using System.Collections.Generic;
using System.Data.SqlClient;

namespace LastChance
{
    internal static class DBIntegration
    {
        private const string dbConnectionString = "Server=tcp:bloghostdbserver.database.windows.net,1433;Initial Catalog=bloghostdb;Persist Security Info=False;User ID=adminbloghost;Password=123qweASD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static void InserIpAddress(string ipAdress)
        {
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                string sqlCommand = string.Format("INSERT INTO DBO.ipconnections (Adress) VALUES ( '{0}' )", ipAdress);
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<string> GetIpAddress()
        {
            List<string> addresses = new List<string>();
            string sqlExpression = "SELECT * FROM DBO.ipconnections";
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов                   
                    while (reader.Read()) // построчно считываем данные
                    {
                        addresses.Add(reader.GetValue(0).ToString());
                    }
                }
                reader.Close();
            }
            return addresses;
        }

        public static void ClearIpAddresses()
        {
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                string sqlCommand = string.Format("drop table dbo.ipconnections; create table dbo.ipconnections(Adress varchar(30)); ");
                connection.Open();
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
