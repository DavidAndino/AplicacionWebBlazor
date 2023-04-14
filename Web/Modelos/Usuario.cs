using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Usuario
    {
        //propiedades
        //aplicando el DataAnnotationValidator en los objetos que no pueden ser nulos
        [Required(ErrorMessage = "Se requiere el código de usuario")]
        public string UserCode { get; set; }
        [Required(ErrorMessage = "Se requiere el nombre del usuario")]
        public String Name { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        [Required(ErrorMessage = "Se requiere el rol")]
        public string Role { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public byte[] Photo { get; set; }

        //creando constructor vacio
        public Usuario()
        {

        }

        public Usuario(string userCode, string name, string password, string mail, string role, DateTime creationDate, bool active, byte[] photo)
        {
            UserCode = userCode;
            Name = name;
            Password = password;
            Mail = mail;
            Role = role;
            CreationDate = creationDate;
            Active = active;
            Photo = photo;
        }

        //creando constructor con parametros


    }
}
