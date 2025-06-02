using System.Data.Linq;
using Microsoft.Data.SqlClient;
using System.Configuration;
using ADO_DENTAL_CLINIC;

namespace ADO_DENTAL_CLINIC_Two
{
    internal class ProgramTwo
    {
        static void Main()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DENTAL_CLINIC"].ToString();
            SqlConnection connection = new(connectionString);

            var dc = new DENTAL_ClassesDataContext(connection);

            Console.WriteLine("Дантисты высшей категории:");
            var dentists = (from d in dc.Dentist
                                    where d.CategoryID == 1
                                    select $"{d.Name.Trim()} {d.Surname.Trim()}")
                   .Distinct()
                   .ToList();
            foreach (var c in dentists)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("\nВсе Дантисты:");
            Table<Dentist> dents = dc.GetTable<Dentist>();
            foreach (var c in dents)
            {
                Console.WriteLine(c.Name.ToString().Trim() + " " + c.Surname.ToString().Trim());
            }
        }
    }
}
