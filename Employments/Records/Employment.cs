using System;
using System.Collections.Generic;
using BusinessCard.Employers.Records;
using BusinessCard.JobTitles.Records;
using BusinessCard.People.Records;

namespace BusinessCard.Employments.Records
{
    public class Employment
    {
        public int Id { get; set; }
        
        public virtual Person Person { get; set; }
        
        public int PersonId { get; set; }
        
        public virtual Employer Employer { get; set; }

        public virtual ICollection<JobTitle> JobTitles { get; set; } = new List<JobTitle>();
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        
        public EmploymentType Type { get; set; }

    }
}