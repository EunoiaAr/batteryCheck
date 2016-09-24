using System;
using System.IO;
using System.Runtime.InteropServices;

namespace batteryCheck
{
    internal static partial class Win32
    {
        internal static readonly Guid GUID_DEVCLASS_BATTERY = new Guid(0x72631E54, 0x78A4, 0x11D0, 0xBC, 0xF7, 0x00, 0xAA, 0x00, 0xB7, 0xB3, 0x2A);
        internal const uint IOCTL_BATTERY_QUERY_TAG = (0x00000029 << 16) | ((int)FileAccess.Read << 14) | (0x10 << 2) | (0);
        internal const uint IOCTL_BATTERY_QUERY_INFORMATION = (0x00000029 << 16) | ((int)FileAccess.Read << 14) | (0x11 << 2) | (0);
        internal const uint IOCTL_BATTERY_QUERY_STATUS = (0x00000029 << 16) | ((int)FileAccess.Read << 14) | (0x13 << 2) | (0);

        internal const long ERROR_INSUFFICIENT_BUFFER = 122;


        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetupDiGetClassDevs(
            ref Guid guid,
            [MarshalAs(UnmanagedType.LPTStr)] string enumerator,
            IntPtr hwnd,
            DEVICE_GET_CLASS_FLAGS flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetupDiEnumDeviceInterfaces(
            IntPtr hdevInfo,
            IntPtr devInfo,
            ref Guid guid,
            uint memberIndex,
            ref SP_DEVICE_INTERFACE_DATA devInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr hdevInfo,
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
            ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
            uint deviceInterfaceDetailDataSize,
            out uint requiredSize,
            IntPtr deviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr hdevInfo,
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
            IntPtr deviceInterfaceDetailData,
            uint deviceInterfaceDetailDataSize,
            out uint requiredSize,
            IntPtr deviceInfoData);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateFile(
            string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess desiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare shareMode,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FILE_ATTRIBUTES flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool DeviceIoControl(
            IntPtr handle,
            uint controlCode,
            [In] IntPtr inBuffer,
            uint inBufferSize,
            [Out] IntPtr outBuffer,
            uint outBufferSize,
            out uint bytesReturned,
            IntPtr overlapped);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool DeviceIoControl(
            IntPtr handle,
            uint controlCode,
            ref uint inBuffer,
            uint inBufferSize,
            ref uint outBuffer,
            uint outBufferSize,
            out uint bytesReturned,
            IntPtr overlapped);

    }

}