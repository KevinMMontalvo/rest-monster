using System;
using System.Collections.Generic;

namespace ConcertShowRestServer.Model
{
    public partial class Ticket
    {
        public long Id { get; set; }
        public long LocationId { get; set; }
        public long SpectacleId { get; set; }
        public int Availability { get; set; }
        public float Price { get; set; }

        public Location Location { get; set; }
        public Spectacle Spectacle { get; set; }
    }
}
