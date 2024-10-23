using System;
using System.Collections.Generic;
using BusinessCard.Technologies.Records;

namespace BusinessCard.Employments.Records
{
    public class Assignment
    {
        public int Id { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Employment Employment { get; set; }

        public virtual ICollection<Duty> Duties { get; set; } = new List<Duty>();
        public string Description { get; set; }
        public string Summary { get; set; }
        public virtual ICollection<Technology> Technologies { get; set; } = new List<Technology>();

        public virtual AssignmentLink? Link { get; set; }
    }

    public class AssignmentLink
    {
        public string Address { get; set; }

        public string Caption { get; set; }
    }
}