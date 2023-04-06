using Modelos;

namespace Datos.Interface
{
    public interface IUsuarioRepositorio
    {
        //metodos que permitiran la manipulacion de la tabla de usuarios en la dB
        Task<Usuario> TraerPorCodigoAsync(string Codigo);
        Task<bool> NuevoRegistroAsync(Usuario user);
        Task<bool> ActualizarAsync(Usuario user);
        Task<bool> EliminarAsync(string codigo);
        Task<IEnumerable<Usuario>> TraerListaAsync();/*metodo que retorna todos los registros de usuarios
                                                     la interfaz "IEnumerable" es de donde se heredan todas las colecciones en C# */
    }
}
