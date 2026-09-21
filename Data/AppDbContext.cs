using Microsoft.EntityFrameworkCore;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.ClassRoom)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassRoomId);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Teachers)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DepartmentId);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithOne(s => s.Teacher)
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Mathematics", Description = "Math and Sciences department" },
                new Department { Id = 2, Name = "Languages", Description = "Languages and Literature department" }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, FirstName = "Ahmed", LastName = "Hassan", Email = "ahmed.hassan@school.com", PhoneNumber = "01000000001", Salary = 15000m, DepartmentId = 1 },
                new Teacher { Id = 2, FirstName = "Mona", LastName = "Ali", Email = "mona.ali@school.com", PhoneNumber = "01000000002", Salary = 14000m, DepartmentId = 2 },
                new Teacher { Id = 3, FirstName = "Sara", LastName = "Khaled", Email = "sara.khaled@school.com", PhoneNumber = "01000000003", Salary = 16000m, DepartmentId = 1 }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Algebra", Description = "Algebra 1", MaxGrade = 100, TeacherId = 1 },
                new Subject { Id = 2, Name = "Geometry", Description = "Geometry", MaxGrade = 100, TeacherId = 1 },
                new Subject { Id = 3, Name = "English", Description = "English Language", MaxGrade = 100, TeacherId = 2 },
                new Subject { Id = 4, Name = "Physics", Description = "Physics", MaxGrade = 100, TeacherId = 3 }
            );

            modelBuilder.Entity<ClassRoom>().HasData(
                new ClassRoom { Id = 1, Name = "Grade 10 A", Gradelevel = 10, Capacity = 30 },
                new ClassRoom { Id = 2, Name = "Grade 10 B", Gradelevel = 10, Capacity = 25 }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Karim", LastName = "Mostafa", Email = "karim@student.com", PhoneNumber = "01100000001", DateofBirth = new DateTime(2009, 5, 15), ClassRoomId = 1 },
                new Student { Id = 2, FirstName = "Omar", LastName = "Tarek", Email = "omar@student.com", PhoneNumber = "01100000002", DateofBirth = new DateTime(2009, 8, 2), ClassRoomId = 1 },
                new Student { Id = 3, FirstName = "Laila", LastName = "Youssef", Email = "laila@student.com", PhoneNumber = "01100000003", DateofBirth = new DateTime(2010, 1, 20), ClassRoomId = 2 }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, StudentId = 1, SubjectId = 1, EnrollmentDate = new DateTime(2026, 9, 1), Grade = 92 },
                new Enrollment { Id = 2, StudentId = 1, SubjectId = 3, EnrollmentDate = new DateTime(2026, 9, 1), Grade = 85 },
                new Enrollment { Id = 3, StudentId = 2, SubjectId = 1, EnrollmentDate = new DateTime(2026, 9, 1), Grade = 78 },
                new Enrollment { Id = 4, StudentId = 2, SubjectId = 2, EnrollmentDate = new DateTime(2026, 9, 1), Grade = 90 },
                new Enrollment { Id = 5, StudentId = 3, SubjectId = 4, EnrollmentDate = new DateTime(2026, 9, 1), Grade = 88 }
            );
        }
    }
}