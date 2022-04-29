using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoEsteSi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoEsteSi.Models.Cita> Citas { get; set; }
        public DbSet<ProyectoEsteSi.Models.Direccion> Direcciones { get; set; }
        public DbSet<ProyectoEsteSi.Models.Dosi> Dosis { get; set; }
        public DbSet<ProyectoEsteSi.Models.Informacion_paciente> Pacientes { get; set; }
        public DbSet<ProyectoEsteSi.Models.Vacuna> Vacunas { get; set; }
    }
}
