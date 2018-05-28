
namespace AudioDeviceSwitchApp.Exception
{
    public class AudioDeviceSwitchException : System.Exception
    {
        public AudioDeviceSwitchException() : base() { }

        public AudioDeviceSwitchException(string message) : base(message) { }

        public AudioDeviceSwitchException(string message, System.Exception inner) : base(message, inner) { }
    }
}
