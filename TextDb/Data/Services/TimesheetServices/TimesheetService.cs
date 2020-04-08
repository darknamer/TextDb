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
        IEnumerable<Timesheet> GetAll();
        Timesheet GetById(int id);
        Timesheet Insert(Timesheet timesheet);
        Timesheet Update(Timesheet timesheet);
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

        #region private methods

        private string[] ReadAllLines()
        {
            string[] texts;
            lock (LockPermission)
            {
                texts = File.ReadAllLines(_path);
            }

            return texts;
        }

        private int NewId()
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

        #endregion
        
        public IEnumerable<Timesheet> GetAll()
        {
            var texts = ReadAllLines();
            var timesheets = texts
                .Select(x => x.ToTimesheet())
                .OrderBy(x => x.Id)
                .ToList();
            return timesheets;
        }

        public Timesheet GetById(int id)
        {
            return GetAll()
                .Where(x => x.Id == id)
                .OrderBy(x => x.Id)
                .FirstOrDefault();
        }

        public Timesheet Insert(Timesheet timesheet)
        {
            var timesheets = GetAll()
                .OrderBy(x => x.Id)
                .ToList();
            
            timesheet.Id = NewId();
            timesheet.CreatedDate = DateTime.Now.ToString(TimesheetExtension.Format);
            timesheets.Add(timesheet);

            var texts = timesheets.Select(x => x.ToText()).ToList();
            
            lock (LockPermission)
            {
                File.WriteAllLines(_path, texts);
            }

            return timesheet;
        }

        public Timesheet Update(Timesheet timesheet)
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