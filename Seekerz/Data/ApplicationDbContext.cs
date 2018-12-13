using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Seekerz.Models;

namespace Seekerz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<QA> QA { get; set; }
        public DbSet<TaskToDo> TaskToDo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            // Restrict deletion of related order when Tasks entry is removed
            modelBuilder.Entity<Job>()
                .HasMany(j => j.UserTasks)
                .WithOne(l => l.Jobs)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Jobs)
                .WithOne(l => l.Company)
                .OnDelete(DeleteBehavior.Restrict);

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Admina",
                LastName = "Straytor",
                UserName = "Admina",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            ApplicationUser Helen = new ApplicationUser
            {
                FirstName = "Helen",
                LastName = "Chalmers",
                UserName = "Helen",
                NormalizedUserName = "HChALMERS23@GMAIL.COM",
                Email = "hchalmers23@gmail.com",
                NormalizedEmail = "HCHALMERS23@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHash2 = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash2.HashPassword(Helen, "helen*23");
            modelBuilder.Entity<ApplicationUser>().HasData(Helen);


            modelBuilder.Entity<Company>().HasData(
               new Company()
               {
                   CompanyId = 1,
                   Name = "Maize Analytics",
                   Location = "West End, Nashville",
                   URL = "https://www.maizeanalytics.com/"
               },
               new Company()
               {
                   CompanyId = 2,
                   Name = "Claris Health",
                   Location = "Nashville, TN",
                   URL = "https://www.clarishealth.com/"
               },
               new Company()
               {
                   CompanyId = 3,
                   Name = "Info Works",
                   Location = "Nashville, TN",
                   URL = "https://www.infoworks.io/"
               },
               new Company()
               {
                   CompanyId = 4,
                   Name = "Ramsey Solutions",
                   Location = "Franklin, TN",
                   URL = ""
               }

           );

            modelBuilder.Entity<Job>().HasData(
                new Job()
                {
                    JobId = 1,
                    Position = "Software Developer 1",
                    PersonalNotes = "Liked the Company and is growing dramatically over the next year.",
                    ToldNss = "Nss KNows - employer came in to NSS to interview",
                    IsActive = true,
                    UserId = Helen.Id,
                    CompanyId = 2
                },
                new Job()
                {
                    JobId = 2,
                    Position = "Software Developer",
                    PersonalNotes = "Had a mock interview that could turn into a real one.",
                    ToldNss = "Kristin knows about the mock interview might turn into a real one",
                    IsActive = true,
                    UserId = Helen.Id,
                    CompanyId = 3
                },
                new Job()
                {
                    JobId = 3,
                    Position = "Technical Operations",
                    PersonalNotes = "Interviewed with Chase Ramsey - have a 2nd interview scheduled",
                    ToldNss = "knows that I have a technical interview scheduled",
                    IsActive = true,
                    UserId = Helen.Id,
                    CompanyId = 1
                }
            );
            modelBuilder.Entity<TaskToDo>().HasData(
               new TaskToDo()
               {
                   TaskToDoId = 1,
                   NewTask = "Study-Technical Interview",
                   CompleteDate = new DateTime(2018, 12, 18, 13, 30, 00),
                   IsCompleted = false,
                   JobId = 3
               },
               new TaskToDo()
               {
                   TaskToDoId = 2,
                   NewTask = "Follow up with Claris Health",
                   CompleteDate = new DateTime(2018, 12, 17, 12, 30, 00),
                   IsCompleted = false,
                   JobId = 1
               },
               new TaskToDo()
               {
                   TaskToDoId = 3,
                   NewTask = "Follow up with Infoworks",
                   CompleteDate = new DateTime(2018, 12, 17, 12, 30, 00),
                   IsCompleted = false,
                   JobId = 3
               }
               );

            modelBuilder.Entity<QA>().HasData(
               new QA()
               {
                   QAId = 1,
                   Question = "Tell me about what you use in the CommandLine",
                   Answer = "OhmyZsh on Mac side and GitBash on WindowsSide",
                   Notes = "Maize Analytics asked this",
                   UserId = Helen.Id
               },
               new QA()
               {
                   QAId = 2,
                   Question = "What has been your greatest weakness",
                   Answer = "Confidence Level",
                   Notes = "Kyle from Infoworks - during Mock Interview",
                   UserId = Helen.Id
               },
               new QA()
               {
                   QAId = 3,
                   Question = "What was your biggest challenge at NSS",
                   Answer = "Hard Personalities to work with ",
                   Notes = "Claris Health",
                   UserId = Helen.Id
               }
               );
        }
    }
}
