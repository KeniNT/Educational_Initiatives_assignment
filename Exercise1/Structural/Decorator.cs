using System;

namespace Structural
{
    public interface ICoffee
    {
        string GetDescription();
        double GetCost();
    }

    public class SimpleCoffee : ICoffee
    {
        public string GetDescription() => "Simple Coffee";
        public double GetCost() => 5.0;
    }

    public class MilkDecorator : ICoffee
    {
        private ICoffee coffee;
        public MilkDecorator(ICoffee c) { coffee = c; }
        public string GetDescription() => coffee.GetDescription() + ", Milk";
        public double GetCost() => coffee.GetCost() + 1.5;
    }

    public class SugarDecorator : ICoffee
    {
        private ICoffee coffee;
        public SugarDecorator(ICoffee c) { coffee = c; }
        public string GetDescription() => coffee.GetDescription() + ", Sugar";
        public double GetCost() => coffee.GetCost() + 0.5;
    }
}

