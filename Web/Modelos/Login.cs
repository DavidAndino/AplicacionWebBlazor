namespace Modelos
{
    public class Login
    {
        //propiedades
        public string UserCode { get; set; }
        public string Password { get; set; }

        public Login()//generando constructor
        {
        }
        public Login(string userCode, string password)
        {
            UserCode = userCode;
            Password = password;
        }
    }
}
