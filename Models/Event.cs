using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat2.Models
{
    public class Event
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public AlcoholStatus Alcohol { get; set; }
        public string Icon { get; set; }
        public HandicapStatus Handicap { get; set; }
        public SmokingStatus Smoking { get; set; }
        public Space Space { get; set; }
        public Price Price { get; set; }
        public TargetAudience Audience { get; set; }
        public List<Tag> Tags { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
