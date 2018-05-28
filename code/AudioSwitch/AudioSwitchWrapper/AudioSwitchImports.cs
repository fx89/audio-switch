using System.Runtime.InteropServices;

namespace AudioSwitchWrapper
{
    public class AudioSwitchImports
    {
        [DllImport("AudioSwitch")]
        public static extern long CreateAudioSwitchInstance();

        [DllImport("AudioSwitch")]
        public static extern void DisposeAudioSwitchInstance(long audioDeviceSwitch);

        [DllImport("AudioSwitch")]
        public static extern long SetDefaultAudioPlaybackDevice(long hInstance, string devID);

        [DllImport("AudioSwitch")]
        public static extern long GetDefaultAudioPlaybackDeviceId(long hInstance, byte [] buffer, int maxLen, out int outLen);

        [DllImport("AudioSwitch")]
        public static extern long GetAudioPlaybackDevicesCount(long hInstance, out int outDevicesCount);

        [DllImport("AudioSwitch")]
        public static extern long GetAudioPlaybackDeviceProperty(long hInstance, int deviceIndex, int propertyIndex, byte [] buffer, int maxLength, out int outLength);

        [DllImport("AudioSwitch")]
        public static extern void ResetAudioSwitchInstance(long hInstance);

        /*
        [DllImport("AudioSwitch")]
        public static extern long GetAudioPlaybackDevicesInfo(long hInstance, out string[] outDeviceIDs, out string[] outDeviceNames, out int outDevicesCount);
        */
    }
}
