namespace Blazor
{
    public class Config
    {
        //esta clase permitira leer la cadena de conexion de la base de datos
        public string CadenaConexion { get; set; }

        public Config(string _CadenaConexion)
        {
            CadenaConexion = _CadenaConexion;//pasando el parametro "_CadenaConexion, a la conexion
        }
    }
}
