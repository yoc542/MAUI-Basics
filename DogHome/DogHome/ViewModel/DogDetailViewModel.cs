using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogHome.Interface;
using DogHome.Model;

namespace DogHome.ViewModel
{
    [QueryProperty(nameof(Dog), "DogDetail")]
    public partial class DogDetailViewModel : ObservableObject
    {
        private Dog _Dog;

        [ObservableProperty]
        private string _Breed;

        [ObservableProperty]
        Auth _Seller;

        [ObservableProperty]
        ImageSource _Imagepath;

        IBreedRepository _BreedRepository;
        IAuthRepository _AuthRepository;
        public Dog Dog
        {
            get => _Dog;
            set
            {
                SetProperty(ref _Dog, value);
                UpdateBreed();
                UpdateSeller();
                UpdateImage();
            }
        }
        public DogDetailViewModel(IAuthRepository authRepository, IBreedRepository breedRepository)
        {
            _AuthRepository = authRepository;
            _BreedRepository = breedRepository;
        }
        private async void UpdateBreed()
        {
            var breed = await _BreedRepository.GetByIdAsync(Dog.BreedId);
            Breed = breed.BreedName;

        }

        private async void UpdateSeller()
        {
            var sellers = await _AuthRepository.GetAllAsync();
            Seller = sellers.FirstOrDefault(seller => seller.Id == Dog.SellerId);

        }
        private void UpdateImage()
        {
            Imagepath = ImageSource.FromFile(Dog.ImagePath);
        }

        [RelayCommand]
        private void Back()
        {
            Shell.Current.GoToAsync("//MainDogsPage");
        }

        [RelayCommand]
        private async void Submit()
        {
            try
            {
                var parameter = new Dictionary<string, object> { { "Seller", Seller } };
                await Shell.Current.GoToAsync("//FormSubmitPage", parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
