using System;
using System.IO;
using System.Runtime.InteropServices;

namespace batteryCheck
{
    using Win32;

    public static class BatteryInfo
    {
        public static BatteryInformation GetBatteryInformation()
        {
            IntPtr queryInfoPointer = IntPtr.Zero;
            IntPtr batteryInfoPointer = IntPtr.Zero;
            IntPtr batteryWaitStatusPointer = IntPtr.Zero;
            IntPtr batteryStatusPointer = IntPtr.Zero;
            try {
                var deviceHandle = SetupDiGetClassDevs(Api.GUID_DEVCLASS_BATTERY,
                                                       DEVICE_GET_CLASS_FLAGS.DIGCF_PRESENT | DEVICE_GET_CLASS_FLAGS.DIGCF_DEVICEINTERFACE);

                var deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
                deviceInterfaceData.CbSize = Marshal.SizeOf(deviceInterfaceData);

                SetupDiEnumDeviceInterfaces(deviceHandle, Api.GUID_DEVCLASS_BATTERY, 0, ref deviceInterfaceData);

                var deviceDetailData    = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                deviceDetailData.CbSize = (IntPtr.Size == 8) ? 8 : 4 + Marshal.SystemDefaultCharSize;

                SetupDiGetDeviceInterfaceDetail(deviceHandle, ref deviceInterfaceData, ref deviceDetailData);

                var batteryHandle       = CreateFile(deviceDetailData.DevicePath, FileAccess.ReadWrite, FileShare.ReadWrite, FileMode.Open, FILE_ATTRIBUTES.Normal);
                var queryInformation    = new BATTERY_QUERY_INFORMATION();

                DeviceIoControl(batteryHandle, Api.IOCTL_BATTERY_QUERY_TAG, ref queryInformation.BatteryTag);

                var batteryInformation  = new BATTERY_INFORMATION();
                queryInformation.InformationLevel = BATTERY_QUERY_INFORMATION_LEVEL.BatteryInformation;

                var queryInfoSize       = Marshal.SizeOf(queryInformation);
                var batteryInfoSize     = Marshal.SizeOf(batteryInformation);

                queryInfoPointer        = Marshal.AllocHGlobal(queryInfoSize);
                Marshal.StructureToPtr(queryInformation, queryInfoPointer, false);

                batteryInfoPointer = Marshal.AllocHGlobal(batteryInfoSize);
                Marshal.StructureToPtr(batteryInformation, batteryInfoPointer, false);

                DeviceIoControl(batteryHandle, Api.IOCTL_BATTERY_QUERY_INFORMATION, queryInfoPointer, queryInfoSize, batteryInfoPointer, batteryInfoSize);

                var updatedBatteryInformation   = (BATTERY_INFORMATION) Marshal.PtrToStructure(batteryInfoPointer, typeof(BATTERY_INFORMATION));
                var batteryWaitStatus           = new BATTERY_WAIT_STATUS();

                batteryWaitStatus.BatteryTag    = queryInformation.BatteryTag;

                var batteryStatus       = new BATTERY_STATUS();
                var waitStatusSize      = Marshal.SizeOf(batteryWaitStatus);
                var batteryStatusSize   = Marshal.SizeOf(batteryStatus);

                batteryWaitStatusPointer = Marshal.AllocHGlobal(waitStatusSize);
                Marshal.StructureToPtr(batteryWaitStatus, batteryWaitStatusPointer, false);

                batteryStatusPointer    = Marshal.AllocHGlobal(batteryStatusSize);
                Marshal.StructureToPtr(batteryStatus, batteryStatusPointer, false);

                DeviceIoControl(batteryHandle, Api.IOCTL_BATTERY_QUERY_STATUS, batteryWaitStatusPointer, waitStatusSize, batteryStatusPointer, batteryStatusSize);

                var updatedStatus           = (BATTERY_STATUS) Marshal.PtrToStructure(batteryStatusPointer, typeof(BATTERY_STATUS));
                var updatedInformation      = (BATTERY_INFORMATION) Marshal.PtrToStructure(batteryInfoPointer, typeof(BATTERY_INFORMATION));
                var updatedQueryInformation = (BATTERY_QUERY_INFORMATION) Marshal.PtrToStructure(batteryInfoPointer, typeof(BATTERY_QUERY_INFORMATION));
                SetupDiDestroyDeviceInfoList(deviceHandle);

                return new BatteryInformation() {
                    PowerState = updatedStatus.PowerState,
                    DesignedMaxCapacity = updatedBatteryInformation.DesignedCapacity,
                    FullChargeCapacity = updatedBatteryInformation.FullChargedCapacity,
                    CurrentCapacity = updatedStatus.Capacity,
                    Voltage = updatedStatus.Voltage,
                    DischargeRate = updatedStatus.Rate
                };
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            } finally {
                Marshal.FreeHGlobal(queryInfoPointer);
                Marshal.FreeHGlobal(batteryInfoPointer);
                Marshal.FreeHGlobal(batteryStatusPointer);
                Marshal.FreeHGlobal(batteryWaitStatusPointer);
            }
            return null;
        }

