namespace FacetBuilder.Examples.Examples.BasicExample.Models
{
    public class FacetDataModel
    {
        public Employee Employee { get; set; }

        public Company Company { get; set; }
    }

    public class Employee
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string CompanyId { get; set; }
    }

    public class Company
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string City { get; set; }

        public string EmployeeId { get; set; }
    }
}