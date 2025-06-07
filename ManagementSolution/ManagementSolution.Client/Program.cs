using Blazored.LocalStorage;
using ManagementSolution.Client.ApplicationStates;
using ManagementSolution.Client.DependencyInjection;
using ManagementSolution.Client.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddTransient<CustomHttpHandler>();
        builder.Services.AddSyncfusionBlazor();
        builder.Services.AddScoped<SfDialogService>();
        builder.Services.AddHttpClient("SystemApiClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7108/");
            client.Timeout = TimeSpan.FromMinutes(2);
        }).AddHttpMessageHandler<CustomHttpHandler>();

        builder.Services.AddAuthorizationCore();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddHttpClient();
        builder.Services.AddScoped<AllState>();
        builder.Services.AddScoped<UserProfileState>();

        builder.Services.AddClientServices(builder.Configuration);
        await builder.Build().RunAsync();
    }
}