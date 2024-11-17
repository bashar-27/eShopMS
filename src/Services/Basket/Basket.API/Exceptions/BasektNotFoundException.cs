
namespace Basket.API.Exceptions
{
    public class BasektNotFoundException : NotFoundEx
    {
        public BasektNotFoundException(string userName) : base("Basket",userName)
        {
        }
    }
}
