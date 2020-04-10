using Blazor.FileReader;
using BlazorMovies.Client.Auth;
using BlazorMovies.Client.Helpers;
using BlazorMovies.Client.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BlazorMovies.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddBaseAddressHttpClient();
            services.AddOptions();
            services.AddTransient<IRepository, RepositoryInMemory>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IDisplayMessage, DisplayMessage>();
            services.AddScoped<IUsersRepository, UserRepository>();

            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
            services.AddAuthorizationCore();

            services.AddScoped<JWTAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
            );
            services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
               provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
                );

            services.AddScoped<TokenRenewer>();
        }
    }

}
