using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using static System.Console;

namespace ADO_DENTAL_CLINIC
{
    internal class ProgramTwo
    {
        public void MainTwo()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DENTAL_CLINIC"].ToString();


            Write("Введите название страны: ");
            string? reg = ReadLine();

            using SqlConnection connection = new(connectionString);

            const string sqlString = "CityName";
            SqlCommand command = new(sqlString, connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter regParam = new SqlParameter { ParameterName = "@reg", Value = "%" + reg + "%" };
            command.Parameters.Add(regParam);

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WriteLine(reader[0]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
