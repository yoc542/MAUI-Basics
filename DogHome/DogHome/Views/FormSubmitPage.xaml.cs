using DogHome.ViewModel;

namespace DogHome.Views;

public partial class FormSubmitPage : ContentPage
{
    public FormSubmitPage(FormSubmitViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}