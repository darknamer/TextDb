using AutoMapper;
using TextDb.Core.Models;
using TextDb.Core.ViewModels.Timesheets;

namespace TextDb.Configs.Profiles
{
    public class TimesheetProfile : Profile
    {
        public TimesheetProfile()
        {
            CreateMap<TimesheetInsertRequest, Timesheet>();
            CreateMap<Timesheet, TimesheetInsertResponse>();
            
            CreateMap<TimesheetUpdateRequest, Timesheet>();
            CreateMap<Timesheet, TimesheetUpdateResponse>();
        }
    }
}