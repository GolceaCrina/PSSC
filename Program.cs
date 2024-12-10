using PSSC_Proiect.Domain.Models;
using PSSC_Proiect.Domain.Services;

var cartService = new CartService();
bool running = true;

while (running)
{
    Console.WriteLine("\nShopping Cart Menu:");
    Console.WriteLine("1. Add product");
    Console.WriteLine("2. Remove product");
    Console.WriteLine("3. Display cart");
    Console.WriteLine("4. Calculate total");
    Console.WriteLine("5. Exit");
    Console.WriteLine("6. alegere cart");
    Console.Write("Choose an option: ");
    var choice = Console.ReadLine();
    
    

    switch (choice)
    {
        case "1":
            AddProductToCart(cartService);
            break;
        case "2":
            RemoveProductFromCart(cartService);
            break;
        case "3":
            cartService.DisplayCart();
            break;
        case "4":
            Console.WriteLine($"Total price: {cartService.CalculateTotal():C}");
            break;
        case "5":
            running = false;
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

static void AddProductToCart(CartService cartService)
{
    Console.Write("Enter product name: ");
    var name = Console.ReadLine();

    Console.Write("Enter product price: ");
    if (!decimal.TryParse(Console.ReadLine(), out var price))
    {
        Console.WriteLine("Invalid price.");
        return;
    }

    Console.Write("Enter quantity type (units/kilograms): ");
    var quantityType = Console.ReadLine()?.ToLower();

    IQuantity quantity = quantityType switch
    {
        "units" => new UnitQuantity(GetIntegerQuantity()),
        "kilograms" => new KilogramQuantity(GetDecimalQuantity()),
        _ => null
    };

    if (quantity == null)
    {
        Console.WriteLine("Invalid quantity type.");
        return;
    }

    cartService.AddItem(new Product(name, price), quantity);
    Console.WriteLine("Product added to cart.");
}

static void RemoveProductFromCart(CartService cartService)
{
    Console.Write("Enter product name to remove: ");
    var productName = Console.ReadLine();
    cartService.RemoveItem(productName);
    Console.WriteLine("Product removed from cart.");
}

static int GetIntegerQuantity()
{
    Console.Write("Enter quantity (units): ");
    return int.TryParse(Console.ReadLine(), out var units) ? units : 0;
}

static decimal GetDecimalQuantity()
{
    Console.Write("Enter quantity (kilograms): ");
    return decimal.TryParse(Console.ReadLine(), out var kilograms) ? kilograms : 0;
}
