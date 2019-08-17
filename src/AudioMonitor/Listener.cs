using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioMonitor
{
    class Listener
    {
        public static string[] GetInputDevices()
        {
            string[] devices = new string[NAudio.Wave.WaveIn.DeviceCount];
            for (int i = 0; i < devices.Length; i++)
                devices[i] = NAudio.Wave.WaveIn.GetCapabilities(i).ProductName;
            return devices;
        }
    }

}
