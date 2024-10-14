using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessCard.Employers.Records;
using BusinessCard.Employments.Records;
using BusinessCard.Infrastructure;
using BusinessCard.JobTitles.Records;
using BusinessCard.People.Records;
using BusinessCard.Technologies.Records;

namespace BusinessCard
{
    public static class Seeder
    {
        public static void Seed(Ctx context)
        {
            int dutiesCounter = 0;
            int employmentsCount = 0;
            int assignmentsCount = 0;
            int jobTitleCounter = 0;
            int technologyCount = 0;

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BusinessCard.Assets.image.png");

            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer);

            Person person = new()
            {
                FirstName = "Andrei",
                LastName = "Kuharchuk",
                YearsOld = (int) (DateTime.UtcNow - new DateTime(1988, 07, 21)).TotalDays / 365,
                Id = 1,
                Location = "Brest, Belarus",
                Specialization = ".Net Developer",
                Image = new PersonImage
                {
                    ContentType = "image/png",
                    Bytes = buffer.ToArray()
                },
                Summary =
                    "Experienced C# developer specializing in ASP.NET Core with a background in both back-end and front-end development using Angular (mostly), React etc Skilled in building robust web applications and APIs, with a focus on clean architecture and best practices.",
            };

            var godel = new Employer()
            {
                Id = 1,
                Name = "Godel Technologies Europe, BY"
            };
            var eComm = new Employer()
            {
                Id = 2,
                Name = "E-commerce Product Company, BY"
            };

            var sabbatical = new Employer()
            {
                Id = 3,
                Name = "Sabbatical"
            };

            var logistics = new Employer()
            {
                Name = "Logistics Company, BY", Id = 5
            };

            context.Employers.Add(godel);
            context.Employers.Add(eComm);
            context.Employers.Add(sabbatical);
            context.Employers.Add(logistics);

            JobTitle baPm = new()
            {
                Id = ++jobTitleCounter,
                Name = "BA / PM",
                StartDate = new DateTime(2017, 5, 1),
                EndDate = new DateTime(2018, 4, 1)
            };

            JobTitle dev = new()
            {
                Id = ++jobTitleCounter,
                Name = "Software Developer",
                StartDate = new DateTime(2018, 4, 13),
                EndDate = new DateTime(2021, 11, 1)
            };
            JobTitle seniorDev = new()
            {
                Id = ++jobTitleCounter,
                Name = "Senior Software Developer",
                StartDate = new DateTime(2021, 11, 1),
                EndDate = new DateTime(2023, 4, 12)
            };

            context.JobTitles.Add(baPm);
            context.JobTitles.Add(dev);
            context.JobTitles.Add(seniorDev);

            Technology html = new()
            {
                Id = ++technologyCount, Title = "HTML"
            };
            Technology css = new()
            {
                Id = ++technologyCount, Title = "CSS"
            };
            Assignment baAssignment = new()
            {
                Id = ++assignmentsCount, Name = "BA / PM support of development",
                Role = "Business analyst / Project manager",
                StartDate = new DateTime(2017, 5, 1),
                Summary = "E-commerce application build with Ruby on Rail framework",
                Description = "Cloth shop",
                EndDate = new DateTime(2018, 4, 1),
                Duties = new List<Duty>
                {
                    new()
                    {
                        Id = ++dutiesCounter,
                        Description = "Business needs analysis"
                    },
                    new()
                    {
                        Id = ++dutiesCounter,
                        Description = "Feature management"
                    },
                    new()
                    {
                        Id = ++dutiesCounter,
                        Description = "Technical discussions participation"
                    },
                    new()
                    {
                        Id = ++dutiesCounter,
                        Description = "UI/UX"
                    },
                    new()
                    {
                        Id = ++dutiesCounter,
                        Description = "Manual quality assurance"
                    }
                },
                Technologies = new List<Technology>()
                {
                    html, css,
                }
            };

            context.Assignments.Add(baAssignment);

            Technology cSharp = new()
            {
                Id = ++technologyCount, Title = "C#"
            };
            Technology typeScript = new()
            {
                Id = ++technologyCount, Title = "Typescript"
            };
            Technology javaScript = new()
            {
                Id = ++technologyCount, Title = "Javascript"
            };
            Technology efCore = new()
            {
                Id = ++technologyCount, Title = "Entity Framework Core"
            };
            Technology vbNet = new()
            {
                Id = ++technologyCount, Title = "VB.NET"
            };
            Technology rabbitMq = new()
            {
                Id = ++technologyCount, Title = "Rabbit MQ"
            };
            Technology aspNetCore = new()
            {
                Id = ++technologyCount, Title = "ASP.NET Core"
            };
            Technology msSqlServer = new()
            {
                Id = ++technologyCount, Title = "MS SQL Server"
            };
            Technology angular = new()
            {
                Id = ++technologyCount, Title = "Angular"
            };
            Technology mySql = new()
            {
                Id = ++technologyCount, Title = "MySql"
            };

