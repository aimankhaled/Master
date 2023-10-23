namespace POS.Server.Domain
{
    public class RepositertyReturn<T>
    {
        public int Count { get; set; }

        public T Data { get; set; }
    }
}
