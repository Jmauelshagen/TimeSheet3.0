//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Timesheet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeSheet
    {
        public int Id { get; set; }
        public string WeekEnding { get; set; }
        public string Date { get; set; }
        public string TimeIn { get; set; }
        public string OutForLunch { get; set; }
        public string InFromLunch { get; set; }
        public string TimeOut { get; set; }
        public Nullable<int> LeaveId { get; set; }
        public string LeaveHours { get; set; }
        public string AdditionalHours { get; set; }
        public string TotalHoursWorked { get; set; }
        public string Submitted { get; set; }
        public string AuthorizedBySupervisor { get; set; }
        public Nullable<int> EmpId { get; set; }
<<<<<<< HEAD
        public IEnumerable<SelectListItem> WeekEndingDates { get; set; }
        public IEnumerable<SelectListItem> EmpNames { get; set; }
        public string Name { get; set; }


        //Constructors
        //no-args constructor
        public TimeSheet()
        {
            Id = 0;
            WeekEnding = "";
            Date = "";
            TimeIn = "";
            OutForLunch = "";
            InFromLunch = "";
            TimeOut = "";
            LeaveId = 0;
<<<<<<< HEAD
            LeaveHours = "";
            AdditionalHours = "";
=======
            LeaveHours = 0;
            AdditionalHours = 0;
>>>>>>> temp fixes
            TotalHoursWorked = "";
            Submitted = "False";
            AuthorizedBySupervisor = "False";
            EmpId = 0;
        }

        //all-args constructor
        public TimeSheet(int id, string wEnd, string date, string inT, string outL, string inL, string outT,
<<<<<<< HEAD
            int leaveId, string leaveHrs, string addlHrs, string tlHrs, string sub, string auth, int empId)
=======
            int leaveId, int leaveHrs, int addlHrs, string tlHrs, string sub, string auth, int empId)
>>>>>>> temp fixes
        {
            Id = id;
            WeekEnding = wEnd;
            Date = date;
            TimeIn = inT;
            OutForLunch = outL;
            InFromLunch = inL;
            TimeOut = outT;
            LeaveId = leaveId;
            LeaveHours = leaveHrs;
            AdditionalHours = addlHrs;
            TotalHoursWorked = tlHrs;
            Submitted = sub;
            AuthorizedBySupervisor = auth;
            EmpId = empId;
        }

        //Method to get list of Timesheet objects by employee id and week ending date
        //Queries the TimeSheet table by employee id and WeekEnding date and returns List collection of Timesheet objects
        public List<TimeSheet> GetTimeSheetByWeek(int empId, List<string> dates)
        {
            List<TimeSheet> timesheets = new List<TimeSheet>();
            string wEnd = dates[0].Trim();
            var sheets = from tsheets in db.TimeSheets
                         where tsheets.EmpId == empId && tsheets.WeekEnding == wEnd
                         orderby tsheets.Id ascending
                         select tsheets;
            var count = sheets.Count();
            Console.WriteLine("TimeSheet count is: " + count.ToString());
            if (count == 0)
            {
                for (int i = 1; i < 8; i++)
                {
                    TimeSheet sheet = new TimeSheet
                    {
                        Id = this.GetMaxTimeSheetId() + 1,
                        WeekEnding = dates[0].Trim(),
                        Date = dates[i].Trim(),
                        TimeIn = "00:00:00",
                        OutForLunch = "00:00:00",
                        InFromLunch = "00:00:00",
                        TimeOut = "0:00",
                        LeaveId = 0,
<<<<<<< HEAD
                        LeaveHours = "0:00",
                        AdditionalHours = "0:00",
=======
                        LeaveHours = 0,
                        AdditionalHours = 0,
>>>>>>> temp fixes
                        TotalHoursWorked = "0:00",
                        Submitted = "False",
                        AuthorizedBySupervisor = "False",
                        EmpId = empId
                    };
                    this.InsertTimeSheet(sheet);
                    timesheets.Add(sheet);
                }

            }
            else
            {
                foreach (TimeSheet sheet in sheets)
                {
                    timesheets.Add(sheet);
                }
            }
            return timesheets;
        }

        //Method to retrieve list of TimeSheet objects by employee name and week ending date
        public List<TimeSheet> GetTimeSheetByNameAndDate(string name, string wED)
        {
            Debug.WriteLine("Name value is: " + name);
            List<TimeSheet> timesheets = new List<TimeSheet>();
            string[] splitNames = name.Split(' ');
            string fName = splitNames[0].Trim();
            string lName = splitNames[1].Trim();
            //Find the employee id based on the name passed in to the method
            var empId = (from emps in db.Employees
                         where emps.FirstName == fName && emps.LastName == lName
                         select emps.EmpId).FirstOrDefault();
            //Select the TimeSheet objects based on the employee id and week ending date
            var sheets = from tsheets in db.TimeSheets
                         where tsheets.EmpId == empId && tsheets.WeekEnding == wED
                         orderby tsheets.Id ascending
                         select tsheets;

            foreach (TimeSheet sheet in sheets)
            {
                timesheets.Add(sheet);
            }

            return timesheets;
        }

        //Method to get the max id from the TimeSheet data table
        public int GetMaxTimeSheetId()
        {
            var ids = from tsheets in db.TimeSheets
                      orderby tsheets.Id descending
                      select tsheets.Id;
            int maxId = ids.FirstOrDefault();
            return maxId;
        }

        //Method to insert TimeSheet data into the TimeSheet data table
        public void InsertTimeSheet(TimeSheet sheet)
        {
            db.TimeSheets.Add(sheet);
            db.SaveChanges();
        }

        //Method to update TimeSheet data in the TimeSheet data table
        public void UpdateTimeSheet(TimeSheet sheet)
        {
            TimeSheet tsheet = (from tsheets in db.TimeSheets
                                where tsheets.Id == sheet.Id
                                select tsheets).First();

            tsheet.Id = sheet.Id;
            tsheet.WeekEnding = sheet.WeekEnding;
            tsheet.Date = sheet.Date;
            tsheet.TimeIn = sheet.TimeIn;
            tsheet.OutForLunch = sheet.OutForLunch;
            tsheet.InFromLunch = sheet.InFromLunch;
            tsheet.TimeOut = sheet.TimeOut;
            tsheet.LeaveId = sheet.LeaveId;
            tsheet.LeaveHours = sheet.LeaveHours;
            tsheet.AdditionalHours = sheet.AdditionalHours;
            tsheet.TotalHoursWorked =  tsheet.CalculateTotalHoursWorked(sheet);
            tsheet.Submitted = sheet.Submitted;
            tsheet.AuthorizedBySupervisor = sheet.AuthorizedBySupervisor;
            tsheet.EmpId = sheet.EmpId;

            db.SaveChanges();
        }

        //Method to calculate total hours worked
