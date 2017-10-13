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
    using System.Linq;

    public partial class Employee
    {
        //Instance variables
        LoginDatabaseEntities2 db = new LoginDatabaseEntities2();

        //Class properties
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Department { get; set; }
        public int Supervisor { get; set; }

        //Constructors
        //0-arg constructor
        public Employee()
        {
            EmpId = 0;
            FirstName = "";
            LastName = "";
            Email = "";
            RoleId = 0;
            Department = "";
            Supervisor = 0;
        }

        //all-arg constructor
        public Employee(int id, string fName, string lName, string email, int role, string dept, int super)
        {
            EmpId = id;
            FirstName = fName;
            LastName = lName;
            Email = email;
            RoleId = role;
            Department = dept;
            Supervisor = super;
        }

        //Method to obtain employee data from the database
        //Method accepts the employee id as a paramter and returns an Employee object
        public Employee GetEmployee(int id)
        {
            var e = from employees in db.Employees
                    where employees.EmpId == id
                    select employees;
            Employee emp = (Employee)e.FirstOrDefault();
            return emp;
        }

        //Method is to obtain employee data for multiple employees
        //Method accepts a supervisor id as a parameter and returns a list of Employee objects
        public List<Employee> GetEmployees(int id)
        {
            List<Employee> empList = new List<Employee>();

            var emps = from employees in db.Employees
                       where employees.Supervisor == id
                       select employees;

            foreach (Employee e in emps)
            {
                empList.Add(e);
            }

            return empList;
        }

    }
}
