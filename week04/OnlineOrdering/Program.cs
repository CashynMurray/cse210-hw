using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
        Address address2 = new Address("456 Oak Ave", "Othercity", "ON", "Canada");

        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        Product product1 = new Product("Laptop", "P101", 1200.50, 1);
        Product product2 = new Product("Mouse", "A205", 25.00, 2);
        Product product3 = new Product("Keyboard", "A206", 75.00, 1);
        Product product4 = new Product("Monitor", "P102", 300.00, 1);
        Product product5 = new Product("Webcam", "A207", 50.00, 3);

        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Order order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);

        List<Order> orders = new List<Order> { order1, order2 };

        foreach (Order order in orders)
        {
            Console.WriteLine("==============================");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine("------------------------------");
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Total Cost: ${order.CalculateTotalCost():F2}");
            Console.WriteLine("==============================");
            Console.WriteLine();
        }
    }
}