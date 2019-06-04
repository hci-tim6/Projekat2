using HCI_Projekat2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Projekat2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
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

        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<Models.Type> Types { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }

        Point startPoint = new Point();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            List<Models.Type> TypeList = new List<Models.Type>
            {
                new Models.Type{Label = "TL1", Name = "TN1", Description = "TDESC1", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL2", Name = "TN2", Description = "TDESC2", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL3", Name = "TN3", Description = "TDESC3", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL4", Name = "TN4", Description = "TDESC4", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL5", Name = "TN5", Description = "TDESC5", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL6", Name = "TN6", Description = "TDESC6", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL7", Name = "TN7", Description = "TDESC7", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL8", Name = "TN8", Description = "TDESC8", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL9", Name = "TN9", Description = "TDESC9", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
                new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="C:\\Users\\Milan\\Projekat2\\pin.png" },
            };

            List<Tag> TagList = new List<Tag>
            {
                new Tag{Label = "TagL1", Description = "TagDesc1", Color = Brushes.Red},
                new Tag{Label = "TagL2", Description = "TagDesc2", Color = Brushes.Blue},
                new Tag{Label = "TagL3", Description = "TagDesc3", Color = Brushes.Green},
                new Tag{Label = "TagL4", Description = "TagDesc4", Color = Brushes.Gray},
                new Tag{Label = "TagL5", Description = "TagDesc5", Color = Brushes.Purple},
            };

            Types = new ObservableCollection<Models.Type>(TypeList);
            Tags = new ObservableCollection<Tag>(TagList);

            List<Event> EventList = new List<Event> {
                new Event{Label = "EA1", Name = "Djole Event", Description = "Descc", Type = Types[0], Alcohol = AlcoholStatus.Prohibited, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(), Date = new DateTime().AddDays(1)},

                new Event{Label = "EE2", Name = "Djole Event 2", Description = "Descc", Type = Types[1], Alcohol = AlcoholStatus.Allowed, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Inaccesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.High, Tags = new ObservableCollection<Tag>()
                {
                    Tags[2],
                    Tags[3],
                }, Date = new DateTime().AddDays(2)} ,

                new Event{Label = "EE3", Name = "Djole Event 3", Description = "Descc", Type = Types[2], Alcohol = AlcoholStatus.Sold, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Elderly, Price = Price.Low, Tags = new ObservableCollection<Tag>(), Date = new DateTime().AddDays(5)},

                new Event{Label = "ED4", Name = "ED4 Event 4", Description = "Descc", Type = Types[3], Alcohol = AlcoholStatus.Sold, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Inaccesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.Medium, Tags = new ObservableCollection<Tag>(), Date = new DateTime().AddDays(4)},

                new Event{Label = "EC5", Name = "EC5 Event 5", Description = "Descc", Type = TypeList[4], Alcohol = AlcoholStatus.Prohibited, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.High, Tags = new ObservableCollection<Tag>(){
                    Tags[0],
                    Tags[4],
                }, Date = new DateTime().AddDays(3)},

                new Event{Label = "EB6", Name = "Djole Event", Description = "Descc", Type = Types[5], Alcohol = AlcoholStatus.Allowed, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Inaccesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Elderly, Price = Price.Low, Tags = new ObservableCollection<Tag>(){
                    Tags[2],
                    Tags[4],
                }, Date = new DateTime().AddDays(5)},

                new Event{Label = "EA7", Name = "Djole Event", Description = "Descc", Type = Types[6], Alcohol = AlcoholStatus.Allowed, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Adults, Price = Price.Medium, Tags = new ObservableCollection<Tag>(){
                    Tags[0],
                    Tags[1],
                }, Date = new DateTime().AddDays(7)},

                new Event{Label = "EA8", Name = "Djole Event", Description = "Descc", Type = Types[7], Alcohol = AlcoholStatus.Prohibited, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Inaccesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Elderly, Price = Price.Free, Tags = new ObservableCollection<Tag>(), Date = new DateTime()},

                new Event{Label = "EB9", Name = "Djole Event", Description = "Descc", Type = Types[8], Alcohol = AlcoholStatus.Sold, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.Medium, Tags = new ObservableCollection<Tag>(){
                    Tags[0],
                    Tags[1],
                    Tags[2],
                    Tags[3],
                    Tags[4],
                }, Date = new DateTime()},

                new Event{Label = "EC10", Name = "Djole Event", Description = "Descc", Type = Types[9], Alcohol = AlcoholStatus.Prohibited, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Inaccesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.High, Tags = new ObservableCollection<Tag>(){
                    Tags[1],
                    Tags[2],
                }, Date = new DateTime()},

                new Event{Label = "ED11", Name = "Djole Event", Description = "Descc", Type = Types[0], Alcohol = AlcoholStatus.Allowed, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.Low, Tags = new ObservableCollection<Tag>(){
                    Tags[0],
                    Tags[3],
                }, Date = new DateTime()},

                new Event{Label = "EE12", Name = "Djole Event", Description = "Descc", Type = Types[1], Alcohol = AlcoholStatus.Sold, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Inaccesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(){
                    Tags[0],
                    Tags[1],
                    Tags[2],
                }, Date = new DateTime()},

                new Event{Label = "EC13", Name = "Djole Event", Description = "Descc", Type = Types[2], Alcohol = AlcoholStatus.Prohibited, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Inaccesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.High, Tags = new ObservableCollection<Tag>(){
                    Tags[3],
                    Tags[4],
                }, Date = new DateTime()},

                new Event{Label = "ED14", Name = "Djole Event", Description = "Descc", Type = Types[3], Alcohol = AlcoholStatus.Allowed, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.Low, Tags = new ObservableCollection<Tag>(){
                    Tags[2],
                    Tags[3],
                }, Date = new DateTime()},

                new Event{Label = "EE15", Name = "Djole Event", Description = "Descc", Type = Types[4], Alcohol = AlcoholStatus.Sold, Icon="C:\\Users\\Milan\\Projekat2\\pin.png", Handicap = HandicapStatus.Inaccesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(){
                    Tags[0],
                    Tags[1],
                }, Date = new DateTime()}
            };

            Events = new ObservableCollection<Event>(EventList);            

            View = CollectionViewSource.GetDefaultView(Events);
            ViewType = CollectionViewSource.GetDefaultView(Types);
            ViewTag = CollectionViewSource.GetDefaultView(Tags);

            var customFilter = new Predicate<object>(ComplexFilter);
         
            View.Filter = customFilter;
        }

        private bool ComplexFilter(object obj)
        {
            if (obj != null)
            {
                bool[] alchohol = new bool[3];
                bool alchohol_final = false;
                bool alchohol_usage = false;
                if (Alchohol_Allowed.IsChecked == true)
                {
                    alchohol_usage = true;
                    alchohol[0] = ((Event)obj).Alcohol.Equals(AlcoholStatus.Allowed);
                }

                if (Alchohol_Prohibited.IsChecked == true) 
                {
                    alchohol_usage = true;
                    alchohol[1] = ((Event)obj).Alcohol.Equals(AlcoholStatus.Prohibited);
                }

                if (Alchohol_Sold.IsChecked == true)
                {
                    alchohol_usage = true;
                    alchohol[2] = ((Event)obj).Alcohol.Equals(AlcoholStatus.Sold);
                }

                foreach (bool var in alchohol) {
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
                    handicap[0] = ((Event)obj).Handicap.Equals(HandicapStatus.Accesible);
                }

                if (Handicap_Inaccesible.IsChecked == true)
                {
                    handicap_usage = true;
                    handicap[1] = ((Event)obj).Handicap.Equals(HandicapStatus.Inaccesible);
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
                    smoking[0] = ((Event)obj).Smoking.Equals(SmokingStatus.Allowed);
                }

                if (Smoking_Prohibited.IsChecked == true)
                {
                    smoking_usage = true;
                    smoking[1] = ((Event)obj).Smoking.Equals(SmokingStatus.Prohibited);
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
                    space[0] = ((Event)obj).Space.Equals(Space.Indoors);
                }

                if (Space_Outdoors.IsChecked == true)
                {
                    space_usage = true;
                    space[1] = ((Event)obj).Space.Equals(Space.Outdoors);
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
                    audience[0] = ((Event)obj).Audience.Equals(TargetAudience.Adults);
                }

                if (Audience_Children.IsChecked == true)
                {
                    audience_usage = true;
                    audience[1] = ((Event)obj).Audience.Equals(TargetAudience.Children);
                }

                if (Audience_Elderly.IsChecked == true)
                {
                    audience_usage = true;
                    audience[2] = ((Event)obj).Audience.Equals(TargetAudience.Elderly);
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
                    price[0] = ((Event)obj).Price.Equals(Price.Free);
                }

                if (Price_High.IsChecked == true)
                {
                    price_usage = true;
                    price[1] = ((Event)obj).Price.Equals(Price.High);
                }

                if (Price_Low.IsChecked == true)
                {
                    price_usage = true;
                    price[2] = ((Event)obj).Price.Equals(Price.Low);
                }

                if (Price_Medium.IsChecked == true)
                {
                    price_usage = true;
                    price[3] = ((Event)obj).Price.Equals(Price.Medium);
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

        public static ScrollViewer GetScrollViewer(UIElement element)
        {
            if (element == null) return null;

            ScrollViewer retour = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element) && retour == null; i++)
            {
                if (VisualTreeHelper.GetChild(element, i) is ScrollViewer)
                {
                    retour = (ScrollViewer)(VisualTreeHelper.GetChild(element, i));
                }
                else
                {
                    retour = GetScrollViewer(VisualTreeHelper.GetChild(element, i) as UIElement);
                }
            }
            return retour;
        }

        private TargetType GetParent<TargetType>(DependencyObject o)
            where TargetType : DependencyObject
        {
            if (o == null || o is TargetType) return (TargetType)o;
            return GetParent<TargetType>(VisualTreeHelper.GetParent(o));
        }

        private void TableEvent_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void TableEvent_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            var viewer = GetScrollViewer(TableEvent);

            if (e.LeftButton == MouseButtonState.Pressed &&
                !viewer.IsMouseCaptureWithin && 
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                DataGrid dataGrid = sender as DataGrid;
                Event selectedEvent = (Event)dataGrid.SelectedItem;
                Image img = new Image();
                try
                {
                    img.Source = new BitmapImage(new Uri(selectedEvent.Icon));
                }
                catch (Exception)
                {
                    return;
                }
                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("eventFormat", selectedEvent);
                DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);
            }
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("eventFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("eventFormat"))
            {
                Event m = e.Data.GetData("eventFormat") as Event;
                        
                Image img = new Image();
                if (!m.Icon.Equals(""))
                    img.Source = new BitmapImage(new Uri(m.Icon));
                //else
                //img.Source = new BitmapImage(new Uri(m.Tip.Slika));

                img.Width = 50;
                img.Height = 50;
                img.Tag = m.Label;
                img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                img.PreviewMouseMove += DraggedImageMouseMove;

                var positionX = e.GetPosition(sender as Canvas).X;
                var positionY = e.GetPosition(sender as Canvas).Y;

                m.X = positionX;
                m.Y = positionY;

                (sender as Canvas).Children.Add(img);
                Canvas.SetLeft(img, positionX - img.Width / 2.0);
                Canvas.SetTop(img, positionY - img.Height / 2.0);
            }
        }

        private void DraggedImagePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            startPoint = e.GetPosition(null);
            Image img = sender as Image;

            foreach (Event man in Events)
            {
                if (man.Label.Equals(img.Tag))
                {

                    TableEvent.SelectedItem = man;   
                    SelectedEvent = man;
                }
            }

            try
            {
                Event selectedEvent = (Event)TableEvent.SelectedItem;
                TableEvent.ScrollIntoView(TableEvent.SelectedItem);
            }
            catch { }

            if (e.LeftButton == MouseButtonState.Released)
                e.Handled = true;

        }

        private void DraggedImageMouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {               
                Image img = sender as Image;
                (img.Parent as Canvas).Children.Remove(img);

                Event selectedEvent = null;                               

                selectedEvent = SelectedEvent;

                DataObject dragData = new DataObject("eventFormat", selectedEvent);
                DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);
                e.Handled = true;

            }

        }

        Point scrollMousePoint = new Point();
        double hOff = 1;
        double vOff = 1;

        private void ScrollViewer_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            
            scrollMousePoint = e.GetPosition(scrollViewer);  

            foreach (FrameworkElement fe in Canvas1.Children)
            {
                if (e.GetPosition(Canvas1).X >= Canvas.GetLeft(fe) && e.GetPosition(Canvas1).X <= Canvas.GetLeft(fe) + fe.ActualWidth &&
                        e.GetPosition(Canvas1).Y >= Canvas.GetTop(fe) && e.GetPosition(Canvas1).Y <= Canvas.GetTop(fe) + fe.ActualHeight)
                {
                    return;
                }
            }
            hOff = scrollViewer.HorizontalOffset;
            vOff = scrollViewer.VerticalOffset;
            scrollViewer.CaptureMouse();
        }

        private void ScrollViewer_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
        }

        private void ScrollViewer_PreviewMouseMove_1(object sender, MouseEventArgs e)
        {
            if (scrollViewer.IsMouseCaptured)
            {
                foreach (FrameworkElement fe in Canvas1.Children)
                {
                    if (e.GetPosition(Canvas1).X >= Canvas.GetLeft(fe) && e.GetPosition(Canvas1).X <= Canvas.GetLeft(fe) + fe.ActualWidth &&
                        e.GetPosition(Canvas1).Y >= Canvas.GetTop(fe) && e.GetPosition(Canvas1).Y <= Canvas.GetTop(fe) + fe.ActualHeight)
                    {
                        return;
                    }
                }
                scrollViewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollViewer).X));
                scrollViewer.ScrollToVerticalOffset(vOff + (scrollMousePoint.Y - e.GetPosition(scrollViewer).Y));
            }
        }

        private void RefreshView(object sender, RoutedEventArgs e)
        {
            View?.Refresh();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var addDialog = new EventModal(this, new Event(), Tags, Types);
            addDialog.ShowDialog();
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            var addDialog = new EventModal(this, (Event) row.Item, Tags, Types);
            addDialog.ShowDialog();
        }

        private void Button_PreviewMouseLeftButtonDownDelete(object sender, MouseButtonEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            Events.Remove((Event)row.Item);
        }

        private void Type_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            var addDialog = new TypeModal(this, (Models.Type)row.Item);
            addDialog.ShowDialog();
        }

        private void Type_PreviewMouseLeftButtonDownDelete(object sender, MouseButtonEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            Types.Remove((Models.Type)row.Item);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var addDialog = new TypeModal(this, new Models.Type());
            addDialog.ShowDialog();
        }

        private void Tag_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            var addDialog = new TagModal(this, (Tag)row.Item);
            addDialog.ShowDialog();
        }

        private void Tag_PreviewMouseLeftButtonDownDelete(object sender, MouseButtonEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            Tags.Remove((Tag)row.Item);
        }

        static bool searchPredicate(Event e, string query)
        {
            bool retVal = false;
            if (e.Label.ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Name.ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Description.ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Type.Name.ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Alcohol.ToString().ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Handicap.ToString().ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Smoking.ToString().ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Space.ToString().ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Price.ToString().ToLower().Contains(query))
            {
                retVal = true;
            }
            if (e.Audience.ToString().ToLower().Contains(query))
            {
                retVal = true;
            }
            return retVal;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
