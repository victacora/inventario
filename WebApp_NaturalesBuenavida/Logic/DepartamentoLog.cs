using System.Data;
using Data;

namespace Logic
{
    public class DepartamentoLog
    {
        DepartamentoDat objDep = new DepartamentoDat();

        // Lógica para obtener todos los departamentos
        public DataSet ShowDepartamentos()
        {
            return objDep.ShowDepartamentos();
        }

        // Lógica para insertar un nuevo departamento
        public bool InsertDepartamento(string codigo, string nombre, int fkPaisId)
        {
            return objDep.InsertDepartamento(codigo, nombre, fkPaisId);
        }

        // Lógica para actualizar un departamento existente
        public bool UpdateDepartamento(int id, string codigo, string nombre, int paisId)
        {
            return objDep.UpdateDepartamento(id, codigo, nombre, paisId);
        }

        // Lógica para eliminar un departamento
        public bool DeleteDepartamento(int id)
        {
            return objDep.DeleteDepartamento(id);
        }
		
		// Lógica para obtener los departamentos para DDL
        public DataSet ShowDepartamentosDDL(int paiId)
        {
            return objDep.ShowDepartamentosDDL(paiId);
        }
    }
}
