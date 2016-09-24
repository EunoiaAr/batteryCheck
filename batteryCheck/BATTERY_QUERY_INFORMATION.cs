using System.Runtime.InteropServices;

namespace batteryCheck
{
    internal static partial class Win32
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct BATTERY_QUERY_INFORMATION
        {
            public uint BatteryTag;
            public BATTERY_QUERY_INFORMATION_LEVEL InformationLevel;
            public int AtRate;
        }
    }

}