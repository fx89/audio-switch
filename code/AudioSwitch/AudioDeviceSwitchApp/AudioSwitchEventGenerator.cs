using AudioDeviceSwitchApp.Exception;
using AudioDeviceSwitchApp.Model;

namespace AudioDeviceSwitchApp
{
    public class AudioSwitchEventGenerator
    {
        private string LastActiveWindowTitle = "";

        private long CurrentTimeoutMillis = 0;

        private SwitchRule CurrentRule = null;

        private bool AlreadyReturned = false;

        public SwitchConfig Configuration { get; set; }



        public SwitchRule GenerateEvent() {
            CheckConfig();

            string activeWindowTitle = User32Wrapper.User32ImportsWrapper.GetActiveWindowTitle();
            if (activeWindowTitle == null) activeWindowTitle = "";

            if (LastActiveWindowTitle.Equals(activeWindowTitle)) {
                if (CurrentRule != null) {
                    CurrentTimeoutMillis += Configuration.PollingIntervalMillis;

                    if (CurrentTimeoutMillis >= Configuration.TimeoutMillis && !AlreadyReturned) {
                        AlreadyReturned = true;
                        return CurrentRule;
                    }
                }
            }
            else {
                SwitchRule newRule = Configuration.Rules.FindLast(rule => rule.Polarity == true && activeWindowTitle.Contains(rule.ActiveWindowTitleContent));

                if (newRule == null)
                    newRule = Configuration.Rules.FindLast(rule => rule.Polarity == false && !(activeWindowTitle.Contains(rule.ActiveWindowTitleContent)));

                if (RuleHasChanged(newRule))
                    ResetVariables();

                CurrentRule = newRule;
                LastActiveWindowTitle = activeWindowTitle;
            }

            return null;
        }

        private bool RuleHasChanged(SwitchRule newRule) => (newRule == null && CurrentRule != null) || !(newRule.Equals(CurrentRule));

        private void ResetVariables() {
            CurrentTimeoutMillis = 0;
            AlreadyReturned = false;
        }



        private void CheckConfig() {
            if (Configuration == null || Configuration.Rules == null || Configuration.Rules.Count == 00)
                throw new AudioDeviceSwitchEventGeneratorException("Cannot genereate events based on non-existent rules. Set the Configuration first and make sure it has Rules.");
        }
    }
}
