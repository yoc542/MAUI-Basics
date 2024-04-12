using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogHome.Interface;

namespace DogHome.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        string _Email;

        [ObservableProperty]
        string _Password;

        IAuthRepository _AuthRepository;
        public LoginViewModel(IAuthRepository authRepository)
        {
            _AuthRepository = authRepository;
            Email = string.Empty;
            Password = string.Empty;
        }

        [RelayCommand]
        private async void Login()
        {

            var auth = await _AuthRepository.GetByEmailAsync(Email);
            if (auth != null && auth.Password == Password)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                     { "Auth", auth }
                 };

                await Shell.Current.GoToAsync("//SellerDogsPage", navigationParameter);
            }

            else
                await Shell.Current.DisplayAlert("Login", "Login Failed", "cancel");
        }

        [RelayCommand]
        private void Register()
        {
            Shell.Current.GoToAsync("//RegisterPage");
        }


        [RelayCommand]
        private void Back()
        {
            Shell.Current.GoToAsync("//OnboardingPage");
        }

    }
}
