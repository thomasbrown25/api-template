

namespace template_api.Models
{
    public class LoggingDataExchange
    {
        public int Id { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now.ToLocalTime();
        public string? MessageSource { get; set; }
        public string? MessageTarget { get; set; }
        public string? MethodCall { get; set; }
        public string? MessagePayload { get; set; }
    }
}