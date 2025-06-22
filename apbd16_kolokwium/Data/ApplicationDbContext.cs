using System;
using ExampleTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<ExhibitionArtwork> ExhibitionArtworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExhibitionArtwork>()
                .HasKey(ea => new { ea.ExhibitionId, ea.ArtworkId });

            modelBuilder.Entity<Gallery>()
                .HasData(
                    new Gallery { GalleryId = 1, Name = "Modern Art Space", EstablishedDate = new DateTime(2001, 9, 12) }
                );

            modelBuilder.Entity<Artist>()
                .HasData(
                    new Artist { ArtistId = 1, FirstName = "Pablo", LastName = "Picasso", BirthDate = new DateTime(1881, 10, 25) },
                    new Artist { ArtistId = 2, FirstName = "Frida", LastName = "Kahlo", BirthDate = new DateTime(1907, 7, 6) }
                );

            modelBuilder.Entity<Artwork>()
                .HasData(
                    new Artwork { ArtworkId = 1, ArtistId = 1, Title = "Guernica", YearCreated = 1937 },
                    new Artwork { ArtworkId = 2, ArtistId = 2, Title = "The two fridas", YearCreated = 1939 }
                );

            modelBuilder.Entity<Exhibition>()
                .HasData(
                    new Exhibition { ExhibitionId = 1, GalleryId = 1, Title = "20th Century Giants", StartDate = new DateTime(2024, 5, 1), EndDate = new DateTime(2024, 9, 1), NumberOfArtworks = 2 }
                );

            modelBuilder.Entity<ExhibitionArtwork>()
                .HasData(
                    new ExhibitionArtwork { ExhibitionId = 1, ArtworkId = 1, InsuranceValue = 1000000.00m },
                    new ExhibitionArtwork { ExhibitionId = 1, ArtworkId = 2, InsuranceValue = 800000.00m }
                );
        }
    }
}
