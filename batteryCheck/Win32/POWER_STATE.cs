using System;

namespace batteryCheck
{
    [Flags]
    public enum POWER_STATE
    {
        BATTERY_POWER_ONLINE = 0x00000001,
        BATTERY_DISCHARGING = 0x00000002,
        BATTERY_CHARGING = 0x00000004,
        BATTERY_CRITICAL = 0x00000008
    }
}

