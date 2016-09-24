using System.Runtime.InteropServices;

namespace batteryCheck
{
    internal static partial class Win32
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct BATTERY_INFORMATION
        {
            public int Capabilities;
            public byte Technology;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Reserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Chemistry;

            public int DesignedCapacity;
            public int FullChargedCapacity;
            public int DefaultAlert1;
            public int DefaultAlert2;
            public int CriticalBias;
            public int CycleCount;
        }
    }

}