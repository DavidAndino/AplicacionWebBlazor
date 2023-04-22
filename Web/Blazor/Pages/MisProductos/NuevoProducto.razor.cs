using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    public partial class NuevoProducto
    {
        [Inject] private IProductoServicio _productoServicio { get; set; }//revisar nombre _productoservicio
        [Inject] private NavigationManager navigationManager { get; set; }//sirve para navegar entre rutas
        //el bind creado en el componente "EditarUsuario", le hace saber a este objeto "user" a la hora que el usuario hace cambios en el  sitio web
        private Producto product = new Producto();//objeto que se consulatara en la DB para luego poder editar un registro
        //capturando parametro que se recibe  en la ruta del componente Razor "EditarUsuario"
        [Inject] private SweetAlertService Swal { get; set; }
        string imgUrl = "";

        //metodo que permitira adjuntar imagen que el user agregara desde el sistema operativo
        private async Task ElegirImagen(InputFileChangeEventArgs e)
        {
            IBrowserFile imagen = e.File;
            //aqui se captura lo que el user selecciono desde el pc
            var buffers = new byte[imagen.Size];

            //luego de capturarla, se le pasa a la propiedad Photo del atributo Photo del metodo de Modelos
            product.image = buffers;

            //mostrando vista previa de la imagen
            await imagen.OpenReadStream().ReadAsync(buffers);

            //declarando variable para saber tipo de imagen que se ingresara (png, jpeg...). "ContentType sirve  para cualquier tipo de file"
            string tipoImagen = imagen.ContentType;

            //almacenando imagen en variable global para poderla visualizar
            imgUrl = $"data:{tipoImagen};base64,{Convert.ToBase64String(buffers)}";
        }
        //metodo que guarda el producto
        protected async void Guardar()
        {
            //validando que se hayan ingresado datos
            if (string.IsNullOrWhiteSpace(product.code) || string.IsNullOrWhiteSpace(product.description) ||
                product.stock == 0 || product.price == 0)//WhiteSpace detecta los  espacios en blanco como vacios y no permite avanzar aun asi
            {
                return;//cancelando ejecucion
            }

            //validando que no se pueda repetir la llave primaria del  producto (el codigo). consultando a la base para verificar esto
            Producto productExistente = new Producto();//este objeto recibira el valor

            productExistente = await _productoServicio.TraerPorCodigoAsync(product.code);
            //si este objeto no esta vacio, quiere decir que encontro un codigo coincidente en la base de datos, luego que el user ingresara el codigo del nuevo producto
            if (productExistente != null)
            {
                if (!string.IsNullOrEmpty(productExistente.code))//si no esta vacio ni nulo, quiere decir que ya existe un producto con el mismo codigo
                {
                    await Swal.FireAsync("Error", "El código ingresado ya está registrado para otro producto", SweetAlertIcon.Warning);
                }
            }

            //validando que se hayan guardado cambios si
            bool inserto = await _productoServicio.NuevoRegistroAsync(product);
            if (inserto)
            {
                await Swal.FireAsync("Éxito", "Producto Registrado", SweetAlertIcon.Success);
                navigationManager.NavigateTo("/Productos");//redirijiendo a la pagina de lista de productos
            }
            else
            {
                await Swal.FireAsync("Error", "No se pudo registrar el producto", SweetAlertIcon.Error);
            }
        }
        //metodo que cancela la  creacion del nuevo producto
        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Productos");
        }
    }
}
