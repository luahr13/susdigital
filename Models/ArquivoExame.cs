namespace projetoTP3_A2.Models
{
    public class ArquivoExame
    {
        public Guid Id { get; set; }

        // Relacionamento com o exame
        public Guid ExameId { get; set; }
        public Exame Exame { get; set; }

        // Dados do arquivo
        public string NomeArquivo { get; set; }      // Nome original do arquivo
        public string Caminho { get; set; }          // Caminho relativo no servidor (ex: "uploads/exames/...")
        public DateTime UploadEm { get; set; } = DateTime.UtcNow;

        // Opcional: tipo de arquivo (PDF, imagem, etc.)
        public string Tipo { get; set; }
    }
}