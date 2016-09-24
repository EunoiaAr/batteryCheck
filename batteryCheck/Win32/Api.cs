using System;
using System.IO;
using System.Runtime.InteropServices;

namespace batteryCheck.Win32
{
    public static partial class Api
    {
        public static readonly Guid GUID_DEVCLASS_BATTERY = new Guid(0x72631E54, 0x78A4, 0x11D0, 0xBC, 0xF7, 0x00, 0xAA, 0x00, 0xB7, 0xB3, 0x2A);
        public const uint IOCTL_BATTERY_QUERY_TAG = (0x00000029 << 16) | ((int)FileAccess.Read << 14) | (0x10 << 2) | (0);
        public const uint IOCTL_BATTERY_QUERY_INFORMATION = (0x00000029 << 16) | ((int)FileAccess.Read << 14) | (0x11 << 2) | (0);
        public const uint IOCTL_BATTERY_QUERY_STATUS = (0x00000029 << 16) | ((int)FileAccess.Read << 14) | (0x13 << 2) | (0);

        public const long ERROR_INSUFFICIENT_BUFFER = 122;
    }
}
