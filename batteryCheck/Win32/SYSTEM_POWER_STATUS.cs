using System;
using System.Runtime.InteropServices;

namespace batteryCheck.Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class SYSTEM_POWER_STATUS
    {
        public ACLineStatus ACLineStatus;
        public BatteryFlag  BatteryFlag;
        public Byte         BatteryLifePercent;
        public Byte         SystemStatusFlag;
        public uint         BatteryLifeTime;
        public uint         BatteryFullLifeTime;
    }
}
