using DogHome.ViewModel;

namespace DogHome.Views;

public partial class MainDogsPage : ContentPage
{
    MainDogsViewModel _ViewModel;
    public MainDogsPage(MainDogsViewModel viewModel)
    {
        InitializeComponent();
        _ViewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true;
        _ViewModel.GetAllDogs();
        LoadingIndicator.IsRunning = false;
        LoadingIndicator.IsVisible = false;
    }

    private async void DiscoverButton_Clicked(object sender, EventArgs e)
    {
        await MainPageScroll.ScrollToAsync(0, 450, true);
    }

    void OnPointerEntered(object sender, PointerEventArgs e)
    {
        if (sender == SellerLabel)
        {
            SellerLabel.Scale = 1.1;
        }
        if (sender == BackLabel)
        {
            BackLabel.Scale = 1.1;
        }
    }
    void OnPointerExited(object sender, PointerEventArgs e)
    {
        SellerLabel.Scale = 1;
        BackLabel.Scale = 1;
    }
}