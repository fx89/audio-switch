using AudioDeviceSwitchApp.Model;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AudioDeviceSwitchApp
{
    class XmlPersistenceAudioSwitchConfigurationManager : AudioSwitchConfigurationManager
    {
        public string Path { get; set; }



        public SwitchConfig LoadConfiguration() {
            XmlSerializer serializer = new XmlSerializer(typeof(SwitchConfig));

            StreamReader reader = new StreamReader(Path);
            SwitchConfig config = (SwitchConfig)serializer.Deserialize(reader);
            reader.Close();

            return config;
        }



        public void SaveConfiguration(SwitchConfig configuration) {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(SwitchConfig));
            var xml = "";
            var subReq = configuration;

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, subReq);
                    xml = sww.ToString();

                    File.WriteAllText(Path, xml);
                }
            }
        }
    }
}
