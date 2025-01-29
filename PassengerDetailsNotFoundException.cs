
namespace AirlinesLibrary
{
    [Serializable]
    public class PassengerDetailsNotFoundException : Exception
    {
        public PassengerDetailsNotFoundException()
        {
        }

        public PassengerDetailsNotFoundException(string? message) : base(message)
        {
        }

        public PassengerDetailsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}