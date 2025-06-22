using System;
using System.Collections.Generic;

namespace ExampleTest2.DTOs
{
    public class GalleryExhibitionsDto
    {
        public int GalleryId { get; set; }
        public string Name { get; set; }
        public DateTime EstablishedDate { get; set; }
        public List<ExhibitionDto> Exhibitions { get; set; }
    }
}