<<<<<<< HEAD
        public string CalculateTotalHoursWorked(TimeSheet sheet)
=======
        public String CalculateTotalHoursWorked(TimeSheet sheet)
>>>>>>> temp fixes
        {
            DateTime tIn = RoundToNearest(DateTime.Parse(sheet.TimeIn), TimeSpan.FromMinutes(15)); ;
            DateTime lOut = RoundToNearest(DateTime.Parse(sheet.OutForLunch), TimeSpan.FromMinutes(15));
            DateTime lIn = RoundToNearest(DateTime.Parse(sheet.InFromLunch), TimeSpan.FromMinutes(15));
            DateTime tOut = RoundToNearest(DateTime.Parse(sheet.TimeOut), TimeSpan.FromMinutes(15));

            TimeSpan hoursWorked = lOut.Subtract(tIn).Add(tOut.Subtract(lIn));
            int hour = Convert.ToInt16(hoursWorked.TotalHours);
            int minute = Convert.ToInt16(hoursWorked.TotalMinutes) - (hour * 60);


            string totalHours = hour.ToString() + ":" + minute.ToString();

            /*
            double hoursBeforeLunch = (lOut - tIn).TotalMilliseconds; //Calculate the number of hours worked before lunch in milliseconds
            double hoursAfterLunch = (tOut - lIn).TotalMilliseconds; //Calculate the number of hours worked after lunch in milliseconds
            double addlHours = ((double)sheet.AdditionalHours) * 3600000; //Convert additional hours value to milliseconds
            double leaveHours = ((double)sheet.LeaveHours) * 3600000; //Convert leave hours value uto milliseconds
            double totalHours = ((hoursBeforeLunch + hoursAfterLunch + addlHours) - (leaveHours)) / 3600000; //Do the arithmetic and convert from millis to hours
            /*
            TimeSpan hoursBeforeLunch = tIn.Subtract(lOut);
            TimeSpan hoursAfterLunch = lIn.Subtract(tOut);
            DateTime date4 = date4.Add(hoursBeforeLunch);
            double addlHours = ((double)sheet.AdditionalHours) * 3600000; //Convert additional hours value to milliseconds
            double leaveHours = ((double)sheet.LeaveHours) * 3600000; //Convert leave hours value uto milliseconds
            double totalHours = ((timeIn + timeOut + addlHours) - (leaveHours)) / 3600000;
            */
            /************************************/
<<<<<<< HEAD
            return totalHours;
=======
            return System.Convert.ToString(totalHours);
>>>>>>> temp fixes
        }

        //This method determines the current date and then derives the dates for each day of the week
        public List<string> GetDates()
        {
            List<string> dates = new List<string>();
            string endOfWeek = "";
            string sunDate = "";
            string monDate = "";
            string tueDate = "";
            string wedDate = "";
            string thrDate = "";
            string friDate = "";
            string satDate = "";
            int hoy = (int)DateTime.Now.DayOfWeek;

            switch (hoy)
            {
                case 0: //Sunday
                    {
                        endOfWeek = DateTime.Now.AddDays(6).ToShortDateString();
                        dates.Add(endOfWeek);
                        sunDate = DateTime.Now.ToShortDateString();
                        dates.Add(sunDate);
                        monDate = DateTime.Now.AddDays(1).ToShortDateString();
                        dates.Add(monDate);
                        tueDate = DateTime.Now.AddDays(2).ToShortDateString();
                        dates.Add(tueDate);
                        wedDate = DateTime.Now.AddDays(3).ToShortDateString();
                        dates.Add(wedDate);
                        thrDate = DateTime.Now.AddDays(4).ToShortDateString();
                        dates.Add(thrDate);
                        friDate = DateTime.Now.AddDays(5).ToShortDateString();
                        dates.Add(friDate);
                        satDate = DateTime.Now.AddDays(6).ToShortDateString();
                        dates.Add(satDate);

                        break;
                    }

                case 1: //Monday
                    {
                        endOfWeek = DateTime.Now.AddDays(5).ToShortDateString();
                        dates.Add(endOfWeek);
                        sunDate = DateTime.Now.AddDays(-1).ToShortDateString();
                        dates.Add(sunDate);
                        monDate = DateTime.Now.ToShortDateString();
                        dates.Add(monDate);
                        tueDate = DateTime.Now.AddDays(1).ToShortDateString();
                        dates.Add(tueDate);
                        wedDate = DateTime.Now.AddDays(2).ToShortDateString();
                        dates.Add(wedDate);
                        thrDate = DateTime.Now.AddDays(3).ToShortDateString();
                        dates.Add(thrDate);
                        friDate = DateTime.Now.AddDays(4).ToShortDateString();
                        dates.Add(friDate);
                        satDate = DateTime.Now.AddDays(5).ToShortDateString();
                        dates.Add(satDate);

                        break;
                    }

                case 2: //Tuesday
                    {
                        endOfWeek = DateTime.Now.AddDays(4).ToShortDateString();
                        dates.Add(endOfWeek);
                        sunDate = DateTime.Now.AddDays(-2).ToShortDateString();
                        dates.Add(sunDate);
                        monDate = DateTime.Now.AddDays(-1).ToShortDateString();
                        dates.Add(monDate);
                        tueDate = DateTime.Now.ToShortDateString();
                        dates.Add(tueDate);
                        wedDate = DateTime.Now.AddDays(1).ToShortDateString();
                        dates.Add(wedDate);
                        thrDate = DateTime.Now.AddDays(2).ToShortDateString();
                        dates.Add(thrDate);
                        friDate = DateTime.Now.AddDays(3).ToShortDateString();
                        dates.Add(friDate);
                        satDate = DateTime.Now.AddDays(4).ToShortDateString();
                        dates.Add(satDate);

                        break;
                    }

                case 3: //Wednesday
                    {
                        endOfWeek = DateTime.Now.AddDays(3).ToShortDateString();
                        dates.Add(endOfWeek);
                        sunDate = DateTime.Now.AddDays(-3).ToShortDateString();
                        dates.Add(sunDate);
                        monDate = DateTime.Now.AddDays(-2).ToShortDateString();
                        dates.Add(monDate);
                        tueDate = DateTime.Now.AddDays(-1).ToShortDateString();
                        dates.Add(tueDate);
                        wedDate = DateTime.Now.ToShortDateString();
                        dates.Add(wedDate);
                        thrDate = DateTime.Now.AddDays(1).ToShortDateString();
                        dates.Add(thrDate);
                        friDate = DateTime.Now.AddDays(2).ToShortDateString();
                        dates.Add(friDate);
                        satDate = DateTime.Now.AddDays(3).ToShortDateString();
                        dates.Add(satDate);

                        break;
                    }

                case 4: //Thursday
                    {
                        endOfWeek = DateTime.Now.AddDays(2).ToShortDateString();
                        dates.Add(endOfWeek);
                        sunDate = DateTime.Now.AddDays(-4).ToShortDateString();
                        dates.Add(sunDate);
                        monDate = DateTime.Now.AddDays(-3).ToShortDateString();
                        dates.Add(monDate);
                        tueDate = DateTime.Now.AddDays(-2).ToShortDateString();
                        dates.Add(tueDate);
                        wedDate = DateTime.Now.AddDays(-1).ToShortDateString();
                        dates.Add(wedDate);
                        thrDate = DateTime.Now.ToShortDateString();
                        dates.Add(thrDate);
                        friDate = DateTime.Now.AddDays(1).ToShortDateString();
                        dates.Add(friDate);
                        satDate = DateTime.Now.AddDays(2).ToShortDateString();
                        dates.Add(satDate);

                        break;
                    }

                case 5: //Friday
                    {
                        endOfWeek = DateTime.Now.AddDays(1).ToShortDateString();
                        dates.Add(endOfWeek);
                        sunDate = DateTime.Now.AddDays(-5).ToShortDateString();
                        dates.Add(sunDate);
                        monDate = DateTime.Now.AddDays(-4).ToShortDateString();
                        dates.Add(monDate);
                        tueDate = DateTime.Now.AddDays(-3).ToShortDateString();
                        dates.Add(tueDate);
                        wedDate = DateTime.Now.AddDays(-2).ToShortDateString();
                        dates.Add(wedDate);
                        thrDate = DateTime.Now.AddDays(-1).ToShortDateString();
                        dates.Add(thrDate);
                        friDate = DateTime.Now.ToShortDateString();
                        dates.Add(friDate);
                        satDate = DateTime.Now.AddDays(1).ToShortDateString();
                        dates.Add(satDate);

                        break;
                    }

                case 6: //Saturday
                    {
                        endOfWeek = DateTime.Now.ToShortDateString();
                        dates.Add(endOfWeek);
                        sunDate = DateTime.Now.AddDays(-6).ToShortDateString();
                        dates.Add(sunDate);
                        monDate = DateTime.Now.AddDays(-5).ToShortDateString();
                        dates.Add(monDate);
                        tueDate = DateTime.Now.AddDays(-4).ToShortDateString();
                        dates.Add(tueDate);
                        wedDate = DateTime.Now.AddDays(-3).ToShortDateString();
                        dates.Add(wedDate);
                        thrDate = DateTime.Now.AddDays(-2).ToShortDateString();
                        dates.Add(thrDate);
                        friDate = DateTime.Now.AddDays(-1).ToShortDateString();
                        dates.Add(friDate);
                        satDate = DateTime.Now.ToShortDateString();
                        dates.Add(satDate);

                        break;
                    }
            }
            return dates;
        }

        //Queries the TimeSheet table and obtains a list of distinct week ending dates that exist on the table
        public List<string> GetWeekEndingDateList()
        {
            var wED = (from sheets in db.TimeSheets
                       select sheets.WeekEnding).Distinct().OrderBy(WeekEnding => WeekEnding);

            List<string> weekEndDates = new List<string>();
            foreach (string date in wED)
            {
                weekEndDates.Add(date);
            }
            return weekEndDates;
        }

        //Obtains the fist and list names for a distinct list of employee ids that exist on the
        //TimeSheet db table
        public List<string> GetEmployeeNames()
        {
            List<string> names = new List<string>();
            var Id = (from sheets in db.TimeSheets
                      select sheets.EmpId).Distinct();
            foreach (int id in Id)
            {
                var fname = (from emps in db.Employees
                             where emps.EmpId == id
                             select emps.FirstName).FirstOrDefault();
                var lname = (from emps in db.Employees
                             where emps.EmpId == id
                             select emps.LastName).FirstOrDefault();
                string fullname = fname.Trim() + " " + lname.Trim();
                names.Add(fullname);

            }
            return names;

        }

        /* Method Rounds to the nearest 15 minutes and returns a DateTime variable */
        public DateTime RoundToNearest(DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            bool roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;

            return new DateTime(dt.Ticks + offset - delta, dt.Kind);
        }

=======
>>>>>>> place holder
    }
}
