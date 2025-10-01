using System;

namespace Behavioral
{
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(double amount) => Console.WriteLine($"Paid {amount} using Credit Card.");
    }

    public class PayPalPayment : IPaymentStrategy
    {
        public void Pay(double amount) => Console.WriteLine($"Paid {amount} using PayPal.");
    }

    public class ShoppingCart
    {
        private IPaymentStrategy paymentStrategy;
        public void SetPaymentStrategy(IPaymentStrategy strategy) => paymentStrategy = strategy;
        public void Checkout(double amount) => paymentStrategy.Pay(amount);
    }
}
