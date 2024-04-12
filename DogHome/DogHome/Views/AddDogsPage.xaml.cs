using DogHome.ViewModel;

namespace DogHome.Views;

public partial class AddDogsPage : ContentPage
{
    public AddDogsPage(AddDogsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}