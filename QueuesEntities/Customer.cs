using System;
using System.Collections.Generic;
using System.Text;

namespace RafaelMillis.QueueEntities
{
    public class Customer
    {
        public int QueueID { get; set; }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
    }
}
