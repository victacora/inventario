using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum Privilegios
    {
        [Description("Gestión de dashboard")]
        Dashboard = 1,
        [Description("Gestión de productos")]
        Productos = 2,
        [Description("Gestión de ventas")]
        Ventas = 3,
        [Description("Gestión de compras")]
        Compras = 4,
        [Description("Gestión de devoluciones")]
        Devoluciones = 5,
        [Description("Gestión de inventario")]
        Inventario = 6,
        [Description("Gestión de roles")] 
        Roles = 7,
        [Description("Gestión de privilegios")]
        Privilegios = 8,
        [Description("Gestión de  tipos documentos")]
        TiposDocumentos = 9,
        [Description("Gestión de categorias")]
        Categorias = 10,
        [Description("Gestión de presentacion")]
        Presentacion = 11,
        [Description("Gestión de unidad de medida")]
        UnidadDeMedidad = 12,
        [Description("Gestión de paises")]
        Paises = 13,
        [Description("Gestión de departamentos")]
        Departamentos = 13,
        [Description("Gestión de ciudades")]
        Ciudades = 14,
        [Description("Gestión de proveedores")]
        Proveedores = 15,
        [Description("Gestión de empleados")]
        Empleados = 16,
        [Description("Gestión de clientes")]
        Clientes = 17	
    }
}
