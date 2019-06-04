using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat2.Models
{
    public class Type : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Label { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private string icon;
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

        public virtual void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public override string ToString()
        {
            return Label;
        }
    }
}
