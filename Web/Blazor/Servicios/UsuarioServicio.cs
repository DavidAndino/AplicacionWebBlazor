using Blazor.Interfaces;
using Datos.Interface;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly Config _config;
        private IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServicio(Config config)
        {
            _config = config;
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }
        public async Task<bool> ActualizarAsync(Usuario user)
        {
            return await _usuarioRepositorio.ActualizarAsync(user);
        }

        public Task<bool> EliminarAsync(string codigo)
        {
            return _usuarioRepositorio.EliminarAsync(codigo);
        }

        public Task<bool> NuevoRegistroAsync(Usuario user)
        {
            return _usuarioRepositorio.NuevoRegistroAsync(user);
        }

        public Task<IEnumerable<Usuario>> TraerListaAsync()
        {
            return _usuarioRepositorio.TraerListaAsync();
        }

        public Task<Usuario> TraerPorCodigoAsync(string Codigo)
        {
            return _usuarioRepositorio.TraerPorCodigoAsync(Codigo);
        }
    }
}
