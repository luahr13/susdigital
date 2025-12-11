namespace projetoTP3_A2.Models.Enum
{
    public enum ExameStatus
    {
        Pendente = 1,   // Médico solicitou, paciente ainda não enviou
        Realizado = 2,  // Paciente enviou arquivo(s)
        Disponivel = 3,  // Médico validou e liberou resultado
        Negado = 4     // Médico negou o exame após análise
    }
}
