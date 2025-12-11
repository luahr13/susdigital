using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Prontuario
    {
        public Guid Id { get; set; }

        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public Guid MedicoId { get; set; }
        public Medico Medico { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        // Conteúdo clínico
        public string Observacoes { get; set; }
        public List<MedicamentoProntuario> Medicamentos { get; set; } = new();
        public List<ArquivoProntuario> Arquivos { get; set; } = new();
        // Status do prontuário
        public ProntuarioStatus Status { get; set; } = ProntuarioStatus.Aberto;
        // Novo: lista de exames vinculados ao prontuário
        public List<Exame> Exames { get; set; } = new();
    }
}
