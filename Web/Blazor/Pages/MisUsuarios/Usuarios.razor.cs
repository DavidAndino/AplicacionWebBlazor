using Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisUsuarios
{
    public partial class Usuarios//con "partial" se puede tener la misma clase en varios archivos de  c#, con el fin de separar si es muy extensa
    {
        //aqui se agrega elcodigo de c#, ya que, blazor trabaja con html y c#
        //inyectando primero: ayuda a reducir codigo y a no tener que estar instanciando el  mismo objeto varias veces
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }

        //declarando lista para traer todos los usuarios que hay en la tabla
        private IEnumerable<Usuario> lista { get; set; }

        //sobrescribiendo metodo de carga "load" al abrir app web
        protected override async Task OnInitializedAsync()
        {
            lista = await usuarioServicio.TraerListaAsync();//cuando el componente se cargue, se trae la lista desde el servicio,y se le pasa a la lista anteriormente creada
        }
    }
}
