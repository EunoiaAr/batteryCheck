using System.Runtime.InteropServices;

namespace batteryCheck.Win32
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct BATTERY_STATUS
    {
        public POWER_STATE PowerState;
        public uint Capacity;
        public uint Voltage;
        public int Rate;
    }
}