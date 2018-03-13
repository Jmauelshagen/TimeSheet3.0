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
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Mvc;

    public partial class TimeSheet
    {
        //Instance Variables
        LoginDatabaseEntities1 db = new LoginDatabaseEntities1();

        //Class properties
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
        public IEnumerable<SelectListItem> WeekEndingDates { get; set; }
        public IEnumerable<SelectListItem> EmpNames { get; set; }
        public string Note { get; set; }
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
            LeaveHours = "";
            AdditionalHours = "";
            TotalHoursWorked = "";
            Submitted = "No";
            AuthorizedBySupervisor = "False";
            EmpId = 0;
            Note = "";
        }

        //all-args constructor
        public TimeSheet(int id, string wEnd, string date, string inT, string outL, string inL, string outT,
            int leaveId, string leaveHrs, string addlHrs, string tlHrs, string sub, string auth, int empId, string n)
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
            Note = n;
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
            Debug.WriteLine("TimeSheet count is: " + count.ToString());
            if (count == 0)
            {
                for (int i = 1; i < 8; i++)
                {
                    TimeSheet sheet = new TimeSheet
                    {
                        Id = this.GetMaxTimeSheetId() + 1,
                        WeekEnding = dates[0].Trim(),
                        Date = dates[i].Trim(),
                        TimeIn = "0:00",
                        OutForLunch = "0:00",
                        InFromLunch = "0:00",
                        TimeOut = "0:00",
                        LeaveId = 0,
                        LeaveHours = "0:00",
                        AdditionalHours = "0:00",
                        TotalHoursWorked = "0:00",
                        Submitted = "No",
                        AuthorizedBySupervisor = "False",
                        EmpId = empId,
                        Note = ""
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

        public List<TimeSheet> GetApprovedTimesheets(string emp)
        {
            List<TimeSheet> timesheets = new List<TimeSheet>();
            int iemp = Convert.ToInt32(emp);
            Debug.WriteLine("Iemp value is : " + iemp + " ]");
            var sheets = from tsheets in db.TimeSheets
                         where tsheets.AuthorizedBySupervisor.Equals("True") && tsheets.EmpId == iemp
                         group tsheets by tsheets.WeekEnding into weekgroup
                         orderby weekgroup.Key ascending
                         select weekgroup;
            foreach (var weekgroup in sheets)
            {
                Debug.WriteLine("Key is: [0]" + weekgroup.Key + "}}");
                foreach (TimeSheet sheet in weekgroup)
                {
                    
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
            Debug.WriteLine("in database save 1");
            Debug.WriteLine("******************************************************************************************************** "+sheet.LeaveId);

            string timeIn = "";
            string outForLunch = "";
            string inFromLunch = "";
            string timeOut = "";

            if (!String.IsNullOrEmpty(sheet.TimeIn) && !sheet.TimeIn.ToString().Trim().Equals("0:00"))
            {
                timeIn = sheet.TimeIn;
            }
            else { timeIn = "0:00"; }
            if (!String.IsNullOrEmpty(sheet.OutForLunch) && !sheet.OutForLunch.ToString().Trim().Equals("0:00"))
            {
                outForLunch = sheet.OutForLunch;
            }
            else { outForLunch = "0:00"; }
            if (!String.IsNullOrEmpty(sheet.InFromLunch) && !sheet.InFromLunch.ToString().Trim().Equals("0:00"))
            {
                inFromLunch = sheet.InFromLunch;
            }
            else { inFromLunch = "0:00"; }
            if (!String.IsNullOrEmpty(sheet.TimeOut) && !sheet.TimeOut.ToString().Trim().Equals("0:00"))
            {
                timeOut = sheet.TimeOut;
            }
            else { timeOut = "0:00"; }

            TimeSheet tsheet = (from tsheets in db.TimeSheets
                                where tsheets.Id == sheet.Id
                                select tsheets).Single();

            tsheet.Id = sheet.Id;
            tsheet.WeekEnding = sheet.WeekEnding;
            tsheet.Date = sheet.Date;
            tsheet.TimeIn = timeIn;
            tsheet.OutForLunch = outForLunch;
            tsheet.InFromLunch = inFromLunch;
            tsheet.TimeOut = timeOut;
            tsheet.LeaveId = sheet.LeaveId;
            tsheet.LeaveHours = sheet.LeaveHours;
            tsheet.AdditionalHours = sheet.AdditionalHours;
            tsheet.TotalHoursWorked = tsheet.CalculateTotalHoursWorked(sheet);
            tsheet.Submitted = sheet.Submitted;
            tsheet.AuthorizedBySupervisor = sheet.AuthorizedBySupervisor;
            tsheet.EmpId = sheet.EmpId;
            tsheet.Note = sheet.Note;

            db.SaveChanges();
        }

        /**Method to calculate total hours worked daily by taking the time in/out
         * and the lunch time out and returned from lunch then rounds to nearest 15th minute mark
         * **/
        public string CalculateTotalHoursWorked(TimeSheet sheet)
        {
            try
            {
                if (!sheet.TimeIn.ToString().Trim().Equals("0:00") && !sheet.OutForLunch.ToString().Trim().Equals("0:00") && sheet.InFromLunch.ToString().Trim().Equals("0:00") && sheet.TimeOut.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("Calculating the first 2 punches");
                    DateTime tIn = RoundToNearest(DateTime.Parse(sheet.TimeIn), TimeSpan.FromMinutes(15)); ;
                    DateTime lOut = RoundToNearest(DateTime.Parse(sheet.OutForLunch), TimeSpan.FromMinutes(15));
                    //used to view the incoming values
                    Debug.WriteLine("Clocked in at " + tIn + " in 2 Punches");
                    Debug.WriteLine("Clocked out for lunch at " + lOut + " in 2 Punches");
                    string totalHours;
                    if (tIn > lOut)
                    {
                        totalHours = "Error";
                    }
                    else
                    {
                        /*Once the verification it the LeaveHours and AdditionalHours are added we can unblock the following code!*/
                        string leaveTime = sheet.LeaveHours.ToString().Trim();
                        int leaveHour = Convert.ToInt16(leaveTime.Split(':')[0]);
                        int leaveMinute = Convert.ToInt16(leaveTime.Split(':')[1]);

                        string AdditionalHours = sheet.AdditionalHours.ToString().Trim();
                        int addHour = Convert.ToInt16(AdditionalHours.Split(':')[0]);
                        int addMinute = Convert.ToInt16(AdditionalHours.Split(':')[1]);
                        

                        TimeSpan hoursWorked = lOut.Subtract(tIn);
                        int hour = Convert.ToInt16(Math.Truncate(hoursWorked.TotalHours + leaveHour + addHour)); // + leaveHour + addHour;
                        int minute = Convert.ToInt16(hoursWorked.Minutes + leaveMinute + addMinute); // + leaveMinute + addMinute;
                        totalHours = hour.ToString() + ":" + minute.ToString();
                        Debug.WriteLine(hoursWorked + "************* " + hoursWorked.TotalHours + "************************* " + hour + " ************* " + minute + " leave time " + leaveHour + " add time " + AdditionalHours);                        
                    }
                    Debug.WriteLine("TotalHours from the first 2 punches :"+ totalHours);
                    return totalHours;
                }

                else if (!String.IsNullOrEmpty(sheet.TimeIn.ToString().Trim()) && !sheet.TimeIn.ToString().Trim().Equals("0:00") && !String.IsNullOrEmpty(sheet.OutForLunch.ToString().Trim()) && !sheet.OutForLunch.ToString().Trim().Equals("0:00") && !String.IsNullOrEmpty(sheet.InFromLunch.ToString().Trim()) && !sheet.InFromLunch.ToString().Trim().Equals("0:00") && !String.IsNullOrEmpty(sheet.TimeOut.ToString().Trim()) && !sheet.TimeOut.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("Calculating all times");
                    DateTime tIn = RoundToNearest(DateTime.Parse(sheet.TimeIn), TimeSpan.FromMinutes(15)); ;
                    DateTime lOut = RoundToNearest(DateTime.Parse(sheet.OutForLunch), TimeSpan.FromMinutes(15));
                    DateTime lIn = RoundToNearest(DateTime.Parse(sheet.InFromLunch), TimeSpan.FromMinutes(15));
                    DateTime tOut = RoundToNearest(DateTime.Parse(sheet.TimeOut), TimeSpan.FromMinutes(15));
                    //used to view the incoming values
                    Debug.WriteLine("Clocked in at " + tIn + " in 4 Punches");
                    Debug.WriteLine("Clocked out for lunch at " + lOut + " in 4 Punches");
                    Debug.WriteLine("Clocked in from lunch at " + lIn + " in 4 Punches");
                    Debug.WriteLine("Clocked out at " + tOut + " in 4 Punches");
                    string totalHours;
                    if (tIn > lOut || lOut > lIn || lIn > tOut)
                    {
                        totalHours = "Error";
                    }
                    else
                    {
                        /*Once the verification it the LeaveHours and AdditionalHours are added we can unblock the following code!*/ 
                        string leaveTime = sheet.LeaveHours.ToString().Trim();
                        int leaveHour = Convert.ToInt16(leaveTime.Split(':')[0]);
                        int leaveMinute = Convert.ToInt16(leaveTime.Split(':')[1]);

                        string AdditionalHours = sheet.AdditionalHours.ToString().Trim();
                        int addHour = Convert.ToInt16(AdditionalHours.Split(':')[0]);
                        int addMinute = Convert.ToInt16(AdditionalHours.Split(':')[1]);
                        
                        TimeSpan hoursWorked = tOut.Subtract(tIn).Subtract(lIn.Subtract(lOut));
                        int hour = Convert.ToInt16(Math.Truncate(hoursWorked.TotalHours + leaveHour + addHour)); // + leaveHour + addHour;
                        int minute = Convert.ToInt16(hoursWorked.Minutes + leaveMinute + addMinute); // + leaveMinute + addMinute;
                        Debug.WriteLine(hoursWorked + "************* " +hoursWorked.TotalHours + "************************* " + hour + " ************* " + minute + " leave time " + leaveTime + " add time " + AdditionalHours);
                        totalHours = hour.ToString() + ":" + minute.ToString();
                    }
                    return totalHours;
                }

                else if (!String.IsNullOrEmpty(sheet.AdditionalHours.ToString().Trim()) && !sheet.AdditionalHours.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("if only additional hours are worked..");
                    string totalHours;
                    totalHours = sheet.AdditionalHours.ToString().Trim();
                    return totalHours;
                }

                else if (!String.IsNullOrEmpty(sheet.LeaveHours.ToString().Trim()) && !sheet.LeaveHours.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("if only leave hours are worked..");
                    string totalHours;
                    totalHours = sheet.LeaveHours.ToString().Trim();
                    return totalHours;
                }

                else if (sheet.TimeIn.ToString().Trim().Equals("0:00") && sheet.OutForLunch.ToString().Trim().Equals("0:00") && sheet.InFromLunch.ToString().Trim().Equals("0:00") && sheet.TimeOut.ToString().Trim().Equals("0:00") && sheet.AdditionalHours.ToString().Trim().Equals("0:00") && sheet.LeaveHours.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("Skipping over empty day not filled out yet.");
                    string totalHours;
                    totalHours = "NoTime";
                    return totalHours;
                }

                else
                {
                    Debug.WriteLine("Sending 'Missing Out' hours for the day because punches are missing. only gets called for 1 punch and 3 punches");
                    string totalHours;
                    totalHours = "Missing Out";
                    return totalHours;
                }

            }
            catch (ArgumentException ae)
            {
                Debug.WriteLine(ae);
                return "";
            }
        }

        public string CalculateWorkedHours(TimeSheet sheet)
        {
            try
            {
                if (!sheet.TimeIn.ToString().Trim().Equals("0:00") && !sheet.OutForLunch.ToString().Trim().Equals("0:00") && sheet.InFromLunch.ToString().Trim().Equals("0:00") && sheet.TimeOut.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("Calculating the first 2 punches");
                    DateTime tIn = RoundToNearest(DateTime.Parse(sheet.TimeIn), TimeSpan.FromMinutes(15)); ;
                    DateTime lOut = RoundToNearest(DateTime.Parse(sheet.OutForLunch), TimeSpan.FromMinutes(15));
                    //used to view the incoming values
                    Debug.WriteLine("Clocked in at " + tIn + " in 2 Punches");
                    Debug.WriteLine("Clocked out for lunch at " + lOut + " in 2 Punches");
                    string totalHours;
                    if (tIn > lOut)
                    {
                        totalHours = "Error";
                    }
                    else
                    {
                        /*Once the verification it the LeaveHours and AdditionalHours are added we can unblock the following code!*/                     

                        string AdditionalHours = sheet.AdditionalHours.ToString().Trim();
                        int addHour = Convert.ToInt16(AdditionalHours.Split(':')[0]);
                        int addMinute = Convert.ToInt16(AdditionalHours.Split(':')[1]);


                        TimeSpan hoursWorked = lOut.Subtract(tIn);
                        int hour = Convert.ToInt16(Math.Truncate(hoursWorked.TotalHours + addHour)); // + leaveHour + addHour;
                        int minute = Convert.ToInt16(hoursWorked.Minutes + addMinute); // + leaveMinute + addMinute;
                        totalHours = hour.ToString() + ":" + minute.ToString();
                        Debug.WriteLine(hoursWorked + "************* " + hoursWorked.TotalHours + "************************* " + hour + " ************* " + minute + " add time " + AdditionalHours);
                    }
                    Debug.WriteLine("TotalHours from the first 2 punches :" + totalHours);
                    return totalHours;
                }

                else if (!String.IsNullOrEmpty(sheet.TimeIn.ToString().Trim()) && !sheet.TimeIn.ToString().Trim().Equals("0:00") && !String.IsNullOrEmpty(sheet.OutForLunch.ToString().Trim()) && !sheet.OutForLunch.ToString().Trim().Equals("0:00") && !String.IsNullOrEmpty(sheet.InFromLunch.ToString().Trim()) && !sheet.InFromLunch.ToString().Trim().Equals("0:00") && !String.IsNullOrEmpty(sheet.TimeOut.ToString().Trim()) && !sheet.TimeOut.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("Calculating all times");
                    DateTime tIn = RoundToNearest(DateTime.Parse(sheet.TimeIn), TimeSpan.FromMinutes(15)); ;
                    DateTime lOut = RoundToNearest(DateTime.Parse(sheet.OutForLunch), TimeSpan.FromMinutes(15));
                    DateTime lIn = RoundToNearest(DateTime.Parse(sheet.InFromLunch), TimeSpan.FromMinutes(15));
                    DateTime tOut = RoundToNearest(DateTime.Parse(sheet.TimeOut), TimeSpan.FromMinutes(15));
                    //used to view the incoming values
                    Debug.WriteLine("Clocked in at " + tIn + " in 4 Punches");
                    Debug.WriteLine("Clocked out for lunch at " + lOut + " in 4 Punches");
                    Debug.WriteLine("Clocked in from lunch at " + lIn + " in 4 Punches");
                    Debug.WriteLine("Clocked out at " + tOut + " in 4 Punches");
                    string totalHours;
                    if (tIn > lOut || lOut > lIn || lIn > tOut)
                    {
                        totalHours = "Error";
                    }
                    else
                    {
                        /*Once the verification it the LeaveHours and AdditionalHours are added we can unblock the following code!*/           

                        string AdditionalHours = sheet.AdditionalHours.ToString().Trim();
                        int addHour = Convert.ToInt16(AdditionalHours.Split(':')[0]);
                        int addMinute = Convert.ToInt16(AdditionalHours.Split(':')[1]);

                        TimeSpan hoursWorked = tOut.Subtract(tIn).Subtract(lIn.Subtract(lOut));
                        int hour = Convert.ToInt16(Math.Truncate(hoursWorked.TotalHours + addHour)); // + leaveHour + addHour;
                        int minute = Convert.ToInt16(hoursWorked.Minutes + addMinute); // + leaveMinute + addMinute;
                        Debug.WriteLine(hoursWorked + "************* " + hoursWorked.TotalHours + "************************* " + hour + " ************* " + minute + " add time " + AdditionalHours);
                        totalHours = hour.ToString() + ":" + minute.ToString();
                    }
                    return totalHours;
                }

                else if (sheet.TimeIn.ToString().Trim().Equals("0:00") && sheet.OutForLunch.ToString().Trim().Equals("0:00") && sheet.InFromLunch.ToString().Trim().Equals("0:00") && sheet.TimeOut.ToString().Trim().Equals("0:00") && sheet.AdditionalHours.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("Skipping over empty day not filled out yet.");
                    string totalHours;
                    totalHours = "NoTime";
                    return totalHours;
                }

                else if (!String.IsNullOrEmpty(sheet.AdditionalHours.ToString().Trim()) && !sheet.AdditionalHours.ToString().Trim().Equals("0:00"))
                {
                    Debug.WriteLine("if only additional hours are worked..");
                    string totalHours;
                    totalHours = sheet.AdditionalHours.ToString().Trim();
                    return totalHours;
                }

                else
                {
                    Debug.WriteLine("Sending 'Missing Out' hours for the day because punches are missing. only gets called for 1 punch and 3 punches");
                    string totalHours;
                    totalHours = "Missing Out";
                    return totalHours;
                }

            }
            catch (ArgumentException ae)
            {
                Debug.WriteLine(ae);
                return "";
            }
        }

        /**This method determines the current date and then derives the dates for each day of the week **/
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
        /** Method Rounds to the nearest 15 minutes and returns a DateTime variable **/
        public DateTime RoundToNearest(DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            bool roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;
            return new DateTime(dt.Ticks + offset - delta, dt.Kind);
        }
    }
}


