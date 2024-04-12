using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DogHome.Messenger;
using DogHome.Model;

namespace DogHome.ViewModel
{
    public partial class SocialProfilesViewModel : ObservableObject
    {
        [ObservableProperty]
        string? _SocialSite;

        [ObservableProperty]
        string? _ShopName;

        [ObservableProperty]
        bool? _Checkbox;

        public SocialProfilesViewModel()
        {
            Checkbox = false;
        }

        [RelayCommand]
        private void Next()
        {
            if (string.IsNullOrWhiteSpace(SocialSite) || string.IsNullOrWhiteSpace(ShopName))
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Please provide all the detai;s", "OK");
                return;
            }
            var data = new SocialDetail
            {
                SocialSite = SocialSite,
                ShopName = ShopName
            };
            var registerMessage = new RegisterMessage("createaccount");
            var formDataMessage = new FormDataMessage<SocialDetail>(data, registerMessage);
            WeakReferenceMessenger.Default.Send(formDataMessage);
        }

        [RelayCommand]
        private void Previous()
        {
            WeakReferenceMessenger.Default.Send(new RegisterMessage("personal"));
        }
    }
}
