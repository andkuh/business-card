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
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BusinessCard.Assets.image.png");

            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer);

            Person person = new()
            {
                FirstName = "Andrei",
                LastName = "Kuharchuk",
                YearsOld = (int) (DateTime.UtcNow - new DateTime(1988, 07, 21)).TotalDays / 365,
                Location = "Brest, Belarus",
                Specialization = ".Net Developer",
                Image = new PersonImage
                {
                    ContentType = "image/png",
                    Bytes = buffer.ToArray()
                },
                Summary =
                    "Seasoned .NET Developer with up to 10 years of hands-on experience adept at navigating both backend and front end development landscapes." +
                    " Proficient in leveraging .NET technologies to deliver robust solutions effectively."
            };

            var godel = new Employer()
            {
                Name = "Godel Technologies Europe, BY"
            };
            var eComm = new Employer()
            {
                Name = "E-commerce Product Company, BY"
            };

            var sabbatical = new Employer()
            {
                Name = "Career break (Sabbatical)"
            };

            var logistics = new Employer()
            {
                Name = "Logistics Company, BY", //Id = 5
            };

            context.Employers.Add(godel);
            context.Employers.Add(eComm);
            context.Employers.Add(sabbatical);
            context.Employers.Add(logistics);

            JobTitle baPm = new()
            {
                Name = "BA / PM",
                StartDate = new DateTime(2017, 5, 1),
                EndDate = new DateTime(2018, 4, 1)
            };

            JobTitle dev = new()
            {
                Name = "Software Developer",
                StartDate = new DateTime(2018, 4, 13),
                EndDate = new DateTime(2021, 11, 1)
            };
            JobTitle seniorDev = new()
            {
                Name = "Senior Software Developer",
                StartDate = new DateTime(2021, 11, 1),
                EndDate = new DateTime(2023, 4, 12)
            };

            context.JobTitles.Add(baPm);
            context.JobTitles.Add(dev);
            context.JobTitles.Add(seniorDev);

            Technology html = new()
            {
                Title = "HTML"
            };
            Technology css = new()
            {
                Title = "CSS"
            };
            Assignment baAssignment = new()
            {
                Name = "BA / PM support of development",
                Role = "Business analyst / Project manager",
                StartDate = new DateTime(2017, 5, 1),
                Summary = "E-commerce application build with Ruby on Rails framework",
                Description =
                    "Business analysis and project management for one of belarusian e-commerce companies, task creation, workload planning, developing prototypes",
                EndDate = new DateTime(2018, 4, 1),
                Duties = new List<Duty>
                {
                    new()
                    {
                        Description = "Business needs analysis"
                    },
                    new()
                    {
                        Description = "Feature management"
                    },
                    new()
                    {
                        Description = "Technical discussions participation"
                    },
                    new()
                    {
                        Description = "UI/UX"
                    },
                    new()
                    {
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
                Title = "C#"
            };
            Technology typeScript = new()
            {
                Title = "Typescript"
            };
            Technology javaScript = new()
            {
                Title = "Javascript"
            };
            Technology efCore = new()
            {
                Title = "Entity Framework Core"
            };
            Technology vbNet = new()
            {
                Title = "VB.NET"
            };
            Technology rabbitMq = new()
            {
                Title = "Rabbit MQ"
            };
            Technology aspNetCore = new()
            {
                Title = "ASP.NET Core"
            };
            Technology msSqlServer = new()
            {
                Title = "MS SQL Server"
            };
            Technology dotnetFramework = new Technology()
            {
                Title = ".NET Framework 4.8"
            };
            Technology angular = new()
            {
                Title = "Angular"
            };
            Technology mySql = new()
            {
                Title = "MySql"
            };


            person.Employments = new List<Employment>()
            {
                new()
                {
                    StartDate = new DateTime(2023, 4, 13),
                    Employer = sabbatical,
                    Assignments = new List<Assignment>()
                    {
                        new()
                        {
                            StartDate = new DateTime(2020, 5, 1),
                            Name = "'DotExpress' Library (Pet Project)",
                            EndDate = DateTime.Today,
                            Description =
                                "Nuget package providing alternative, fast and easy approach to develop web api. This application uses a little piece of this lib, see the link below the source code",

                            Link = new Link()
                            {
                                Address = "https://github.com/andkuh/business-card",
                                Caption = "This page is implemented using that package along with classic controllers, see details",
                            },
                            Summary = "Router library",
                            Role = "Developer",
                            Technologies = new List<Technology>()
                            {
                                cSharp, efCore, rabbitMq, msSqlServer, typeScript, angular
                            },
                            Duties = new List<Duty>()
                            {
                                new()
                                {
                                    Description = "Features Design and Development",
                                },
                                new()
                                {
                                    Description = "Unit testing",
                                },
                            }
                        }
                    },
                    JobTitles = new List<JobTitle>()
                    {
                        new()
                        {
                            Name = "Software Developer. Still :)",
                            StartDate = new DateTime(2023, 4, 13),
                            EndDate = DateTime.Today
                        }
                    }
                },
                new()
                {
                    Employer = godel,
                    StartDate = new DateTime(2018, 4, 13),
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
                                    Description = "Features Design and Development"
                                },
                                new()
                                {
                                    Description = "Unit-tests and bug fixes"
                                },
                                new()
                                {
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
                            Role = "Developer",
                            StartDate = new DateTime(2018, 09, 01),
                            EndDate = new DateTime(2018, 11, 01),
                            Description = "Internal human resources management tool",
                            Summary = "Application built using .NET, Angular 2+, EF Core etc",
                            Duties = new List<Duty>()
                            {
                                new()
                                {
                                    Description = "Features Design and Development",
                                },
                                new()
                                {
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
                            Name = "Financial advisers system",
                            Description = "Technology solution for financial advisors and financial services firms",
                            Summary = "Migration of micro services from .NET Framework to .NET Core",
                            StartDate = new DateTime(2018, 11, 1),
                            EndDate = new DateTime(2019, 5, 1), Role = "Developer",
                            Duties = new List<Duty>()
                            {
                                new()
                                {
                                    Description = "Migration of microservices from .NET Framework 4.7 to .NET Core",
                                },
                                new()
                                {
                                    Description = "Unit-tests and bug fixes"
                                }
                            },
                            Technologies = new List<Technology>()
                            {
                                aspNetCore, cSharp, efCore, msSqlServer, dotnetFramework
                            }
                        },
                        new()
                        {
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
                                    Description = "Features Design and Development",
                                },
                                new()
                                {
                                    Description = "Code reviews",
                                },
                                new()
                                {
                                    Description = "Unit-tests and bug fixes"
                                },
                                new()
                                {
                                    Description = "Third party service integration"
                                }
                            },
                            Technologies = new List<Technology>()
                            {
                                cSharp,
                                aspNetCore,
                                html,
                                css,
                                rabbitMq, efCore, msSqlServer, typeScript,
                                new Technology() {Title = "Kendo UI"}
                            }
                        }
                    }
                },
                new()
                {
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
                    Person = person,
                    Employer = new Employer() {Name = "Self-Education"},
                    StartDate = new DateTime(2016, 4, 1),
                    JobTitles = new List<JobTitle>()
                    {
                        new()
                        {
                            Name = "Lead .NET Self-Educator :)",
                            StartDate = new DateTime(2016, 4, 1),
                            EndDate = new DateTime(2018, 4, 1)
                        },
                    },
                    Assignments = new List<Assignment>()
                    {
                        new()
                        {
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
                                    Description = "Learning"
                                },
                                new()
                                {
                                    Description = "Learning"
                                },
                                new()
                                {
                                    Description = "Learning once again"
                                }
                            }
                        }
                    },
                },
                new()
                {
                    Employer = logistics,
                    StartDate = new DateTime(2013, 4, 1),
                    EndDate = new DateTime(2016, 4, 1),
                    Person = person,
                    JobTitles = new List<JobTitle>()
                    {
                        new()
                        {
                            Name = "Software Developer",
                            EndDate = new DateTime(2016, 4, 1),
                            StartDate = new DateTime(2014, 8, 1)
                        },
                        new()
                        {
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
                            Description = "Application system for financial department document workflow",
                            Role = "Developer",
                            Technologies =
                                new List<Technology>()
                                {
                                    cSharp, msSqlServer,
                                    new()
                                    {
                                        Title = "Windows Forms"
                                    }
                                }
                        },
                        new()
                        {
                            EndDate = new DateTime(2016, 4, 1),
                            StartDate = new DateTime(2013, 4, 1),
                            Name = "MS Excel driven invoicing application",
                            Summary = "Application to simplify invoicing",
                            Description =
                                "Visual Basic for Applications driven Excel files to manage invoicing process",
                            Role = "Economist / Developer",
                            Technologies =
                                new List<Technology>
                                {
                                    new()
                                    {
                                        Title = "MS ACCESS"
                                    },
                                    new()
                                    {
                                        Title = "VBA"
                                    },
                                    new()
                                    {
                                        Title = "MS Excel"
                                    }
                                }
                        }
                    }
                }
            };

            Technology wpf = new Technology()
            {
                Title = "WPF (personal usage only)"
            };

            Technology xamarin = new Technology()
            {
                Title = "Xamarin (personal usage only)"
            };




            person.EducationSteps = new List<EducationStep>()
            {
                new EducationStep()
                {
                    Institution = "BSTU", 
                    Name = "Bachelor's Degree in Finance",
                    Location = "Brest, BY", 
                    YearStarted = 2005, 
                    YearFinished = 2010
                }
            };

            person.Hobbies = new List<Hobby>()
            {
                new Hobby()
                {
                    Title = "Musician",
                },
                new Hobby()
                {
                    Title = "Bike Traveller",
                },
                new Hobby()
                {
                    Title = "Cat Person",
                },
            };


            context.Technologies.Add(wpf);
            context.Technologies.Add(xamarin);

            context.People.Add(person);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}