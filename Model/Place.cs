using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConcertShowRestServer.Model
{
    public partial class Place
    {
        public Place()
        {
            Spectacle = new HashSet<Spectacle>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [XmlIgnore]
        public HashSet<Spectacle> Spectacle { get; set; }
    }
}
