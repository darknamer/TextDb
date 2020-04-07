using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextDb.Configs.Extensions;
using TextDb.Core.Models;

namespace TextDb.Data.Services.TimesheetServices
{
    public interface ITimesheetService
    {
        string[] ReadAllLines();
        IEnumerable<Timesheet> GetAll();
        int NewId();
        Timesheet Insert(Timesheet timesheet);
        Timesheet Modify(Timesheet timesheet);
        void Delete(Timesheet timesheet);
    }

    public class TimesheetService : ITimesheetService
    {
        private static readonly object LockPermission = new object();
        private readonly string _path;

        public TimesheetService()
        {
            _path = Path.Combine(AppContext.BaseDirectory, "Db.txt");
        }

        public string[] ReadAllLines()
        {
            string[] texts;
            lock (LockPermission)
            {
                texts = File.ReadAllLines(_path);
            }

            return texts;
        }

        public IEnumerable<Timesheet> GetAll()
        {
            var texts = ReadAllLines();
            var timesheets = texts
                .Select(x => x.ToTimesheet())
                .OrderBy(x => x.Id)
                .ToList();
            return timesheets;
        }

        public int NewId()
        {
            var texts = ReadAllLines();
            if (texts.Length <= 0)
            {
                return 1;
            }
            var timesheet = texts
                .Select(x => x.ToTimesheet())
                .OrderByDescending(x => x.Id)
                .First();
            return timesheet.Id + 1;
        }

        public Timesheet Insert(Timesheet timesheet)
        {
            var newId = NewId();
            timesheet.Id = newId;

            var timesheets = GetAll()
                .OrderBy(x => x.Id)
                .Select(X => X.ToText())
                .ToList();
            
            lock (LockPermission)
            {
                File.WriteAllLines(_path, timesheets);
            }

            return timesheet;
        }

        public Timesheet Modify(Timesheet timesheet)
        {
            var timesheets = GetAll().ToList();
            if (timesheets.All(x => x.Id != timesheet.Id))
            {
                throw new Exception("Timesheet is not found.");
            }

            var index = timesheets.FindIndex(x => x.Id == timesheet.Id);
            timesheets[index].ProjectId = timesheet.ProjectId;
            timesheets[index].Username = timesheet.Username;
            timesheets[index].Description = timesheet.Description;
            timesheets[index].Hours = timesheet.Hours;
            timesheets[index].CreatedDate = timesheet.CreatedDate;

            var texts = timesheets
                .OrderBy(x => x.Id)
                .Select(x => x.ToText())
                .ToList();
            lock (LockPermission)
            {
                File.WriteAllLines(_path, texts);
            }

            return timesheet;
        }

        public void Delete(Timesheet timesheet)
        {
            var timesheets = GetAll().ToList();
            if (timesheets.All(x => x.Id != timesheet.Id))
            {
                throw new Exception("Timesheet is not found.");
            }

            var index = timesheets.FindIndex(x => x.Id == timesheet.Id);
            timesheets.RemoveAt(index);

            var texts = timesheets
                .OrderBy(x => x.Id)
                .Select(x => x.ToText())
                .ToList();
            lock (LockPermission)
            {
                File.WriteAllLines(_path, texts);
            }
        }
    }
}