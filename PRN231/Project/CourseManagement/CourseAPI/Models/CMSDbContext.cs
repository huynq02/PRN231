using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CourseAPI.Models
{
    public class CMSDbContext :DbContext
    {
        public CMSDbContext() { }
        public CMSDbContext(DbContextOptions<CMSDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CMSSDb"));
        }
        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<QuizAttendance> QuizAttendances { get; set; } = null!;
        public virtual DbSet<StudentAssignment> StudentAssignments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

    }
}
