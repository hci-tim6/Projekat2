using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat2.Models
{
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
        public double X { get; set; }
        public double Y { get; set; }

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

        public virtual void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }

}
