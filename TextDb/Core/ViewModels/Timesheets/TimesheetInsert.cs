using System.ComponentModel.DataAnnotations;

namespace TextDb.Core.ViewModels.Timesheets
{
    public class TimesheetInsert
    {
    }

    public class TimesheetInsertRequest : TimesheetInsert
    {
        [Required, StringLength(255)]
        public string ProjectId { get; set; }
        [Required, StringLength(255)]
        public string Username { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
        [Required, Range(0, 24)]
        public float Hours { get; set; }
    }

    public class TimesheetInsertResponse : TimesheetInsert
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public float Hours { get; set; }
        public string CreatedDate { get; set; }
    }
}