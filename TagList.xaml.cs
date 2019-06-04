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
    /// Interaction logic for TagList.xaml
    /// </summary>
    public partial class TagList : Window
    {
        public ObservableCollection<Tag> _eventTags;
        public ObservableCollection<Tag> _usableTags;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
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

        public TagList(Window owner, ObservableCollection<Tag> eventTags, ObservableCollection<Tag> alltags)
        {
            this.Owner = owner;
            this.DataContext = this;
            _eventTags = eventTags ?? new ObservableCollection<Tag>();
            _usableTags = new ObservableCollection<Tag>();

            foreach (Tag tag in alltags) {
                bool used = false;
                foreach (Tag used_tag in _eventTags)
                {
                    if (tag.Label.Equals(used_tag.Label))
                    {
                        used = true;
                        break;
                    }
                }
                if (!used)
                {
                    _usableTags.Add(tag);
                }
            }

            ViewTag = CollectionViewSource.GetDefaultView(_usableTags);

            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var selected in TxtTags.SelectedItems)
            {
                _eventTags.Add((Models.Tag)selected);
            }
            (Owner as EventModal).View?.Refresh();
            this.Close();
        }
    }
}