        static bool DeviceIoControl(IntPtr deviceHandle, uint controlCode, ref uint output)
        {
            uint bytesReturned;
            uint junkInput = 0;
            bool retval = Api.DeviceIoControl(
                deviceHandle, controlCode, ref junkInput, 0, ref output, (uint)Marshal.SizeOf(output), out bytesReturned, IntPtr.Zero);

            if (!retval) {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode != 0)
                    throw Marshal.GetExceptionForHR(errorCode);
                else
                    throw new Exception(
                        "DeviceIoControl call failed but Win32 didn't catch an error.");
            }

            return retval;
        }

        static bool DeviceIoControl(IntPtr deviceHandle, uint controlCode, IntPtr input, int inputSize, IntPtr output, int outputSize)
        {
            uint bytesReturned;
            bool retval = Api.DeviceIoControl(
                deviceHandle, controlCode, input, (uint)inputSize, output, (uint)outputSize, out bytesReturned, IntPtr.Zero);

            if (!retval) {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode != 0)
                    throw Marshal.GetExceptionForHR(errorCode);
                else
                    throw new Exception(
                        "DeviceIoControl call failed but Win32 didn't catch an error.");
            }

            return retval;
        }

        static IntPtr SetupDiGetClassDevs(Guid guid, DEVICE_GET_CLASS_FLAGS flags)
        {
            IntPtr handle = Api.SetupDiGetClassDevs(ref guid, null, IntPtr.Zero, flags);

            if (handle == IntPtr.Zero || handle.ToInt32() == -1) {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode != 0)
                    throw Marshal.GetExceptionForHR(errorCode);
                else
                    throw new Exception("SetupDiGetClassDev call returned a bad handle.");
            }
            return handle;
        }

        static bool SetupDiEnumDeviceInterfaces(IntPtr deviceInfoSet, Guid guid, uint memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData)
        {
            bool retval = Api.SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref guid, memberIndex, ref deviceInterfaceData);

            if (!retval) {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode != 0) {
                    if (errorCode == 259)
                        throw new Exception("SetupDeviceInfoEnumerateDeviceInterfaces ran out of batteries to enumerate.");

                    throw Marshal.GetExceptionForHR(errorCode);
                } else
                    throw new Exception(
                        "SetupDeviceInfoEnumerateDeviceInterfaces call failed but Win32 didn't catch an error.");
            }
            return retval;
        }

        static bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet)
        {
            bool retval = SetupDiDestroyDeviceInfoList(deviceInfoSet);

            if (!retval) {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode != 0)
                    throw Marshal.GetExceptionForHR(errorCode);
                else
                    throw new Exception(
                        "SetupDiDestroyDeviceInfoList call failed but Win32 didn't catch an error.");
            }
            return retval;
        }

        static bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData)
        {
            uint reqSize = 0;
            bool retval = Api.SetupDiGetDeviceInterfaceDetail(deviceInfoSet,
                                                                ref deviceInterfaceData,
                                                                IntPtr.Zero,
                                                                reqSize,
                                                                out reqSize,
                                                                IntPtr.Zero);
            if (!retval) {
                if (Marshal.GetLastWin32Error() == Api.ERROR_INSUFFICIENT_BUFFER) {
                    if (reqSize > Marshal.SizeOf(deviceInterfaceDetailData))
                        throw new ApplicationException("insufficient structure memory in SP_DEVIC_INTERFACE_DETAIL_DATA");

                    retval = Api.SetupDiGetDeviceInterfaceDetail(deviceInfoSet,
                                                                        ref deviceInterfaceData,
                                                                        ref deviceInterfaceDetailData,
                                                                        reqSize,
                                                                        out reqSize,
                                                                        IntPtr.Zero);
                }
            }
            if (!retval) {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode != 0)
                    throw Marshal.GetExceptionForHR(errorCode);
                else
                    throw new Exception(
                        "SetupDiGetDeviceInterfaceDetail call failed but Win32 didn't catch an error.");
            }
            return retval;
        }

        static IntPtr CreateFile(string filename, FileAccess access, FileShare shareMode, FileMode creation, FILE_ATTRIBUTES flags)
        {
            IntPtr handle = Api.CreateFile(
                filename, access, shareMode, IntPtr.Zero, creation, flags, IntPtr.Zero);

            if (handle == IntPtr.Zero || handle.ToInt32() == -1) {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode != 0)
                    Marshal.ThrowExceptionForHR(errorCode);
                else
                    throw new Exception(
                        "SetupDiGetDeviceInterfaceDetail call failed but Win32 didn't catch an error.");
            }
            return handle;
        }
    }

}