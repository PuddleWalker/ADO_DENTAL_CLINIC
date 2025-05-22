using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

string connectionString = ConfigurationManager.ConnectionStrings["DENTAL_CLINIC"].ToString();


using SqlConnection connection = new(connectionString);

const string queryString = "SELECT * from Dentist";
/*
const string queryString = "SELECT e.Tab, e.SecondName, e.FirstName, e.CountryId, e.RegionId, c.Name, r.Name " +
                           "from employee e, country c, region r " +
                           "where e.CountryId = c.id and e.RegionCode = r.Code;";
*/
SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
DataSet Dentists = new DataSet("Dentist");
adapter.Fill(Dentists, "Dentist");
// можно и не делать - сразу обратиться
DataTable? dt = Dentists.Tables["Dentist"];

// добавляем строку
DataRow newRow = dt.NewRow();
newRow["id"] = 70;
newRow["Name"] = "Александр";
newRow["Surname"] = "Петров";
newRow["CategoryID"] = 3;
newRow["SpecialityID"] = 2;
newRow["BeginDate"] = "14.03.2008";
dt.Rows.Add(newRow);
// изменяем значение
dt.Rows[0]["SpecialityID"] = 1;
// тут магия - делаем Builder, но нигде его не задаем - она сам дает Update, Insert или Delete
// но только для простых select (без условий и т.д.)
SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
// обновляем источник - БД
adapter.Update(Dentists, "Dentist");
// очищаем полностью DataSet
Dentists.Clear();
// перезагружаем данные
adapter.Fill(Dentists, "Dentist");

try
{
    // обращаемся по имени таблицы в коллекции Tables
    foreach (DataRow row in dt.Rows)
    {
        foreach (var cell in row.ItemArray)
        {
            Console.Write($"{cell}\t");
        }
        Console.WriteLine();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}