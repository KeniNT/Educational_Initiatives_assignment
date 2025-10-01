using System;

namespace Structural
{
    // Target interface
    public interface IThreePinPlug
    {
        void Connect();
    }

    // Adaptee
    public class TwoPinDevice
    {
        public void InsertTwoPin() => Console.WriteLine("Two-pin device connected.");
    }

    // Adapter
    public class PlugAdapter : IThreePinPlug
    {
        private TwoPinDevice device;
        public PlugAdapter(TwoPinDevice d) { device = d; }
        public void Connect() => device.InsertTwoPin();
    }
}

