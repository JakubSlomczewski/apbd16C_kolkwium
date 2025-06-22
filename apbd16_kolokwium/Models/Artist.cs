using System;
using System.Collections.Generic;

namespace ExampleTest2.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Artwork> Artworks { get; set; }
    }
}