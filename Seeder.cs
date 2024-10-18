using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BusinessCard.Employers.Records;
using BusinessCard.Employments.Records;
using BusinessCard.Infrastructure;
using BusinessCard.JobTitles.Records;
using BusinessCard.People.Records;
using BusinessCard.Technologies.Records;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard
{
    public class Seeder
    {
        public static Task SeedAsync(Ctx context)
        {
            return new Seeder(context).SeedAsync();
        }

        private readonly Ctx _context;

        private Seeder(Ctx context)
        {
            _context = context;
        }

        private async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            const string? name = "Andrei";
            const string? lastName = "Kukharchuk";

            var existingPerson = await _context
                .People
                .FirstOrDefaultAsync(s => s.FirstName.Equals(name) && s.LastName.Equals(lastName));

            var person = existingPerson ?? new Person() {Image = new PersonImage()};

            if (existingPerson is null)
            {
                _context.People.Add(person);
            }

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BusinessCard.Assets.image.png");

            if (stream == null)
            {
                throw new Exception("Image is not reachable");
            }

            var buffer = new byte[stream.Length];

            await stream.ReadAsync(buffer);

            person.FirstName = name;
            person.LastName = lastName;
            person.YearsOld = (int) (DateTime.UtcNow - new DateTime(1988, 07, 21)).TotalDays / 365;
            person.Location = "Brest, Belarus";
            person.Specialization = ".Net Developer";
            person.Image.ContentType = "image/png";
            person.Image.Bytes = buffer.ToArray();

            person.Summary =
                "Seasoned .NET Developer with up to 10 years of hands-on experience adept at navigating both backend and front end development landscapes." +
                " Proficient in leveraging .NET technologies to deliver robust solutions effectively.";

            var godel = await EnsureEmployerCreated("Godel Technologies Europe, BY");

            var eComm = await EnsureEmployerCreated("E-commerce Product Company, BY");

            var sabbatical = await EnsureEmployerCreated("Career break (Sabbatical)");

            var logistics = await EnsureEmployerCreated("Logistics Company, BY");

            var baPm = await EnsureJobTitleCreated("BA / PM", t =>
            {
                t.StartDate = new DateTime(2017, 5, 1);
                t.EndDate = new DateTime(2018, 4, 1);
            });

            var dev = await EnsureJobTitleCreated("Software Developer", t =>
            {
                t.StartDate = new DateTime(2018, 4, 13);
                t.EndDate = new DateTime(2021, 11, 1);
            });

            var seniorDev = await EnsureJobTitleCreated("Software Developer", t =>
            {
                t.StartDate = new DateTime(2021, 11, 1);
                t.EndDate = new DateTime(2023, 4, 12);
            });

            var html = await EnsureTechnologyCreated("HTML");

            var css = await EnsureTechnologyCreated("CSS");

            var kendoUi = await EnsureTechnologyCreated("Kendo UI");

            var cSharp = await EnsureTechnologyCreated("C#");

            var typeScript = await EnsureTechnologyCreated("Typescript");

            var javaScript = await EnsureTechnologyCreated("Javascript");

            var efCore = await EnsureTechnologyCreated("Entity Framework Core");

            var vbNet = await EnsureTechnologyCreated("VB.NET");

            var rabbitMq = await EnsureTechnologyCreated("Rabbit MQ");

            var aspNetCore = await EnsureTechnologyCreated("ASP.NET Core");

            var msSqlServer = await EnsureTechnologyCreated("MS SQL Server");

            var dotnetFramework = await EnsureTechnologyCreated(".NET Framework 4.8");

            var angular = await EnsureTechnologyCreated("Angular");

            var mySql = await EnsureTechnologyCreated("MySql");

            await EnsureTechnologyCreated("WPF (personal usage only)");

            await EnsureTechnologyCreated("Xamarin (personal usage only)");

            await EnsureTechnologyCreated("Blazor (personal usage only)");

            var msExcel = await EnsureTechnologyCreated("MS Excel");

            var vba = await EnsureTechnologyCreated("VBA");

            var msAccess = await EnsureTechnologyCreated("MS ACCESS");

            var winForms = await EnsureTechnologyCreated("Windows Forms");

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
                                Caption =
                                    "This page is implemented using that package along with classic controllers, see details",
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
                                rabbitMq,
                                efCore,
                                msSqlServer,
                                typeScript,
                                kendoUi
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
                                    winForms
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
                                    msAccess,
                                    vba,
                                    msExcel
                                }
                        }
                    }
                }
            };


            person.EducationSteps = new List<EducationStep>()
            {
                new()
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
                new()
                {
                    Title = "Musician",
                },
                new()
                {
                    Title = "Bike Traveller",
                },
                new()
                {
                    Title = "Cat Person",
                },
            };

            await _context.SaveChangesAsync();
        }


        private async Task<Employer> EnsureEmployerCreated(string employerName)
        {
            var employer = await _context.Employers.FirstOrDefaultAsync(s => s.Name.Equals(employerName));
            if (employer != null)
            {
                return employer;
            }

            employer = new Employer() {Name = employerName};

            _context.Employers.Add(employer);

            return employer;
        }

        private async Task<JobTitle> EnsureJobTitleCreated(string title, Action<JobTitle> init)
        {
            var jobTitle = await _context.JobTitles.FirstOrDefaultAsync(s => s.Name.Equals(title));
            if (jobTitle != null)
            {
                return jobTitle;
            }

            jobTitle = new JobTitle() {Name = title};

            init(jobTitle);

            _context.JobTitles.Add(jobTitle);

            return jobTitle;
        }

        private async Task<Technology> EnsureTechnologyCreated(string title)
        {
            var jobTitle = await _context.Technologies.FirstOrDefaultAsync(s => s.Title.Equals(title));
            if (jobTitle != null)
            {
                return jobTitle;
            }

            jobTitle = new Technology() {Title = title};

            _context.Technologies.Add(jobTitle);

            return jobTitle;
        }
    }
}