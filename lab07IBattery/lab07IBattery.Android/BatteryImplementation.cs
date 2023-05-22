using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using lab07IBattery.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(BatteryImplementation))]

namespace lab07IBattery.Droid
{
    public class BatteryImplementation : IBattery
    {
        public BatteryImplementation()
        {
        }

        public int RemainingChargePercent
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            var level = battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
                            var scale = battery.GetIntExtra(BatteryManager.ExtraScale, -1);

                            return (int)Math.Floor(level * 100D / scale);
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }

        public lab07IBattery.BatteryStatus Status
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver (null, filter))
                        {
                            int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);

                            var isCharging = status == (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full;

                            var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);

                            var usbCharge = chargePlug == (int)BatteryPlugged.Usb;

                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;

                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                            isCharging = (usbCharge || acCharge || wirelessCharge);

                            if (isCharging)
                                return lab07IBattery.BatteryStatus.Charging;

                            switch (status)
                            {
                                case (int)BatteryStatus.Charging:
                                    return lab07IBattery.BatteryStatus.Charging;
                                case (int)BatteryStatus.Discharging:
                                    return lab07IBattery.BatteryStatus.Discharging;
                                case (int)BatteryStatus.Full:
                                    return lab07IBattery.BatteryStatus.Full;
                                case (int)BatteryStatus.NotCharging:
                                    return lab07IBattery.BatteryStatus.NotCharging;
                                default:
                                    return lab07IBattery.BatteryStatus.Unknown;
                            }

                        }
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }

        public PowerSource PowerSource
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);

                            var isCharging = status == (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full;

                            var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);

                            var usbCharge = chargePlug == (int)BatteryPlugged.Usb;

                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;

                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;


                            isCharging = (usbCharge || acCharge || wirelessCharge);

                            if (!isCharging)
                                return lab07IBattery.PowerSource.Battery;
                            else if (usbCharge)
                                return lab07IBattery.PowerSource.Usb;
                            else if (acCharge)
                                return lab07IBattery.PowerSource.Ac;
                            else if (wirelessCharge)
                                return lab07IBattery.PowerSource.Wireless;
                            else
                                return lab07IBattery.PowerSource.Other;
                        }
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }
    }
}