using System.Text.Json.Serialization;

namespace EmployeeCompany.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public double Salary { get; set; }

        public int CompanyId { get; set; }

        //[JsonIgnore]
        public virtual Company Company { get; set; }
    }
}
