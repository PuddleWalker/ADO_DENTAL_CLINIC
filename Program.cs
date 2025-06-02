using ADO_DENTAL_CLINIC.Models;
using Microsoft.EntityFrameworkCore;
using static System.Console;


namespace ADO_DENTAL_CLINIC
{
    internal class Program
    {
        static void Main()
        {
            using var dc = new DENTAL_Context();

            WriteLine("Дантисты из России:");
            var dents = dc.DentalClinics.Include(c => c.Dentist).ToList().FindAll(c => c.Country.Trim() == "Russia");
            foreach (DentalClinic e in dents)
            {
                WriteLine($"{e.Dentist?.Name.Trim()} {e.Dentist?.Surname.Trim()} {e.City.Trim()}");
            }



            WriteLine("\nДантисты из США:");
            var dentists = (from clinic in dc.DentalClinics
                            join dentist in dc.Dentists
                            on clinic.DentistId equals dentist.Id
                            where clinic.Country.Trim() == "USA"
                            select $"{dentist.Name.Trim()} {dentist.Surname.Trim()}")
                               .Distinct()
                               .ToList();
            foreach (string item in dentists)
            {
                WriteLine(item);
            }

            IQueryable<DentalClinic>? clinics = dc.DentalClinics?.Include(c => c.Dentist);
            if (clinics is null || !clinics.Any())
            {
                WriteLine("Ничего не найдено");
                return;
            }
            WriteLine("\nДантисты из Франции:");
            foreach (DentalClinic c in clinics)
            {
                if (c.Country.Trim() == "France") WriteLine($"{c.Dentist?.Name.Trim()} {c.Dentist?.Surname.Trim()} ");

            }
        }
    }
}