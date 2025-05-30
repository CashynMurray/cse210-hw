using System.Collections.Generic;
using System.Text;

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double totalCost = 0;
        foreach (Product product in _products)
        {
            totalCost += product.GetTotalCost();
        }

        if (_customer.IsInUSA())
        {
            totalCost += 5;
        }
        else
        {
            totalCost += 35;
        }

        return totalCost;
    }

    public string GetPackingLabel()
    {
        StringBuilder packingLabel = new StringBuilder("Packing Label:\n");
        foreach (Product product in _products)
        {
            packingLabel.AppendLine(product.GetPackingLabelDetail());
        }
        return packingLabel.ToString();
    }

    public string GetShippingLabel()
    {
        StringBuilder shippingLabel = new StringBuilder("Shipping Label:\n");
        shippingLabel.AppendLine(_customer.GetName());
        shippingLabel.AppendLine(_customer.GetAddress().GetFullAddress());
        return shippingLabel.ToString();
    }
} 