using Microsoft.Win32;
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
    /// Interaction logic for TypeModal.xaml
    /// </summary>
    public partial class TypeModal : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAdd;

        public virtual void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private Models.Type _type;
        private Models.Type _backupType;

        public Models.Type Type
        {
            get { return _type; }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public TypeModal(Window owner, Models.Type t)
        {
            Owner = owner;
            DataContext = this;
            Type = t;
            _backupType = new Models.Type { Description = t.Description, Icon = t.Icon, Label = t.Label, Name = t.Name };
            if (t.Label == null || t.Label == "")
                isAdd = true;            

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Type.Name = _backupType.Name;
            Type.Label = _backupType.Label;
            Type.Icon = _backupType.Icon;
            Type.Description = _backupType.Description;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Type.Label = TxtLabel.Text;
            Type.Name = TxtName.Text;
            Type.Description = TxtDescription.Text;

            if (isAdd)
            {
                (Owner as MainWindow).Types.Add(Type);
            }

            (Owner as MainWindow).ViewType?.Refresh();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Type.Icon = new BitmapImage(new Uri(op.FileName)).ToString();
            }
        }
    }
}
