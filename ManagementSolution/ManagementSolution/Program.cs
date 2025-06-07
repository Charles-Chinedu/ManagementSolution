using Blazored.LocalStorage;
using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Client.ApplicationStates;
using ManagementSolution.Client.Helpers;
using ManagementSolution.Client.Services.Contracts;
using ManagementSolution.Client.Services.Implementation;
using ManagementSolution.Components;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using Syncfusion.Licensing;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSyncfusionBlazor();
        builder.Services.AddScoped<SfDialogService>();

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        /// Configurations
        builder.Services.ConfigureService(builder.Configuration);

        /// Client-Side Services
        builder.Services.AddScoped<AllState>();
        builder.Services.AddTransient<CustomHttpHandler>();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<LocalStorageService>();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<GetHttpClient>();
        builder.Services.AddScoped<IUserAccountService, UserAccountService>();
        builder.Services.AddScoped<UserProfileState>();

        builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        builder.Services.AddScoped<IGenericService<Employee>, GenericService<Employee>>();

        builder.Services.AddHttpClient("SystemApiClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7108/");
            client.Timeout = TimeSpan.FromHours(1);
        })
        .AddHttpMessageHandler<CustomHttpHandler>();


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorWasm",
                builder => builder
                .WithOrigins("https://localhost:7108/", "http://localhost:5175/")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings!.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });

        builder.Services.AddOpenApi();

        var app = builder.Build();
        SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXpdcHRdRmJYWEZ/X0BWYUA=");

        bool isValid = SyncfusionLicenseProvider.ValidateLicense(Platform.Blazor);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowBlazorWasm");

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(ManagementSolution.Client._Imports).Assembly);
        app.MapControllers();
        app.Run();
    }
}