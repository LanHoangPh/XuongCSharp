using FluentValidation.AspNetCore;
using FluentValidation;
using MudBlazor.Services;
using MudCilent.Components;
using MudCilent.Services;
using XuongCSharp.Validations;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using MudCilent.Validations;

namespace MudCilent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMudServices();
            builder.Services.AddHttpClient<StaffsService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7206/api/");
            });

            builder.Services.AddHttpClient<SpecializationService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7206/api/");
            });

            builder.Services.AddHttpClient<LocationService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7206/api/");
            });

            builder.Services.AddHttpClient<DepartmentService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7206/api/");
            });
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateStaffDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateStaffDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<AddDepartmentViewModelValidator>();
            builder.Services.AddSingleton<XuongCSharp.Import.ExcelService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
