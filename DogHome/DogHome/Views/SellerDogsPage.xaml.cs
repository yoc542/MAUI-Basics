using DogHome.ViewModel;

namespace DogHome.Views;

public partial class SellerDogsPage : ContentPage
{
    SellerDogsViewModel _ViewModel;
    public SellerDogsPage(SellerDogsViewModel viewModel)
    {
        InitializeComponent();
        _ViewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ViewModel.GetAllDogs(); 
    }
}