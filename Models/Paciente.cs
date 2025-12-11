using projetoTP3_A2.Models.Enum;
using System.Xml.Linq;

namespace projetoTP3_A2.Models
{
    public class Paciente : ApplicationUser
    {
        public Sexo Sexo { get; set; }
        public Raca Raca { get; set; }
    }
}
