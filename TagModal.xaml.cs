using HCI_Projekat2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_Projekat2
{
    /// <summary>
    /// Interaction logic for TagModal.xaml
    /// </summary>
    public partial class TagModal : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAdd;

        public virtual void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }        

        private Models.Tag _tag;

        public Models.Tag Tag
        {
            get { return _tag; }
            set
            {
                if (value != _tag)
                {
                    _tag = value;
                    OnPropertyChanged("Tag");
                }
            }
        }
        public TagModal (Window owner, Tag t)
        {
            Owner = owner;
            DataContext = this;
            Tag = t;

            if (t.Label == null || t.Label == "")
                isAdd = true;

            InitializeComponent();
        }
    }
}
