using Microsoft.Data.SqlClient;
using System.Configuration;

//const string connectionString = "Data Source = DESKTOP-DGP3MV4; " +
//                                "Integrated Security = true; " +
//                                "Encrypt = false"; 
string connectionString = ConfigurationManager.ConnectionStrings["DENTAL_CLINIC"].ToString();
const string queryString = "use DENTAL_CLINIC; SELECT top (10) * from Dentist;";

using SqlConnection connection = new(connectionString);
SqlCommand command = new(queryString, connection);
try
{
    connection.Open();
    SqlDataReader reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", reader[0], reader[1], reader[2], reader[3]);
    }
    reader.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}