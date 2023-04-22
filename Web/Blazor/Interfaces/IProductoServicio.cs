using Modelos;

namespace Blazor.Interfaces
{
    public interface IProductoServicio
    {
        Task<Producto> TraerPorCodigoAsync(string Code);
        Task<bool> NuevoRegistroAsync(Producto product);
        Task<bool> ActualizarAsync(Producto product);
        Task<bool> EliminarAsync(string Code);
        Task<IEnumerable<Producto>> TraerListaAsync();
    }
}
