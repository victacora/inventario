using System.Data;
using Data;

namespace Logic
{
    public class SupplierLog
    {
        SupplierDat objSup = new SupplierDat();

        // Lógica para insertar un proveedor
        public void InsertSupplier(int personaId)
        {
            objSup.InsertSupplier(personaId);
        }

        // Lógica para obtener todos los proveedores
        public DataSet ShowSupplier()
        {
            return objSup.GetSupplier();
        }

        // Lógica para actualizar un proveedor
        public void UpdateSupplier(int provId, int personaId)
        {
            objSup.UpdateSupplier(provId, personaId);
        }

        // Lógica para eliminar un proveedor
        public bool DeleteSupplier(int provId, int idPersona)
        {
            return objSup.DeleteSupplier(provId,idPersona);
        }

        // Lógica para obtener proveedores en formato DDL
        public DataSet ShowSupplierDDL()
        {
            return objSup.GetSupplierDDL();
        }
    }
}
