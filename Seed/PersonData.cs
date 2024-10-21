using System.Collections.Generic;

namespace BusinessCard.Seed
{
    public class PersonData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public int YearsOld { get; set; }
        public string Location { get; set; }
        public PersonImageData Image { get; set; }
        public string Summary { get; set; }
        public IEnumerable<EmploymentData> Employments { get; set; }
        public IEnumerable<EducationStepData> EducationSteps { get; set; }
        public IEnumerable<string> Hobbies { get; set; }
    }
}