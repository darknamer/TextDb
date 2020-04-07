namespace TextDb.Core.Models
{
    public class Timesheet
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public float Hours { get; set; }
        public string CreatedDate { get; set; }
    }
}