using System.Linq;
using System.Threading.Tasks;
using ExampleTest2.Data;
using ExampleTest2.DTOs;
using ExampleTest2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExampleTest2.Controllers
{
    [ApiController]
    [Route("api/galleries")]
    public class GalleriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GalleriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/exhibitions")]
        public async Task<ActionResult<GalleryExhibitionsDto>> GetExhibitions(int id)
        {
            var gallery = await _context.Galleries
                .Include(g => g.Exhibitions)
                    .ThenInclude(e => e.ExhibitionArtworks)
                        .ThenInclude(ea => ea.Artwork)
                            .ThenInclude(a => a.Artist)
                .SingleOrDefaultAsync(g => g.GalleryId == id);
            if (gallery == null)
                return NotFound();
            var result = new GalleryExhibitionsDto
            {
                GalleryId = gallery.GalleryId,
                Name = gallery.Name,
                EstablishedDate = gallery.EstablishedDate,
                Exhibitions = gallery.Exhibitions.Select(e => new ExhibitionDto
                {
                    Title = e.Title,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    NumberOfArtworks = e.NumberOfArtworks,
                    Artworks = e.ExhibitionArtworks.Select(ea => new ArtworkDto
                    {
                        Title = ea.Artwork.Title,
                        YearCreated = ea.Artwork.YearCreated,
                        InsuranceValue = ea.InsuranceValue,
                        Artist = new ArtistDto
                        {
                            FirstName = ea.Artwork.Artist.FirstName,
                            LastName = ea.Artwork.Artist.LastName,
                            BirthDate = ea.Artwork.Artist.BirthDate
                        }
                    }).ToList()
                }).ToList()
            };
            return Ok(result);
        }
    }
}
