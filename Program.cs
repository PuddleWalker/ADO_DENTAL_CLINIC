using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using static System.Console;

WriteLine("\n  Выберите базу данных: ");
WriteLine("  1. SQL Server Express");
WriteLine("  2. SQLite");
Write("  ");
int choice = Convert.ToInt32(ReadLine());
DbProviderFactory? factory = null;
string cs;
switch (choice)
{
    case 1:
        factory = SqlClientFactory.Instance;
        cs = ConfigurationManager.ConnectionStrings["DENTAL_CLINIC"].ToString();
        break;
    case 2:
        factory = SqliteFactory.Instance;
        cs = ConfigurationManager.ConnectionStrings["DENTAL_SQLite"].ConnectionString;
        break;
    default:
        Console.WriteLine("  Некорректный выбор");
        return;
}
// создаем соединение и назначаем ему строку подключения
DbConnection? con = factory.CreateConnection();
con.ConnectionString = cs;

DbCommand cmd = con.CreateCommand();
cmd.CommandText = "select Name, Surname, City, Country from Dental_clinic as DC, Dentist as D " +
                                   "where DC.Country like 'Russia%' and DC.DentistID = D.id";

con.Open();

using DbDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine("{0}{1}\t{2}\t{3}", reader["Name"], reader["Surname"], reader[2], reader[3]);
}

WriteLine("  Нажмите любую клавишу...");
ReadKey();

con.Close();