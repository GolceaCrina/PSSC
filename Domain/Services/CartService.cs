using PSSC_Proiect.Domain.Models;

namespace PSSC_Proiect.Domain.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();

    public void AddItem(Product product, IQuantity quantity)
    {
        _items.Add(new CartItem(product, quantity));
    }

    public void RemoveItem(string productName)
    {
        var item = _items.FirstOrDefault(i => i.Product.Name == productName);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public decimal CalculateTotal()
    {
        return _items.Sum(item =>
            item.Quantity switch
            {
                UnitQuantity u => u.GetValue() * item.Product.Price,
                KilogramQuantity k => k.GetValue() * item.Product.Price,
                _ => 0
            });
    }

    public void DisplayCart()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine("The shopping cart is empty.");
            return;
        }

        Console.WriteLine("Shopping Cart:");
        foreach (var item in _items)
        {
            Console.WriteLine($"- {item.Product.Name}, Quantity: {item.Quantity.GetValue()}, Price: {item.Product.Price:C}");
        }
        Console.WriteLine($"Total: {CalculateTotal():C}");
    }
}