using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using static System.Console;

string connectionString = ConfigurationManager.ConnectionStrings["DENTAL_CLINIC"].ToString();


Write("Введите название страны: ");
string? reg = ReadLine();
// string? reg = "моск"; // для проверки - не чувствителен SQL у нас к регистру по умолчанию
SqlParameter regParam = new("@reg", "%" + reg + "%");
const string queryString = "SELECT DC.City from Dental_clinic DC " +
                           "where DC.Country LIKE @reg";
WriteLine(queryString);
using SqlConnection connection = new(connectionString);
SqlCommand command = new(queryString, connection);
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
    connection.Close(); // в принципе не надо, т.к. у нас using
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
