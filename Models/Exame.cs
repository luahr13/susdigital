using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Exame
    {
        public Guid Id { get; set; }

        // Relacionamento com o prontuário
        public Guid ProntuarioId { get; set; }
        public Prontuario Prontuario { get; set; }

        // Informações do exame
        public string Nome { get; set; }            // Ex: "Hemograma", "Raio-X"
        public string Observacoes { get; set; }     // Observações do médico sobre o exame
        public ExameStatus Status { get; set; } = ExameStatus.Pendente;

        // Arquivo(s) do exame enviado pelo paciente
        public List<ArquivoExame> Arquivos { get; set; } = new();

        // Data de solicitação e conclusão
        public DateTime SolicitadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? ConcluidoEm { get; set; }
        public DateTime? ValidadoEm { get; set; }
        // Novo campo para justificativa de negação
        public string? ObservacaoNegacao { get; set; }
    }
}