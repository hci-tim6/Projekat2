using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat2.Models
{
    class SearchPredicate
    {
        public SearchPredicate(string query)
        {
            Query = query;
        }
        public string Query { get; set; }
        public Predicate<Object> Match
        {
            get { return IsMatch; }
        }
        private bool IsMatch(Object obj)
        {
            Event e = (Event) obj;
            bool retVal = false;
            if (e.Label.ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Name.ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Description.ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Type.Name.ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Alcohol.ToString().ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Handicap.ToString().ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Smoking.ToString().ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Space.ToString().ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Price.ToString().ToLower().Contains(Query))
            {
                retVal = true;
            }
            if (e.Audience.ToString().ToLower().Contains(Query))
            {
                retVal = true;
            }
            return retVal;
        }
    }
}
