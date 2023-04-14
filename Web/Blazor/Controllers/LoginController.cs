using Datos.Interface;
using Datos.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;

namespace Blazor.Controllers
{
    public class LoginController : Controller
    {
        private readonly Config _config;
        private ILoginRepositorio _loginRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(Config config)
        {
            _config = config;
            _loginRepositorio = new LoginRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        //metodo para autenticar y abrir sesion
        [HttpPost("/autenticar/validar")]
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
                        rol = user.Role;

                        //creando sesion
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, user.UserCode),//tipo de Claim: un Claim para el codigo usuario y otro para el rol
                            new Claim(ClaimTypes.Role, user.Role)
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);/*pasando el arreglo de "Claims" anterior y tipo de autenticacion
                                                                                  cookies, significa que se utiliza el navegador para auntenticar
                                                                                    en una "cookie" se guardara la sesion (no es la mas segura). al claims identity se le pasan los valores
                                                                                    */
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);//este ejecutara la sesion
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddMinutes(20) });
                        /*Permite acceder al contexto del sitio o pagina web
                         la propiedad (ultimo parametro) es para especificar 
                         cuanto tiempo durara la sesion
                        */
                    }
                    else
                    {
                        //mandando a una ruta, en esta caso a la ruta /login, porque no esta activo el user y no se pudo iniciar sesion
                        return LocalRedirect("/Login/El usuario no está activo");
                    }
                }
                else
                {
                    return LocalRedirect("/Login/Datos ingresados inválidos");
                }

            }
            catch (Exception)
            {
            }
            return LocalRedirect("/");
        }

        //metodo para cerrar la sesion
        [HttpGet("/Salir")]
        public async Task<IActionResult> cierreSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Login");
        }

    }
}
