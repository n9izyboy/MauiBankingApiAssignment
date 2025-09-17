

using MauiBankingExercise.Models;
using MauiBankingExercise.ViewModels;
using MauiBankingExercise.Views;
namespace MauiBankingExercise.Views;

public partial class CustomerSelectionScreenView : ContentPage
{
    public CustomerSelectionScreenView(CustomerSelectionScreenViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
   
   protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CustomerSelectionScreenViewModel vm)
        {
            await vm.LoadCustomersAsync();
        }
    } 
}