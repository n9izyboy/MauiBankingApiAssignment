
using MauiBankingExercise.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace MauiBankingExercise.Services
{
    public class BankingDataBaseServices
    {

        public class DatabaseService
        {
            private readonly HttpClient _httpClient;

            public DatabaseService(HttpClient httpClient)
            {
                // Create HttpClient with custom handler for localhost SSL issues
                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                _httpClient = new HttpClient(handler);
                _httpClient.BaseAddress = new Uri("https://localhost:7258/api/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            private JsonSerializerOptions GetJsonOptions()
            {
                return new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
                };
            }

            public async Task InitAsync()
            {
                await Task.CompletedTask;
            }

            public async Task<List<Customer>> GetCustomersAsync()
            {
                try
                {
                    var response = await _httpClient.GetAsync("Customers");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var customers = JsonSerializer.Deserialize<List<Customer>>(json, GetJsonOptions());
                        return customers ?? new List<Customer>();
                    }
                    return new List<Customer>();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error getting customers: {ex.Message}");
                    return new List<Customer>();
                }
            }

            public async Task<Customer> GetCustomerAsync(int customerId)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"Customers/{customerId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var customer = JsonSerializer.Deserialize<Customer>(json, GetJsonOptions());
                        return customer;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error getting customer: {ex.Message}");
                    return null;
                }
            }

            public async Task<List<Account>> GetAccountsAsync(int customerId)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"Accounts/customer/{customerId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine($"Accounts API Response: {json}");

                        var accounts = JsonSerializer.Deserialize<List<Account>>(json, GetJsonOptions());
                        return accounts ?? new List<Account>();
                    }
                    return new List<Account>();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error getting accounts: {ex.Message}");
                    return new List<Account>();
                }
            }

            public async Task<Account> GetAccountAsync(int accountId)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"Accounts/{accountId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var account = JsonSerializer.Deserialize<Account>(json, GetJsonOptions());
                        return account;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error getting account: {ex.Message}");
                    return null;
                }
            }

            public async Task<List<Transaction>> GetTransactionsAsync(int accountId)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"Transactions/account/{accountId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var transactions = JsonSerializer.Deserialize<List<Transaction>>(json, GetJsonOptions());
                        return transactions ?? new List<Transaction>();
                    }
                    return new List<Transaction>();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error getting transactions: {ex.Message}");
                    return new List<Transaction>();
                }
            }

            public async Task<bool> CreateTransactionAsync(int accountId, decimal amount, string transactionType)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine($"Starting transaction for account {accountId}");

                    // Map transaction type
                    int transactionTypeId = transactionType.ToLower() switch
                    {
                        "deposit" => 1,
                        "withdrawal" => 2,
                        _ => 1
                    };

                    // Get the complete account details first
                    var accountResponse = await _httpClient.GetAsync($"Accounts/{accountId}");
                    if (!accountResponse.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("Failed to get account details");
                        return false;
                    }

                    var accountJson = await accountResponse.Content.ReadAsStringAsync();
                    var accountData = JsonSerializer.Deserialize<JsonElement>(accountJson, GetJsonOptions());
                    System.Diagnostics.Debug.WriteLine($"Account data: {accountJson}");

                    // Get customer details
                    var customerId = accountData.GetProperty("customerId").GetInt32();
                    var customerResponse = await _httpClient.GetAsync($"Customers/{customerId}");
                    if (!customerResponse.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("Failed to get customer details");
                        return false;
                    }

                    var customerJson = await customerResponse.Content.ReadAsStringAsync();
                    var customerData = JsonSerializer.Deserialize<JsonElement>(customerJson, GetJsonOptions());
                    System.Diagnostics.Debug.WriteLine($"Customer data: {customerJson}");

                    // Create the complete request with ALL required fields
                    var completeRequest = new
                    {
                        AccountId = accountId,
                        Amount = amount,
                        TransactionTypeId = transactionTypeId,
                        Description = $"{transactionType} transaction",
                        TransactionDate = DateTime.Now,
                        Account = new
                        {
                            AccountId = accountId,
                            AccountNumber = accountData.GetProperty("accountNumber").GetString(),
                            IsActive = accountData.GetProperty("isActive").GetBoolean(),
                            DateOpened = accountData.GetProperty("dateOpened").GetDateTime(),
                            AccountBalance = accountData.GetProperty("accountBalance").GetDecimal(),
                            CustomerId = customerId,
                            AccountTypeId = accountData.GetProperty("accountTypeId").GetInt32(),
                            Customer = new
                            {
                                CustomerId = customerId,
                                FirstName = customerData.GetProperty("firstName").GetString(),
                                LastName = customerData.GetProperty("lastName").GetString(),
                                Email = customerData.GetProperty("email").GetString(),
                                PhoneNumber = customerData.GetProperty("phoneNumber").GetString(),
                                PhysicalAddress = customerData.GetProperty("physicalAddress").GetString(),
                                IdentityNumber = customerData.GetProperty("identityNumber").GetString(),
                                CustomerTypeId = customerData.GetProperty("customerTypeId").GetInt32(),
                                GenderTypeId = customerData.GetProperty("genderTypeId").GetInt32(),
                                RaceTypeId = customerData.GetProperty("raceTypeId").GetInt32(),
                                Nationality = customerData.GetProperty("nationality").GetString(),
                                MaritalStatusId = customerData.GetProperty("maritalStatusId").GetInt32(),
                                EmploymentStatusId = customerData.GetProperty("employmentStatusId").GetInt32(),
                                BankId = customerData.GetProperty("bankId").GetInt32(),
                                Bank = new
                                {
                                    BankId = customerData.GetProperty("bankId").GetInt32(),
                                    BankName = "Default Bank",
                                    BankAddress = "Default Address",
                                    BranchCode = "001",
                                    ContactEmail = "contact@bank.com",
                                    ContactPhoneNumber = "555-BANK",
                                    IsActive = true,
                                    OperatingHours = "9AM-5PM"
                                },
                                CustomerType = new
                                {
                                    CustomerTypeId = customerData.GetProperty("customerTypeId").GetInt32(),
                                    Name = "Individual"
                                }
                            },
                            AccountType = new
                            {
                                AccountTypeId = accountData.GetProperty("accountTypeId").GetInt32(),
                                Name = accountData.GetProperty("accountTypeId").GetInt32() == 1 ? "Checking" : "Savings"
                            },
                            Transactions = new object[] { }
                        },
                        TransactionType = new
                        {
                            TransactionTypeId = transactionTypeId,
                            Name = transactionType
                        }
                    };

                    var json = JsonSerializer.Serialize(completeRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    System.Diagnostics.Debug.WriteLine($"Sending complete transaction (first 500 chars): {json.Substring(0, Math.Min(500, json.Length))}...");

                    var response = await _httpClient.PostAsync("Transactions", content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    System.Diagnostics.Debug.WriteLine($"Final response ({response.StatusCode}): {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("Transaction created successfully!");
                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error creating transaction: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                    return false;
                }
            }

            public async Task ResetDatabaseAsync()
            {
                await Task.CompletedTask;
            }





        }
    }
}
    

