﻿using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using NorthWindLinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthWindLinq
{
    class Program
    {

        static void Main()
        {
            Console.WriteLine("NORTHWIND LINQ EXAMPLES!");

            // Create a DB context
            using var ctx = new NorthwindContext();
            
            JoinComprehension(ctx);
            JoinLambda(ctx);
            GroupByComprehension(ctx);
            GroupByLambda(ctx);

            Console.ReadKey();
        }

        private static void JoinLambda(NorthwindContext ctx)
        {
            // LINQ-to-Entity query using LINQ method syntax to 
            // eager load multiple related entities with the Include() method
            // with LINQ lambda expression as a parameter in the Include method
            // to Fill the Customer List with all Orders and Details.
            IList<Customers> Customer = ctx.Customers
                                           .Include(customer => customer.Orders)
                                           .ThenInclude(order => order.OrderDetails)
                                           .ThenInclude(orderdetails => orderdetails.Product)
                                           .ToList();

            // Document the query sent to the database
            var queryString = ctx.Customers.ToQueryString();
            Console.WriteLine(queryString);

            // Output to Console
            Console.WriteLine("CUSTOMERS");
            ConsoleTable.From(Customer.Where(c => c.CustomerId == "FRANK")).Write();
            
            Console.WriteLine("\nORDERS");
            ConsoleTable.From(Customer.SelectMany(x => x.Orders).Where(o => o.OrderId == 10267)).Write();

            Console.WriteLine("\nORDER DETAILS");
            ConsoleTable.From(Customer.SelectMany(x => x.Orders.SelectMany(y => y.OrderDetails).Where(od => od.OrderId == 10267))).Write();

            Console.WriteLine("\nPRODUCTS");
            ConsoleTable.From(Customer.SelectMany(x => x.Orders.SelectMany(y => y.OrderDetails.Where(od => od.OrderId == 10267).Select(z => z.Product)))).Write();
        }

        private static void JoinComprehension(NorthwindContext ctx)
        {
            // Nested LINQ-to-Entity query
            var customerOrderQuerySql = (from c in ctx.Customers
                                         join o in ctx.Orders
                                           on c.CustomerId equals o.CustomerId
                                         join od in ctx.OrderDetails
                                           on o.OrderId equals od.OrderId
                                         join p in ctx.Products
                                           on od.ProductId equals p.ProductId
                                         where c.CustomerId == "FRANK"
                                         select new
                                         {
                                             c.CustomerId,
                                             o.OrderId,
                                             p.ProductName,
                                             od.UnitPrice,
                                             od.Quantity,
                                             OrderDate = DateTime.Parse(o.OrderDate.ToString()).ToString("yyyy-MM-dd")
                                         });

            // Nested LINQ-to-Entity query using LINQ comprehension query syntax to return an
            // anonymous object list
            var customerOrderQuery = customerOrderQuerySql.ToList();

            // Document the query sent to the database
            var queryString = customerOrderQuerySql.ToQueryString();
            Console.WriteLine(queryString);

            ConsoleTable.From(customerOrderQuery).Write();
        }

        private static void GroupByComprehension(NorthwindContext ctx)
        {
            IList<Customers> Customer = ctx.Customers
                               .Include(customer => customer.Orders)
                               .ThenInclude(order => order.OrderDetails)
                               .ThenInclude(orderdetails => orderdetails.Product)
                               .ToList();

            // Comprehension Query Syntax
            IEnumerable<IGrouping<string, Customers>> groupQuery =
                from c in Customer
                group c by c.City;

            Console.WriteLine("\n-------------------------------------------------");

            foreach (IGrouping<string, Customers> cityGroup in groupQuery)
            {
                Console.WriteLine("\n{0}: ", cityGroup.Key);
                foreach (Customers c in cityGroup)
                {
                    Console.Write("- {0} {1} ", c.CustomerId, c.CompanyName);
                }
            }
        }

        private static void GroupByLambda(NorthwindContext ctx)
        {

            IList<Customers> Customer = ctx.Customers
                               .Include(customer => customer.Orders)
                               .ThenInclude(order => order.OrderDetails)
                               .ThenInclude(orderdetails => orderdetails.Product)
                               .ToList();

            // Lambda Expression Syntax
            IEnumerable<IGrouping<string, Customers>> groupLambdaQuery =
                Customer.GroupBy(c => c.City);

            Console.WriteLine("\n-------------------------------------------------");

            foreach (IGrouping<string, Customers> cityGroup in groupLambdaQuery)
            {
                Console.WriteLine("\n{0}: ", cityGroup.Key);
                foreach (Customers c in cityGroup)
                {
                    Console.Write("- {0} {1} ", c.CustomerId, c.CompanyName);
                }
            }
        }
    }
}