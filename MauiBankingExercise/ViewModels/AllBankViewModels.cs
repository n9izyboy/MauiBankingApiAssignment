using MauiBankingExercise.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MauiBankingExercise.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiBankingExercise.ViewModels
{
    public partial class AllBankViewModels : BaseViewModel
    {
        public ICommand MyButtonCommand { get; set; }

        private BankingDataBaseServices _bankingDataBaseServices;
        private ObservableCollection<Bank> _banks;


        public ObservableCollection<Bank> Banks
        {
            get => _banks;
            set
            {
                _banks = value;
                OnPropertyChanged();
            }
        }
       public AllBankViewModels(BankingDataBaseServices bankingDataBaseServices)
        {
            _bankingDataBaseServices = bankingDataBaseServices;
            Banks = new ObservableCollection<Bank>(_bankingDataBaseServices.GetAllBanks());
           
        }

        private void MyButtonAction(object obj)
        {

        }

        public string Title { get; set; } = "All Banks";

        public async Task BankSelected(Bank bank)
        {
          var navigationParameters = new Dictionary<string, object>
            {
                { "Bank", bank }
            };
            await Shell.Current.GoToAsync(nameof(BankViewModel), navigationParameters);
        }

        private BankingDataBaseServices bankingDataBaseServices = new BankingDataBaseServices();
        
        public Bank Bank { get; set; }
        public Action<object> MyButtonActiion { get; }

        public List<Bank> GetAllBanks()
        {
            return new List<Bank>();

        }
        public override void OnAppearing()
        {
            base.OnAppearing();
        }
       
      
    }
}
