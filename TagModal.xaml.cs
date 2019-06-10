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
            ResetValidation();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Closing_Click(object sender, CancelEventArgs e)
        {
            TagThis = _backupTag;
            (Owner as MainWindow).ViewTag?.Refresh();
            (Owner as MainWindow).View?.Refresh();
            (Owner as MainWindow).ViewType?.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!CheckValidation())
                return;

            TagThis.Label = TxtLabel.Text;
            TagThis.Description = TxtDescription.Text;

            TagThis.Color = new SolidColorBrush((Color)ColorPicker.SelectedColor);

            if (isAdd)
            {
                (Owner as MainWindow).Tags.Insert(0, TagThis);
            }
            foreach (Event ev1 in (Owner as MainWindow).Events)
            {
                for (int i = 0; i < ev1.Tags.Count; i++)
                {
                    if (ev1.Tags.ElementAt(i).Label.Equals(_backupTag.Label))
                    {
                        ev1.Tags[i] = TagThis;
                        break;
                    }
                }
            }
            (Owner as MainWindow).ViewTag?.Refresh();
            (Owner as MainWindow).View?.Refresh();
            (Owner as MainWindow).TableTag.ScrollIntoView(TagThis);
            (Owner as MainWindow).TableTag.SelectedItem = TagThis;
            (Owner as MainWindow).tagHelper.JsonSerialize((Owner as MainWindow).Tags, "tags.json");
            foreach (Event ev1 in (Owner as MainWindow).Events)
            {
                foreach (Tag tg in ev1.Tags)
                {
                    if (tg.Label.Equals(TagThis.Label))
                    {
                        foreach (Canvas c in (Owner as MainWindow).canvases)
                        {
                            FrameworkElement foundImg = null;

                            foreach (FrameworkElement fe in c.Children)
                            {
                                if (fe.Tag.Equals(ev1.Label) && fe.GetType().Equals(typeof(Image)))
                                {
                                    foundImg = fe;
                                    ((Image)foundImg).ToolTip = (Owner as MainWindow).createTooltipEvent(ev1);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            _backupTag = TagThis;
            Close();
        }

        private void ResetValidation()
        {
            Warning_Label.Visibility = Visibility.Hidden;
            Warning_Color.Visibility = Visibility.Hidden;
        }

        private bool CheckValidation()
        {
            ResetValidation();
            bool allEntered = true;
            bool isUnique = true;

            if (!TxtLabel.Text.Trim().Equals(""))
            {
                var allTags = (Owner as MainWindow).Tags;
                if (!TxtLabel.Text.Equals(_backupTag.Label) || _backupTag.Label.Equals(""))
                {
                    foreach (var tagsIter in allTags)
                    {
                        if (tagsIter.Label.Equals(TxtLabel.Text))
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

            if (ColorPicker.SelectedColor == null)
            {
                allEntered = false;
                Warning_Color.Visibility = Visibility.Visible;
            }

            if (!allEntered)
            {
                MessageBox.Show("Some fields are empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FrameworkElement focusedControl = FocusManager.GetFocusedElement(this) as FrameworkElement;
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);
            }
        }
    }
}
