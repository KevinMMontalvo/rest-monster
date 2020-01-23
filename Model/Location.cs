using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConcertShowRestServer.Model
{
    public partial class Location
    {
        public Location()
        {
            Ticket = new HashSet<Ticket>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        [XmlIgnore]
        public HashSet<Ticket> Ticket { get; set; }
    }
}
