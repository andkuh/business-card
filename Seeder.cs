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
using BusinessCard.Seed;
using BusinessCard.Technologies.Records;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard
{
    public class Seeder
    {
        public static async Task SeedAsync(Ctx context)
        {
            var dataAsync = FactoryOfMe.Produce();

            await new Seeder(context).SeedAsync(dataAsync);
        }

        private async Task SeedAsync(PersonData data)
        {
            await _context.Database.MigrateAsync();

            var person = await _context.People
                .Include(s => s.Employments)
                .ThenInclude(s => s.Assignments)
                .ThenInclude(s => s.Duties)
                .Include(s => s.Employments)
                .ThenInclude(s => s.Assignments)
                .ThenInclude(s => s.Technologies)
                .Include(s => s.Employments)
                .ThenInclude(s => s.Employer)
                .Include(s => s.Employments)
                .ThenInclude(s => s.JobTitles)
                .Include(s => s.Hobbies)
                .Include(s => s.EducationSteps)
                .AsSplitQuery()
                .FirstOrDefaultAsync(s =>
                    s.FirstName.Equals(data.FirstName) && s.LastName.Equals(data.LastName));

            var technologies = await _context.Technologies.ToListAsync();

            if (person == null)
            {
                person = new Person() {Image = new PersonImage()};
                _context.People.Add(person);
            }

            person.FirstName = data.FirstName;
            person.LastName = data.LastName;
            person.Image.ContentType = data.Image.ContentType;
            person.Image.Bytes = data.Image.Bytes;

            person.Location = data.Location;
            person.Summary = data.Summary;
            person.Specialization = data.Specialization;
            person.YearsOld = data.YearsOld;

            person.Hobbies = data.Hobbies
                .Distinct()
                .Synchronize(person.Hobbies,
                    (s, hobby) => s == hobby.Title,
                    s => new Hobby() {Title = s}
                )
                .DistinctBy(s => s.Title)
                .ToList();


            person.EducationSteps =
                data.EducationSteps
                    .Distinct()
                    .Synchronize
                    (
                        targetList: person.EducationSteps,
                        matchPredicate: (stepData, step) => stepData.Name == step.Name,
                        targetItemFactory: stepData => new EducationStep()
                        {
                            Name = stepData.Name
                        },
                        map: (stepData, step) =>
                        {
                            step.Institution = stepData.Institution;
                            step.Location = stepData.Location;
                            step.YearFinished = stepData.YearFinished;
                            step.YearStarted = stepData.YearStarted;
                        }
                    )
                    .DistinctBy(s => s.Name)
                    .ToList();

            person.Employments = data.Employments.Synchronize
            (
                targetList: person.Employments,
                matchPredicate: (employmentData, employment) =>
                    employmentData.Employer.Name == employment.Employer.Name, employmentData => new Employment()
                {
                    Employer = new Employer()
                    {
                        Name = employmentData.Employer.Name
                    }
                },
                map: (employmentData, employment) =>
                {
                    employment.EndDate = employmentData.EndDate;
                    employment.StartDate = employmentData.StartDate;
                    employment.JobTitles = employmentData.JobTitles.Synchronize
                    (
                        targetList: employment.JobTitles,
                        matchPredicate: (titleData, title) => titleData.Name == title.Name,
                        targetItemFactory: titleData => new JobTitle() {Name = titleData.Name}, (titleData, title) =>
                        {
                            title.EndDate = titleData.EndDate;
                            title.StartDate = titleData.StartDate;
                        }
                    );

                    employment.Assignments = employmentData.Assignments.Synchronize(employment.Assignments,
                        (assignmentData, assignment) => assignmentData.Name == assignment.Name,
                        assignmentData => new Assignment() {Name = assignmentData.Name},
                        (
                            assignmentData, assignment) =>
                        {
                            assignment.Description = assignmentData.Description;
                            assignment.Name = assignmentData.Name;
                            assignment.Role = assignmentData.Role;
                            assignment.Summary = assignmentData.Summary;
                            assignment.StartDate = assignmentData.StartDate;
                            assignment.EndDate = assignmentData.EndDate;
                            assignment.Technologies = assignmentData.Technologies.Synchronize(assignment.Technologies,
                                (technologyData, technology) => technologyData.Title == technology.Title,
                                technologyData => new Technology()
                                {
                                    Title = technologyData.Title
                                }, existingList: technologies);

                            assignment.Link = assignmentData.Link != null
                                ? MapLink(assignmentData.Link, assignment.Link ?? new Link())
                                : null;

                            assignment.Duties = assignmentData
                                .Duties
                                .Synchronize(assignment.Duties,
                                    (s, duty) => s == duty.Description, s => new Duty() {Description = s});
                        });
                }
            );

            Link MapLink(LinkData linkData, Link link)
            {
                link.Address = linkData.Address;
                link.Caption = linkData.Caption;

                return link;
            }

            await _context.SaveChangesAsync();
        }

        private readonly Ctx _context;

        private Seeder(Ctx context)
        {
            _context = context;
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


    public static class Ext
    {
        public static List<TTarget> Synchronize<TSource, TTarget>(
            this IEnumerable<TSource> sourceList,
            IEnumerable<TTarget> targetList,
            Func<TSource, TTarget, bool> matchPredicate,
            Func<TSource, TTarget> targetItemFactory, Action<TSource, TTarget>? map = null,
            List<TTarget>? existingList = null)
        {
            var sourceItems = sourceList?.ToList() ?? new List<TSource>();

            List<TTarget> targets = targetList?.ToList() ?? new List<TTarget>();

            IEnumerable<TTarget> pool = existingList ?? targets;

            foreach (var sourceItem in sourceItems)
            {
                var existingTargetItem = pool.FirstOrDefault(t => matchPredicate(sourceItem, t));

                if (existingTargetItem != null)
                {
                    // Update existing target item with source item
                    map?.Invoke(sourceItem, existingTargetItem);
                    // Update other properties as needed
                }
                else
                {
                    // Create a new target item using the factory delegate
                    var newTargetItem = targetItemFactory(sourceItem);

                    map?.Invoke(sourceItem, newTargetItem);

                    targets.Add(newTargetItem);

                    existingList?.Add(newTargetItem);
                }
            }

            // Remove items from the target list that do not have a corresponding source item
            targets.RemoveAll(t => !sourceItems.Any(s => matchPredicate(s, t)));


            return targets;
        }
    }

    public interface IStrategy
    {
        TTarget Find<TSource, TTarget>(Func<TSource, TTarget, bool> matchPredicate);

        void Add();
    }
}