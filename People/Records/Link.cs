namespace BusinessCard.People.Records
{
    public class Link
    {
        public int Id { get; set; }
        public LinkType Type { get; set; }
        
        public string Value { get; set; }
        
        public int Ordinal { get; set; }
        public virtual Person Person { get; set; }
    }

    public enum LinkType
    {
        Email = 0, 
        LinkedIn = 1, 
        MobilePhone = 2,
        GitHub
    }
}