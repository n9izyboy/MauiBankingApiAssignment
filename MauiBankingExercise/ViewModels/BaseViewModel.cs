using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using MauiBankingExercise.Services;
using MauiBankingExercise.Configurations;

namespace MauiBankingExercise.ViewModels
{
    public partial class BaseViewModel : INotifyPropertyChanged
    {
        

        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public virtual void OnAppearing()
        {

        }

        
        public async Task ExampleMethod()
        {
            var settings = new ApplicationSettings();
            var apiService = new BankingApiService(settings);
            var accounts = await apiService.GetAccountsByCustomerIdAsync(1); 
        }
    }
}
