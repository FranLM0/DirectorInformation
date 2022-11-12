using DirectorInformation.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DirectorInformation.API.DbContexts
{
    public class DirectorInformationContext : DbContext
    {
        public DbSet<Director> Directors { get; set; } = null!;
        public DbSet<Film> Films { get; set; } = null!;



        public DirectorInformationContext(DbContextOptions<DirectorInformationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>().HasData(
                new Director("Steven Spielberg")
                {
                    Id = 1,
                    Description = "Se lo considera uno de los pioneros de la era del Nuevo Hollywood"
                },
                new Director("Quentin Tarantino")
                {
                    Id = 2,
                    Description = "PAPÁ"
                },
                new Director("Francis Ford Coppola")
                {
                    Id = 3,
                    Description = "TRILOGO"
                }
                );





            modelBuilder.Entity<Film>().HasData(
                new Film("Empire of the sun")
                {
                    Id = 1,
                    DirectorId = 1,
                    Description = "miPelicula favorita"
                },
                new Film("Schindle's List")
                {
                    Id = 2,
                    DirectorId = 1,
                    Description = "La de la niña con el abrigo rojo"
                },
                new Film("Django")
                {
                    Id = 3,
                    DirectorId = 2,
                    Description = "crítica al racismo norteamericano"
                },
                new Film("Kill Bill")
                {
                    Id = 4,
                    DirectorId = 2,
                    Description = "Mata a Bill"
                },
                new Film("The Godfather I")
                {
                    Id = 5,
                    DirectorId = 3,
                    Description = "Clasicazo"
                },
                new Film("The Godfather II")
                {
                    Id = 6,
                    DirectorId = 3,
                    Description = "Clasicazox2"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
