
namespace AudioSwitchWrapper.Model
{
    public class AudioDevice
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public AudioDevice(string id, string name) {
            this.ID = id;
            this.Name = name;
        }

        public bool Equals(AudioDevice other) {
            if (other == null) return false;
            if (ID == null) return false;
            return this.ID.Equals(other.ID);
        }

        override
        public string ToString() {
            return "[" + ID + "]:[" + Name + "]";
        }
    }
}
