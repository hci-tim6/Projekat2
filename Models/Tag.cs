using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HCI_Projekat2.Models
{
    public class Tag
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public SolidColorBrush Color { get; set; }

        public override string ToString()
        {
            return Label;
        }
    }
}
