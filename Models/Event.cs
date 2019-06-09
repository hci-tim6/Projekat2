using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat2.Models
{
    public class MapPoint
    {
        public double X { get; set; } = -1;
        public double Y { get; set; } = -1;
    }

    public class Event : INotifyPropertyChanged
    {
        private string icon;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Label { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public AlcoholStatus Alcohol { get; set; }
        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                if (value != icon)
                {
                    icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }
        public HandicapStatus Handicap { get; set; }
        public SmokingStatus Smoking { get; set; }
        public Space Space { get; set; }
        public Price Price { get; set; }
        public TargetAudience Audience { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }

        public MapPoint[] Points { get; set; } =
        {
            new MapPoint(){},
            new MapPoint(){},
            new MapPoint(){},
            new MapPoint(){},
        };
        
        public string FormattedDate {
            get
            {
                return Date.ToString("dd.MM.yyyy.");
            }
        } 

        public Event()
        {
            Icon = "";
            Type = null;
            Tags = new ObservableCollection<Tag>();
            Date = DateTime.Today;
        }

        public Event(Event oldEvent)
        {
            Name = String.Copy(oldEvent.Name);
            Label = String.Copy(oldEvent.Label);
            Icon = String.Copy(oldEvent.Icon);
            Date = oldEvent.Date;
            Description = String.Copy(oldEvent.Description);
            Alcohol = oldEvent.Alcohol;
            Handicap = oldEvent.Handicap;
            Smoking = oldEvent.Smoking;
            Space = oldEvent.Space;
            Audience = oldEvent.Audience;
            Price = oldEvent.Price;
            Tags = new ObservableCollection<Tag>(oldEvent.Tags);
            Type = oldEvent.Type;
        }

        public virtual void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Event event1 = (Event)obj;
                return this.Label.Equals(event1.Label);
            }
        }

    }

}
