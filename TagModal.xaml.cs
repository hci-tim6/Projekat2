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

        private Tag _tag;
        private Tag _backupTag;

        public Tag TagThis
        {
            get { return _tag; }
            set
            {
                if (value != _tag)
                {
                    _tag = value;
                    OnPropertyChanged("TagThis");
                }
            }
        }
        public TagModal (Window owner, Tag t)
        {
            Owner = owner;
            DataContext = this;
            TagThis = t;
            _backupTag = new Tag { Label = t.Label, Description = t.Description, Color = t.Color };

            if (t.Label == null || t.Label == "")
                isAdd = true;

            InitializeComponent();        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TagThis = _backupTag;
            (Owner as MainWindow).ViewTag?.Refresh();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TagThis.Label = TxtLabel.Text;
            TagThis.Description = TxtDescription.Text;

            if(ColorPicker.SelectedColor != null)
            {
                TagThis.Color = new SolidColorBrush((Color)ColorPicker.SelectedColor);
            }            

            if (isAdd)
            {
                (Owner as MainWindow).Tags.Add(TagThis);
            }

            (Owner as MainWindow).ViewTag?.Refresh();
            Close();
        }
    }
}
