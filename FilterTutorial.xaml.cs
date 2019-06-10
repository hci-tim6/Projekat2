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

namespace HCI_Projekat2
{
    /// <summary>
    /// Interaction logic for FilterTutorial.xaml
    /// </summary>
    public partial class FilterTutorial : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Event> Events { get; set; }

        protected void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
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

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                OnPropertyChanged("SelectedEvent");
            }
        }

        bool completed = false;
        public FilterTutorial(Window owner)
        {
            InitializeComponent();
            DataContext = this;
            this.Owner = owner;
            TutorialTxt.FontSize = 18;
            TutorialTxt.Text = "Welcome to filter tutorial!\n\n" +
                "Below this text is a filter. " +
                "By checking/unchecking the checkboxes, you are filtering the results below. You can also reset the filter by clicking the \"Reset filter\" button.\n"+
                "Try finding all events where price is Low and smoking is Allowed.";

            List<Event> EventList = new List<Event> {
                new Event{Label = "tut_ev1", Name = "Crazy Disco", Description = "Crazy Disco party", Type = new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" }, Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Low, Tags = new ObservableCollection<Tag>(), Date = DateTime.Now
                },
                new Event{Label = "tut_ev2", Name = "Poetry Night", Description = "Poetry night description", Type = new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" }, Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Indoors, Audience = TargetAudience.Elderly, Price = Price.Low, Tags = new ObservableCollection<Tag>(), Date = DateTime.Now
                },
                new Event{Label = "tut_ev3", Name = "Guilty Pleasures", Description = "Guilty pleasures party description", Type = new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" }, Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.High, Tags = new ObservableCollection<Tag>(), Date = DateTime.Now
                },
                new Event{Label = "tut_ev4", Name = "Rave 3000", Description = "The best rave there is", Type = new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" }, Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Low, Tags = new ObservableCollection<Tag>(), Date = DateTime.Now
                },
                new Event{Label = "tut_ev5", Name = "Hammerfall", Description = "The band hammerfall is having a concert here", Type = new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" }, Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(), Date = DateTime.Now
                }
            };

            Events = new ObservableCollection<Event>(EventList);
            View = CollectionViewSource.GetDefaultView(Events);

            var customFilter = new Predicate<object>(ComplexFilter);

            View.Filter = customFilter;
        }

        private bool ComplexFilter(object obj)
        {
            if (obj != null)
            {
                Event ev = obj as Event;             

                bool[] map = new bool[2];
                bool map_final = false;
                bool map_usage = false;
                bool map_usage_show = false;
                bool map_usage_hide = false;


                foreach (bool var in map)
                {
                    if (var)
                    {
                        map_final = true;
                        break;
                    }
                }

                bool[] alchohol = new bool[3];
                bool alchohol_final = false;
                bool alchohol_usage = false;
                if (Alchohol_Allowed.IsChecked == true)
                {
                    alchohol_usage = true;
                    alchohol[0] = (ev).Alcohol.Equals(AlcoholStatus.Allowed);
                }

                if (Alchohol_Prohibited.IsChecked == true)
                {
                    alchohol_usage = true;
                    alchohol[1] = (ev).Alcohol.Equals(AlcoholStatus.Prohibited);
                }

                if (Alchohol_Sold.IsChecked == true)
                {
                    alchohol_usage = true;
                    alchohol[2] = (ev).Alcohol.Equals(AlcoholStatus.Sold);
                }

                foreach (bool var in alchohol)
                {
                    if (var)
                    {
                        alchohol_final = true;
                        break;
                    }
                }

                bool[] handicap = new bool[2];
                bool handicap_final = false;
                bool handicap_usage = false;

                if (Handicap_Accessible.IsChecked == true)
                {
                    handicap_usage = true;
                    handicap[0] = (ev).Handicap.Equals(HandicapStatus.Accesible);
                }

                if (Handicap_Inaccesible.IsChecked == true)
                {
                    handicap_usage = true;
                    handicap[1] = (ev).Handicap.Equals(HandicapStatus.Inaccesible);
                }

                foreach (bool var in handicap)
                {
                    if (var)
                    {
                        handicap_final = true;
                        break;
                    }
                }

                bool[] smoking = new bool[2];
                bool smoking_final = false;
                bool smoking_usage = false;

                if (Smoking_Allowed.IsChecked == true)
                {
                    smoking_usage = true;
                    smoking[0] = (ev).Smoking.Equals(SmokingStatus.Allowed);
                }

                if (Smoking_Prohibited.IsChecked == true)
                {
                    smoking_usage = true;
                    smoking[1] = (ev).Smoking.Equals(SmokingStatus.Prohibited);
                }

                foreach (bool var in smoking)
                {
                    if (var)
                    {
                        smoking_final = true;
                        break;
                    }
                }

                bool[] space = new bool[2];
                bool space_final = false;
                bool space_usage = false;
                if (Space_Indoors.IsChecked == true)
                {
                    space_usage = true;
                    space[0] = (ev).Space.Equals(Space.Indoors);
                }

                if (Space_Outdoors.IsChecked == true)
                {
                    space_usage = true;
                    space[1] = (ev).Space.Equals(Space.Outdoors);
                }

                foreach (bool var in space)
                {
                    if (var)
                    {
                        space_final = true;
                        break;
                    }
                }

                bool[] audience = new bool[3];
                bool audience_final = false;
                bool audience_usage = false;
                if (Audience_Adults.IsChecked == true)
                {
                    audience_usage = true;
                    audience[0] = (ev).Audience.Equals(TargetAudience.Adults);
                }

                if (Audience_Children.IsChecked == true)
                {
                    audience_usage = true;
                    audience[1] = (ev).Audience.Equals(TargetAudience.Children);
                }

                if (Audience_Elderly.IsChecked == true)
                {
                    audience_usage = true;
                    audience[2] = (ev).Audience.Equals(TargetAudience.Elderly);
                }

                foreach (bool var in audience)
                {
                    if (var)
                    {
                        audience_final = true;
                        break;
                    }
                }

                bool[] price = new bool[4];
                bool price_final = false;
                bool price_usage = false;
                if (Price_Free.IsChecked == true)
                {
                    price_usage = true;
                    price[0] = (ev).Price.Equals(Price.Free);
                }

                if (Price_High.IsChecked == true)
                {
                    price_usage = true;
                    price[1] = (ev).Price.Equals(Price.High);
                }

                if (Price_Low.IsChecked == true)
                {
                    price_usage = true;
                    price[2] = (ev).Price.Equals(Price.Low);
                }

                if (Price_Medium.IsChecked == true)
                {
                    price_usage = true;
                    price[3] = (ev).Price.Equals(Price.Medium);
                }

                foreach (bool var in price)
                {
                    if (var)
                    {
                        price_final = true;
                        break;
                    }
                }

                bool final = true;
                bool first_usage = true;

                if (alchohol_usage)
                {
                    if (first_usage)
                        final = alchohol_final;
                    else
                    {
                        final = final && alchohol_final;
                    }
                    first_usage = false;
                }

                if (handicap_usage)
                {
                    if (first_usage)
                    {
                        final = handicap_final;
                    }
                    else
                    {
                        final = final && handicap_final;
                    }
                    first_usage = false;
                }

                if (smoking_usage)
                {
                    if (first_usage)
                    {
                        final = smoking_final;
                    }
                    else
                    {
                        final = final && smoking_final;
                    }
                    first_usage = false;
                }

                if (space_usage)
                {
                    if (first_usage)
                    {
                        final = space_final;
                    }
                    else
                    {
                        final = final && space_final;
                    }
                    first_usage = false;
                }


                if (audience_usage)
                {
                    if (first_usage)
                    {
                        final = audience_final;
                    }
                    else
                    {
                        final = final && audience_final;
                    }
                    first_usage = false;
                }

                if (price_usage)
                {
                    if (first_usage)
                    {
                        final = price_final;
                    }
                    else
                    {
                        final = final && price_final;
                    }
                    first_usage = false;
                }

                return final;
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Alchohol_Allowed.IsChecked = false;
            Alchohol_Prohibited.IsChecked = false;
            Alchohol_Sold.IsChecked = false;

            Smoking_Allowed.IsChecked = false;
            Smoking_Prohibited.IsChecked = false;

            Audience_Adults.IsChecked = false;
            Audience_Children.IsChecked = false;
            Audience_Elderly.IsChecked = false;

            Price_Free.IsChecked = false;
            Price_High.IsChecked = false;
            Price_Low.IsChecked = false;
            Price_Medium.IsChecked = false;

            Handicap_Accessible.IsChecked = false;
            Handicap_Inaccesible.IsChecked = false;

            Space_Indoors.IsChecked = false;
            Space_Outdoors.IsChecked = false;

        }
        private void RefreshView(object sender, RoutedEventArgs e)
        {
            View?.Refresh();
            if(Smoking_Allowed.IsChecked == true && Price_Low.IsChecked == true && !completed)
            {
                TutorialTxt.Text += "\n\nGreat job! You are now a master of filtering events!\n" +
                    "You can exit the tutorial by pressing X in the top right corner or keep playing with filter functionality.";
                completed = true;
            }
        }
    }
}
