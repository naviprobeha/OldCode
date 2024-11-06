using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class ItemCalendarBookingCollection : CollectionBase
    {
        public ItemCalendarBooking this[int index]
        {
            get { return (ItemCalendarBooking)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ItemCalendarBooking itemCalendarBooking)
        {
            return (List.Add(itemCalendarBooking));
        }
        public int IndexOf(ItemCalendarBooking itemCalendarBooking)
        {
            return (List.IndexOf(itemCalendarBooking));
        }
        public void Insert(int index, ItemCalendarBooking itemCalendarBooking)
        {
            List.Insert(index, itemCalendarBooking);
        }
        public void Remove(ItemCalendarBooking itemCalendarBooking)
        {
            List.Remove(itemCalendarBooking);
        }
        public bool Contains(ItemCalendarBooking itemCalendarBooking)
        {
            return (List.Contains(itemCalendarBooking));
        }


    }
}
