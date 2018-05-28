using AudioSwitchWrapper;
using AudioSwitchWrapper.Model;
using User32Wrapper;


namespace AudioDeviceSwitchApp
{
    class Program
    {
        static void Main(string[] args) {
            if (args != null && args.Length >= 1 && args[0].ToUpper().Equals("LIST")) {
                PrintDevices();
                return;
            }

            if (args != null && args.Length >= 1 && args[0].ToUpper().Equals("START")) {
                if (args.Length >= 2)
                    StartAudioSwitch(args[1]);
                else
                    System.Console.WriteLine("The START command requires the additional CONFIG_PATH paramete.");

                return;
            }

            PrintHelp();
        }

        static void PrintHelp() {
            System.Console.WriteLine("SYNTAX: audio-device-switch.exe [help / list / start] [<CONFIG_PATH>]");
            System.Console.WriteLine("        help  = display this message");
            System.Console.WriteLine("        list  = lists the active audio devices on the system");
            System.Console.WriteLine("        start = starts the automated audio device switch using the given <CONFIG_PATH>");
        }

        static void PrintDevices() {
            AudioDeviceSwitch audioDeviceSwitch = new AudioDeviceSwitch();

            AudioDevice defaultDevice = audioDeviceSwitch.GetDefaultAudioDevice();

            audioDeviceSwitch.GetAudioDevices().ForEach(dev => {
                System.Console.WriteLine(dev + (dev.Equals(defaultDevice) ? "(default)" : ""));
            });
        }

        static void StartAudioSwitch(string path) {
            XmlPersistenceAudioSwitchConfigurationManager configManager = new XmlPersistenceAudioSwitchConfigurationManager();
            configManager.Path = path;

            AudioSwitch aSwitch = new AudioSwitch(configManager);

            aSwitch.Start();
        }

    }
}
