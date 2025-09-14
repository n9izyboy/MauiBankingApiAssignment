using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiBankingExercise.Models;
using MauiBankingExercise.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;

namespace MauiBankingExercise.ViewModels
{
    public class TransactionScreenViewModel
    {
        

        public class TransactionViewModel : INotifyPropertyChanged, IQueryAttributable
        {
            private readonly  BankingSeeder _bankingSeeder;
            private bool _isLoading;
            private Account _account;
            private ObservableCollection<Transaction> _transactions;
            private string _transactionAmount;
            private TransactionType _selectedTransactionType;
            private List<TransactionType> _transactionTypes;

            public TransactionViewModel(BankingSeeder _bankingSeeder, BankingSeeder bankingSeeder)
            {
                _bankingSeeder = bankingSeeder;
                _transactions = new ObservableCollection<Transaction>();
                _transactionTypes = new List<TransactionType>
        {
            new TransactionType { Name = "Deposit" },
            new TransactionType { Name = "Withdrawal" }
        };
                SubmitTransactionCommand = new Command(OnSubmitTransaction);
            }

            public Account Account
            {
                get => _account;
                set { _account = value; OnPropertyChanged(); }
            }

            public ObservableCollection<Transaction> Transactions
            {
                get => _transactions;
                set { _transactions = value; OnPropertyChanged(); }
            }

            public List<TransactionType> TransactionTypes
            {
                get => _transactionTypes;
                set { _transactionTypes = value; OnPropertyChanged(); }
            }

            public TransactionType SelectedTransactionType
            {
                get => _selectedTransactionType;
                set { _selectedTransactionType = value; OnPropertyChanged(); }
            }

            public string TransactionAmount
            {
                get => _transactionAmount;
                set { _transactionAmount = value; OnPropertyChanged(); }
            }

            public bool IsLoading
            {
                get => _isLoading;
                set { _isLoading = value; OnPropertyChanged(); }
            }

            public ICommand SubmitTransactionCommand { get; }

            // Handle navigation parameters
            public void ApplyQueryAttributes(IDictionary<string, object> query)
            {
                if (query.ContainsKey("customerId"))
                {
                    var customerId = int.Parse(query["customerId"].ToString());
                    LoadAccountDataAsync(customerId);
                }
            }

            private async Task LoadAccountDataAsync(int customerId)
            {
                IsLoading = true;
                try
                {
                    Account = await _bankingSeeder.GetAccountAsync(customerId);
                    var transactions = await _bankingSeeder.GetTransactionsAsync(customerId);

                    Transactions.Clear();
                    foreach (var transaction in transactions)
                    {
                        Transactions.Add((Transaction)transaction);
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load account: {ex.Message}", "OK");
                }
                finally
                {
                    IsLoading = false;
                }
            }

            private async void OnSubmitTransaction()
            {
                if (SelectedTransactionType == null || string.IsNullOrWhiteSpace(TransactionAmount))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all fields", "OK");
                    return;
                }

                if (!decimal.TryParse(TransactionAmount, out decimal amount) || amount <= 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid amount", "OK");
                    return;
                }

                IsLoading = true;
                try
                {
                    await _bankingSeeder.ProcessTransactionAsync(Account.CustomerId, SelectedTransactionType.Name, amount);

                    // Refresh account data
                    await LoadAccountDataAsync(Account.CustomerId);

                    // Clear form
                    TransactionAmount = string.Empty;
                    SelectedTransactionType = null;

                    await Application.Current.MainPage.DisplayAlert("Success", "Transaction processed successfully", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Transaction failed: {ex.Message}", "OK");
                }
                finally
                {
                    IsLoading = false;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
