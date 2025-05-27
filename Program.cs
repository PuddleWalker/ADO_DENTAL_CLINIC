using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static System.Console;
using System.Configuration;

using (ApplicationContext db = new ApplicationContext())
{
    // получаем объекты из бд и выводим на консоль
    var dentists = db.Dentist.ToList();
    WriteLine("Список объектов из БД:");
    foreach (Dentist d in dentists)
    {
        Console.WriteLine($"{d.Id} - {d.Name?.Trim()} {d.Surname?.Trim()}");
    }

    // создаем два новых объекта User
    WriteLine("Добавляем новых...");
    Dentist andrew = new Dentist { Name = "Андрей", Surname = "Быков", SpecialityID = 4, CategoryID = 1, BeginDate = new DateTime(2002, 1, 13) };
    Dentist dmitriy = new Dentist { Name = "Дмитрий", Surname = "Старцев", SpecialityID = 1, CategoryID = 2, BeginDate = new DateTime(2008, 12, 30) };

    // добавляем их в бд
    db.Dentist.Add(andrew);
    db.Dentist.Add(dmitriy);
    db.SaveChanges();
    WriteLine("Объекты успешно сохранены");

    // снова получаем объекты из бд и выводим на консоль
    dentists = db.Dentist.ToList();
    Console.WriteLine("Список объектов:");
    foreach (Dentist d in dentists)
    {
        Console.WriteLine($"{d.Id} - {d.Name?.Trim()} {d.Surname?.Trim()}");
    }
}
public class Dentist
{
    public int Id { get; set; } 
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int CategoryID { get; set; }
    public int SpecialityID { get; set; }
    public DateTime? BeginDate { get; set; }
}
public class ApplicationContext : DbContext
{
    string connectionString = ConfigurationManager.ConnectionStrings["DENTAL_First"].ToString();
    public DbSet<Dentist> Dentist => Set<Dentist>();
    public ApplicationContext() => Database.EnsureCreated();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}