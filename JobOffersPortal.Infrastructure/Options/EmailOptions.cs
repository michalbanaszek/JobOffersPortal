namespace Infrastructure.Options
{
    public class EmailOptions
    {
        public string SmtpUsername { get; set; }    
        public string SmtpPassword { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
    }
}
