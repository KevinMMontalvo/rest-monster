using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConcertShowRestServer.Model
{
    public partial class Category
    {
        public Category()
        {
            Spectacle = new HashSet<Spectacle>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }

        [XmlIgnore]
        public HashSet<Spectacle> Spectacle { get; set; }
    }
}
