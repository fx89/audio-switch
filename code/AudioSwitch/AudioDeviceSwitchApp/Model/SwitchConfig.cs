using System;
using System.Collections.Generic;

namespace AudioDeviceSwitchApp.Model
{
    [Serializable()]
    public class SwitchConfig
    {
        /*
         * How often to check for the active window
         */
        public int PollingIntervalMillis { get; set; }

        /*
         * For how long the active window should remain active to triger the audio switch
         */
        public int TimeoutMillis { get; set; }

        /*
         * Rules for switching from one audio device to another
         */
        public List<SwitchRule> Rules = new List<SwitchRule>();
    }
}
