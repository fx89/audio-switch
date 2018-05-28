using AudioSwitchWrapper.Exception;
using AudioSwitchWrapper.Model;
using System.Collections.Generic;

namespace AudioSwitchWrapper
{
    public class AudioDevicesRepository
    {
        private List<AudioDevice> audioDevices = new List<AudioDevice>();

        public void Add(AudioDevice audioDevice) {
            if (Exists(audioDevice))
                throw new AudioSwitchWrapperException("The device already exists in the repository");

            audioDevices.Add(audioDevice);
        }

        public bool Exists(AudioDevice audioDevice) {
            if (audioDevice == null)
                return false;

            return (audioDevices.Find(dev => dev.Equals(audioDevice)) != null);
        }

        public AudioDevice GetById(string id) {
            return id == null ? null : audioDevices.FindLast(dev => dev.ID.Equals(id));
        }

        public AudioDevice GetByName(string name) {
            return name == null ? null : audioDevices.FindLast(dev => dev.Name.Equals(name));
        }

        public List<AudioDevice> GetAll() {
            return audioDevices;
        }

        public void Flush() {
            audioDevices = new List<AudioDevice>();
        }
    }
}
