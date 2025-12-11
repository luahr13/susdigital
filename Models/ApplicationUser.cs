using Microsoft.AspNetCore.Identity;
using projetoTP3_A2.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace projetoTP3_A2.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Nome { get; set; }
        
        public DateTime DataNascimento { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? UltimoLoginEm { get; set; }
        public Perfis Perfil { get; set; }

        // Campo CPF já existe no banco, então apenas mapeie
        public string? CPF { get; set; }
    }
}