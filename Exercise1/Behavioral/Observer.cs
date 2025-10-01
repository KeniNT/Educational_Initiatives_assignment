using System;
using System.Collections.Generic;

namespace Behavioral
{
    public interface IObserver
    {
        void Update(float temperature);
    }

    public class WeatherStation
    {
        private List<IObserver> observers = new();
        private float temperature;

        public void AddObserver(IObserver obs) => observers.Add(obs);
        public void RemoveObserver(IObserver obs) => observers.Remove(obs);

        public void SetTemperature(float temp)
        {
            temperature = temp;
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            foreach (var obs in observers)
                obs.Update(temperature);
        }
    }

    public class MobileApp : IObserver
    {
        private string name;
        public MobileApp(string n) { name = n; }
        public void Update(float temperature)
        {
            Console.WriteLine($"{name} got weather update: {temperature}°C");
        }
    }
}
