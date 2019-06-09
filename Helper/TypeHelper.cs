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
    public class TypeHelper
    {
        public void JsonSerialize(ObservableCollection<Models.Type> events, string fileName)
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

        public ObservableCollection<Models.Type> JsonDeserialize(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                ObservableCollection<Models.Type> types = (ObservableCollection<Models.Type>)serializer.Deserialize(file, typeof(ObservableCollection<Models.Type>));
                return types;
            }
        }
    }
}
