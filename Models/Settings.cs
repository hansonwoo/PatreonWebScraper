namespace PatreonWebScraper.Models
{
    public class Settings
    {
        public string TargetUrl { get; set; } = "";
        public string LoginCookie { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool HeadlessMode { get; set; } = false;
        public bool Overwrite { get; set; } = false;
        public string OutputPath { get; set; } = "";
        public int Delay { get; set; } = 100;
    }
}
