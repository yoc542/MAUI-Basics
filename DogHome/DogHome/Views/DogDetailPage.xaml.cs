using DogHome.ViewModel;

namespace DogHome.Views;

public partial class DogDetailPage : ContentPage
{
    public DogDetailPage(DogDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}