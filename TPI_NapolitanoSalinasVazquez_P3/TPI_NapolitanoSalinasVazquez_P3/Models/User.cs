namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class User
    {
        private int userID { get; set; }
        public string userName { get; set; }
        protected string userPassword { get; set; }
        public string userMail { get; set; }
        public string userRol { get; set; }

        public (bool, string) Login(string providedUserName, string providedUserPassword) // Provided se utiliza para los valores que se compara con la base de datos, osea son los valores obtenidos en un imput
        {
            if (providedUserName != userName)
            { return (false, "Nombre de usuario incorrecto"); }

            else
            {
                if (providedUserPassword == userPassword)
                {

                    return (true, "Inicio de sesión exitoso");
                }
                else
                {
                    return (false, "Contraseña incorrecta");
                }
            }
        }

        // public (bool,string) Register()


    }
}
