using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace H3Calc
{
    public enum ApplicationMode { Simple, Standard, Scientific };
    public enum Sorting { Alphabetically, ById };

    [XmlRoot("Settings")]
    public class ApplicationSettings
    {
        public ApplicationMode Mode { get; set; }
        public Sorting UnitSorting { get; set; }
    }

    public class ApplicationSettingsManager
    {
        private const string SettingsFilePath = "settings.xml";

        public ApplicationSettings LoadSettings()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ApplicationSettings));
            TextReader reader = new StreamReader(SettingsFilePath);
            ApplicationSettings settings = (ApplicationSettings)deserializer.Deserialize(reader);
            reader.Close();

            return settings;
        }

        public void UpdateSettings(ApplicationSettings newSettings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
            TextWriter writer = new StreamWriter(SettingsFilePath);
            serializer.Serialize(writer, newSettings);            
            writer.Close();
        }
    }
}
