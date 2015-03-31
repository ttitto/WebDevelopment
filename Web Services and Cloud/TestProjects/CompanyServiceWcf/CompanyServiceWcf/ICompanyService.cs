namespace CompanyServiceWcf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;

    [ServiceContract]
    public interface ICompanyService
    {

        [OperationContract]
        decimal GetEmployeeSalary(int employeeId);

        [OperationContract]
        int GetEmployeesCount();

        [OperationContract]
        EmployeeInfo GetEmployeeInfo(int employeeId);
    }
}
