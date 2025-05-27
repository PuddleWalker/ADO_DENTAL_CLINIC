using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static System.Console;

WriteLine("Спасибо Марку Прайсу " +
          "cs13net9-main\\docs\\ch10-code-first");

using (Clinic c = new())
{
    bool deleted = await c.Database.EnsureDeletedAsync();
    WriteLine($"\nБаза данных Clinic удалена: {deleted}");

    bool created = await c.Database.EnsureCreatedAsync();
    WriteLine($"\nБаза данных Clinic создана: {created}");

    WriteLine("\nSQL скрипт для создания БД:");
    WriteLine(c.Database.GenerateCreateScript());

    WriteLine("\nДоктора:");
    foreach (Doctor d in c.Doctors.Include(d => d.Speciality))
    {
        WriteLine("{0} {1} является доктором со специальностью {2}",
          d.FirstName, d.SecondName, d.Speciality.Name);
    }
}
public class Clinic : DbContext
{
    public DbSet<Doctor>? Doctors { get; set; }
    public DbSet<Speciality>? Specialities { get; set; }
    public DbSet<Category>? Categories { get; set; }
    protected override void OnConfiguring(
      DbContextOptionsBuilder optionsBuilder)
    {
        string connection = "Data Source=AstraCore;Initial Catalog=Clinic;" +
                            "Integrated Security=true;Encrypt=False";
        optionsBuilder.UseSqlServer(connection);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API validation rules
        modelBuilder.Entity<Doctor>()
          .Property(d => d.SecondName).HasMaxLength(30).IsRequired();
        // populate database with sample data
        Doctor a = new()
        {
            id = 1,
            FirstName = "Алексей",
            SecondName = "Политехнический",
            CategoryID = 4,
            SpecialityID = 2
        };
        Doctor b = new()
        {
            id = 2,
            FirstName = "Вениамин",
            SecondName = "Вересаев",
            CategoryID = 1,
            SpecialityID = 3
        };
        Doctor c = new()
        {
            id = 3,
            FirstName = "Джон",
            SecondName = "Хизклифф",
            CategoryID = 2,
            SpecialityID = 3
        };
        Category s = new()
        {
            id = 1,
            Name = "Врач-стажёр"
        };
        Category o = new()
        {
            id = 2,
            Name = "Врач-ординатор"
        };
        Category f = new()
        {
            id = 3,
            Name = "Врач первой степени"
        };
        Category u = new()
        {
            id = 4,
            Name = "Врач высшей категории"
        };
        Speciality k = new()
        {
            id = 1,
            Name = "Кардиолог"
        };
        Speciality d = new()
        {
            id = 2,
            Name = "Дерматовенеролог"
        };
        Speciality g = new()
        {
            id = 3,
            Name = "Гастроэнтеролог"
        };
        Speciality n = new()
        {
            id = 4,
            Name = "Нейрохирург"
        };

        modelBuilder.Entity<Doctor>()
          .HasData(a, b, c);

        modelBuilder.Entity<Category>()
          .HasData(s, o, f, u);

        modelBuilder.Entity<Speciality>()
          .HasData(k, d, g, n);


        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Category)
            .WithMany(c => c.Doctors)
            .HasForeignKey(d => d.CategoryID);

        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Speciality)
            .WithMany(s => s.Doctors)
            .HasForeignKey(d => d.SpecialityID);
    }
}
public class Speciality
{
    public int id { get; set; }
    public string? Name { get; set; }
    public ICollection<Doctor>? Doctors { get; set; }
}
public class Category
{
    public int id { get; set; }
    public string? Name { get; set; }
    public ICollection<Doctor>? Doctors { get; set; }
}
public class Doctor
{
    public int id { get; set; }

    public int CategoryID { get; set; }
    public int SpecialityID { get; set; }

    public string? FirstName { get; set; }
    public string? SecondName { get; set; }


    public Category? Category { get; set; }
    public Speciality? Speciality { get; set; }
}