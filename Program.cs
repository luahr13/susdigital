using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------
// CONFIGURAÇÃO DO BANCO DE DADOS SQL SERVER
// --------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// --------------------------------------------------------
// IDENTITY
// --------------------------------------------------------
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // deixe true se quiser confirmação por e-mail
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// --------------------------------------------------------
// MVC + CONTROLLERS + VIEWS
// --------------------------------------------------------
builder.Services.AddControllersWithViews();

// Razor Pages (mantido para login/registro do Identity)
builder.Services.AddRazorPages();

var app = builder.Build();

// --------------------------------------------------------
// PIPELINE DE REQUISIÇÃO
// --------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// --------------------------------------------------------
// ROTAS MVC
// --------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Razor Pages (necessário para Identity)
app.MapRazorPages();

app.Run();
