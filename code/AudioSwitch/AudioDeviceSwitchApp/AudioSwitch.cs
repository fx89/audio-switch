using AudioDeviceSwitchApp.Model;
using System.Threading;
using AudioSwitchWrapper;
using AudioSwitchWrapper.Model;
using AudioDeviceSwitchApp.Exception;

namespace AudioDeviceSwitchApp
{
    public class AudioSwitch
    {
        private SwitchConfig Configuration;

        private AudioSwitchConfigurationManager ConfigurationManager;

        private AudioSwitchEventGenerator EventGenerator = new AudioSwitchEventGenerator();

        private AudioDeviceSwitch DeviceSwitch = new AudioDeviceSwitch();

        private Thread TickerThread;

        private bool IsRunning = false;



        public AudioSwitch(AudioSwitchConfigurationManager configManager) {
            ConfigurationManager = configManager;
        }



        public void Start() {
            try {
                LoadConfiguration();
                IsRunning = true;
                InitTickerThread();
            }
            catch (System.Exception exc) {
                throw new AudioDeviceSwitchException("Could not start the process. " + exc.Message, exc);
            }
        }

        public void Stop() {
            IsRunning = false;
        }




        public void SaveConfiguration() {
            try {
                ConfigurationManager.SaveConfiguration(Configuration);
            }
            catch (System.Exception exc) {
                throw new AudioDeviceSwitchException("Could not save the configuration. " + exc.Message, exc);
            }
        }

        private void LoadConfiguration() {
            try {
                Configuration = ConfigurationManager.LoadConfiguration();
                EventGenerator.Configuration = Configuration;
            }
            catch (System.Exception exc) {
                throw new AudioDeviceSwitchException("Could not load the configuration. " + exc.Message, exc);
            }
        }




        private void Tick() {
            SwitchRule aswEvent = EventGenerator.GenerateEvent();

            if (aswEvent != null) {
                AudioDevice device = DeviceSwitch.GetAudioDeviceByName(aswEvent.AudioDeviceName);

                if (device == null)
                    throw new AudioDeviceSwitchException("The system does not contain an audio device with the given name [" + aswEvent.AudioDeviceName + "].");

                DeviceSwitch.SetDefaultAudioDevice(device);
            }
        }

        private void InitTickerThread() {
            TickerThread = new Thread(new ThreadStart(() => {
                while (IsRunning) {
                    Tick();
                    Thread.Sleep(Configuration.PollingIntervalMillis);
                }
            }));

            TickerThread.Start();
        }

    }
}
