using HCI_Projekat2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat2.Helper
{
    public class EventHelper
    {
        public void JsonSerialize(ObservableCollection<Event> events, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;


            using (StreamWriter sw = new StreamWriter(fileName))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, events);
                }
            }
        }

        public ObservableCollection<Event> JsonDeserialize(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                ObservableCollection<Event> events = (ObservableCollection<Event>)serializer.Deserialize(file, typeof(ObservableCollection<Event>));
                return events;
            }
        }

        public void saveMapIndex(int mapIndex, string fileName)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                file.WriteLine(mapIndex);
            }
        }

        public int loadMapIndex(string fileName)
        {
            string index;
            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName))
            {
                index = file.ReadToEnd(); 
            }
            return int.Parse(index);
        }
    }
}
