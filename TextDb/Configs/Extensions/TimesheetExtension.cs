using TextDb.Core.Models;

namespace TextDb.Configs.Extensions
{
    public static class TimesheetExtension
    {
        public static string ToText(this Timesheet timesheet)
        {
            return string.Join('|', new string []
            {
                timesheet.Id.ToString(), 
                timesheet.ProjectId,
                timesheet.Username,
                timesheet.Description,
                timesheet.Hours.ToString(),
                timesheet.CreatedDate,
            });
        }
        
        public static Timesheet ToTimesheet(this string text)
        {
            var splitWords = text.Split('|');
            return new Timesheet
            {
                Id = int.Parse(splitWords.Length >= 1 ? splitWords[0] : "0"),
                ProjectId = splitWords.Length >= 2 ? splitWords[1] : "",
                Username = splitWords.Length >= 3 ? splitWords[2] : "",
                Description = splitWords.Length >= 4 ? splitWords[3] : "",
                Hours = float.Parse(splitWords.Length >= 5 ? splitWords[4] : "0"),
                CreatedDate = splitWords.Length >= 6 ? splitWords[5] : ""
            };
        }
        
    }
}