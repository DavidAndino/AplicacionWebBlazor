using Dapper;
using Datos.Interface;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        //declarando variable que contiene la conexion
        private string cadenaConexion;
        public ProductoRepositorio(string CC)//"CC" cadena de conexion
        {
            cadenaConexion = CC;
        }

        //metodo de conexion a la DB
        private MySqlConnection ConexionDB()
        {
            return new MySqlConnection(cadenaConexion);
        }
        public async Task<bool> ActualizarAsync(Producto product)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();
                //otra forma de declarar una cadena de MySql (distinta a stringbuilder):
                string sql = @"UPDATE product SET Description = @Description, Stock = @Stock, Price = @Price, Image = @Image, ActiveProduct = @ActiveProducto
                                WHERE Code = @Code;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, product));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<bool> EliminarAsync(string Code)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();

                string sql = "DELETE FROM product WHERE Code = @Code;";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { Code }));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<bool> NuevoRegistroAsync(Producto product)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();

                string sql = @"INSERT INTO product (Code, Description, Stock, Price, Image, ActiveProduct)
                                VALUES (@Code, @Description, @Stock, @Price, @Image, @ActiveProduct);";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, product));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<IEnumerable<Producto>> TraerListaAsync()
        {
            IEnumerable<Producto> listaProducts = new List<Producto>();
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();

                string sql = "SELECT * FROM product;";
                listaProducts = await _conexion.QueryAsync<Producto>(sql);//Query (metodo Dapper) devuelve un conjunto de resultados, segun el tipo especificado <>
            }
            catch (Exception)
            {
            }
            return listaProducts;
        }

        public async Task<Producto> TraerPorCodigoAsync(string Code)
        {
            Producto product = new Producto();
            try
            {
                using MySqlConnection _conexion = ConexionDB();
                await _conexion.OpenAsync();

                string sql = "SELECT * FROM product WHERE Code = @Code;";
                product = await _conexion.QueryFirstAsync<Producto>(sql, new { Code });//QueryFirst (metodo Dapper) devuelve un solo resultado, segun el tipo especificado <>
            }
            catch (Exception)
            {
            }
            return product;
        }
    }
}
