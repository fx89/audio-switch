using AudioDeviceSwitchApp.Model;

namespace AudioDeviceSwitchApp
{
    public interface AudioSwitchConfigurationManager
    {
        SwitchConfig LoadConfiguration();

        void SaveConfiguration(SwitchConfig configuration);
    }
}
