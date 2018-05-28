using System;

namespace AudioDeviceSwitchApp.Model
{
    [Serializable()]
    public class SwitchRule
    {
        /*
         *  If set to TRUE then the rule is valid when a given window is focused.
         *  If set to FALSE then the rule is valid when a given window is de-focused.
         */
        public bool Polarity { get; set; }

        /*
         * If Polarity = TRUE and the active window title contains this string then the switch will be triggerred
         * If Polaroty = FALSE and the active window title does not contain this string then the switch will be triggerred
         */
        public string ActiveWindowTitleContent { get; set; }

        /*
         *  The friendly name of the audio device to set as primary when the switch is triggerred
         */
        public string AudioDeviceName { get; set; }


        public SwitchRule() {

        }


        public SwitchRule(bool polarity, string activeWindowTitleContent, string audioDeviceName) {
            Polarity = polarity;
            ActiveWindowTitleContent = activeWindowTitleContent;
            AudioDeviceName = audioDeviceName;
        }
    }
}
