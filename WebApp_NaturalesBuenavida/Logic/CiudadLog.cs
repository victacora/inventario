using System.Data;
using Data;

namespace Logic
{
    public class CiudadLog
    {
        CiudadDat objCiu = new CiudadDat();

        // Lógica para obtener todas las ciudades
        public DataSet ShowCiudades()
        {
            return objCiu.ShowCiudades();
        }

        // Lógica para insertar una nueva ciudad
        public bool InsertCiudad(string codigo, string nombre, int depId)
        {
            return objCiu.InsertCiudad(codigo, nombre, depId);
        }

        // Lógica para actualizar una ciudad existente
        public bool UpdateCiudad(int id, string codigo, string nombre, int depId)
        {
            return objCiu.UpdateCiudad(id, codigo, nombre, depId);
        }

        // Lógica para eliminar una ciudad
        public bool DeleteCiudad(int id)
        {
            return objCiu.DeleteCiudad(id);
        }

        // Lógica para obtener las ciudades para DDL
        public DataSet ShowCiudadesDDL()
        {
            return objCiu.ShowCiudadesDDL();
        }
    }
}
