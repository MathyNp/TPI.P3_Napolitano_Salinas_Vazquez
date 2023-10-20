using Microsoft.EntityFrameworkCore;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Admin> Questions { get; set; }
        public DbSet<Client> Responses { get; set; }
        public DbSet<Product> Subjects { get; set; }
        public DbSet<ShoppingCart> Students { get; set; }
        public DbSet<User> Professors { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);
            modelBuilder.Entity<Student>().HasData(
               new Student
               {
                   LastName = "Bologna",
                   Name = "Nicolas",
                   Email = "nbologna31@gmail.com",
                   UserName = "nbologna_alumno",
                   Password = "123456",
                   Id = 1
               },
                new Student
                {
                    LastName = "Perez",
                    Name = "Juan",
                    Email = "Jperez@gmail.com",
                    UserName = "jperez",
                    Password = "123456",
                    Id = 2
                },
                new Student
                {
                    LastName = "Garcia",
                    Name = "Pedro",
                    Email = "pgarcia@gmail.com",
                    UserName = "pgarcia",
                    Password = "123456",
                    Id = 3
                });

            modelBuilder.Entity<Professor>().HasData(
             new Professor
             {
                 LastName = "Bologna",
                 Name = "Nicolas",
                 Email = "nbologna31@gmail.com",
                 UserName = "nbologna_profesor",
                 Password = "123456",
                 Id = 4
             },
             new Professor
             {
                 LastName = "Paez",
                 Name = "Pablo",
                 Email = "ppaez@gmail.com",
                 UserName = "ppaez",
                 Password = "123456",
                 Id = 5
             });

            modelBuilder.Entity<Subject>().HasData(
               new Subject
               {
                   Id = 1,
                   Name = "Programación 3",
                   Quarter = "Tercer"
               },
               new Subject
               {
                   Id = 2,
                   Name = "Labarotario de Computacion 4",
                   Quarter = "Cuarto"
               });

            modelBuilder.Entity<Subject>()
                .HasMany(su => su.Students)
                .WithMany(st => st.SubjectsAttended)
                .UsingEntity(ri => ri
                    .ToTable("SubjectsStudents")
                    .HasData(new[]
                     {
                            new { StudentsId = 1, SubjectsAttendedId = 1},
                            new { StudentsId = 1, SubjectsAttendedId = 2},
                     }
                    )
                 );

            modelBuilder.Entity<Subject>()
                .HasMany(sub => sub.Professors)
                .WithMany(pro => pro.Subjects)
                .UsingEntity(j => j
                    .ToTable("ProfessorsSubjects")
                    .HasData(new[]
                        {
                            new { ProfessorsId = 4, SubjectsId = 1},
                            new { ProfessorsId= 5, SubjectsId = 1},
                            new { ProfessorsId = 4, SubjectsId = 2},
                        }
                    ));

            base.OnModelCreating(modelBuilder);


        }
}
