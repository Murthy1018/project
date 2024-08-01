using Microsoft.EntityFrameworkCore;

namespace Project1.Models
{
    public class RegisterAPIDbContext : DbContext
    {
        public RegisterAPIDbContext()
        {
        }

        public RegisterAPIDbContext(DbContextOptions<RegisterAPIDbContext> options)
           : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=DESKTOP-UKJ70SI\\SQLEXPRESS2022;Database=Register;Trusted_Connection=True;");


                IConfigurationRoot configuration = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json")
                                                    .Build();
                var connectionString = configuration.GetConnectionString("DbConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<AppliedJobs> AppliedJobs { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
    }
}

