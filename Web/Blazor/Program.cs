using Blazor;
using Blazor.Interfaces;
using Blazor.Servicios;

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
