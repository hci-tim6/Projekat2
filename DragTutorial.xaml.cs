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
    /// Interaction logic for DragTutorial.xaml
    /// </summary>
    public partial class DragTutorial : Window
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



        public DragTutorial()
        {
            DataContext = this;
            InitializeComponent();
            List<Event> EventList = new List<Event> {
                new Event{Label = "EA1", Name = "Djole Event", Description = "Descc", Type = new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" }, Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(), Date = DateTime.Now
                }
            };
        }

        }
}
