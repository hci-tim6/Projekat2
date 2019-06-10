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
        public Type()
        {
            Label = "";
        }

        public Type(Type oldType)
        {
            Label = String.Copy(oldType.Label);
            Name = String.Copy(oldType.Name);
            Description = String.Copy(oldType.Description);
            Icon = String.Copy(oldType.Icon);
        }

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
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {

                Type type1 = (Type)obj;
                return this.Label.Equals(type1.Label);
            }
        }
    }
}
