using System;
using batteryCheck.Win32;

namespace batteryCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            // see: https://gist.github.com/ahawker/9715872
            BatteryInformation cap = BatteryInfo.GetBatteryInformation();

            Console.WriteLine("cap PowerState {0}", cap.PowerState);
            Console.WriteLine("cap discharge rate {0}", cap.DischargeRate);
            Console.WriteLine("cap voltage {0}", cap.Voltage);
            Console.WriteLine("cap FullChargeCapacity {0}", cap.FullChargeCapacity);
            Console.WriteLine("cap DesignedMaxCapacity {0}", cap.DesignedMaxCapacity);
            Console.WriteLine("cap CurrentCapacity {0}", cap.CurrentCapacity);

            // see: https://msdn.microsoft.com/en-us/library/windows/desktop/aa372693(v=vs.85).aspx

            SYSTEM_POWER_STATUS sps = new SYSTEM_POWER_STATUS();

            Api.Kernel.GetSystemPowerStatus(sps);

            Console.WriteLine("AC Line status {0}", sps.ACLineStatus);
            Console.WriteLine("Battery flag {0}", sps.BatteryFlag);
            Console.WriteLine("Battery Life Percent {0}", sps.BatteryLifePercent);
        }
    }
}
