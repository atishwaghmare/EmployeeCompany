namespace EmployeeCompany.Models.Dtos
{
    public class AddEmployeeDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public int CompanyId { get; set; }
    }
}