            person.Employments = new List<Employment>()
            {
                new()
                {
                    Id = ++employmentsCount,
                    StartDate = new DateTime(2023, 4, 13),
                    Employer = sabbatical,
                    Assignments = new List<Assignment>()
                    {
                        new()
                        {
                            StartDate = new DateTime(2020, 5, 1),
                            Name = "Router (Pet Project)",
                            EndDate = DateTime.Today,
                            Description =
                                "Nuget package providing alternative, fast and easy approach to develop web api",
                            Summary = "Router library",
                            Role = "Developer",
                            Id = ++assignmentsCount,
                            Technologies = new List<Technology>()
                            {
                                cSharp, efCore, rabbitMq, msSqlServer
                            }
                        }
                    },
                    JobTitles = new List<JobTitle>()
                    {
                        new()
                        {
                            Name = "Software Developer. Still :)",
                            StartDate = new DateTime(2023, 4, 13), Id = ++jobTitleCounter, EndDate = DateTime.Today
                        }
                    }
                },
                new()
                {
                    Employer = godel,
                    Id = ++employmentsCount,
                    StartDate = new DateTime(2018, 4, 13),
                    PersonId = 1,
                    Person = person,
                    EndDate = new DateTime(2023, 4, 12),
                    JobTitles = new List<JobTitle>()
                    {
                        dev,
                        seniorDev
                    },
                    Assignments = new List<Assignment>()
                    {
                        new()
                        {
                            Name = "E-Commerce app",
                            Id = ++assignmentsCount,
                            Role = "Developer",
                            StartDate = new DateTime(2018, 06, 01),
                            EndDate = new DateTime(2018, 09, 01),
                            Summary = "Microservice system built based on .NET Core, AngularJS, RabbitMQ etc",
                            Description =
                                "A global digital sports platform that consists of several businesses, " +
                                "including licensed sports merchandise, trading cards and collectibles, " +
                                "sports betting and iGaming, special events, and live commerce",
                            Duties = new List<Duty>()
                            {
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Features Design and Development"
                                },
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Unit-tests and bug fixes"
                                },
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Third party service integration"
                                }
                            },
                            Technologies = new List<Technology>()
                            {
                                cSharp,
                                typeScript,
                                javaScript,
                                efCore,
                                vbNet,
                                html,
                                css,
                                rabbitMq,
                                aspNetCore,
                                msSqlServer
                            }
                        },
                        new()
                        {
                            Name = "Godel's internal project",
                            Id = ++assignmentsCount,
                            Role = "Developer",
                            StartDate = new DateTime(2018, 09, 01),
                            EndDate = new DateTime(2018, 11, 01),
                            Description = "Internal human resources management tool",
                            Summary = "Application built using .NET, Angular 2+, EF Core etc",
                            Duties = new List<Duty>()
                            {
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Features Design and Development",
                                },
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Unit-tests and bug fixes"
                                },
                            },
                            Technologies = new List<Technology>()
                            {
                                cSharp,
                                angular,
                                msSqlServer,
                                mySql,
                                aspNetCore,
                                css,
                                html,
                                efCore,
                                typeScript
                            }
                        },
                        new()
                        {
                            Id = ++assignmentsCount,
                            Name = "Financial advisers system",
                            Description = "Technology solution for financial advisors and financial services firms",
                            Summary = "Migration of micro services from .NET Framework to .NET Core",
                            StartDate = new DateTime(2018, 11, 1),
                            EndDate = new DateTime(2019, 5, 1), Role = "Developer",
                            Duties = new List<Duty>()
                            {
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Migration of microservices from .NET Framework 4.7 to .NET Core",
                                },
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Unit-tests and bug fixes"
                                }
                            }
                        },
                        new()
                        {
                            Id = ++assignmentsCount,
                            Name = "Printing production costs calculation application",
                            StartDate = new DateTime(2019, 5, 1),
                            Summary = "Application system built using .NET Core, EF Core, Rabbit MQ, etc",
                            EndDate = new DateTime(2023, 2, 1),
                            Description =
                                "Management Information Software for the printing, packaging and label industries",
                            Role = "Developer",
                            Duties = new List<Duty>()
                            {
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Features Design and Development",
                                },
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Code reviews",
                                },
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Unit-tests and bug fixes"
                                },
                                new()
                                {
                                    Id = ++dutiesCounter,
                                    Description = "Third party service integration"
                                }
                            },
                            Technologies = new List<Technology>()
                            {
                                cSharp,
                                aspNetCore,
                                html,
                                css,
                                rabbitMq
                            }
                        }
                    }
                },
                new()
                {
                    Id = ++employmentsCount,
                    PersonId = 1,
                    Employer = eComm,
                    StartDate = new DateTime(2017, 5, 1),
                    EndDate = new DateTime(2018, 4, 1),
                    Person = person,
                    JobTitles = new List<JobTitle>()
                    {
                        baPm
                    },
                    Assignments = new List<Assignment>()
                    {
                        baAssignment
                    }
                },
                new()
                {
                    Id = ++employmentsCount,
                    PersonId = 1,
                    Person = person,
                    Employer = new Employer() {Name = "Self-Education", Id = 4},
                    StartDate = new DateTime(2016, 4, 1),
                    JobTitles = new List<JobTitle>()
                    {
                        new()
                        {
                            Name = "Lead .NET Self-Educator :)",
                            Id = ++jobTitleCounter,
                            StartDate = new DateTime(2016, 4, 1),
                            EndDate = new DateTime(2018, 4, 1)
                        },
                    },
                    Assignments = new List<Assignment>()
                    {
                        new()
                        {
                            Id = ++assignmentsCount,
                            StartDate = new DateTime(2016, 4, 1),
                            EndDate = new DateTime(2018, 4, 1),
                            Name = "Learning ASP.NET C# Coding",
                            Description =
                                "Follow my experience as I delve into the world of ASP.NET C# coding, a framework essential for web application development.",
                            Summary =
                                "My path to mastering ASP.NET C# coding begins with grasping the fundamentals of the C# language, " +
                                "exploring the intricacies of the ASP.NET framework, delving into web forms, MVC, and APIs, " +
                                "honing my skills through hands-on coding challenges and projects, and keeping abreast of the ever-evolving web development landscape.",
                            Role = "Student", Technologies = new List<Technology>()
                            {
                                aspNetCore, efCore, css, html, javaScript, cSharp
                            },
                            Duties = new List<Duty>()
                            {
                                new()
                                {
                                    Id = ++dutiesCounter, Description = "Learning"
                                },
                                new()
                                {
                                    Id = ++dutiesCounter, Description = "Learning"
                                },
                                new()
                                {
                                    Id = ++dutiesCounter, Description = "Learning once again"
                                }
                            }
                        }
                    },
                },
                new()
                {
                    Id = ++employmentsCount, Employer = logistics,
                    StartDate = new DateTime(2013, 4, 1),
                    EndDate = new DateTime(2016, 4, 1),
                    Person = person, 
                    PersonId = 1,
                    JobTitles = new List<JobTitle>()
                    {
                        new()
                        {
                            Id = ++jobTitleCounter,
                            Name = "Software Developer",
                            EndDate = new DateTime(2016, 4, 1),
                            StartDate = new DateTime(2014, 8, 1)
                        },
                        new()
                        {
                            Id = ++jobTitleCounter,
                            Name = "Economist",
                            EndDate = new DateTime(2014, 8, 1),
                            StartDate = new DateTime(2013, 4, 1)
                        }
                    },
                    Assignments = new List<Assignment>()
                    {
                        new()
                        {
                            EndDate = new DateTime(2016, 4, 1),
                            StartDate = new DateTime(2014, 8, 1),
                            Name = "Financial department workflow application",
                            Summary = "Application to simplify financial workflow",
                            Description = "Application system",
                            Id = ++assignmentsCount,
                            Role = "Developer",
                            Technologies =
                                new List<Technology>()
                                {
                                    cSharp, mySql,
                                    new() {Id = ++technologyCount, Title = "Windows Forms"}
                                }
                        },
                        new()
                        {
                            EndDate = new DateTime(2016, 4, 1),
                            StartDate = new DateTime(2013, 4, 1),
                            Name = "MS Excel driven invoicing application",
                            Summary = "Application to simplify invoicing",
                            Description = "Some description",
                            Id = ++assignmentsCount,
                            Role = "Economist / Developer",
                            Technologies =
                                new List<Technology>
                                {
                                    new() {Id = ++technologyCount, Title = "MS ACCESS"},
                                    new() {Id = ++technologyCount, Title = "VBA"},
                                    new() {Id = ++technologyCount, Title = "MS Excel"}
                                }
                        }
                    }
                }
            };

            context.People.Add(person);


            context.SaveChanges();
        }
    }
}