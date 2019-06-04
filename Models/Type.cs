using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat2.Models
{
    public class Type
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public override string ToString()
        {
            return Label;
        }
    }
}
