using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisUsuarios
{
    public partial class EditarUsuario
    {
        //inyectando servicio de la interfaz "IusuarioServicio"
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }
        //inyectando objeto de navegacion, para que cada vez que guarde, devuelva a la  pagina  de lista  de  usuarios
        [Inject] private NavigationManager navigationManager { get; set; }//sirve para navegar entre rutas
        //el bind creado en el componente "EditarUsuario", le hace saber a este objeto "user" a la hora que el usuario hace cambios en el  sitio web
        private Usuario user = new Usuario();//objeto que se consulatara en la DB para luego poder editar un registro
        //capturando parametro que se recibe  en la ruta del componente Razor "EditarUsuario"
        [Inject] private SweetAlertService Swal { get; set; }
        [Parameter] public string CodigoUsuario { get; set; }  //revisar nombre

        //sobreescribiendo el metodo  que se carga al cargar el componente, ya que, este metodo se carga al momento  de  cargar el componente
        protected override async Task OnInitializedAsync()
        {
            //consultando a la base de datos si traer el objeto "User" y llenarlo con el objeto antes creado arriba
            //validando que el parametro no venga  vacio
            if (!string.IsNullOrEmpty(CodigoUsuario))
            {
                user = await usuarioServicio.TraerPorCodigoAsync(CodigoUsuario);//objeto "user" consultado y cargado
            }
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

            //validando que se hayan guardado cambios si asi fue
            bool edito = await usuarioServicio.ActualizarAsync(user);
            if (edito)
            {
                await Swal.FireAsync("Éxito", "Usuario Actualizado", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Usuarios");//redirijiendo a la pagina de lista de  usuarios
            }
            else
            {
                await Swal.FireAsync("Error", "No se pudo actualizar el usuario", SweetAlertIcon.Error);
            }
        }
        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Usuarios");
        }
        protected async void Eliminar()
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions//si da click en "Si", el result se llena
            {
                Title = "¿Seguro que quiere eliminar el usuario seleccionado?",
                Text = "Esta acción no se podrá revertir",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Sí",
                CancelButtonText = "No"
            });
            //validando que se elimine el registro
            if (!string.IsNullOrEmpty(result.Value))//si el result esta lleno, significa que se dio click en "Si"
            {
                bool elimino = await usuarioServicio.EliminarAsync(user.UserCode);//eliminando registro
                if (elimino)
                {
                    await Swal.FireAsync("Éxito", "El Usuario ha sido eliminado", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Usuarios");//redirijiendo a la pagina de lista de  usuarios
                }
                else
                {
                    await Swal.FireAsync("Error", "No se pudo eliminar el usuario", SweetAlertIcon.Error);
                }
            }
        }
    }
}
//creando enumerable  que se reutilizara en el inputselect o comboBox
enum Roles
{
    //estos se mostraran en el inputselect
    Seleccionar,
    Administrador,
    Usuario
}
