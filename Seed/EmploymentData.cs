using System;
using System.Collections.Generic;

namespace BusinessCard.Seed
{
    public class EmploymentData
    {
        public DateTime StartDate { get; set; }
        public EmployerData Employer { get; set; }
        public IEnumerable<AssignmentData> Assignments { get; set; }
        public IEnumerable<JobTitleData> JobTitles { get; set; }
        public DateTime? EndDate { get; set; }
    }
}