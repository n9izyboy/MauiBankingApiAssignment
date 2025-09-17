using MauiBankingExercise.Configurations;
using MauiBankingExercise.Interface;
using MauiBankingExercise.Models;
using System.Net.Http.Json;

public class BankingApiService : IBankingService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationSettings _settings;

    public BankingApiService(ApplicationSettings settings)
#if DEBUG
    {
        _settings = settings;
        _httpClient = new HttpClient();

        HttpClientHandler insecureHandler = GetInsecureHandler();
        _httpClient = new HttpClient(insecureHandler);
    }

    private HttpClientHandler GetInsecureHandler()
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (errors != System.Net.Security.SslPolicyErrors.None)
            {
                
                System.Diagnostics.Debug.WriteLine($"SSL Certificate Error: {errors}");
            }
            return true;
        };
        return handler;
    }
#else
    {
        _settings = settings;
        _httpClient = new HttpClient();
    }
#endif

    public async Task<List<Account>> GetAccountsByCustomerIdAsync(int customerId)
    {
        var url = $"{_settings.ServiceUrl}/customer/{customerId}";
        var accounts = await _httpClient.GetFromJsonAsync<List<Account>>(url);
        return accounts ?? new List<Account>();
    }

    public IBankingService CreateBankingService()
    {
        throw new NotImplementedException();
    }

    public void InitializeDatabase()
    {
        throw new NotImplementedException();
    }

    public Task SeedDatabaseAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Customer>> GetAllCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetCustomerByIdAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> CreateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCustomerAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Customer>> SearchCustomersAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAccountByIdAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<Account> CreateAccountAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<Account> UpdateAccountAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAccountAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> GetTransactionByIdAsync(int transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> UpdateTransactionByIdAsync(int transactionId, Transaction updatedTransaction)
    {
        throw new NotImplementedException();
    }

    internal async Task<Account> GetAccountAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    internal async Task<IEnumerable<object>> GetTransactionsAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    internal async Task ProcessTransactionAsync(int customerId, string name, decimal amount)
    {
        throw new NotImplementedException();
    }

    internal async Task<IEnumerable<object>> GetCustomersAsync()
    {
        throw new NotImplementedException();
    }
}
