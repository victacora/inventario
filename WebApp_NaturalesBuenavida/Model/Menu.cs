using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum Menu
    {
        [Description("Menu principal")]
        Principal = 1,
        [Description("Menu configuracion")]
        Configuracion = 2,
        [Description("Menu usuarios")]
        Usuarios = 3
    }
}
