using DogHome.ViewModel;

namespace DogHome.Views;

public partial class BuyerFormPage : ContentPage
{
    BuyerFormViewModel _ViewModel;
    public BuyerFormPage(BuyerFormViewModel viewModel)
    {
        InitializeComponent();
        _ViewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ViewModel.GetAllBuyers(); 
    }
}
