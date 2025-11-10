namespace MyDictionary.Application.Common.Exceptions
{
    public class NotFoundExceptions : Exception
    {
        public NotFoundExceptions(string name, object key)
            : base($"Entity: '{name}' by ({key}) not found") { }
    }
}
