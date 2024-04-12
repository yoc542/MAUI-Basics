using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogHome.Interface;
using DogHome.Model;
using System.Collections.ObjectModel;

namespace DogHome.ViewModel
{
    [QueryProperty(nameof(Dog), "Dog")]
    [QueryProperty(nameof(Auth), "Auth")]
    [QueryProperty(nameof(Update), "Update")]
    public partial class AddDogsViewModel : ObservableObject
    {
        [ObservableProperty]
        Auth _Auth;

        [ObservableProperty]
        Dog _Dog;

        [ObservableProperty]
        bool _VisibleImage;

        [ObservableProperty]
        bool _Update;
        [ObservableProperty]
        bool _BreedEntry;

        [ObservableProperty]
        string _NewBreed;

        [ObservableProperty]
        bool _MaleRadio;

        [ObservableProperty]
        bool _FemaleRadio;

        [ObservableProperty]
        string _SelectedSize;

        [ObservableProperty]
        string _SelectedBreed;

        [ObservableProperty]
        ImageSource _ImagePath;

        IRepository _DogRepository;
        IBreedRepository _BreedRepository;

        public ObservableCollection<string> BreedOptions { get; set; }
        public AddDogsViewModel(IBreedRepository breedRepository, IRepository dogRepository)
        {
            _BreedRepository = breedRepository;
            _DogRepository = dogRepository;
            MaleRadio = false;
            FemaleRadio = false;
            BreedEntry = false;
            VisibleImage = false;
            Dog = new Dog();
            BreedOptions = new ObservableCollection<string>();
            GetBreeds();
            if (Update)
                Console.WriteLine(Dog);
        }

        public async void GetBreeds()
        {
            var breeds = await _BreedRepository.GetAllAsync();

            if (breeds != null)
            {
                BreedOptions.Clear();

                foreach (var breed in breeds)
                {
                    BreedOptions.Add(breed.BreedName);
                }
            }
        }

        [RelayCommand]
        private void Back()
        {
            Dog = null;
            MaleRadio = false;
            FemaleRadio = false;
            ImagePath = null;
            SelectedBreed = string.Empty;
            VisibleImage = false;
            var parameter = new Dictionary<string, object>
             {
               { "Auth", Auth}
              };
            Shell.Current.GoToAsync("//SellerDogsPage", parameter);
        }

        [RelayCommand]
        private void AddNewBreed()
        {
            BreedEntry = true;
        }

        [RelayCommand]
        private void SubmitBreed()
        {
            if (!string.IsNullOrWhiteSpace(NewBreed) && !string.IsNullOrEmpty(NewBreed))
            {
                BreedOptions.Add(NewBreed);
                SelectedBreed = NewBreed;
            }
            BreedEntry = false;
        }

        [RelayCommand]
        private async void UploadImage()
        {

            var file = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick an Image",
                FileTypes = FilePickerFileType.Images
            });
            if (file == null)
                return;

            var stream = await file.OpenReadAsync();
            VisibleImage = true;
            string destinationFolderPath = "E:\\Bajra Practice\\final-project\\DogHome\\DogHome\\Resources\\Images";

            string destinationFilePath = Path.Combine(destinationFolderPath, file.FileName);
            try
            {
                using (var outputStream = File.Create(destinationFilePath))
                {
                    await stream.CopyToAsync(outputStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
            ImagePath = destinationFilePath;

            stream.Close();
        }

        [RelayCommand]
        private async void Add()
        {
            if (Dog == null)
                return;
            if (string.IsNullOrEmpty(Dog.Name))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter the dog's name", "OK");
                return;
            }
            if (Dog.Price == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Please enter the dog's price", "OK");
                return;
            }
            if (Dog.Age == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Please enter the dog's age", "OK");
                return;
            }

            if (Update)
            {
                if (Dog.Gender == "Female")
                    FemaleRadio = true;
                else
                    MaleRadio = true;
                var imagesource = Dog.ImagePath;
                try
                {
                    if (ImagePath != null)
                    {
                        Dog.ImagePath = ImagePath.ToString().Substring("File: ".Length);
                    }
                    await _DogRepository.UpdateAsync(Dog);
                    Dog = null;
                    MaleRadio = false;
                    FemaleRadio = false;
                    ImagePath = null;
                    SelectedBreed = string.Empty;
                    VisibleImage = false;
                    CreateToast("Data updated successfully");
                }
                catch (Exception ex)
                {
                    CreateToast("Error while updating");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(SelectedBreed))
                {
                    await Shell.Current.DisplayAlert("Error", "Please select the dog's breed", "OK");
                    return;
                }
                var dbImage = ImagePath.ToString();
                if (string.IsNullOrEmpty(dbImage))
                {
                    await Shell.Current.DisplayAlert("Error", "Please upload an image of the dog", "OK");
                    return;
                }
                if (FemaleRadio)
                    Dog.Gender = "Female";
                else if (MaleRadio)
                    Dog.Gender = "Male";
                else
                {
                    await Shell.Current.DisplayAlert("Gender", "Please check one", "OK");
                    return;
                }
                Dog.ImagePath = dbImage.Substring("File: ".Length);
                Dog.SellerId = Auth.Id;
                var fetchBreed = await _BreedRepository.GetByBreedAsync(SelectedBreed);
                if (fetchBreed == null)
                    await _BreedRepository.CreateAsync(SelectedBreed);
                var updatedBreed = await _BreedRepository.GetByBreedAsync(SelectedBreed);
                Dog.BreedId = updatedBreed.Id;
                try
                {
                    await _DogRepository.CreateAsync(Dog);
                    Dog = null;
                    MaleRadio = false;
                    FemaleRadio = false;
                    ImagePath = null;
                    SelectedBreed = string.Empty;
                    VisibleImage = false;
                    CreateToast("Data added successfully");
                }
                catch (Exception ex)
                {
                    CreateToast("Error: " + ex);
                }
            }
        }
        private async void CreateToast(string text)
        {
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show();
        }
    }
}
