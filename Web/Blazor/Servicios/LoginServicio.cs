using Blazor.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class LoginServicio : ILoginServicio
    {
        //aqui se implementara el servicio que se declaro en la interfaz "ILoginServicio". Este servicio se consumira en los componentes
        private readonly Config _Config;
        private readonly LoginRepositorio _LoginRepositorio;
        public LoginServicio(Config config)
        {
            _Config = config;
            _LoginRepositorio = new LoginRepositorio(config.CadenaConexion);
        }
        public async Task<bool> validacionUsuarioAsync(Login IniciarSesion)
        {
            return await _LoginRepositorio.validacionUsuarioAsync(IniciarSesion);
        }
    }
}
