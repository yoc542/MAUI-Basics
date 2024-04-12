using DogHome.ViewModel;

namespace DogHome.Views;

public partial class SellersListPage : ContentPage
{
    SellersListViewModel _ViewModel;
    public SellersListPage(SellersListViewModel viewModel)
    {
        InitializeComponent();
        _ViewModel = viewModel;
        BindingContext = viewModel;
    }
}