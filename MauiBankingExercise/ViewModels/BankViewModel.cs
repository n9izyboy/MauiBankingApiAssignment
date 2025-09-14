using MauiBankingExercise.Services;
using MauiBankingExercise.Models;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBankingExercise.ViewModels
{
    
    public partial class BankViewModel : BaseViewModel
    {
        private BankingDataBaseServices _bankingDataBaseServices;
        private Bank _bank;


        public Bank Bank
        {
            get { return _bank; }
            set
            {
                _bank = value;
                OnPropertyChanged();
            }
        }
    }
}
