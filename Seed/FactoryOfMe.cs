using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BusinessCard.Seed
{
    public static class FactoryOfMe
    {
        public static PersonData Produce()
        {
            const string? name = "Andrei";
            const string? lastName = "Kukharchuk";

            var person = new PersonData() {Image = new PersonImageData()};

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BusinessCard.Assets.image.png");

            if (stream == null)
            {
                throw new Exception("Image is not reachable");
            }

            var buffer = new byte[stream.Length];

            stream.Read(buffer);

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

            var godel = new EmployerData()
            {
                Name = "Godel Technologies Europe, BY"
            };

            var eComm = new EmployerData()
            {
                Name = "E-commerce Product Company, BY"
            };

            var sabbatical = new EmployerData()
            {
                Name = "Career break (Sabbatical)"
            };

            var logistics = new EmployerData()
            {
                Name = "Logistics Company, BY"
            };

            var baPm = new JobTitleData()
                {Name = "BA / PM", StartDate = new DateTime(2017, 5, 1), EndDate = new DateTime(2018, 4, 1)};

            var dev = new JobTitleData()
            {
                Name = "Software Developer", StartDate = new DateTime(2018, 4, 13), EndDate = new DateTime(2021, 11, 1)
            };

            var seniorDev = new JobTitleData()
            {
                Name = "Software Developer", StartDate = new DateTime(2021, 11, 1), EndDate = new DateTime(2023, 4, 12)
            };

            var html = NewTechnology("HTML");

            var css = NewTechnology("CSS");

            var kendoUi = NewTechnology("Kendo UI");

            var cSharp = NewTechnology("C#");

            var typeScript = NewTechnology("Typescript");

            var javaScript = NewTechnology("Javascript");

            var efCore = NewTechnology("Entity Framework Core");

            var vbNet = NewTechnology("VB.NET");

            var rabbitMq = NewTechnology("Rabbit MQ");

            var aspNetCore = NewTechnology("ASP.NET Core");

            var msSqlServer = NewTechnology("MS SQL Server");

            var dotnetFramework = NewTechnology(".NET Framework 4.8");

            var angular = NewTechnology("Angular");

            var mySql = NewTechnology("MySql");

            NewTechnology("WPF (personal usage only)");

            NewTechnology("Xamarin (personal usage only)");

            NewTechnology("Blazor (personal usage only)");

            var msExcel = NewTechnology("MS Excel");

            var vba = NewTechnology("VBA");

            var msAccess = NewTechnology("MS ACCESS");

            var winForms = NewTechnology("Windows Forms");


            AssignmentData baAssignment = new()
            {
                Name = "BA / PM support of development",
                Role = "Business analyst / Project manager",
                StartDate = new DateTime(2017, 5, 1),
                Summary = "E-commerce application build with Ruby on Rails framework",
                Description =
                    "Business analysis and project management for one of belarusian e-commerce companies, task creation, workload planning, developing prototypes",
                EndDate = new DateTime(2018, 4, 1),
                Duties = new List<string>
                {
                    "Business needs analysis",
                    "Feature management",
                    "Technical discussions participation",
                    "UI/UX",
                    "Manual quality assurance"
                },
                Technologies = new List<TechnologyData>()
                {
                    html, css,
                }
            };

            person.Employments = new List<EmploymentData>()
            {
                new()
                {
                    StartDate = new DateTime(2023, 4, 13),
                    Employer = sabbatical,
                    Assignments = new List<AssignmentData>()
                    {
                        new()
                        {
                            StartDate = new DateTime(2020, 5, 1),
                            Name = "'DotExpress' Library (Pet Project)",
                            EndDate = DateTime.Today,
                            Description =
                                "Nuget package providing alternative, fast and easy approach to develop web api. This application uses a little piece of this lib, see the link below the source code",

                            Link = new LinkData()
                            {
                                Address = "https://github.com/andkuh/business-card",
                                Caption =
                                    "This page is implemented using that package along with classic controllers, see details",
                            },
                            Summary = "Router library",
                            Role = "Developer",
                            Technologies = new List<TechnologyData>()
                            {
                                cSharp, efCore, rabbitMq, msSqlServer, typeScript, angular
                            },
                            Duties = new List<string>()
                            {
                                "Features Design and Development",
                                "Unit testing",
                            }
                        }
                    },
                    JobTitles = new List<JobTitleData>()
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
                    EndDate = new DateTime(2023, 4, 12),
                    JobTitles = new List<JobTitleData>()
                    {
                        dev,
                        seniorDev
                    },
                    Assignments = new List<AssignmentData>()
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
                            Duties = new List<string>()
                            {
                                "Features Design and Development",
                                "Unit-tests and bug fixes",
                                "Third party service integration"
                            },
                            Technologies = new List<TechnologyData>()
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
                            Duties = new List<string>()
                            {
                                "Features Design and Development",
                                "Unit-tests and bug fixes",
                            },
                            Technologies = new List<TechnologyData>()
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
                            Duties = new List<string>()
                            {
                                "Migration of microservices from .NET Framework 4.7 to .NET Core",
                                "Unit-tests and bug fixes"
                            },
                            Technologies = new List<TechnologyData>()
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
                            Duties = new List<string>()
                            {
                                "Features Design and Development",
                                "Code reviews",
                                "Unit-tests and bug fixes",
                                "Third party service integration"
                            },
                            Technologies = new List<TechnologyData>()
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
                    Employer = eComm,
                    StartDate = new DateTime(2017, 5, 1),
                    EndDate = new DateTime(2018, 4, 1),
                    JobTitles = new List<JobTitleData>()
                    {
                        baPm
                    },
                    Assignments = new List<AssignmentData>()
                    {
                        baAssignment
                    }
                },
                new()
                {
                    Employer = new EmployerData() {Name = "Self-Education"},
                    StartDate = new DateTime(2016, 4, 1),
                    JobTitles = new List<JobTitleData>()
                    {
                        new()
                        {
                            Name = "Lead .NET Self-Educator :)",
                            StartDate = new DateTime(2016, 4, 1),
                            EndDate = new DateTime(2018, 4, 1)
                        },
                    },
                    Assignments = new List<AssignmentData>()
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
                            Role = "Student", Technologies = new List<TechnologyData>()
                            {
                                aspNetCore, efCore, css, html, javaScript, cSharp
                            },
                            Duties = new List<string>()
                            {
                                "Learning",
                                "Learning",
                                "Learning once again"
                            }
                        }
                    },
                },
                new()
                {
                    Employer = logistics,
                    StartDate = new DateTime(2013, 4, 1),
                    EndDate = new DateTime(2016, 4, 1),
                    JobTitles = new List<JobTitleData>()
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
                    Assignments = new List<AssignmentData>()
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
                                new List<TechnologyData>()
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
                                new List<TechnologyData>
                                {
                                    msAccess,
                                    vba,
                                    msExcel
                                }
                        }
                    }
                }
            };


            person.EducationSteps = new List<EducationStepData>()
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

            person.Hobbies = new List<string>()
            {
                "Musician",
                "Bike Traveller",
                "Cat Person",
            };

            return person;
        }

        private static TechnologyData NewTechnology(string title)
        {
            return new TechnologyData() {Title = title};
        }
    }
}