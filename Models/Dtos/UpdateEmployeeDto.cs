namespace EmployeeCompany.Models.Dtos
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public int CompanyId { get; set; }
    }
}
