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
        public bool IsMatch(Object obj)
        {
            Event e = (Event) obj;
            Query = Query.ToLower();
            if (e.Label.ToLower().Contains(Query))
            {
                return true;
            }
            if (e.Name.ToLower().Contains(Query))
            {
                return true;
            }
            if (e.Description.ToLower().Contains(Query))
            {
                return true;
            }
            if (e.Type.Name.ToLower().Contains(Query))
            {
                return true;
            }
            return false;
        }
        public int compareEvents(Event e1, Event e2)
        {
            int retVal = 0;
            int e1Val = eventEval(e1);
            int e2Val = eventEval(e2);
            if (e1Val < e2Val)
            {
                retVal = 1;
            } else if (e1Val == e2Val)
            {
                retVal = 0;
            }
            else
            {
                retVal = -1;
            }
            return retVal;
        }
        private int eventEval(Event e1)
        {
            int retVal = 0;
            if (e1.Label.ToLower().Contains(Query))
            {
                retVal += 25; 
            }
            if (e1.Name.ToLower().Contains(Query))
            {
                retVal += 15;
            }
            if (e1.Description.ToLower().Contains(Query))
            {
                retVal += 4;
            }
            if (e1.Type.Name.ToLower().Contains(Query))
            {
                retVal += 3;
            }
            return retVal;
        }
    }
}
