using System;
using FacetBuilder.Examples.Examples.BasicExample;

namespace FacetBuilder.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicExample();

            Console.ReadKey();
        }

        static void BasicExample()
        {
            var result = new BasicExample().Start();

            Console.WriteLine("EmployeeIds: ");
            foreach (var one in result.EmployeeId)
                Console.WriteLine($"Id: {one.Id}, Name: {one.Name}");
            
            Console.WriteLine("CompaniesNames: ");
            foreach (var one in result.CompanyName)
                Console.WriteLine($"Id: {one.Id}, Name: {one.Name}");
            
            Console.WriteLine("Cities: ");
            foreach (var one in result.City)
                Console.WriteLine($"Id: {one.Id}, Name: {one.Name}");
        }
    }
}