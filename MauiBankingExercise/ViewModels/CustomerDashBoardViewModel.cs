using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MauiBankingExercise.Models;

    


namespace MauiBankingExercise.ViewModels
{
    [QueryProperty(nameof(Customer), "Customer")]
    [QueryProperty(nameof(Account), "Account")]
    public partial class CustomerDashBoardViewModel : BaseViewModel
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
        public Customer customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged();
            }
        }
        public CustomerDashBoardViewModel(Account account, Customer customer)
        {
            this.account = account;
            this.customer = customer;
        }
    }    

       
        
}
