using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DogHome.Interface;
using DogHome.Messenger;
using DogHome.Model;

namespace DogHome.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        string? _Message;

        [ObservableProperty]
        object _SelectedViewModel;
        public RegisterFormViewModel RegisterFormViewModel { get; set; }
        public PersonalDetailViewModel PersonalDetailViewModel { get; set; }
        public SocialProfilesViewModel SocialProfilesViewModel { get; set; }

        IAuthRepository _AuthRepository;
        PersonalDetail personalData;
        SocialDetail socialData;
        AccountDetail accountData;

        #region Constructor
        public RegisterViewModel(IAuthRepository authRepository)
        {
            _AuthRepository = authRepository;
            RegisterFormViewModel = new RegisterFormViewModel();
            PersonalDetailViewModel = new PersonalDetailViewModel();
            SocialProfilesViewModel = new SocialProfilesViewModel();
            SelectedViewModel = PersonalDetailViewModel;
            WeakReferenceMessenger.Default.Register<RegisterMessage>(this, (r, m) =>
            {
                Message = m.Value;
                if (Message == "personal")
                    SelectedViewModel = PersonalDetailViewModel;
                else if (Message == "social")
                    SelectedViewModel = SocialProfilesViewModel;
                else if (Message == "createaccount")
                    SelectedViewModel = RegisterFormViewModel;
                else
                    SelectedViewModel = PersonalDetailViewModel;
            });
            WeakReferenceMessenger.Default.Register<FormDataMessage<PersonalDetail>>(this, (r, message) =>
            {
                // Handle FormDataMessage with PersonalDetail data
                personalData = message.Data;
                var registerMessage = message.RegisterMessage;
                SelectedViewModel = SocialProfilesViewModel;
            });
            var data = personalData;
            WeakReferenceMessenger.Default.Register<FormDataMessage<SocialDetail>>(this, (r, message) =>
            {
                socialData = message.Data;
                var registerMessage = message.RegisterMessage;
                SelectedViewModel = RegisterFormViewModel;
            });

            WeakReferenceMessenger.Default.Register<FormDataMessage<AccountDetail>>(this, async (r, message) =>
            {
                var accountData = message.Data;
                var registerMessage = message.RegisterMessage;
                Auth auth = new Auth
                {
                    FullName = personalData.FullName,
                    BirthDate = personalData.DateOfBirth,
                    Phone = personalData.PhoneNumber,
                    Location = personalData.Address,
                    Gender = personalData.Gender,
                    SocialSite = socialData.SocialSite,
                    ShopName = socialData.ShopName,
                    Email = accountData.Email,
                    Password = accountData.Password,

                };
                try
                {
                    await _AuthRepository.CreateAsync(auth);

                    await Shell.Current.GoToAsync("//LoginPage");
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Auth", "Registartion failed", "ok");
                }
            });
        }
        #endregion

        [RelayCommand]
        private void Back()
        {
            Shell.Current.GoToAsync("//LoginPage");
        }
    }
}