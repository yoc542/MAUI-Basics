using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DogHome.Messenger;
using DogHome.Model;

namespace DogHome.ViewModel
{
    public partial class PersonalDetailViewModel : ObservableObject
    {

        [ObservableProperty]
        string? _FullName;

        [ObservableProperty]
        DateTime _DateOfBirth;

        [ObservableProperty]
        string? _PhoneNumber;

        [ObservableProperty]
        string? _Address;

        [ObservableProperty]
        bool _MaleRadio;

        [ObservableProperty]
        bool _FemaleRadio;

        [ObservableProperty]
        string _Gender;
        public PersonalDetailViewModel()
        {
            MaleRadio = false;
            FemaleRadio = false;
        }

        [RelayCommand]
        private void Next()
        {
            if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Address))
            {
                Application.Current?.MainPage?.DisplayAlert("Register", "Fill all the data", "OK");
                return;
            }
            if (!IsPhoneNumberValid(PhoneNumber))
            {
                Application.Current?.MainPage?.DisplayAlert("Register", "Please fill correct phone number", "OK");
                return;
            }
            if (FemaleRadio)
                Gender = "Female";
            else if (MaleRadio)
                Gender = "Male";
            else
            {
                Shell.Current.DisplayAlert("Gender", "Please check one", "OK");
                return;
            }

            var data = new PersonalDetail
            {
                FullName = FullName,
                DateOfBirth = DateOfBirth,
                PhoneNumber = PhoneNumber,
                Address = Address,
                Gender = Gender
            };

            var registerMessage = new RegisterMessage("social");
            var formDataMessage = new FormDataMessage<PersonalDetail>(data, registerMessage);
            WeakReferenceMessenger.Default.Send(formDataMessage);
        }
        private bool IsPhoneNumberValid(string phoneNumber)
        {
            return phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
        }


    }
}
