using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class MedicamentoProntuario
    {
        public int Id { get; set; }

        public Guid ProntuarioId { get; set; }
        public Prontuario Prontuario { get; set; }

        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; }

        public PrescricaoStatus Status { get; set; } = PrescricaoStatus.Pendente;

        public string Dosagem { get; set; }
        public string Frequencia { get; set; }
        public string Observacoes { get; set; }

        // Novo: código único da prescrição
        public string CodigoPrescricao { get; set; } = Guid.NewGuid().ToString("N");

        // Novo: data da baixa
        public DateTime? DataBaixa { get; set; }

        // Novo: farmacêutico responsável pela baixa
        public Guid? FarmaceuticoId { get; set; }
        public Farmaceutico? Farmaceutico { get; set; }
    }
}
