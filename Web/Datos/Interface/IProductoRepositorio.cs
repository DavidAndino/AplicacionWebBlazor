using Modelos;

namespace Datos.Interface
{
    public interface IProductoRepositorio
    {
        //metodos que permitiran la manipulacion de la tabla de usuarios en la dB
        Task<Producto> TraerPorCodigoAsync(string Code);
        Task<bool> NuevoRegistroAsync(Producto product);
        Task<bool> ActualizarAsync(Producto product);
        Task<bool> EliminarAsync(string Code);
        Task<IEnumerable<Producto>> TraerListaAsync();
    }
}
