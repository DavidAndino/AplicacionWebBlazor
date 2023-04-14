using Modelos;

namespace Blazor.Interfaces
{
    public interface IUsuarioServicio
    {
        //metodos que permitiran la manipulacion de la tabla de usuarios en la dB
        Task<Usuario> TraerPorCodigoAsync(string userCode);
        Task<bool> NuevoRegistroAsync(Usuario user);
        Task<bool> ActualizarAsync(Usuario user);
        Task<bool> EliminarAsync(string userCode);
        Task<IEnumerable<Usuario>> TraerListaAsync();/*metodo que retorna todos los registros de usuarios
                                                     la interfaz "IEnumerable" es de donde se heredan todas las colecciones en C# */
    }
}
