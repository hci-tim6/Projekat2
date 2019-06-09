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
            ResetValidation();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }
        private void Closing_Click(object sender, CancelEventArgs e)
        {
            Type.Name = _backupType.Name;
            Type.Label = _backupType.Label;
            Type.Icon = _backupType.Icon;
            Type.Description = _backupType.Description;
            (Owner as MainWindow).ViewTag.Refresh();
            (Owner as MainWindow).ViewType.Refresh();
            (Owner as MainWindow).View.Refresh();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!CheckValidation())
                return;

            Type.Label = TxtLabel.Text;
            Type.Name = TxtName.Text;
            Type.Description = TxtDescription.Text;

            if (isAdd)
            {
                (Owner as MainWindow).Types.Insert(0, Type);
            }
            (Owner as MainWindow).typeHelper.JsonSerialize((Owner as MainWindow).Types, "types.json");

            (Owner as MainWindow).TableType.ScrollIntoView(Type);
            (Owner as MainWindow).TableType.SelectedItem = Type;
            (Owner as MainWindow).ViewType?.Refresh();
            _backupType = Type;
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

        private void ResetValidation()
        {
            Warning_Icon.Visibility = Visibility.Hidden;
            Warning_Label.Visibility = Visibility.Hidden;
            Warning_Name.Visibility = Visibility.Hidden;
        }

        private bool CheckValidation()
        {
            ResetValidation();
            bool allEntered = true;
            bool isUnique = true;
            bool iconChosen = true;

            if (!TxtLabel.Text.Trim().Equals(""))
            {
                var allType = (Owner as MainWindow).Types;
                if (!TxtLabel.Text.Equals(_backupType.Label) || _backupType.Label.Equals(""))
                {
                    foreach (var typeIter in allType)
                    {
                        if (typeIter.Label.Equals(TxtLabel.Text))
                        {
                            isUnique = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                allEntered = false;
                Warning_Label.Visibility = Visibility.Visible;
            }

            if (TxtName.Text.Trim().Equals(""))
            {
                allEntered = false;
                Warning_Name.Visibility = Visibility.Visible;
            }

            if (IconImage.Source == null || IconImage.Source.ToString().Trim().Equals(""))
            {
                iconChosen = false;
                Warning_Icon.Visibility = Visibility.Visible;
            }

            if (!allEntered)
            {
                MessageBox.Show("Some fields are empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!iconChosen)
            {
                MessageBox.Show("Please pick an image!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!isUnique)
            {
                Warning_Label.Visibility = Visibility.Visible;
                MessageBox.Show("Label must be unique!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

    }
}
