namespace CompanyServiceWcf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    [DataContract]
    public class EmployeeInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string JobTitle { get; set; }

        [DataMember]
        public DateTime HireDate { get; set; }
    }
}