using System;

namespace batteryCheck
{
    internal static partial class Win32
    {

        [Flags]
        internal enum POWER_STATE : uint
        {
            BATTERY_POWER_ONLINE = 0x00000001,
            BATTERY_DISCHARGING = 0x00000002,
            BATTERY_CHARGING = 0x00000004,
            BATTERY_CRITICAL = 0x00000008
        }
    }

}