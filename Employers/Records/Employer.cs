using System.Collections.Generic;
using BusinessCard.Employments.Records;

namespace BusinessCard.Employers.Records
{
    public class Employer
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public virtual ICollection<Employment> Employments { get; set; } = new List<Employment>();
    }
}