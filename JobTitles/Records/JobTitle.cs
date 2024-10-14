using System;
using BusinessCard.Employments.Records;

namespace BusinessCard.JobTitles.Records
{
    public class JobTitle
    {
        public int Id { get; set; }
        
        public virtual Employment Employment { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
    }
}