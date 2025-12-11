using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Models;
using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        internal object Medicamentos;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmacia> Farmacia { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasDiscriminator<Perfis>("Perfil")
                .HasValue<ApplicationUser>(Perfis.Administrador)
                .HasValue<Medico>(Perfis.Medico)
                .HasValue<Farmaceutico>(Perfis.Farmaceutico)
                .HasValue<Paciente>(Perfis.Paciente);

            // Configuração de MedicamentoProntuario
            builder.Entity<MedicamentoProntuario>()
                .HasOne(mp => mp.Prontuario)
                .WithMany(p => p.Medicamentos)
                .HasForeignKey(mp => mp.ProntuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MedicamentoProntuario>()
                .HasOne(mp => mp.Medicamento)
                .WithMany()
                .HasForeignKey(mp => mp.MedicamentoId);

            // Configuração de ArquivoProntuario
            builder.Entity<ArquivoProntuario>()
                .HasOne(a => a.Prontuario)
                .WithMany(p => p.Arquivos)
                .HasForeignKey(a => a.ProntuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Exame
            builder.Entity<Exame>()
                .HasOne(e => e.Prontuario)
                .WithMany(p => p.Exames)
                .HasForeignKey(e => e.ProntuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // ArquivoExame
            builder.Entity<ArquivoExame>()
                .HasOne(ae => ae.Exame)
                .WithMany(e => e.Arquivos)
                .HasForeignKey(ae => ae.ExameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MedicamentoProntuario>()
                .HasOne(mp => mp.Farmaceutico)
                .WithMany()
                .HasForeignKey(mp => mp.FarmaceuticoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<projetoTP3_A2.Models.Medicamento> Medicamento { get; set; } = default!;
        public DbSet<projetoTP3_A2.Models.Patologia> Patologia { get; set; } = default!;
        public DbSet<projetoTP3_A2.Models.Alergia> Alergia { get; set; } = default!;
        public DbSet<projetoTP3_A2.Models.Prontuario> Prontuario { get; set; } = default!;
        public DbSet<projetoTP3_A2.Models.MedicamentoProntuario> MedicamentoProntuario { get; set; } = default!;
        public DbSet<projetoTP3_A2.Models.ArquivoProntuario> ArquivoProntuario { get; set; } = default!;
        public DbSet<projetoTP3_A2.Models.Exame> Exames { get; set; }
        public DbSet<projetoTP3_A2.Models.ArquivoExame> ArquivosExame { get; set; }
    }
}