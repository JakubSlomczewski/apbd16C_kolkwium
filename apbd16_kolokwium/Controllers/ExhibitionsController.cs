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
    [Route("api/exhibitions")]
    public class ExhibitionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ExhibitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExhibition(NewExhibitionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var gallery = await _context.Galleries.SingleOrDefaultAsync(g => g.Name == dto.Gallery);
            if (gallery == null)
                return NotFound($"Galeria o nazwie {dto.Gallery} nie znaleziona");
            var artworkIds = dto.Artworks.Select(a => a.ArtworkId).ToList();
            var artworks = await _context.Artworks.Where(a => artworkIds.Contains(a.ArtworkId)).ToListAsync();
            if (artworks.Count != artworkIds.Count)
                return NotFound("Nie znaleziono sztuki");
            var exhibition = new Exhibition
            {
                Title = dto.Title,
                GalleryId = gallery.GalleryId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                NumberOfArtworks = dto.Artworks.Count,
                ExhibitionArtworks = dto.Artworks.Select(a => new ExhibitionArtwork
                {
                    ArtworkId = a.ArtworkId,
                    InsuranceValue = a.InsuranceValue
                }).ToList()
            };
            _context.Exhibitions.Add(exhibition);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}