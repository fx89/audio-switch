using AudioDeviceSwitchApp.Model;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AudioDeviceSwitchApp
{
    public class DummyAudioSwitchConfigurationManager : AudioSwitchConfigurationManager
    {
        public SwitchConfig LoadConfiguration() {
            SwitchConfig config = new SwitchConfig();
            config.PollingIntervalMillis = 1000;
            config.TimeoutMillis = 0;

            config.Rules.Add(new SwitchRule(true, "Netflix", "123"));

            config.Rules.Add(new SwitchRule(false, "Netflix", "456"));

            return config;
        }

        public void SaveConfiguration(SwitchConfig configuration) {
            throw new System.NotImplementedException();
        }
    }
}
