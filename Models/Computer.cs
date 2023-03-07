
namespace SocialSite.Models
{
public class Computer 
    {
        public int ComputerId { get; set; }
        public string Motherboard { get; set; } = "";
        public int CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = "";

        public Computer() {
            Motherboard = "";
            CPUCores = 0;
            HasWifi = false;
            HasLTE = false;
            ReleaseDate = DateTime.Now;
            Price = 0;
            VideoCard =  "";
        }

    }
}