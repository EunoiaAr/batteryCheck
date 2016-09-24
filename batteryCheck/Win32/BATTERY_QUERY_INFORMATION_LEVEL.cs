namespace batteryCheck.Win32
{
    internal enum BATTERY_QUERY_INFORMATION_LEVEL
    {
        BatteryInformation = 0,
        BatteryGranularityInformation = 1,
        BatteryTemperature = 2,
        BatteryEstimatedTime = 3,
        BatteryDeviceName = 4,
        BatteryManufactureDate = 5,
        BatteryManufactureName = 6,
        BatteryUniqueID = 7
    }
}
