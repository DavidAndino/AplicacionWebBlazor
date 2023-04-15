using Blazor;
using Blazor.Interfaces;
using Blazor.Servicios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//configurando servicio de cadena de conexion a la DB
Config Cadena = new Config(builder.Configuration.GetConnectionString("ConexionMySql"));//la cadena de conexion de la DB esta en el archivo "appsettings.json"
builder.Services.AddSingleton(Cadena);//configurando servicio: ahora se puede utilizar en cualquier parte de la app

//configurando servicios de la clase "UsuarioServicio"
builder.Services.AddScoped<ILoginServicio, LoginServicio>();//se le pasa la interfaz y la clase del servicio como parametros
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();//pansando el servicio de tipo de autenticacion
builder.Services.AddHttpContextAccessor();//sirve para poder acceder a los datos del  usuario que inicio sesion
builder.Services.AddResponseCompression();
builder.Services.AddControllers();
builder.Services.AddSweetAlert2();
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

app.UseAuthentication();//indicando que se maneja autenticacion: que exista el user en la dB
app.UseAuthorization();//indicando que se maneja autorizacion: tipos de roles del  usuario

app.MapControllers();//indicando que la app de  Blazor utilizara controladores; sino tira error.
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
