using BusinessCard.Employers.Records;
using BusinessCard.Employments.Records;
using BusinessCard.Infrastructure.Configurations.Employers;
using BusinessCard.Infrastructure.Configurations.Employments;
using BusinessCard.Infrastructure.Configurations.JobTitles;
using BusinessCard.Infrastructure.Configurations.People;
using BusinessCard.Infrastructure.Configurations.Technologies;
using BusinessCard.JobTitles.Records;
using BusinessCard.People.Records;
using BusinessCard.Technologies.Records;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard.Infrastructure
{
    public class Ctx : DbContext
    {
        public Ctx(DbContextOptions<Ctx> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new EmployerConfiguration());
            modelBuilder.ApplyConfiguration(new JobTitleConfiguration());
            modelBuilder.ApplyConfiguration(new EmploymentConfiguration());
            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new DutyConfiguration());
            modelBuilder.ApplyConfiguration(new TechnologyConfiguration());
            modelBuilder.ApplyConfiguration(new HobbyConfiguration());
        }
        
        public DbSet<Person> People { get; set; }
        
        public DbSet<Employment> Employments { get; set; }
        
        public DbSet<Hobby> Hobbies { get; set; }
        
        public DbSet<EducationStep> EducationSteps { get; set; }

        public DbSet<Employer> Employers { get; set; }
        
        public DbSet<JobTitle> JobTitles { get; set; }
        
        public DbSet<Assignment> Assignments { get; set; }
        
        public DbSet<Technology> Technologies { get; set; }
    }
}