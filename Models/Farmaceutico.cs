using System.ComponentModel.DataAnnotations;

namespace projetoTP3_A2.Models
{
    public class Farmaceutico : ApplicationUser
    {
        [Required]
        [Display(Name = "CRF")] // Conselho Regional de Farmácia
        public string CRF { get; set; }

        [Display(Name = "Área de atuação")]
        public string AreaAtuacao { get; set; }
    }
}
