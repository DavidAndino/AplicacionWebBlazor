using Modelos;

namespace Blazor.Interfaces
{
    public interface ILoginServicio
    {
        Task<bool> validacionUsuarioAsync(Login IniciarSesion);//declarando metodo asincrono
    }
}
