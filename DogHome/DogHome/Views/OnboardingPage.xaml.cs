using DogHome.ViewModel;

namespace DogHome.Views;

public partial class OnboardingPage : ContentPage
{
    public OnboardingPage()
    {
        InitializeComponent();
        BindingContext = new OnboardingViewModel();
    }
    void OnPointerEntered(object sender, PointerEventArgs e)
    {
        if (sender == SellerFrame)
        {
            SellerFrame.Scale = 1.05;
        }
        if (sender == CustomerFrame)
        {
            CustomerFrame.Scale = 1.05;
        }
    }
    void OnPointerExited(object sender, PointerEventArgs e)
    {
        SellerFrame.Scale = 1;
        CustomerFrame.Scale = 1;
    }

}