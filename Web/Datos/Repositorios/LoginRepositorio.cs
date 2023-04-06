using Dapper;
using Datos.Interface;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private string cadenaConexion;//esta variable recibe la cadena de conexion desde el proyecto Blazor

        public LoginRepositorio(string CC)
        {
            cadenaConexion = CC;
        }

        //este metodo retorna una nueva conexion de MySql
        private MySqlConnection conexion()
        {
            return new MySqlConnection(cadenaConexion);
        }

        //implementando todos los metodos declarados anteriormente
        public async Task<bool> validacionUsuarioAsync(Login InicioSesion)
        {
            bool valido = false;
            try
            {
                //el micro ORM ayuda a mapear la  DB, lo  que hace que los codigos sean mas resumidos
                using MySqlConnection Conexion = conexion();//asignandole el metodo "conexion"  que retorna un  objeto "MySqlConnection"
                await Conexion.OpenAsync();
                //declarando sentencia SQL
                string sql = "SELECT 1 FROM user WHERE UserCode = @UserCode AND Password = @Password;";//retornando "1" si encuentra una coincidencia en la tabla de user
                valido = await Conexion.ExecuteScalarAsync<bool>(sql, InicioSesion);//executeScalar solo devuelve 2 valores: 1 o 0
            }
            catch (Exception)
            {
            }
            return valido;
        }
    }
}
