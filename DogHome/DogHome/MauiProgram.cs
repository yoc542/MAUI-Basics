using DogHome.Interface;
using DogHome.Repositories;
using DogHome.ViewModel;
using DogHome.Views;
using Microsoft.Extensions.Logging;

namespace DogHome
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
               .UseMauiApp<App>()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                   fonts.AddFont("fa-brands-400.ttf", "FaBrands");
                   fonts.AddFont("fa-solid-900.ttf", "FaSolid");
               });
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<IAuthRepository, AuthRepository>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<IBreedRepository, BreedRepository>();
            builder.Services.AddTransient<IRepository, DogRepository>();
            builder.Services.AddTransient<IFormRepository, FormRepository>();
            builder.Services.AddTransient<AddDogsPage>();
            builder.Services.AddTransient<AddDogsViewModel>();
            builder.Services.AddTransient<MainDogsPage>();
            builder.Services.AddTransient<MainDogsViewModel>();
            builder.Services.AddTransient<SellerDogsPage>();
            builder.Services.AddTransient<SellerDogsViewModel>();
            builder.Services.AddTransient<SellersListPage>();
            builder.Services.AddTransient<SellersListViewModel>();
            builder.Services.AddTransient<BuyerFormPage>();
            builder.Services.AddTransient<BuyerFormViewModel>();
            builder.Services.AddTransient<FormSubmitPage>();
            builder.Services.AddTransient<FormSubmitViewModel>();
            builder.Services.AddTransient<DogDetailPage>();
            builder.Services.AddTransient<DogDetailViewModel>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
