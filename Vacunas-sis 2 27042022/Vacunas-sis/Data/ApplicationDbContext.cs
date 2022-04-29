using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Vacunas_sis.Models;

namespace Vacunas_sis.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vacunas_sis.Models.Detalle_Vacuna> Detalle_Vacuna { get; set; }
        public DbSet<Vacunas_sis.Models.Direccion> Direccion { get; set; }
        public DbSet<Vacunas_sis.Models.Informacion_nino> Informacion_nino { get; set; }
        public DbSet<Vacunas_sis.Models.Registro> Registro { get; set; }
        public DbSet<Vacunas_sis.Models.Vacunas> Vacunas { get; set; }

        public DbSet<Vacunas_sis.Models.Cita> Citas { get; set; }
        public DbSet<Vacunas_sis.Models.Dosis> Doses { get; set; }
    }
}
