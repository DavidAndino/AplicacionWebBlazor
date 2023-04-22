using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor.Pages.MisUsuarios
{
    public partial class NuevoUsuario
    {
        //inyectando servicio de la interfaz "IusuarioServicio"
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }
        //inyectando objeto de navegacion, para que cada vez que guarde, devuelva a la  pagina  de lista  de  usuarios
        [Inject] private NavigationManager navigationManager { get; set; }//sirve para navegar entre rutas
        //el bind creado en el componente "EditarUsuario", le hace saber a este objeto "user" a la hora que el usuario hace cambios en el  sitio web
        private Usuario user = new Usuario();//objeto que se consulatara en la DB para luego poder editar un registro
        //capturando parametro que se recibe  en la ruta del componente Razor "EditarUsuario"
        [Inject] private SweetAlertService Swal { get; set; }
        [Parameter] public string CodigoUsuario { get; set; }

        string imgUrl = "";

        private async Task ElegirImagen(InputFileChangeEventArgs e)
        {
            IBrowserFile imagen = e.File;
            //aqui se captura lo que el user selecciono desde el pc
            var buffers = new byte[imagen.Size];

            //luego de capturarla, se le pasa a la propiedad Photo del atributo Photo del metodo de Modelos
            user.Photo = buffers;

            //mostrando vista previa de la imagen
            await imagen.OpenReadStream().ReadAsync(buffers);

            //declarando variable para saber tipo de imagen que se ingresara (png, jpeg...). "ContentType sirve  para cualquier tipo de file"
            string tipoImagen = imagen.ContentType;

            //almacenando imagen en variable global para poderla visualizar
            imgUrl = $"data:{tipoImagen};base64,{Convert.ToBase64String(buffers)}";
        }

        protected async void Guardar()
        {
            //validando que se hayan ingresado datos y que el rol sea distinto de seleccionar 
            if (string.IsNullOrWhiteSpace(user.UserCode) || string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Role) ||
                user.Role == "Seleccionar")//WhiteSpace detecta los  espacios en blanco como vacios y no permite avanzar aun asi
            {
                return;//cancelando ejecucion
            }

            //pasando fecha actual cuando se crea el registro
            user.CreationDate = DateTime.Now;

            //validando que se hayan guardado cambios si asi fue
            bool inserto = await usuarioServicio.NuevoRegistroAsync(user);
            if (inserto)
            {
                await Swal.FireAsync("Éxito", "Usuario Registrado", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Usuarios");//redirijiendo a la pagina de lista de  usuarios
            }
            else
            {
                await Swal.FireAsync("Error", "No se pudo registrar el usuario", SweetAlertIcon.Error);
            }
        }
        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Usuarios");
        }
    }
}
