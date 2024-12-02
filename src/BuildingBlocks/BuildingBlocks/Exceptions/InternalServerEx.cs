namespace BuildingBlocks.Exceptions
{
    public class InternalServerEx : Exception
    {
        public string? Details { get;}
        public InternalServerEx(string message) :base(message){ }
    
        public InternalServerEx(string message , string details): base(message) {

            Details = details;   
        }
    }
}
