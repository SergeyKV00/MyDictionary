namespace MyDictionary.Application.Common
{
    public class ListModel<T>
    {
        public IList<T> Data { get; set; }
        public int Total { get; set; } = 0;
    }
}
