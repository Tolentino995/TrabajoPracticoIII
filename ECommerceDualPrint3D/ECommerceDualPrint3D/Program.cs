using Ecommerce.Utility;
using ECommerce.DataAccess;
using ECommerce.DataAccess.Repository;
using ECommerce.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configurar el contexto con la cadena de conexion

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")
));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddRazorPages();

//Agregamos el servicio de envio de correos
builder.Services.AddSingleton<IEmailSender, EmailSender>();

//Agregamos Repositorios de contenedor de Inyeccion de dependencias
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); ;

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

app.UseAuthorization();
app.UseAuthentication();    
app.MapRazorPages();

// Redirigimos manualmente a la pagina deseada  
app.MapGet( "/", context =>
    {
        context.Response.Redirect("/Cliente/Inicio/Index");
        return Task.CompletedTask;
    });

app.Run();
