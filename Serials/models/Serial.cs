using System;

namespace Serials.models
{
    internal class Serial
    {
        public int SerialID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreID { get; set; }
    }
}
