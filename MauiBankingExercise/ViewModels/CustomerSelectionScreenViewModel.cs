using MauiBankingExercise.Models;
using MauiBankingExercise.Services;
using MauiBankingExercise.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using MauiBankingExercise.Interface;



namespace MauiBankingExercise.ViewModels
{
    public partial class CustomerSelectionScreenViewModel : BaseViewModel
    {
        private readonly IBankingService _bankingApiService;
        private bool _isLoading;
        private ObservableCollection<Customer> _customers;

        public CustomerSelectionScreenViewModel(IBankingService bankingApiService)
        {
            _bankingApiService = bankingApiService ?? throw new ArgumentNullException(nameof(bankingApiService));
            _customers = new ObservableCollection<Customer>();
            SelectCustomerCommand = new Command<Customer>(OnSelectCustomer);
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand SelectCustomerCommand { get; }

        public async Task LoadCustomersAsync()
        {
            IsLoading = true;
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading customers...");
                var customers = await _bankingApiService.GetAllCustomersAsync();
                System.Diagnostics.Debug.WriteLine($"Loaded {customers?.Count ?? 0} customers");

                Customers.Clear();
                if (customers != null)
                {
                    foreach (var customer in customers)
                    {
                        Customers.Add((Customer)customer);
                        System.Diagnostics.Debug.WriteLine($"Added customer: {customer}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading customers: {ex.Message}");
                // You might want to show an alert to the user
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void OnSelectCustomer(Customer customer)
        {
            if (customer != null)
            {
                await Shell.Current.GoToAsync($"CustomerDashboard?customerId={customer.CustomerId}");
            }
        }
       
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action? onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}












