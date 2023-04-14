using Dapper;
using Datos.Interface;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio//heredando  de  la interfaz "IUsuarioRepositorio"
    {
        //declarando variable que contiene la conexion
        private string cadenaConexion;
        public UsuarioRepositorio(string CC)//"CC" cadena de conexion
        {
            cadenaConexion = CC;
        }

        //metodo de conexion a la DB
        private MySqlConnection ConexionDB()
        {
            return new MySqlConnection(cadenaConexion);
        }

        public async Task<bool> ActualizarAsync(Usuario user)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();
                //otra forma de declarar una cadena de MySql (distinta a stringbuilder):
                string sql = @"UPDATE user SET Name = @Name, Password = @Password, Mail = @Mail, Role = @Role, Active = @Active, Photo = @Photo
                                WHERE UserCode = @UserCode;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, user));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<bool> EliminarAsync(string userCode)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();

                string sql = "DELETE FROM user WHERE UserCode = @UserCode;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { userCode }));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<bool> NuevoRegistroAsync(Usuario user)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();

                string sql = @"INSERT INTO user (UserCode, Name, Password, Mail, Role, CreationDate, Active, Photo)
                                VALUES (@UserCode, @Name, @Password, @Mail, @Role, @CreationDate, @Active, @Photo);";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { user }));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<IEnumerable<Usuario>> TraerListaAsync()
        {
            IEnumerable<Usuario> listaUsers = new List<Usuario>();
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();

                string sql = "SELECT FROM user;";
                listaUsers = await _conexion.QueryAsync<Usuario>(sql);//Query (metodo Dapper) devuelve un conjunto de resultados, segun el tipo especificado <>
            }
            catch (Exception)
            {
            }
            return listaUsers;
        }

        public async Task<Usuario> TraerPorCodigoAsync(string userCode)
        {
            Usuario user = new Usuario();
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();

                string sql = "SELECT * FROM user WHERE  UserCode = @UserCode;";
                user = await _conexion.QueryFirstAsync<Usuario>(sql, new { userCode });//QueryFirst (metodo Dapper) devuelve un solo resultado, segun el tipo especificado <>
            }
            catch (Exception)
            {
            }
            return user;
        }
    }
}
