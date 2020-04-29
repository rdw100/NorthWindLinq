using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using NorthWindLinq.Models;

namespace NorthWindLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NORTHWIND LINQ EXAMPLES!");

            // Create a DB context
            using var ctx = new NorthwindContext();

            // Eager Load of Multiple Related Entities with the Include() Method
            // LINQ lambda expression as a parameter in the Include method
            // Fill the Customer List with all Orders and Details
            IList<Customers> Customer = ctx.Customers
                        .Include(customer => customer.Orders)
                        .ThenInclude(order => order.OrderDetails)
                        .ThenInclude(orderdetails => orderdetails.Product)
                        .ToList();

            // Output to Console
            Console.WriteLine("CUSTOMERS");
            ConsoleTable.From(Customer.Where(c => c.CustomerId=="FRANK")).Write();

            Console.WriteLine("\nORDERS");
            ConsoleTable.From(Customer.SelectMany(x => x.Orders).Where(o => o.OrderId == 10267)).Write();

            Console.WriteLine("\nORDER DETAILS");
            ConsoleTable.From(Customer.SelectMany(x => x.Orders.SelectMany(y => y.OrderDetails).Where(od => od.OrderId == 10267))).Write();

            Console.WriteLine("\nPRODUCTS");
            ConsoleTable.From(Customer.SelectMany(x => x.Orders.SelectMany(y => y.OrderDetails.Where(od => od.OrderId == 10267).Select(z => z.Product)))).Write();

            Console.ReadKey();
        }
    }
}
