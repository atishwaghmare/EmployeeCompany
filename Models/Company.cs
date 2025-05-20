using System.Text.Json.Serialization;

namespace EmployeeCompany.Models
{
    public class Company
    {
        public int CompanyId {  get; set; }

        public string CompanyName { get; set; }

        //[JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
