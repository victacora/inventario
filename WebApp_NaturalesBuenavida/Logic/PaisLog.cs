using System.Data;
using Data;

namespace Logic
{
    public class PaisLog
    {
       PaisDat objPai = new PaisDat();

        // Lógica para mostrar todos los países
        public DataSet ShowPais()
        {
            return objPai.ShowPais();
        }

        // Lógica para insertar un nuevo país
        public bool InsertPais(string codigo, string nombre)
        {
            return objPai.InsertPais(codigo, nombre);
        }

        // Lógica para actualizar un país existente
        public bool UpdatePais(int id, string codigo, string nombre)
        {
            return objPai.UpdatePais(id, codigo, nombre);
        }

        // Lógica para eliminar un país
        public bool DeletePais(int id)
        {
            return objPai.DeletePais(id);
        }

        // Lógica para mostrar DDL de país
        public DataSet ShowPaisDDL()
        {
            return objPai.ShowPaisDDL();
        }
    }
}
