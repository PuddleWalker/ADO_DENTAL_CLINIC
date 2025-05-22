using Microsoft.Data.Sqlite;
//using System.Configuration;

//string conString = ConfigurationManager.ConnectionStrings["DENTAL_CLINIC"].ToString();
string conString = "Data Source = DENTAL_CLINIC.db";

string query = "select Name, Surname, City, Country from Dental_clinic as DC, Dentist as D " +
                       "where DC.Country like 'Russia%' and DC.DentistID = D.id";

using SqliteConnection connection = new(conString);
SqliteCommand c = new(query, connection);
connection.Open();
using SqliteDataReader reader = c.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine("{0}{1}\t{2}\t{3}", reader["Name"], reader["Surname"], reader[2], reader[3]);
}