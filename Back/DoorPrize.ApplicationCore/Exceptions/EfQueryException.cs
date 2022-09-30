namespace DoorPrize.ApplicationCore.Exceptions
{
    public class EfQueryException : Exception
    {
        public string Query { get; set; }

        public EfQueryException(Exception exception, string query) : base(exception.Message, exception) =>
            Query = query;
    }
}