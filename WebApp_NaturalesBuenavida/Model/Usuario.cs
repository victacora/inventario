using System.Collections.Generic;

namespace Model
{
    public class Usuario
    {
        public int Usu_Id { get; set; }
        public int Rol_Id { get; set; }
        public string Rol { get; set; }
        public string Login { get; set; }
        public string Correo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public List<string> Privilegios { get; set; }  
    }
}