using System.Collections.Generic;
using BusinessCard.Employments.Records;

namespace BusinessCard.Technologies.Records
{
    public class Technology
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}