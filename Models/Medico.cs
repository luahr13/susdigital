using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Medico : ApplicationUser
    {
        public string CRM { get; set; }
        public string Especialidade { get; set; }
    }
}
