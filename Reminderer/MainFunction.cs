using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PostmarkDotNet;
using System.Globalization;

namespace Reminderer
{
    public class MainFunction(ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<MainFunction>();

        [Function(nameof(MainFunction))]
        //public async Task Run([TimerTrigger("0 */1 * * * *" // every minute
        public async Task Run([TimerTrigger("0 0 0 * * *" // at 00:00:00 daily
#if DEBUG
            , RunOnStartup=true
#endif
            )] TimerObject timerObject)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {timerObject.ScheduleStatus?.Next}");

            var now = DateTime.UtcNow;

            // tableconvert.com/excel-to-csv
            var allExpiringItemsAsCsv = """
                "US DL (JP)","4/19/2027","150"
                "US DL (Tracy)","11/8/2026","150"
                "UK DL (JP)","4/18/2034","30"
                "UK DL (Tracy)","9/17/2034","30"
                "US Passport (JP)","4/19/2028","270"
                "US Passport (Tracy)","4/19/2028","270"
                "UK Passport (JP)","11/24/2032","180"
                "UK Passport (Tracy)","11/23/2032","180"
                "Global Entry (JP)","4/19/2032","364"
                "Global Entry (Tracy)","11/8/2031","264"
                "UK EES (JP)","12/31/2099","0"
                "UK EES (Tracy)","12/31/2099","0"
                "UK ETIAS (JP)","12/31/2099","120"
                "UK ETIAS (Tracy)","12/31/2099","120"
                "Canada eTA (JP)","5/15/2031","90"
                "Canada eTA (Tracy)","5/15/2031","90"
                "GHIC (JP)","8/9/2031","270"
                "GHIC (Tracy)","8/9/2031","270"
                "Two Together Railcard","7/20/2027","30"
                "Senior Railcard","4/25/2027","30"
                "AARP","11/1/2027","30"
            """;
            // "Kitten Railcard","10/22/2026","30"

            var allExpiringItems = allExpiringItemsAsCsv.Replace("\"", string.Empty).Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(x => {
                    var parts = x.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    var name = parts[0];
                    var expirationDate = DateTime.ParseExact(parts[1], "M/d/yyyy", CultureInfo.InvariantCulture);
                    var daysInAdvance = int.Parse(parts[2]);
                    return new
                    {
                        Name = name,
                        ExpirationDate = expirationDate,
                        DaysInAdvance = daysInAdvance,
                        ReminderDate = expirationDate.AddDays(-1 * daysInAdvance)
                    };
                });

            var currentExpiringItems = allExpiringItems.Where(x => (x.ExpirationDate.Date - now.Date).Days == x.DaysInAdvance);

            if (currentExpiringItems.Any()) 
            {
                try
                {
                    var postmarkServerToken = configuration["PostmarkServerToken"];
                    var postmarkClient = new PostmarkClient(postmarkServerToken);

                    var postmarkMessage = new PostmarkMessage
                    {
                        From = configuration["MessageFrom"], // must be verified in Postmark
                        To = configuration["MessageTo"],
                        Subject = $"Reminders generated on {now:D}",
                        HtmlBody = string.Join("<br/>", currentExpiringItems.Select(x => $"{x.Name} expires on {x.ExpirationDate:D}")),
                        //TextBody = "<...text version of email message body...>",
                        TrackOpens = false,
                    };

                    var postmarkResponse = await postmarkClient.SendMessageAsync(postmarkMessage);

                    if (postmarkResponse.Status == PostmarkStatus.Success)
                    {
                        _logger.LogInformation($"Email sent successfully. Message ID: {postmarkResponse.MessageID}");
                    }
                    else
                    {
                        _logger.LogError($"Postmark rejected request: {postmarkResponse.Message}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Internal Error sending email: {ex}");
                }
            }

        }
    }

    public class TimerObject
    {
        public TimerScheduleStatus? ScheduleStatus { get; set; }
        public bool IsPastDue { get; set; }
    }

    public class TimerScheduleStatus
    {
        public DateTime Last { get; set; }
        public DateTime Next { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
