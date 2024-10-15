using System;
using System.Collections.Generic;

namespace BusinessCard.Employments.Endpoints
{
    public class EmploymentDto
    {
        public class EmployerDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public EmployerDto Employer { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<CareerStep> CareerSteps { get; set; }

        public class CareerStep
        {
            public string Title { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public IEnumerable<AssignmentDto> Assignments { get; set; }
        }

        public class AssignmentDto
        {
            public string Description { get; set; }
            public int Id { get; set; }
            
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Summary { get; set; }
            public IEnumerable<string> Technologies { get; set; }
            public IEnumerable<string> Duties { get; set; }
            public LinkDto? Link { get; set; }
        }

        public class LinkDto
        {
            public string Address { get; set; }
            public string Caption { get; set; }
        }
    }
}