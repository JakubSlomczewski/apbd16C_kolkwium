using System;
using System.Collections.Generic;

namespace ExampleTest2.DTOs
{
    public class ExhibitionDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfArtworks { get; set; }
        public List<ArtworkDto> Artworks { get; set; }
    }
}