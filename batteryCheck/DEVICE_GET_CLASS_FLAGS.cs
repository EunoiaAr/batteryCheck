using System;

namespace batteryCheck
{
    internal static partial class Win32
    {

        [Flags]
        internal enum DEVICE_GET_CLASS_FLAGS : uint
        {
            DIGCF_DEFAULT = 0x00000001,
            DIGCF_PRESENT = 0x00000002,
            DIGCF_ALLCLASSES = 0x00000004,
            DIGCF_PROFILE = 0x00000008,
            DIGCF_DEVICEINTERFACE = 0x00000010
        }
    }

}