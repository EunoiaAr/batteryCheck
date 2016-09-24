namespace batteryCheck
{
    public class BatteryInformation
    {
        public  POWER_STATE  PowerState          { get; set; }
        public  uint        CurrentCapacity     { get; set; }
        public  int         DesignedMaxCapacity { get; set; }
        public  int         FullChargeCapacity  { get; set; }
        public  uint        Voltage             { get; set; }
        public  int         DischargeRate       { get; set; }
    }

}