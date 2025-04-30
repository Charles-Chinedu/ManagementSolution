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
        builder.Services.AddSyncfusionBlazor();
        builder.Services.AddTransient<CustomHttpHandler>();
        builder.Services.AddHttpClient("SystemApiClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7108/");
            client.Timeout = TimeSpan.FromMinutes(2);
        }).AddHttpMessageHandler<CustomHttpHandler>();
        /// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7108") });
        builder.Services.AddAuthorizationCore();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddHttpClient();
        builder.Services.AddScoped<DepartmentState>();

        builder.Services.AddClientServices(builder.Configuration);
        builder.Services.AddScoped<SfDialogService>();
        await builder.Build().RunAsync();
    }
}