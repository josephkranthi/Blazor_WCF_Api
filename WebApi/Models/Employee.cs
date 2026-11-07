using System.Runtime.Serialization;

namespace WebApi.Models
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WcfService1")]
    public class Employee
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Salary { get; set; }
    }
}
