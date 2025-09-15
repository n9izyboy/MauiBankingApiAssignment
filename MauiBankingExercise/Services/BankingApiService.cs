using MauiBankingExercise.Configurations;
using MauiBankingExercise.Models;
using System.Net.Http.Json;

public class BankingApiService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationSettings _settings;

    public BankingApiService(ApplicationSettings settings)
    {
        _settings = settings;
        _httpClient = new HttpClient();
    }

    public async Task<List<Account>> GetAccountsByCustomerIdAsync(int customerId)
    {
        var url = $"{_settings.ServiceUrl}/customer/{customerId}";
        var accounts = await _httpClient.GetFromJsonAsync<List<Account>>(url);
        return accounts ?? new List<Account>();
    }
}
