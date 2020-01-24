using System.Collections.Generic;
using FacetBuilder.Enums;
using FacetBuilder.Examples.Examples.BasicExample.Models;
using FacetBuilder.Extensions;

namespace FacetBuilder.Examples.Examples.BasicExample
{
    public class BasicExample
    {
        public ResultFacet Start()
        {
            var builder = new FacetBuilder<ResultFacet, FacetDataModel, Filter, FacetWithIdName>();

            builder.AddRule(FilterType.Equal)
                .FilterBy(x => x.EmployeeId)
                .FilterWhat(x => x.Employee.Id)
                .To(x => x.EmployeeId)
                .As(x => new FacetWithIdName { Id = x.Employee.Id, Name = x.Employee.Name })
                .AsFilter(x => x.Id != null);

            builder.AddRule(FilterType.Equal)
                .FilterBy(x => x.CompanyName)
                .FilterWhat(x => x.Company.Name)
                .To(x => x.CompanyName)
                .As(x => new FacetWithIdName { Id = x.Company.Name, Name = x.Company.Name })
                .AsFilter(x => x.Id != null);

            builder.AddRule(FilterType.Equal)
                .FilterBy(x => x.City)
                .FilterWhat(x => x.Company.City)
                .To(x => x.City)
                .As(x => new FacetWithIdName { Id = x.Company.City, Name = x.Company.City })
                .AsFilter(x => x.Id != null);

            return builder.Compile(GetData(), GetFilter());
        }

        private IEnumerable<FacetDataModel> GetData()
        {
            return new[]
            {
                new FacetDataModel
                {
                    Employee = new Employee
                    {
                        Id = "1001",
                        Name = "E_Name1",
                        CompanyId = "2001"
                    },
                    Company = new Company
                    {
                        Id = "2001",
                        Name = "C_Name1",
                        City = "City1",
                        EmployeeId = "1001"
                    }
                },
                new FacetDataModel
                {
                    Employee = new Employee
                    {
                        Id = "1002",
                        Name = "E_Name2",
                        CompanyId = "2002"
                    },
                    Company = new Company
                    {
                        Id = "2002",
                        Name = "C_Name2",
                        City = "City2",
                        EmployeeId = "1002"
                    }
                },
                new FacetDataModel
                {
                    Employee = new Employee
                    {
                        Id = "1003",
                        Name = "E_Name3",
                        CompanyId = "2003"
                    },
                    Company = new Company
                    {
                        Id = "2003",
                        Name = "C_Name3",
                        City = "City3",
                        EmployeeId = "1003"
                    }
                }
            };
        }

        private Filter GetFilter()
        {
            return new Filter
            {
                EmployeeId = "1001"
            };
        }
    }
}