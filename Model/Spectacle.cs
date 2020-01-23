using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConcertShowRestServer.Model
{
    public partial class Spectacle
    {
        public Spectacle()
        {
            Ticket = new HashSet<Ticket>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public long PlaceId { get; set; }
        public long CategoryId { get; set; }
        public byte[] Poster { get; set; }

        public Category Category { get; set; }
        public Place Place { get; set; }

        [XmlIgnore]
        public HashSet<Ticket> Ticket { get; set; }
    }
}
