using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBankingExercise.Models;

namespace MauiBankingExercise.ViewModels
{
    public class AccountDetailsViewModel : BaseViewModel
    {
        public Account account
        {


            get { return account; }
            set
            {
                account = value;
                OnPropertyChanged();
            }


        }
        public AccountDetailsViewModel(Account account)
        {
            this.account = account;
        }
        public AccountType accountType
        {
            get { return account.AccountType; }
            set
            {
                account.AccountType = value; OnPropertyChanged();
            }
        }




    }
}
