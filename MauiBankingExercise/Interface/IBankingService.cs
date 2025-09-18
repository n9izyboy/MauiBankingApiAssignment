using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBankingExercise.Models;
using MauiBankingExercise.ViewModels;
using System.Collections.ObjectModel;


namespace MauiBankingExercise.Interface
{
     public interface IBankingService
     {
        IBankingService CreateBankingService();
        void InitializeDatabase();

        Task SeedDatabaseAsync();

        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);

        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int customerId);
        Task<List<Customer>> SearchCustomersAsync(string searchTerm);

        Task<List<Account>> GetAccountsByCustomerIdAsync(int customerId);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task<bool> DeleteAccountAsync(int accountId);

        Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        Task<Transaction> GetTransactionByIdAsync(int transactionId);
        Task<Transaction> UpdateTransactionByIdAsync(int transactionId, Transaction updatedTransaction);

        void LoadData();

        void SaveData();


        



    }
}
