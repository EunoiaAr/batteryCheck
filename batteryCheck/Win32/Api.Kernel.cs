using System;
using System.IO;
using System.Runtime.InteropServices;

namespace batteryCheck.Win32
{
    public static partial class Api
    {
        public static class Kernel
        {
            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr CreateFile(
                string filename,
                [MarshalAs(UnmanagedType.U4)] FileAccess desiredAccess,
                [MarshalAs(UnmanagedType.U4)] FileShare shareMode,
                IntPtr securityAttributes,
                [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                [MarshalAs(UnmanagedType.U4)] FILE_ATTRIBUTES flags,
                IntPtr template);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern bool DeviceIoControl(
                IntPtr handle,
                uint controlCode,
                [In] IntPtr inBuffer,
                uint inBufferSize,
                [Out] IntPtr outBuffer,
                uint outBufferSize,
                out uint bytesReturned,
                IntPtr overlapped);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern bool DeviceIoControl(
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
}