using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HCI_Projekat2.Models
{
    public class Tag : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Label { get; set; }
        public string Description { get; set; }        

        private SolidColorBrush _color;   

        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                if (value != _color)
                {
                    _color = value;
                    OnPropertyChanged("Color");
                }
            }
        }

        public Tag()
        {
            Label = "";
        }

        public virtual void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public override string ToString()
        {
            return Label;
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

                Tag tag = (Tag)obj;
                return this.Label.Equals(tag.Label);
            }
        }
    }
}
