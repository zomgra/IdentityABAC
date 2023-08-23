using Domain.Authentication;
using Duende.IdentityServer.Services;
using IdentityServer.Configurations;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityDbContext>(x=>x.UseInMemoryDatabase("IdentityDbContext"));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(c =>
{
    c.SignIn.RequireConfirmedPhoneNumber = false;
    c.Password.RequireNonAlphanumeric = false;
    c.Password.RequiredLength = 5;
    c.Password.RequireUppercase = false;
})
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddIdentityServer(options =>
{
    
})
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryApiScopes(IdentityConfiguration.GetApiScopes())
    .AddInMemoryIdentityResources(IdentityConfiguration.GetIdentityResources())
    .AddInMemoryClients(IdentityConfiguration.GetClients())
    .AddProfileService<ProfileService>()
    .AddDeveloperSigningCredential();

builder.Services.AddScoped<IProfileService, ProfileService>();


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

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
