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
        public DragTutorial(Window owner)
        {
            InitializeComponent();
            DataContext = this;
            this.Owner = owner;
            TutorialTxt.FontSize = 18;
            TutorialTxt.Text = "Welcome to drag and drop tutorial!\n\n" +
                "On the left side of the window is the table containing 2 events. Try using the mouse to drag the event to the map on the right. " +
                "Hover your mouse over the event you want to drag, press left click and keep holding. " +
                "Move the mouse over the map on the right while still holding left mouse button. " +
                "When you decide to drop the item, release the left mouse button."; 
            
            List<Event> EventList = new List<Event> {
                new Event{Label = "tut_ev1", Name = "Rave 3000", Description = "The best rave there is", Type = new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" }, Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Allowed, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Low, Tags = new ObservableCollection<Tag>(), Date = DateTime.Now
                },
                new Event{Label = "tut_ev2", Name = "Hammerfall", Description = "The band hammerfall is having a concert here", Type = new Models.Type{Label = "TL10", Name = "TN10", Description = "TDESC10", Icon="pin.png" }, Alcohol = AlcoholStatus.Prohibited, Icon="pin.png", Handicap = HandicapStatus.Accesible,
                Smoking = SmokingStatus.Prohibited, Space = Space.Indoors, Audience = TargetAudience.Adults, Price = Price.Free, Tags = new ObservableCollection<Tag>(), Date = DateTime.Now
                }
            };

            Events = new ObservableCollection<Event>(EventList);
            View = CollectionViewSource.GetDefaultView(Events);
        }

        private void Canvas_DragEnterTutorial(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("eventFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
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
                
                TutorialCanvas.Children.Remove(img);

                Event selectedEvent = null;

                selectedEvent = SelectedEvent;

                FrameworkElement foundLabel = null;
                foreach (FrameworkElement fe in TutorialCanvas.Children)
                {
                    if (fe.Tag == img.Tag && fe.GetType().Equals(typeof(TextBlock)))
                    {
                        foundLabel = fe;
                        break;
                    }
                }
                if (foundLabel != null)
                    TutorialCanvas.Children.Remove(foundLabel);

                DataObject dragData = new DataObject("eventFormat", selectedEvent);
                DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);
                e.Handled = true;
            }


        }

        private void Canvas_DropTutorial(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("eventFormat"))
            {
                Event m = e.Data.GetData("eventFormat") as Event;
                foreach (FrameworkElement fe in TutorialCanvas.Children)
                {
                    if (fe.Name.Equals(m.Label))
                    {
                        MessageBox.Show("This event already exists on map!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
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

                m.Points[0].X = positionX;
                m.Points[0].Y = positionY;

                ToolTip tt = createToolTipEvent(m);
                img.ToolTip = tt;

                (sender as Canvas).Children.Add(img);
                Canvas.SetLeft(img, positionX - img.Width / 2.0);
                Canvas.SetTop(img, positionY - img.Height / 2.0);

                TextBlock eventName = new TextBlock
                {
                    Text = img.Name,
                    Tag = img.Name,
                    Foreground = Brushes.Black,
                    FontSize = 14.0,
                    Background = Brushes.White,
                    FontWeight = FontWeights.Bold,
                    Padding = new Thickness(3)
                };
                (sender as Canvas).Children.Add(eventName);
                Canvas.SetLeft(eventName, Canvas.GetLeft(img));
                Canvas.SetTop(eventName, Canvas.GetTop(img) - img.Height / 2.0);

                View.Refresh();

                if (!completed)
                {
                    TutorialTxt.Text += "\n\nGreat job! You are now a master of dragging and dropping items!\n" +
                    "You can exit the tutorial by pressing X in the top right corner or keep playing with the drag and drop functionality.";
                    completed = true;
                }
            }
        }

        public ToolTip createToolTipEvent(Event m)
        {
            WrapPanel wp = new WrapPanel();
            wp.Orientation = Orientation.Vertical;
            wp.MaxWidth = 300;

            TextBlock nameTxtBlock = new TextBlock();
            nameTxtBlock.TextWrapping = TextWrapping.Wrap;
            nameTxtBlock.FontSize = 14;
            nameTxtBlock.Text = "Name: " + m.Name;
            wp.Children.Add(nameTxtBlock);

            TextBlock typeTxtBlock = new TextBlock();
            typeTxtBlock.TextWrapping = TextWrapping.Wrap;
            typeTxtBlock.FontSize = 14;
            typeTxtBlock.Text = "Type: " + m.Type.Label;
            wp.Children.Add(typeTxtBlock);

            TextBlock dateTxtBlock = new TextBlock();
            dateTxtBlock.TextWrapping = TextWrapping.Wrap;
            dateTxtBlock.FontSize = 14;
            dateTxtBlock.Text = "Date: " + m.FormattedDate;
            wp.Children.Add(dateTxtBlock);

            TextBlock alcoholTxtBlock = new TextBlock();
            alcoholTxtBlock.TextWrapping = TextWrapping.Wrap;
            alcoholTxtBlock.FontSize = 14;
            alcoholTxtBlock.Text = "Alcohol: " + m.Alcohol;
            wp.Children.Add(alcoholTxtBlock);

            TextBlock handicapTxtBlock = new TextBlock();
            handicapTxtBlock.TextWrapping = TextWrapping.Wrap;
            handicapTxtBlock.FontSize = 14;
            handicapTxtBlock.Text = "Handicap: " + m.Handicap;
            wp.Children.Add(handicapTxtBlock);

            TextBlock smokingTxtBlock = new TextBlock();
            smokingTxtBlock.TextWrapping = TextWrapping.Wrap;
            smokingTxtBlock.FontSize = 14;
            smokingTxtBlock.Text = "Smoking: " + m.Smoking;
            wp.Children.Add(smokingTxtBlock);

            TextBlock spaceTxtBlock = new TextBlock();
            spaceTxtBlock.TextWrapping = TextWrapping.Wrap;
            spaceTxtBlock.FontSize = 14;
            spaceTxtBlock.Text = "Space: " + m.Space;
            wp.Children.Add(spaceTxtBlock);

            TextBlock audienceTxtBlock = new TextBlock();
            audienceTxtBlock.TextWrapping = TextWrapping.Wrap;
            audienceTxtBlock.FontSize = 14;
            audienceTxtBlock.Text = "Audience: " + m.Audience;
            wp.Children.Add(audienceTxtBlock);

            TextBlock priceTxtBlock = new TextBlock();
            priceTxtBlock.TextWrapping = TextWrapping.Wrap;
            priceTxtBlock.FontSize = 14;
            priceTxtBlock.Text = "Price: " + m.Price;
            wp.Children.Add(priceTxtBlock);

            TextBlock tagTxtBlock = new TextBlock();
            tagTxtBlock.TextWrapping = TextWrapping.Wrap;
            tagTxtBlock.FontSize = 14;
            if (m.Tags.ToList().Count == 0)
            {
                tagTxtBlock.Text = "No tags";
                wp.Children.Add(tagTxtBlock);
            }
            else
            {
                tagTxtBlock.Text = "Tags";
                WrapPanel wpTags = new WrapPanel();
                Border tagBorder;
                Label tagLabel;
                foreach (Tag item in m.Tags)
                {
                    tagBorder = new Border();
                    tagBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
                    tagBorder.Background = item.Color;
                    tagBorder.BorderThickness = new Thickness(1.5);
                    tagBorder.CornerRadius = new CornerRadius(6);

                    tagLabel = new Label();
                    tagLabel.FontSize = 14;
                    tagLabel.Content = item.Label;
                    tagLabel.Foreground = new SolidColorBrush(Colors.White);
                    tagLabel.Margin = new Thickness(0, 0, 3, 3);
                    tagLabel.Padding = new Thickness(2, 0, 2, 0);

                    tagBorder.Child = tagLabel;
                    wpTags.Children.Add(tagBorder);
                }
                wp.Children.Add(tagTxtBlock);
                wp.Children.Add(wpTags);
            }
            ToolTip tt = new ToolTip();
            tt.Content = wp;
            return tt;
        }

        Point startPoint = new Point();

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
                (Math.Abs(diff.X) > 10 * SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > 10 * SystemParameters.MinimumVerticalDragDistance))
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

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("eventFormat", selectedEvent);
                DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);
            }
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            foreach (Canvas c in (Owner as MainWindow).canvases)
            {
                c.AllowDrop = true;
            }
        }
    }
}
