using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Producto
    {
        //creando propiedades
        [Required(ErrorMessage = "Se requiere el código de producto")]
        public string code { get; set; }
        [Required(ErrorMessage = "Se requiere la descripción del producto")]
        public string description { get; set; }
        [Required(ErrorMessage = "Se requiere especificar la existencia")]
        public int stock { get; set; }
        [Required(ErrorMessage = "Se requiere el precio")]
        public decimal price { get; set; }
        public byte[] image { get; set; }
        public bool activeProduct { get; set; }

        //creando metodos constructores
        public Producto()
        {

        }

        public Producto(string code, string description, int stock, decimal price, byte[] image, bool activeProduct)
        {
            this.code = code;
            this.description = description;
            this.stock = stock;
            this.price = price;
            this.image = image;
            this.activeProduct = activeProduct;
        }
    }
}
