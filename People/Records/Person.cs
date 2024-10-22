using System.Collections.Generic;
using BusinessCard.Employments.Records;

namespace BusinessCard.People.Records
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int YearsOld { get; set; }

        public string Location { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<Employment> Employments { get; set; } = new List<Employment>();
        public string Specialization { get; set; }
        
        public virtual PersonImage Image { get; set; }
        public virtual ICollection<EducationStep> EducationSteps { get; set; }
        public virtual ICollection<Hobby> Hobbies { get; set; }

        public virtual ICollection<Link> Links { get; set; } = new List<Link>();
    }
}