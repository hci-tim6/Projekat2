using HCI_Projekat2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

namespace HCI_Projekat2
{
    /// <summary>
    /// Interaction logic for EventModal.xaml
    /// </summary>
    public partial class EventModal : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAdd;

        public virtual void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private DateTime backupDateTime;

        private ICollectionView _View;
        public ICollectionView View
        {
            get
            {
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged("View");
            }
        }

        private string Backup_Icon;

        private ObservableCollection<Tag> Tags;
        private ObservableCollection<Tag> Backup_Tags;

        private Event _event;
        private Event _backupEvent;

        public Event Event
        {
            get { return _event; }
            set
            {
                if (value != _event)
                {
                    _event = value;
                    OnPropertyChanged("Event");
                }
            }
        }

        private ICollectionView _ViewType;
        public ICollectionView ViewType
        {
            get
            {
                return _ViewType;
            }
            set
            {
                _ViewType = value;
                OnPropertyChanged("ViewType");
            }
        }

        private ICollectionView _ViewTag;
        public ICollectionView ViewTag
        {
            get
            {
                return _ViewTag;
            }
            set
            {
                _ViewTag = value;
                OnPropertyChanged("ViewTag");
            }
        }

        private Models.Type _selectedType;
        public Models.Type SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }

        private AlcoholStatus _selectedAlcohol;
        public AlcoholStatus SelectedAlcohol
        {
            get { return _selectedAlcohol; }
            set
            {
                _selectedAlcohol = value;
                OnPropertyChanged("SelectedAlcohol");
            }
        }

        public IEnumerable<AlcoholStatus> CBListAlcohol
        {
            get
            {
                return Enum.GetValues(typeof(AlcoholStatus))
                    .Cast<AlcoholStatus>();
            }
        }

        private HandicapStatus _selectedHandicap;
        public HandicapStatus SelectedHandicap
        {
            get { return _selectedHandicap; }
            set
            {
                _selectedHandicap = value;
                OnPropertyChanged("SelectedAlcohol");
            }
        }

        public IEnumerable<HandicapStatus> CBListHandicap
        {
            get
            {
                return Enum.GetValues(typeof(HandicapStatus))
                    .Cast<HandicapStatus>();
            }
        }

        private SmokingStatus _selectedSmoking;
        public SmokingStatus SelectedSmoking
        {
            get { return _selectedSmoking; }
            set
            {
                _selectedSmoking = value;
                OnPropertyChanged("SelectedSmoking");
            }
        }

        public IEnumerable<SmokingStatus> CBListSmoking
        {
            get
            {
                return Enum.GetValues(typeof(SmokingStatus))
                    .Cast<SmokingStatus>();
            }
        }

        private Space _selectedSpace;
        public Space SelectedSpace
        {
            get { return _selectedSpace; }
            set
            {
                _selectedSpace = value;
                OnPropertyChanged("SelectedSpace");
            }
        }

        public IEnumerable<Space> CBListSpace
        {
            get
            {
                return Enum.GetValues(typeof(Space))
                    .Cast<Space>();
            }
        }

        private TargetAudience _selectedTA;
        public TargetAudience SelectedTA
        {
            get { return _selectedTA; }
            set
            {
                _selectedTA = value;
                OnPropertyChanged("SelectedTA");
            }
        }

        public IEnumerable<TargetAudience> CBListTA
        {
            get
            {
                return Enum.GetValues(typeof(TargetAudience))
                    .Cast<TargetAudience>();
            }
        }

        private Price _selectedPrice;
        public Price SelectedPrice
        {
            get { return _selectedPrice; }
            set
            {
                _selectedPrice = value;
                OnPropertyChanged("SelectedPrice");
            }
        }

        public IEnumerable<Price> CBListPrice
        {
            get
            {
                return Enum.GetValues(typeof(Price))
                    .Cast<Price>();
            }
        }

        public EventModal(Window owner, Event e, ObservableCollection<Tag> tags, ObservableCollection<Models.Type> types)
        {
            _backupEvent = new Event{ Label= e.Label, Alcohol= e.Alcohol, Audience= e.Audience, Type= e.Type, Date= e.Date, Description= e.Description,
                                     Handicap= e.Handicap, Icon= e.Icon, Name= e.Name, Points= e.Points, Price= e.Price, Smoking= e.Smoking, Space= e.Space, Tags= new ObservableCollection<Tag>(e.Tags)};
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy.";
            Thread.CurrentThread.CurrentCulture = ci;
            Owner = owner;
            DataContext = this;
            Tags = tags;
            if (e != null)
                Backup_Tags = new ObservableCollection<Tag>(e.Tags);
            else
                Backup_Tags = new ObservableCollection<Tag>();
            backupDateTime = e.Date;

            Backup_Icon = e.Icon;
            ViewType = CollectionViewSource.GetDefaultView(types);
            ViewTag = CollectionViewSource.GetDefaultView(tags);
            Event = e;

            if (e.Label == null || e.Label == "")
                isAdd = true;
            SelectedType = e.Type;
            SelectedAlcohol = e.Alcohol;
            SelectedHandicap = e.Handicap;
            SelectedSmoking = e.Smoking;
            SelectedSpace = e.Space;
            SelectedTA = e.Audience;
            SelectedPrice = e.Price;           

            InitializeComponent();

            if (isAdd)
            {
                TxtType.SelectedIndex = -1;
                TxtAlcohol.SelectedIndex = -1;
                TxtHandicap.SelectedIndex = -1;
                TxtSmoking.SelectedIndex = -1;
                TxtSpace.SelectedIndex = -1;
                TxtAudience.SelectedIndex = -1;
                TxtPrice.SelectedIndex = -1;
            }
            ResetValidation();

            DateTime today = DateTime.Now;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Closing_Click(object sender, CancelEventArgs e)
        {
            Revert();
        }

        private void Revert()
        {
            Event.Name = _backupEvent.Name;
            Event.Handicap = _backupEvent.Handicap;
            Event.Alcohol = _backupEvent.Alcohol;
            Event.Audience = _backupEvent.Audience;
            Event.Date = _backupEvent.Date;
            Event.Description = _backupEvent.Description;
            Event.Icon = _backupEvent.Icon;
            Event.Label = _backupEvent.Label;
            Event.Points = _backupEvent.Points;
            Event.Price = _backupEvent.Price;
            Event.Smoking = _backupEvent.Smoking;
            Event.Space = _backupEvent.Space;
            Event.Type = _backupEvent.Type;
            Event.Tags = new ObservableCollection<Tag>(_backupEvent.Tags);
            (Owner as MainWindow).View?.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!CheckValidation())
                return;
                
            Event.Label = TxtLabel.Text;
            Event.Name = TxtName.Text;
            Event.Description = TxtDescription.Text;
            Event.Alcohol = (AlcoholStatus) TxtAlcohol.SelectedItem;
            Event.Audience = (TargetAudience)TxtAudience.SelectedItem;
            Event.Handicap = (HandicapStatus)TxtHandicap.SelectedItem;
            Event.Smoking = (SmokingStatus)TxtSmoking.SelectedItem;
            Event.Space = (Space)TxtSpace.SelectedItem;
            Event.Price = (Price)TxtPrice.SelectedItem;
            Event.Type = (Models.Type)TxtType.SelectedItem;
            Event.Date = Convert.ToDateTime(TxtDatePicker.Text);
            Event.Tags = (ObservableCollection<Tag>)TxtTags.ItemsSource;

            Event.Icon = Event.Icon == "" ? Event.Type.Icon : Event.Icon;
            
            if (isAdd)
            {
                (Owner as MainWindow).Events.Insert(0, Event);
            }
            (Owner as MainWindow).eventHelper.JsonSerialize((Owner as MainWindow).Events, "events.json");

            foreach (Canvas c in (Owner as MainWindow).canvases)
            {
                FrameworkElement foundImg = null;
                FrameworkElement foundTxt = null;

                foreach (FrameworkElement fe in c.Children)
                {
                    if (fe.Tag.Equals(_backupEvent.Label) && fe.GetType().Equals(typeof(Image)))
                    {
                        foundImg = fe;
                    }
                    if (fe.Tag.Equals(_backupEvent.Label) && fe.GetType().Equals(typeof(TextBlock)))
                    {
                        foundTxt = fe;
                    }
                    if (foundImg != null && foundTxt != null)
                    {
                        break;
                    }
                }
                ToolTip tt = (Owner as MainWindow).createTooltipEvent(Event);
                if (foundImg != null)
                {
                    (foundTxt as TextBlock).Text = String.Copy(Event.Label);
                    foundTxt.Tag = String.Copy(Event.Label);
                    foundTxt.Name = String.Copy(Event.Label);

                    ((Image)foundImg).Source = new BitmapImage(new Uri(Event.Icon, UriKind.RelativeOrAbsolute));
                    foundImg.Tag = String.Copy(Event.Label);
                    foundImg.Name = String.Copy(Event.Label);
                    foundImg.ToolTip = tt;

                }
            }
            (Owner as MainWindow).View.Refresh();
            (Owner as MainWindow).TableEvent.ScrollIntoView(Event);
            (Owner as MainWindow).TableEvent.SelectedItem = Event;
            (Owner as MainWindow).SelectedEvent = Event;
            Backup_Tags = Event.Tags;
            backupDateTime = Event.Date;
            Backup_Icon = Event.Icon;
            _backupEvent = new Event(Event);
            
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Tag> leftoutTags = new ObservableCollection<Tag>();

            foreach (Models.Tag tag in Event.Tags)
            {
                bool used = true;
                foreach (var selected in TxtTags.SelectedItems)
                {
                    if (tag.Label.Equals(((Models.Tag)selected).Label.ToString())) {
                        used = false;
                        break;
                    }
                }
                if (used)
                    leftoutTags.Add(tag);
            }

            Event.Tags = leftoutTags;
            TxtTags.ItemsSource = Event.Tags;
            ViewTag?.Refresh();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var addTagDialog = new TagList(this, (ObservableCollection<Tag>)TxtTags.ItemsSource, Tags);
            addTagDialog.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Event.Icon = new BitmapImage(new Uri(op.FileName)).ToString();
            }
            this.View?.Refresh();
        }

        private void ResetValidation()
        {
            Warning_Alchohol.Visibility = Visibility.Hidden;
            Warning_Audience.Visibility = Visibility.Hidden;
            Warning_Date.Visibility = Visibility.Hidden;
            Warning_Handicap.Visibility = Visibility.Hidden;
            Warning_Label.Visibility = Visibility.Hidden;
            Warning_Name.Visibility = Visibility.Hidden;
            Warning_Price.Visibility = Visibility.Hidden;
            Warning_Smoking.Visibility = Visibility.Hidden;
            Warning_Space.Visibility = Visibility.Hidden;
            Warning_Type.Visibility = Visibility.Hidden;
        }

        private bool CheckValidation()
        {
            ResetValidation();
            bool allEntered = true;
            bool isUnique = true;

            if (TxtAlcohol.SelectedIndex == -1 || TxtAlcohol.SelectedItem == null)
            {
                Warning_Alchohol.Visibility = Visibility.Visible;
                allEntered = false;
            }

            if (TxtAudience.SelectedIndex == -1 || TxtAudience.SelectedItem == null)
            {
                Warning_Audience.Visibility = Visibility.Visible;
                allEntered = false;
            }

            if (TxtHandicap.SelectedIndex == -1 || TxtHandicap.SelectedItem == null)
            {
                Warning_Handicap.Visibility = Visibility.Visible;
                allEntered = false;
            }

            if (TxtPrice.SelectedIndex == -1 || TxtPrice.SelectedItem == null)
            {
                Warning_Price.Visibility = Visibility.Visible;
                allEntered = false;
            }


            if (TxtSmoking.SelectedIndex == -1 || TxtSmoking.SelectedItem == null)
            {
                Warning_Smoking.Visibility = Visibility.Visible;
                allEntered = false;
            }

            if (TxtSpace.SelectedIndex == -1 || TxtSpace.SelectedItem == null)
            {
                Warning_Space.Visibility = Visibility.Visible;
                allEntered = false;
            }

            if (TxtType.SelectedIndex == -1 || TxtType.SelectedItem == null)
            {
                Warning_Type.Visibility = Visibility.Visible;
                allEntered = false;
            }

            if (!TxtLabel.Text.Trim().Equals(""))
            {
                var events = (Owner as MainWindow).Events;
                if (!TxtLabel.Text.Equals(Event.Label) || Event.Label.Equals(""))
                {
                    foreach (var eventIter in events)
                    {
                        if (eventIter.Label.Equals(TxtLabel.Text))
                        {
                            isUnique = false;
                            break;
                        }
                    }
                }
            } else
            {
                allEntered = false;
                Warning_Label.Visibility = Visibility.Visible;
            }

            if (TxtName.Text.Trim().Equals(""))
            {
                allEntered = false;
                Warning_Name.Visibility = Visibility.Visible;
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
