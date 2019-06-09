using HCI_Projekat2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
        private double backupX;
        private double backupY;

        public Helper.EventHelper eventHelper = new Helper.EventHelper();
        public Helper.TagHelper tagHelper = new Helper.TagHelper();
        public Helper.TypeHelper typeHelper = new Helper.TypeHelper();

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

        Canvas activeCanvas = null;
        ScrollViewer scrollViewer;
        MapPoint oldPos = new MapPoint { X = -1, Y = -1 };

        public List<Canvas> canvases = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            activeCanvas = Canvas1;
            scrollViewer = scrollViewer1;

            //List<Models.Type> TypeList = new List<Models.Type>
            //{
            //    new Models.Type{Label = "TL1", Name = "TN1", Description = "TDESC1", Icon="pin.png" },
            //    new Models.Type{Label = "TL2", Name = "TN2", Description = "TDESC2", Icon="pin.png" },
            //    new Models.Type{Label = "TL3", Name = "TN3", Description = "TDESC3", Icon="pin.png" },
            //    new Models.Type{Label = "TL4", Name = "TN4", Description = "TDESC4", Icon="pin.png" },
            //    new Models.Type{Label = "TL5", Name = "TN5", Description = "TDESC5", Icon="pin.png" },
            //    new Models.Type{Label = "TL6", Name = "TN6", Description = "TDESC6", Icon="pin.png" },
            //    new Models.Type{Label = "TL7", Name = "TN7", Description = "TDESC7", Icon="pin.png" },
            //    new Models.Type{Label = "TL8", Name = "TN8", Description = "TDESC8", Icon="pin.png" },
            //    new Models.Type{Label = "TL9", Name = "TN9", Description = "TDESC9", Icon="pin.png" },
            //    new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" },
            //};

            //Types = new ObservableCollection<Models.Type>(TypeList);

            //List<Tag> TagList = new List<Tag>
            //{
            //    new Tag{Label = "TagL1", Description = "TagDesc1", Color = Brushes.Red},
            //    new Tag{Label = "TagL2", Description = "TagDesc2", Color = Brushes.Blue},
            //    new Tag{Label = "TagL3", Description = "TagDesc3", Color = Brushes.Green},
            //    new Tag{Label = "TagL4", Description = "TagDesc4", Color = Brushes.Gray},
            //    new Tag{Label = "TagL5", Description = "TagDesc5", Color = Brushes.Purple},
            //};

            //Tags = new ObservableCollection<Tag>(TagList);

            //List<Event> EventList = new List<Event> {
            //    new Event{Label = "EA1", Name = "Djole Event", Description = "Descc", Type = Types[0], Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
            //    Smoking = SmokingStatus.Prohibited, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(), Date = new DateTime().AddDays(1)},

            //    new Event{Label = "EE2", Name = "Djole Event 2", Description = "Descc", Type = Types[1], Alcohol = AlcoholStatus.Allowed, Icon="pin.png", Handicap = HandicapStatus.Inaccesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.High, Tags = new ObservableCollection<Tag>()
            //    {
            //        Tags[2],
            //        Tags[3],
            //    }, Date = new DateTime().AddDays(2)} ,

            //    new Event{Label = "EE3", Name = "Djole Event 3", Description = "Descc", Type = Types[2], Alcohol = AlcoholStatus.Sold, Icon="pin.png", Handicap = HandicapStatus.Accesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Elderly, Price = Price.Low, Tags = new ObservableCollection<Tag>(), Date = new DateTime().AddDays(5)},

            //    new Event{Label = "ED4", Name = "ED4 Event 4", Description = "Descc", Type = Types[3], Alcohol = AlcoholStatus.Sold, Icon="pin.png", Handicap = HandicapStatus.Inaccesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.Medium, Tags = new ObservableCollection<Tag>(), Date = new DateTime().AddDays(4)},

            //    new Event{Label = "EC5", Name = "EC5 Event 5", Description = "Descc", Type = TypeList[4], Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
            //    Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.High, Tags = new ObservableCollection<Tag>(){
            //        Tags[0],
            //        Tags[4],
            //    }, Date = new DateTime().AddDays(3)},

            //    new Event{Label = "EB6", Name = "Djole Event", Description = "Descc", Type = Types[5], Alcohol = AlcoholStatus.Allowed, Icon="pin.png", Handicap = HandicapStatus.Inaccesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Elderly, Price = Price.Low, Tags = new ObservableCollection<Tag>(){
            //        Tags[2],
            //        Tags[4],
            //    }, Date = new DateTime().AddDays(5)},

            //    new Event{Label = "EA7", Name = "Djole Event", Description = "Descc", Type = Types[6], Alcohol = AlcoholStatus.Allowed, Icon="pin.png", Handicap = HandicapStatus.Accesible,
            //    Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Adults, Price = Price.Medium, Tags = new ObservableCollection<Tag>(){
            //        Tags[0],
            //        Tags[1],
            //    }, Date = new DateTime().AddDays(7)},

            //    new Event{Label = "EA8", Name = "Djole Event", Description = "Descc", Type = Types[7], Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Inaccesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Elderly, Price = Price.Free, Tags = new ObservableCollection<Tag>(), Date = new DateTime()},

            //    new Event{Label = "EB9", Name = "EA1", Description = "Descc", Type = Types[8], Alcohol = AlcoholStatus.Sold, Icon="pin.png", Handicap = HandicapStatus.Accesible,
            //    Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.Medium, Tags = new ObservableCollection<Tag>(){
            //        Tags[0],
            //        Tags[1],
            //        Tags[2],
            //        Tags[3],
            //        Tags[4],
            //    }, Date = new DateTime()},

            //    new Event{Label = "EC10", Name = "Djole Event", Description = "Descc", Type = Types[9], Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Inaccesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.High, Tags = new ObservableCollection<Tag>(){
            //        Tags[1],
            //        Tags[2],
            //    }, Date = new DateTime()},

            //    new Event{Label = "ED11", Name = "Djole Event", Description = "Descc", Type = Types[0], Alcohol = AlcoholStatus.Allowed, Icon="pin.png", Handicap = HandicapStatus.Accesible,
            //    Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.Low, Tags = new ObservableCollection<Tag>(){
            //        Tags[0],
            //        Tags[3],
            //    }, Date = new DateTime()},

            //    new Event{Label = "EE12", Name = "Djole Event", Description = "Descc", Type = Types[1], Alcohol = AlcoholStatus.Sold, Icon="pin.png", Handicap = HandicapStatus.Inaccesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(){
            //        Tags[0],
            //        Tags[1],
            //        Tags[2],
            //    }, Date = new DateTime()},

            //    new Event{Label = "EC13", Name = "Djole Event", Description = "Descc", Type = Types[2], Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Inaccesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.High, Tags = new ObservableCollection<Tag>(){
            //        Tags[3],
            //        Tags[4],
            //    }, Date = new DateTime()},

            //    new Event{Label = "ED14", Name = "Djole Event", Description = "Descc", Type = Types[3], Alcohol = AlcoholStatus.Allowed, Icon="pin.png", Handicap = HandicapStatus.Accesible,
            //    Smoking = SmokingStatus.Prohibited, Space = Space.Outdoors, Audience = TargetAudience.Children, Price = Price.Low, Tags = new ObservableCollection<Tag>(){
            //        Tags[2],
            //        Tags[3],
            //    }, Date = new DateTime()},

            //    new Event{Label = "EE15", Name = "Djole Event", Description = "Descc", Type = Types[4], Alcohol = AlcoholStatus.Sold, Icon="pin.png", Handicap = HandicapStatus.Inaccesible,
            //    Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(){
            //        Tags[0],
            //        Tags[1],
            //    }, Date = new DateTime()}
            //};

            //Events = new ObservableCollection<Event>(EventList);

            //eventHelper.JsonSerialize(Events, "events.json");
            //tagHelper.JsonSerialize(Tags, "tags.json");
            //typeHelper.JsonSerialize(Types, "types.json");


            Tags = tagHelper.JsonDeserialize("tags.json");
            Types = typeHelper.JsonDeserialize("types.json");
            Events = eventHelper.JsonDeserialize("events.json");

            canvases = new List<Canvas>
            {
                Canvas1,
                Canvas2,
                Canvas3,
                Canvas4
            };

            initMaps();

            ViewType = CollectionViewSource.GetDefaultView(Types);
            ViewTag = CollectionViewSource.GetDefaultView(Tags);
            View = CollectionViewSource.GetDefaultView(Events);

            var customFilter = new Predicate<object>(ComplexFilter);
            var typeFilter = new Predicate<object>(TypeFilter);
            var tagsFilter = new Predicate<object>(TagFilter);
         
            View.Filter = customFilter;
            ViewType.Filter = typeFilter;
            ViewTag.Filter = tagsFilter;
        }

        private bool TagFilter(object obj)
        {
            if (obj != null)
            {
                if (((Tag)obj).Label.Contains(SearchTags.Text))
                    return true;
                return false;
            }
            return true;
        }

        private bool TypeFilter(object obj)
        {
            bool search = true;
            bool filter = true;

            if (obj != null)
            {
                if (!((Models.Type)obj).Label.Contains(SearchTypes.Text))
                    search = false;

                if (Display_Unused_Types.IsChecked == true)
                {
                    Models.Type type = obj as Models.Type;
                    foreach (Event ev in Events)
                    {
                        if (ev.Type.Label.Equals(type.Label))
                        {
                            filter = false;
                            break;
                        }
                    }
                }
            }

            return search && filter;
        }

        private bool ComplexFilter(object obj)
        {
            if (obj != null)
            {
                if (!SearchLabel.Text.Equals(""))
                {
                    SearchPredicate search = new SearchPredicate(SearchLabel.Text);
                    if (!search.IsMatch((Event)obj))
                        return false;
                }

                if (Display_Map.IsChecked == true && ((Event)obj).Points[Maps.SelectedIndex].X == -1)
                {
                    return false;
                }

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
                    img.Source = new BitmapImage(new Uri(selectedEvent.Icon, UriKind.RelativeOrAbsolute));
                }
                catch (Exception)
                {
                    return;
                }
                // #1 Treba prozor da se implementira
                foreach (FrameworkElement fe in activeCanvas.Children)
                {
                    if (fe.Name.Equals(selectedEvent.Label))
                    {
                        return;
                    }
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
            double foundElementX = backupX;
            double foundElementY = backupY;
            if (e.Data.GetDataPresent("eventFormat"))
            {
                Event m = e.Data.GetData("eventFormat") as Event;
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(m.Icon, UriKind.RelativeOrAbsolute));
                img.Name = m.Label;
                img.Height = 50;
                img.Width = 50;
                img.Tag = m.Label;
                img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                img.PreviewMouseMove += DraggedImageMouseMove;

                var positionX = e.GetPosition(sender as Canvas).X;
                var positionY = e.GetPosition(sender as Canvas).Y;

                var r1 = new Rect(positionX - img.Width / 2.0, positionY - img.Height / 2.0, img.Width, img.Height);
                Rect r2;
                foreach (FrameworkElement fe in (sender as Canvas).Children)
                {
                    r2 = new Rect(Canvas.GetLeft(fe), Canvas.GetTop(fe), fe.ActualWidth, fe.ActualHeight);
                    if (r1.IntersectsWith(r2))
                    {
                        (sender as Canvas).Children.Add(img);
                        Canvas.SetLeft(img, foundElementX);
                        Canvas.SetTop(img, foundElementY);

                        TextBlock eventName2= new TextBlock
                        {
                            Text = img.Name,
                            Tag = img.Name,
                            Foreground = Brushes.Black
                        };
                        (sender as Canvas).Children.Add(eventName2);
                        Canvas.SetLeft(eventName2, Canvas.GetLeft(img));
                        Canvas.SetTop(eventName2, Canvas.GetTop(img) - img.Height / 2.0);

                        m.Points[Maps.SelectedIndex].X = oldPos.X;
                        m.Points[Maps.SelectedIndex].Y = oldPos.Y;

                        oldPos.X = -1;
                        oldPos.Y = -1;
                        return;
                    }
                }

                m.Points[Maps.SelectedIndex].X = positionX;
                m.Points[Maps.SelectedIndex].Y = positionY;

                (sender as Canvas).Children.Add(img);
                Canvas.SetLeft(img, positionX - img.Width / 2.0);
                Canvas.SetTop(img, positionY - img.Height / 2.0);

                TextBlock eventName = new TextBlock
                {
                    Text = img.Name,
                    Tag = img.Name,
                    Foreground = Brushes.Black
                };
                (sender as Canvas).Children.Add(eventName);
                Canvas.SetLeft(eventName, Canvas.GetLeft(img));
                Canvas.SetTop(eventName, Canvas.GetTop(img) - img.Height / 2.0);

                eventHelper.JsonSerialize(Events, "events.json");
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
                    oldPos.X = man.Points[Maps.SelectedIndex].X;
                    oldPos.Y = man.Points[Maps.SelectedIndex].Y;
                    man.Points[Maps.SelectedIndex].X = -1.0;
                    man.Points[Maps.SelectedIndex].Y = -1.0;                    
                    TableEvent.SelectedItem = man;   
                    SelectedEvent = man;
                    break;
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

            eventHelper.JsonSerialize(Events, "events.json");

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
                backupY = Canvas.GetTop(img);
                backupX = Canvas.GetLeft(img);
                Canvas canvas = img.Parent as Canvas;
                canvas.Children.Remove(img);

                Event selectedEvent = null;                               

                selectedEvent = SelectedEvent;

                FrameworkElement foundLabel = null;
                foreach (FrameworkElement fe in canvas.Children)
                {
                    if (fe.Tag == img.Tag && fe.GetType().Equals(typeof(TextBlock)))
                    {
                        foundLabel = fe;
                        break;
                    }
                }
                if (foundLabel != null)
                    canvas.Children.Remove(foundLabel);

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
            foreach (FrameworkElement fe in activeCanvas.Children)
            {
                if (e.GetPosition(activeCanvas).X >= Canvas.GetLeft(fe) && e.GetPosition(activeCanvas).X <= Canvas.GetLeft(fe) + fe.Width &&
                        e.GetPosition(activeCanvas).Y >= Canvas.GetTop(fe) && e.GetPosition(activeCanvas).Y <= Canvas.GetTop(fe) + fe.Height)
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
                foreach (FrameworkElement fe in (activeCanvas).Children)
                {
                    if (e.GetPosition(activeCanvas).X >= Canvas.GetLeft(fe) && e.GetPosition(activeCanvas).X <= Canvas.GetLeft(fe) + fe.Width &&
                        e.GetPosition(activeCanvas).Y >= Canvas.GetTop(fe) && e.GetPosition(activeCanvas).Y <= Canvas.GetTop(fe) + fe.Height)
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
        private void RefreshTypes(object sender, RoutedEventArgs e)
        {
            ViewType?.Refresh();
        }

        private void RefreshTags(object sender, RoutedEventArgs e)
        {
            ViewTag?.Refresh();
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

            Display_Map.IsChecked = false;
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
            for (int i = 0; i < canvases.Count; i++)
            {
                var row = GetParent<DataGridRow>((Button)sender);
                Event ev = ((Event)row.Item);

                FrameworkElement foundImg = null;
                FrameworkElement foundTxt = null;

                foreach (FrameworkElement fe in canvases[i].Children)
                {
                    if (fe.Tag.Equals(ev.Label) && fe.GetType().Equals(typeof(Image)))
                    {
                        foundImg = fe;
                    }
                    if (fe.Tag.Equals(ev.Label) && fe.GetType().Equals(typeof(TextBlock)))
                    {
                        foundTxt = fe;
                    }
                    if (foundImg != null && foundTxt != null)
                    {
                        break;
                    }
                }

                if (foundImg != null)
                {
                    canvases[i].Children.Remove(foundImg);
                    canvases[i].Children.Remove(foundTxt);
                }
                Events.Remove(ev);               
                View.Refresh();

                
            }
            eventHelper.JsonSerialize(Events, "events.json");
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
            foreach (Event ev in Events)
            {
                if (ev.Type.Label.Equals(((row.Item) as Models.Type).Label)){
                    return;
                }
            }
            Types.Remove((Models.Type)row.Item);
            typeHelper.JsonSerialize(Types, "types.json");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var addDialog = new TypeModal(this, new Models.Type());
            addDialog.ShowDialog();
        }

        private void Tag_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            var addDialog = new TagModal(this, (Models.Tag)row.Item);
            addDialog.ShowDialog();
        }

        private void Tag_PreviewMouseLeftButtonDownDelete(object sender, MouseButtonEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);            
            Tags.Remove((Models.Tag)row.Item);
            tagHelper.JsonSerialize(Tags, "tags.json");
            updateEvents((Models.Tag)row.Item);
            eventHelper.JsonSerialize(Events, "events.json");
            View.Refresh();
        }

        private void Button_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var addDialog = new TagModal(this, new Models.Tag());
            addDialog.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(AllTabs.Visibility == Visibility.Hidden)
            {
                LeftCol.Width = new GridLength(800.0);
                AllTabs.Visibility = Visibility.Visible;
            }
            else
            {
                LeftCol.Width = new GridLength(50.0);
                AllTabs.Visibility = Visibility.Hidden;
            }            
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //var customFilter = new SearchPredicate(SearchLabel.Text);        
            //List<Event> list = Events.ToList();
            //list.Sort((ev1, ev2) => customFilter.compareEvents(ev1, ev2));
            //Events = new ObservableCollection<Event>(list);
            View.Refresh();
            for (int i = 0; i < canvases.Count; i++)
            {
                foreach (FrameworkElement fe in canvases[i].Children)
                {
                    if (!checkIfExist(fe))
                    {
                        fe.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        fe.Visibility = Visibility.Visible;

                    }
                }
            }


            //Thread th1 = new Thread(filterMap);
            //th1.Start();
            //Pocetak search-a na mapi heheh

            //var filteredList = Events.Where(customFilter.IsMatch);
            //List<Event> list = filteredList.ToList();
            //list.Sort((ev1, ev2) => customFilter.compareEvents(ev1, ev2));
            //TableEvent.ItemsSource = list;
            //View. = CollectionViewSource.GetDefaultView(new ObservableCollection<Event>(list));
        }
        private bool checkIfExist(FrameworkElement fe)
        {
            foreach (Event eventIter in View)
            {
                if (fe.Tag.Equals(eventIter.Label))
                {
                    return true;
                }
            }
            return false;
        }
        private void updateEvents(Tag removedTag)
        {
            foreach (Event e in Events)
            {
                if (e.Tags.Contains(removedTag))
                {
                    e.Tags.Remove(removedTag);
                }
            }
        }

        private void initMaps()
        {        
            foreach (Event m in Events)
            {
                for (int i = 0; i < m.Points.Length; i++)
                {
                    if(m.Points[i].X == -1)
                    {
                        continue;
                    }
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(m.Icon, UriKind.RelativeOrAbsolute));
                    img.Name = m.Label;
                    img.Height = 50;
                    img.Width = 50;
                    img.Tag = m.Label;
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    img.PreviewMouseMove += DraggedImageMouseMove;

                    var positionX = m.Points[i].X;
                    var positionY = m.Points[i].Y;

                    canvases[i].Children.Add(img);
                    Canvas.SetLeft(img, positionX - img.Width / 2.0);
                    Canvas.SetTop(img, positionY - img.Height / 2.0);

                    TextBlock eventName = new TextBlock
                    {
                        Text = img.Name,
                        Tag = img.Tag,
                        Name = img.Name,
                        Foreground = Brushes.Black
                    };
                    canvases[i].Children.Add(eventName);
                    Canvas.SetLeft(eventName, Canvas.GetLeft(img));
                    Canvas.SetTop(eventName, Canvas.GetTop(img) - img.Height / 2.0);
                }                
            }            
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Maps.SelectedIndex)
            {
                case 0:
                    activeCanvas = Canvas1;
                    scrollViewer = scrollViewer1;
                    break;
                case 1:
                    activeCanvas = Canvas2;
                    scrollViewer = scrollViewer2;
                    break;
                case 2:
                    activeCanvas = Canvas3;
                    scrollViewer = scrollViewer3;
                    break;
                case 3:
                    activeCanvas = Canvas4;
                    scrollViewer = scrollViewer4;
                    break;
            }
            View.Refresh();
            ViewTag.Refresh();
            ViewType.Refresh();
        }

        private void SaveEvents()
        {
            eventHelper.JsonSerialize(Events, "events.json");
        }

        private void SaveTags()
        {
            tagHelper.JsonDeserialize("tags.json");
        }

        private void SaveTypes()
        {
            typeHelper.JsonDeserialize("types.json");
        }
    }
}
