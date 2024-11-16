namespace BuildingBlocks.Exceptions
{
    public class NotFoundEx : Exception
    {
        public NotFoundEx(string message) : base(message) { }
        public NotFoundEx(string name, object value) : base($"Entity \"{name}\"({value}) was not found")
        {
        }
    }
}
