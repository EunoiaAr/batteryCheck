namespace batteryCheck
{
    public class BatteryInformation
    {
        public  PowerState  PowerState          { get; set; }
        public  uint        CurrentCapacity     { get; set; }
        public  int         DesignedMaxCapacity { get; set; }
        public  int         FullChargeCapacity  { get; set; }
        public  uint        Voltage             { get; set; }
        public  int         DischargeRate       { get; set; }
    }

}