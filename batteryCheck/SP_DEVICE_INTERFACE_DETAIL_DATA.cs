using System.Runtime.InteropServices;

namespace batteryCheck
{
    internal static partial class Win32
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int CbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }
    }

}