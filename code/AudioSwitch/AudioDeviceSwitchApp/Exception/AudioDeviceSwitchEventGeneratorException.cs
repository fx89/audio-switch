
namespace AudioDeviceSwitchApp.Exception
{
    public class AudioDeviceSwitchEventGeneratorException : AudioDeviceSwitchException
    {
        public AudioDeviceSwitchEventGeneratorException() : base() { }

        public AudioDeviceSwitchEventGeneratorException(string message) : base(message) { }

        public AudioDeviceSwitchEventGeneratorException(string message, System.Exception inner) : base(message, inner) { }
    }
}
