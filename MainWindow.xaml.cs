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
        public ObservableCollection<Event> Events { get; set; }

        Point startPoint = new Point();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            List<Event> EventList = new List<Event> {
                new Event{Label = "E1", Name = "Djole Event", Description = "Descc", Type = new Models.Type(), Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new List<Tag>()},
                new Event{Label = "E2", Name = "Djole Event", Description = "Descc", Type = new Models.Type(), Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new List<Tag>()}
            };

            Events = new ObservableCollection<Event>(EventList);

            View = CollectionViewSource.GetDefaultView(Events);
        }

        private void TableEvent_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void TableEvent_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                DataGrid dataGrid = sender as DataGrid;               
                Event selectedEvent = (Event) dataGrid.SelectedItem;

                Image img = new Image();  
                img.Source = new BitmapImage(new Uri("pack://application:,,,/"+selectedEvent.Icon));

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
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/" + m.Icon));
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
                }
            }

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

                Event selectedEvent = (Event) TableEvent.SelectedItem;

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
    }
}
