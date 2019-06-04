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

        private ObservableCollection<Tag> Tags;
        private ObservableCollection<Tag> Backup_Tags;

        private Event _event;

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
            Owner = owner;
            DataContext = this;
            Tags = tags;
            if (e != null)
                Backup_Tags = new ObservableCollection<Tag>(e.Tags);
            else
                Backup_Tags = new ObservableCollection<Tag>();
            ViewType = CollectionViewSource.GetDefaultView(types);
            ViewTag = CollectionViewSource.GetDefaultView(tags);
            Event = e;

            if (e.Label == null || e.Label == "")
                isAdd = true;

            SelectedAlcohol = e.Alcohol;
            SelectedHandicap = e.Handicap;
            SelectedSmoking = e.Smoking;
            SelectedSpace = e.Space;
            SelectedTA = e.Audience;
            SelectedPrice = e.Price;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Event.Tags = Backup_Tags;
            (Owner as MainWindow).View?.Refresh();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
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
            Event.Tags = (ObservableCollection<Tag>)TxtTags.ItemsSource;

            Event.Icon = Event.Icon == null ? Event.Type.Icon : Event.Icon;

            if (isAdd)
            {
                (Owner as MainWindow).Events.Add(Event);
            }

            (Owner as MainWindow).View?.Refresh();
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
    }
}
