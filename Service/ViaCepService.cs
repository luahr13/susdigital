using projetoTP3_A2.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ViaCepService
{
    private readonly HttpClient _httpClient;

    public ViaCepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EnderecoResponse> ConsultarCepAsync(string cep)
    {
        // valida formato: apenas 8 dígitos
        if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8 || !cep.All(char.IsDigit))
            throw new ArgumentException("CEP inválido. Deve conter 8 dígitos numéricos.");

        var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var endereco = JsonSerializer.Deserialize<EnderecoResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return endereco;
    }
}