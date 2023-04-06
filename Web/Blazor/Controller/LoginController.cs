using Datos.Interface;
using Datos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace Blazor.Controller
{
    public class LoginController : Controller
    {
        private readonly Config _config;
        private ILoginRepositorio _loginRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;

        LoginController(Config config)
        {
            _config = config;
            _loginRepositorio = new LoginRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        [HttpPost("Autenticar/Validar")]
        public async Task<IActionResult> validacion(Login login)
        {
            string rol = string.Empty;//almacenando rol temporalmente. Inicialmente la variable estara vacia
            try
            {
                bool usuarioValido = await _loginRepositorio.validacionUsuarioAsync(login);//validando que el user ingrese contrasenia y usuario

                //validando si el user esta activo
                if (usuarioValido)
                {
                    //se consume un metodo de UsuarioRepositorio que permita devolver el usuario. Se consulta a todo el objeto "usuario", para validar el estado y rol tambien
                    Usuario user = await _usuarioRepositorio.TraerPorCodigoAsync(login.UserCode);

                    if (user.Active)
                    {

                    }
                }

            }
            catch (Exception)
            {
            }
        }
    }
}
