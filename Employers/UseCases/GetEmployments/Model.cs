using System;
using System.Collections.Generic;

namespace BusinessCard.Employers.UseCases.GetEmployments
{
    public class Model
    {
        public class EmployerModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Assignment
        {
            public IEnumerable<string> Duties { get; set; }
            public IEnumerable<string> Technologies { get; set; }
            public string Summary { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime StartDate { get; set; }
            public string Name { get; set; }
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public class CareerStep
        {
            public DateTime EndDate { get; set; }
            public DateTime StartDate { get; set; }
            public string Title { get; set; }
            public IEnumerable<Assignment> Assignments { get; set; }
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmployerModel Employer { get; set; }
        public IEnumerable<CareerStep> CareerSteps { get; set; }
    }
}