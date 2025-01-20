using System;
using System.Data;
using Data;

namespace Logic
{
    public class InventoryLog
    {
        InventoryDat objInv = new InventoryDat();

        // Lógica para mostrar todos los registros de inventario
        public DataSet ShowInventory()
        {
            return objInv.ShowInventory();
        }

        // Lógica para insertar un nuevo registro de inventario
        public bool AddInventory(int cantidad, DateTime fecha, string observacion, int fkProductoId, int fkEmpleadoId)
        {
            return objInv.InsertInventory(cantidad, fecha, observacion, fkProductoId, fkEmpleadoId);
        }

        // Lógica para actualizar un registro de inventario
        public bool UpdateInventory(int invId, int cantidad, DateTime fecha, string observacion, int fkProducto, int fkEmpleado)
        {
            return objInv.UpdateInventory(invId, cantidad, fecha, observacion, fkProducto, fkEmpleado);
        }

        // Lógica para eliminar un registro de inventario
        public bool DeleteInventory(int invId)
        {
            return objInv.DeleteInventory(invId);
        }

        // Lógica para mostrar el inventario en formato DDL
        public DataSet ShowInventoryDDL()
        {
            return objInv.ShowInventoryDDL();
        }
    }
}
