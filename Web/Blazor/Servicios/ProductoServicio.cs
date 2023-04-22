using Blazor.Interfaces;
using Datos.Interface;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly Config _config;
        private IProductoRepositorio _productoRepositorio;

        public ProductoServicio(Config config)
        {
            _config = config;
            _productoRepositorio = new ProductoRepositorio(config.CadenaConexion);
        }
        public async Task<bool> ActualizarAsync(Producto product)
        {
            return await _productoRepositorio.ActualizarAsync(product);
        }

        public async Task<bool> EliminarAsync(string Code)
        {
            return await _productoRepositorio.EliminarAsync(Code);
        }

        public async Task<bool> NuevoRegistroAsync(Producto product)
        {
            return await _productoRepositorio.NuevoRegistroAsync(product);
        }

        public async Task<IEnumerable<Producto>> TraerListaAsync()
        {
            return await _productoRepositorio.TraerListaAsync();
        }

        public async Task<Producto> TraerPorCodigoAsync(string Code)
        {
            return await _productoRepositorio.TraerPorCodigoAsync(Code);
        }
    }
}
