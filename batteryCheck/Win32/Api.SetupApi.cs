using System;
using System.IO;
using System.Runtime.InteropServices;

namespace batteryCheck.Win32
{
    public static partial class Api
    {
        public static class SetupApi
        {
            [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SetupDiGetClassDevs(
				ref Guid guid,
				[MarshalAs(UnmanagedType.LPTStr)] string enumerator,
				IntPtr hwnd,
				DEVICE_GET_CLASS_FLAGS flags);

            [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

            [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetupDiEnumDeviceInterfaces(
                IntPtr hdevInfo,
                IntPtr devInfo,
                ref Guid guid,
                uint memberIndex,
                ref SP_DEVICE_INTERFACE_DATA devInterfaceData);

            [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetupDiGetDeviceInterfaceDetail(
                IntPtr hdevInfo,
                ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
                ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
                uint deviceInterfaceDetailDataSize,
                out uint requiredSize,
                IntPtr deviceInfoData);

            [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetupDiGetDeviceInterfaceDetail(
                IntPtr hdevInfo,
                ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
                IntPtr deviceInterfaceDetailData,
                uint deviceInterfaceDetailDataSize,
                out uint requiredSize,
                IntPtr deviceInfoData);

        }
    }
}