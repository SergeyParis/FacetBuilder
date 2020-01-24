using System.Collections.Generic;

namespace FacetBuilder.Examples.Examples.BasicExample.Models
{
    public class ResultFacet
    {
        public IEnumerable<FacetWithIdName> EmployeeId { get; set; }

        public IEnumerable<FacetWithIdName> City { get; set; }

        public IEnumerable<FacetWithIdName> CompanyName { get; set; }
    }
}