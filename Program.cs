using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SweetMagic;
using SweetMagic.Components;
using SweetMagic.Data;
using SweetMagic.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>(options =>
   // options.UseSqlServer("Server=localhost;Database=SweetMagic;User Id=sweetMagicApp;Password=batataepao2409@;TrustServerCertificate=True;"));



builder.Services.AddScoped<UserService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
}
else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Manually forcing migrations
using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();  // Ensures the latest migrations are applied
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
