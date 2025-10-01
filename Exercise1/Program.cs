using System;
using Behavioral;
using Creational;
using Structural;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Observer Pattern Demo ===");
        var station = new WeatherStation();
        var app1 = new MobileApp("App1");
        var app2 = new MobileApp("App2");
        station.AddObserver(app1);
        station.AddObserver(app2);
        station.SetTemperature(30);

        Console.WriteLine("\n=== Strategy Pattern Demo ===");
        var cart = new ShoppingCart();
        cart.SetPaymentStrategy(new CreditCardPayment());
        cart.Checkout(100);
        cart.SetPaymentStrategy(new PayPalPayment());
        cart.Checkout(200);

        Console.WriteLine("\n=== Singleton Pattern Demo ===");
        var logger = Logger.GetInstance();
        logger.Log("Singleton working!");

        Console.WriteLine("\n=== Factory Method Demo ===");
        var shape1 = ShapeFactory.GetShape("circle");
        shape1.Draw();
        var shape2 = ShapeFactory.GetShape("rectangle");
        shape2.Draw();

        Console.WriteLine("\n=== Adapter Pattern Demo ===");
        var twoPin = new TwoPinDevice();
        IThreePinPlug adapter = new PlugAdapter(twoPin);
        adapter.Connect();

        Console.WriteLine("\n=== Decorator Pattern Demo ===");
        ICoffee coffee = new SimpleCoffee();
        coffee = new MilkDecorator(coffee);
        coffee = new SugarDecorator(coffee);
        Console.WriteLine($"{coffee.GetDescription()} costs {coffee.GetCost()}");
    }
}
