namespace projetoTP3_A2.Models.Enum
{
    public enum ProntuarioStatus
    {
        Aberto = 1,           // Prontuário em andamento, médico pode editar, paciente pode responder exames
        PendenteExame = 2,    // Médico solicitou exame, aguardando envio do paciente
        ExameRecebido = 3,    // Paciente enviou exame, aguardando validação do médico
        Fechado = 4,          // Prontuário concluído, bloqueado para edição
        Reaberto = 5          // Reaberto por admin para correções ou exceções
    }
}