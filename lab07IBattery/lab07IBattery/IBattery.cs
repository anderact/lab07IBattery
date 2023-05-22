using System;
using System.Collections.Generic;
using System.Text;

namespace lab07IBattery
{
    public enum BatteryStatus
    {
        Charging,
        Discharging,
        Full,
        NotCharging,
        Unknown
    }
    public enum PowerSource
    {
        Battery,
        Ac,
        Usb,
        Wireless,
        Other
    }
    public interface IBattery
    {
        int RemainingChargePercent { get; }
        BatteryStatus Status { get; }
        PowerSource PowerSource { get; }

    }
}
