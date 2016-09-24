using System.Runtime.InteropServices;

namespace batteryCheck.Win32
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct BATTERY_WAIT_STATUS
    {
        public uint BatteryTag;
        public uint Timeout;
        public POWER_STATE PowerState;
        public uint LowCapacity;
        public uint HighCapacity;
    }
}

