
namespace SocialSite.Models
{
    public class Error
    {
        public string Message { get; set; } = "";

        public Error(string message)
        {
            this.Message = message;

        }
    }
}