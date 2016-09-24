using System.Runtime.InteropServices;

namespace batteryCheck
{
    internal static partial class Win32
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct BATTERY_STATUS
        {
            public PowerState PowerState;
            public uint Capacity;
            public uint Voltage;
            public int Rate;
        }
    }

}