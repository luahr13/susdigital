using System;
using System.ComponentModel.DataAnnotations;

namespace projetoTP3_A2.Models
{
    public class Farmacia
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da farmácia é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres.")]
        public string Endereco { get; set; }
    }
}
