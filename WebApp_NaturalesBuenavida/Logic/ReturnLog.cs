using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class ReturnLog
    {
        ReturnDat objReturn = new ReturnDat();

        // Método para mostrar todas las devoluciones
        public DataSet ShowReturns()
        {
            return objReturn.ShowReturns();
        }
        // Método para mostrar ventas disponibles (DropDownList)
        public DataSet ShowDevolucionesDDL()
        {
            return objReturn.ShowDevolucionesDDL();
        }

        // Método para guardar una nueva devolución
        public bool SaveReturn(DateTime fechaDevolucion, string motivo, int ventaId)
        {
            return objReturn.SaveReturn(fechaDevolucion, motivo, ventaId);
        }

        // Método para actualizar una devolución
        public bool UpdateReturn(int devId, DateTime fechaDevolucion, string motivo, int ventaId)
        {
            return objReturn.UpdateReturn(devId, fechaDevolucion, motivo, ventaId);
        }

        // Método para eliminar una devolución
        public bool DeleteReturn(int devId)
        {
            return objReturn.DeleteReturn(devId);
        }
    }
}