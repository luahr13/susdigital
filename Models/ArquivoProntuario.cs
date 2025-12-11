namespace projetoTP3_A2.Models
{
    public class ArquivoProntuario
    {
        public Guid Id { get; set; }
        public Guid ProntuarioId { get; set; }
        public Prontuario Prontuario { get; set; }

        public string NomeArquivo { get; set; }
        public string Caminho { get; set; }
        public DateTime UploadEm { get; set; } = DateTime.UtcNow;
    }
}
