using System;

namespace Creational
{
    public interface IShape
    {
        void Draw();
    }

    public class Circle : IShape
    {
        public void Draw() => Console.WriteLine("Drawing Circle");
    }

    public class Rectangle : IShape
    {
        public void Draw() => Console.WriteLine("Drawing Rectangle");
    }

    public class ShapeFactory
    {
        public static IShape GetShape(string type)
        {
            return type switch
            {
                "circle" => new Circle(),
                "rectangle" => new Rectangle(),
                _ => throw new ArgumentException("Invalid shape type")
            };
        }
    }
}
