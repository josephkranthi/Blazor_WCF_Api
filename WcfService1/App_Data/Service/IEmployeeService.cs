using System.Collections.Generic;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WcfService1
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        List<Employee> GetEmployees();

        [OperationContract]
        Employee GetEmployeeById(int id);

        [OperationContract]
        int AddEmployee(Employee employee);

        [OperationContract]
        bool UpdateEmployee(Employee employee);

        [OperationContract]
        bool DeleteEmployee(int id);
    }

    [DataContract]
    public class Employee
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Salary { get; set; } 


        // add additional properties as needed
    }
}
