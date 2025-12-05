using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Medicamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Dosagem { get; set; }
        public TipoMedicamento Tipo { get; set; }
        public string Frequencia { get; set; }
        public string Observacoes { get; set; }
    }
}
