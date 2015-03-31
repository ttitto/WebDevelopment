namespace CompanyServiceWcf
{
    using System.Linq;
    using CompanyServiceWcf.Models;

    public class CompanyService : ICompanyService
    {
        public decimal GetEmployeeSalary(int employeeId)
        {
            var dbContext = new SoftUniEntities();

            decimal salary = dbContext.Employees
                .Where(e => e.EmployeeID == employeeId)
                .Select(e => e.Salary)
                .FirstOrDefault();

            return salary;
        }

        public int GetEmployeesCount()
        {
            var dbContext = new SoftUniEntities();
            int employeesCount = dbContext.Employees.Count();
            return employeesCount;
        }

        public EmployeeInfo GetEmployeeInfo(int employeeId)
        {
            var dbContext = new SoftUniEntities();

            return dbContext.Employees
                .Where(e => e.EmployeeID == employeeId)
                .Select(e => new EmployeeInfo
                {
                    HireDate = e.HireDate,
                    JobTitle = e.JobTitle,
                    Name = e.MiddleName == null ?
                    string.Format("{0} {2}", e.FirstName, e.LastName) :
                    string.Format("{0} {1} {2}", e.FirstName, e.MiddleName, e.LastName)
                })
                .FirstOrDefault();
        }
    }
}
