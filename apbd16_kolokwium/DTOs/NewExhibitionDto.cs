using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExampleTest2.DTOs
{
    public class NewExhibitionDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Gallery { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public List<NewExhibitionArtworkDto> Artworks { get; set; }
    }

    public class NewExhibitionArtworkDto
    {
        [Required]
        public int ArtworkId { get; set; }
        [Required]
        public decimal InsuranceValue { get; set; }
    }
}