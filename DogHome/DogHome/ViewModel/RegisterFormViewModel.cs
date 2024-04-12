using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DogHome.Messenger;
using DogHome.Model;
using System.Text.RegularExpressions;

namespace DogHome.ViewModel
{
    public partial class RegisterFormViewModel : ObservableObject
    {
        [ObservableProperty]
        string? _Email;

        [ObservableProperty]
        string? _Password;

        [ObservableProperty]
        string? _Confirm;

        [RelayCommand]
        private void Previous()
        {
            WeakReferenceMessenger.Default.Send(new RegisterMessage("social"));
        }

        [RelayCommand]
        private void Submit()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Confirm))
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Please fill all the details.", "OK");
                return;
            }
            if (!IsEmailValid(Email))
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Please enter a valid email address.", "OK");
                return;
            }
            if (!IsPasswordValid(Password))
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Password should contain 8 characters,at least one uppercase and one special character.", "OK");
                return;
            }

            if (Password != Confirm)
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Password doesn't match the confirm password.", "OK");
                return;
            }
            var data = new AccountDetail
            {
                Email = Email,
                Password = Password,
                Confirm = Confirm
            };
            var registerMessage = new RegisterMessage("submit");
            var formDataMessage = new FormDataMessage<AccountDetail>(data, registerMessage);
            WeakReferenceMessenger.Default.Send(formDataMessage);
        }

        private bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        private bool IsPasswordValid(string password)
        {
            if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(c => !char.IsLetterOrDigit(c)))
                return false;

            return true;
        }
    }
}
