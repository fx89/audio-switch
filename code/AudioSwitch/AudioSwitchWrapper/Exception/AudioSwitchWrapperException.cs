
namespace AudioSwitchWrapper.Exception
{
    public class AudioSwitchWrapperException : System.Exception
    {
        public AudioSwitchWrapperException() : base() { }

        public AudioSwitchWrapperException(string message) : base(message) { }

        public AudioSwitchWrapperException(string message, System.Exception inner) : base(message, inner) { }
    }
}
