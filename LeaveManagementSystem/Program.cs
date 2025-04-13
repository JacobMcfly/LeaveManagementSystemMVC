using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LeaveManagementSystem.Infraestructure.Persistence.Data;
using LeaveManagementSystem.Application.Interfaces;
using LeaveManagementSystem.Application.Services;
using LeaveManagementSystem.Domain.Interfaces;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Infraestructure.Persistence.Data.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Registrar el repositorio LeaveTypeRepository
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
// Registrar otros servicios
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Esto desactiva la necesidad de confirmar la cuenta
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Web/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Web/Views/Shared/{0}.cshtml");
    });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
// Esta es la forma actual (y no obsoleta):
builder.Services.AddValidatorsFromAssemblyContaining<CreateLeaveTypeDtoValidator>();
builder.Services.AddFluentValidationAutoValidation(options =>
{
    options.DisableDataAnnotationsValidation = true;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else {
    app.UseMigrationsEndPoint(); // Opcional para mostrar UI para aplicar migraciones
    app.UseDeveloperExceptionPage(); // Para mostrar errores generale
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Activar autenticación
app.UseAuthorization();   // Activar autorización

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
    
app.MapRazorPages();

app.Run();
