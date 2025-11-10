namespace MyDictionary.Domain.Interfaces
{
    public interface IAuditable
    {
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
