using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Farmaceutico : ApplicationUser
    {
        public string RegistroProfissional { get; set; }   
        public string IdFarmacia { get; set; }
        public Perfis Papel { get; set; }
    }
}
