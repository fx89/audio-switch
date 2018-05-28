using AudioSwitchWrapper.Exception;
using AudioSwitchWrapper.Model;
using System.Collections.Generic;

namespace AudioSwitchWrapper
{
    public class AudioDeviceSwitch
    {
        private long LowLevelHandle = AudioSwitchImports.CreateAudioSwitchInstance();

        private AudioDevicesRepository Repository = new AudioDevicesRepository();



        public AudioDeviceSwitch() {
            Reset();
        }



        public List<AudioDevice> GetAudioDevices() => Repository.GetAll();

        public AudioDevice GetAudioDeviceById(string id) => Repository.GetById(id);

        public AudioDevice GetAudioDeviceByName(string name) => Repository.GetByName(name);



        public AudioDevice GetDefaultAudioDevice() {
            byte [] buffer = new byte[500];
            int len = 0;

            AudioSwitchImports.GetDefaultAudioPlaybackDeviceId(LowLevelHandle, buffer, 500, out len);
            string defaultInstanceId = System.Text.Encoding.UTF8.GetString(buffer, 0, len);

            AudioDevice ret = Repository.GetById(defaultInstanceId);

            if (ret == null) {
                Reset();
                return GetDefaultAudioDevice();
            }

            return ret;
        }


        public void SetDefaultAudioDevice(AudioDevice audioDevice) {
            if (!Repository.Exists(audioDevice))
                throw new AudioSwitchWrapperException("The audio device " + audioDevice + " is not present in the local repository. The repository must be refreshed before continuing.");

            long result = AudioSwitchImports.SetDefaultAudioPlaybackDevice(LowLevelHandle, audioDevice.ID);
        }

        public void Reset() {
            AudioSwitchImports.ResetAudioSwitchInstance(LowLevelHandle);
            RebuildRepository();
        }



        private void RebuildRepository() {
            Repository.Flush();

            int nDevices = 0;

            AudioSwitchImports.GetAudioPlaybackDevicesCount(LowLevelHandle, out nDevices);

            for (int i = 0; i < nDevices; i++) {
                byte [] buffer = new byte[500];
                int len;

                AudioSwitchImports.GetAudioPlaybackDeviceProperty(LowLevelHandle, i, 1, buffer, 500, out len);
                string deviceId = System.Text.Encoding.UTF8.GetString(buffer, 0, len);

                AudioSwitchImports.GetAudioPlaybackDeviceProperty(LowLevelHandle, i, 2, buffer, 500, out len);
                string deviceName = System.Text.Encoding.UTF8.GetString(buffer, 0, len);

                Repository.Add(new AudioDevice(deviceId, deviceName));
            }
        }
    }
}
