using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextDb.Core.Models;
using TextDb.Core.ViewModels.Timesheets;
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
        private readonly IMapper _mapper;

        public TimesheetController(ITimesheetService timesheetService, IMapper mapper)
        {
            _timesheetService = timesheetService;
            _mapper = mapper;
        }

        [Route("GetAll")]
        [HttpGet]
        public IEnumerable<Timesheet> GetAll()
        {
            return _timesheetService.GetAll();
        }

        [Route("Insert")]
        [HttpPost]
        public TimesheetInsertResponse Insert([FromBody] TimesheetInsertRequest request)
        {
            var timesheet = _mapper.Map<Timesheet>(request);
            timesheet = _timesheetService.Insert(timesheet);
            return _mapper.Map<TimesheetInsertResponse>(timesheet);
        }
        
        [Route("Update")]
        [HttpPut]
        public TimesheetUpdateResponse Update([FromBody] TimesheetUpdateRequest request)
        {
            var timesheet = _mapper.Map<Timesheet>(request);
            timesheet = _timesheetService.Update(timesheet);
            return _mapper.Map<TimesheetUpdateResponse>(timesheet);
        }
        
        [Route("Delete/{id}")]
        [HttpDelete]
        public void Delete([FromRoute] int id)
        {
            var timesheet = _timesheetService.GetById(id);
            if (timesheet == null)
            {
                throw new Exception("Timesheet is not found.");
            }
            
            _timesheetService.Delete(timesheet);
        }
    }
}