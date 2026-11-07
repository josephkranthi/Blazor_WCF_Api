using System.Runtime.Serialization;

namespace BlazorApp1.Models
{
    [DataContract]
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
