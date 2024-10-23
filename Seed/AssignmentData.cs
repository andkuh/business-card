using System;
using System.Collections.Generic;

namespace BusinessCard.Seed
{
    public class AssignmentData
    {
        public DateTime StartDate { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public AssignmentLinkData? Link { get; set; }
        public string Summary { get; set; }
        public string Role { get; set; }
        public IEnumerable<TechnologyData> Technologies { get; set; }
        public IEnumerable<string> Duties { get; set; }
    }
}