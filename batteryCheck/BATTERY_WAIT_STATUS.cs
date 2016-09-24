using System.Runtime.InteropServices;

namespace batteryCheck
{
    internal static partial class Win32
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

}