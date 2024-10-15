using System;
using System.Collections.Generic;

namespace BusinessCard.Employments.Services
{
    public class Model
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmployerModel Employer { get; set; }
        public IEnumerable<CareerStepModel> CareerSteps { get; set; }


        public class EmployerModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class AssignmentModel
        {
            public IEnumerable<string> Duties { get; set; }
            public IEnumerable<string> Technologies { get; set; }
            public string Summary { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime StartDate { get; set; }
            public string Name { get; set; }
            public int Id { get; set; }
            public string Description { get; set; }
            public LinkModel? Link { get; set; }
        }

        public class CareerStepModel
        {
            public DateTime EndDate { get; set; }
            public DateTime StartDate { get; set; }
            public string Title { get; set; }
            public IEnumerable<AssignmentModel> Assignments { get; set; }
        }
        
        public class LinkModel
        {
            public string Address { get; set; }
            
            public string Caption { get; set; }
        }
    }
}