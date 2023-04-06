using Modelos;

namespace Datos.Interface
{
    public interface ILoginRepositorio
    {
        //en las interfaces solo se declaran los metodos para implementarlos en las clases
        Task<bool> validacionUsuarioAsync(Login IniciarSesion);//declarando metodo asincrono

    }
}
