using System.ComponentModel.DataAnnotations;

namespace TextDb.Core.ViewModels.Timesheets
{
    public class TimesheetUpdate
    {
    }

    public class TimesheetUpdateRequest : TimesheetUpdate
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string ProjectId { get; set; }
        [Required, StringLength(255)]
        public string Username { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
        [Required, Range(0, 24)]
        public float Hours { get; set; }
        [Required]
        public string CreatedDate { get; set; }
    }

    public class TimesheetUpdateResponse : TimesheetUpdate
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public float Hours { get; set; }
        public string CreatedDate { get; set; }
    }
}