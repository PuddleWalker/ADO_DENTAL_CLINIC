using Microsoft.EntityFrameworkCore;
using static System.Console;
using ADO_DENTAL_CLINIC.Models;

namespace ADO_DENTAL_CLINIC_Three
{
    internal class ProgramThree
    {
        static void Main()
        {
            using DENTAL_Context dc = new();


            // так выбираем модели со связью (FK) с таблицей Brands 
            IQueryable<Dentist>? dentists = dc.Dentists?.Include(c => c.Speciality);

            WriteLine("Список дантистов:");
            if (dentists is null || !dentists.Any())
            {
                WriteLine("Ничего не найдено");
            }
            else
            {
                foreach (Dentist c in dentists)
                {
                    WriteLine($"{c.Speciality.Name} {c.Name} ");
                }
            }

            // добавляем марки и модели
            // обратите внимание - несколько раз подряд их добавить будет нельзя !!!
            // стоят уникальные PK и FK
            WriteLine("\nДобавляем специальности и дантистов...");
            var l = new Speciality { Name = "Oral Radiology" };
            var g = new Dentist { Name = "Jessica", Surname = "Marshall", CategoryId = 2, SpecialityId = 10, BeginDate = new DateOnly(2014, 4, 13)};
            var v = new Dentist { Name = "Alexander", Surname = "Crown", CategoryId = 3, SpecialityId = 10, BeginDate = new DateOnly(2011, 8, 27)};
            dc.Specialities.Add(l);
            dc.SaveChanges();
            try
            {
                dc.Dentists.Add(g);
                dc.Dentists.Add(v);
                dc.SaveChanges();
            }
            catch (Exception e)
            {
                WriteLine("Наверное такие данные уже есть " +
                          "или нарушены правила ссылочной целостности !\n" +
                          "См. текст ошибки ниже:\n");
                WriteLine(e.Message);
                WriteLine(e.InnerException.Message);
            }

            WriteLine("Объекты успешно сохранены");

            // обновляем и выводим новый список
            dentists = dc.Dentists?.Include(c => c.Speciality);
            WriteLine("\nНовый список дантистов");
            foreach (Dentist c in dentists)
            {
                WriteLine($"{c.Speciality.Name}   {c.Name} ");
            }
        }
    }
}