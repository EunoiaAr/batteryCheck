using System;

namespace batteryCheck
{
    class Program
    {
        // see: https://gist.github.com/ahawker/9715872

        static void Main(string[] args)
        {
            BatteryInformation cap = BatteryInfo.GetBatteryInformation();

            Console.WriteLine("cap PowerState {0}", cap.PowerState);
            Console.WriteLine("cap discharge rate {0}", cap.DischargeRate);
            Console.WriteLine("cap voltage {0}", cap.Voltage);
            Console.WriteLine("cap FullChargeCapacity {0}", cap.FullChargeCapacity);
            Console.WriteLine("cap DesignedMaxCapacity {0}", cap.DesignedMaxCapacity);
            Console.WriteLine("cap CurrentCapacity {0}", cap.CurrentCapacity);
        }
    }
}
