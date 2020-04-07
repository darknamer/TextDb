using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextDb.Core.Models;
using TextDb.Data.Services.TimesheetServices;

namespace TextDb.Controllers.Apis
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [Route("GetAll")]
        [HttpGet]
        public IEnumerable<Timesheet> GetAll()
        {
            return _timesheetService.GetAll();
        }
    }
}