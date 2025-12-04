using Microsoft.AspNetCore.Identity;
using projetoTP3_A2.Models;
using projetoTP3_A2.Models.Enum;

public static class SeedPacientes
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        string senhaPadrao = "Luahr123@";

        var pacientes = new List<ApplicationUser>
        {
            new ApplicationUser { UserName = "joao.silva@teste.com", Email = "joao.silva@teste.com", Nome = "João Silva", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "maria.souza@teste.com", Email = "maria.souza@teste.com", Nome = "Maria Souza", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "carlos.pereira@teste.com", Email = "carlos.pereira@teste.com", Nome = "Carlos Pereira", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "ana.lima@teste.com", Email = "ana.lima@teste.com", Nome = "Ana Lima", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "paulo.mendes@teste.com", Email = "paulo.mendes@teste.com", Nome = "Paulo Mendes", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "fernanda.alves@teste.com", Email = "fernanda.alves@teste.com", Nome = "Fernanda Alves", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "ricardo.gomes@teste.com", Email = "ricardo.gomes@teste.com", Nome = "Ricardo Gomes", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "juliana.rocha@teste.com", Email = "juliana.rocha@teste.com", Nome = "Juliana Rocha", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "marcos.santos@teste.com", Email = "marcos.santos@teste.com", Nome = "Marcos Santos", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "patricia.martins@teste.com", Email = "patricia.martins@teste.com", Nome = "Patrícia Martins", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "carlos.pereira@teste.com", Email = "carlos.pereira@teste.com", Nome = "Carlos Pereira", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "ana.lima@teste.com", Email = "ana.lima@teste.com", Nome = "Ana Lima", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "paulo.mendes@teste.com", Email = "paulo.mendes@teste.com", Nome = "Paulo Mendes", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "fernanda.alves@teste.com", Email = "fernanda.alves@teste.com", Nome = "Fernanda Alves", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "ricardo.gomes@teste.com", Email = "ricardo.gomes@teste.com", Nome = "Ricardo Gomes", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "juliana.rocha@teste.com", Email = "juliana.rocha@teste.com", Nome = "Juliana Rocha", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "marcos.santos@teste.com", Email = "marcos.santos@teste.com", Nome = "Marcos Santos", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "patricia.martins@teste.com", Email = "patricia.martins@teste.com", Nome = "Patrícia Martins", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "roberto.costa@teste.com", Email = "roberto.costa@teste.com", Nome = "Roberto Costa", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "camila.ferreira@teste.com", Email = "camila.ferreira@teste.com", Nome = "Camila Ferreira", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "lucas.araujo@teste.com", Email = "lucas.araujo@teste.com", Nome = "Lucas Araújo", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "beatriz.monteiro@teste.com", Email = "beatriz.monteiro@teste.com", Nome = "Beatriz Monteiro", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "felipe.ramos@teste.com", Email = "felipe.ramos@teste.com", Nome = "Felipe Ramos", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "sandra.barros@teste.com", Email = "sandra.barros@teste.com", Nome = "Sandra Barros", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "thiago.cardoso@teste.com", Email = "thiago.cardoso@teste.com", Nome = "Thiago Cardoso", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "aline.machado@teste.com", Email = "aline.machado@teste.com", Nome = "Aline Machado", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "gustavo.teixeira@teste.com", Email = "gustavo.teixeira@teste.com", Nome = "Gustavo Teixeira", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "renata.pinto@teste.com", Email = "renata.pinto@teste.com", Nome = "Renata Pinto", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "eduardo.nunes@teste.com", Email = "eduardo.nunes@teste.com", Nome = "Eduardo Nunes", Perfil = Perfis.Paciente },
            new ApplicationUser { UserName = "carolina.dias@teste.com", Email = "carolina.dias@teste.com", Nome = "Carolina Dias", Perfil = Perfis.Paciente },
        };

        foreach (var paciente in pacientes)
        {
            var userExist = await userManager.FindByEmailAsync(paciente.Email);
            if (userExist == null)
            {
                await userManager.CreateAsync(paciente, senhaPadrao);
                await userManager.AddToRoleAsync(paciente, "Paciente");
            }
        }
    }
}