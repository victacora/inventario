using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class SalesLog
    {
        SalesDat objSales = new SalesDat();

        // Método para mostrar todas las ventas
        public DataSet ShowSales()
        {   
            return objSales.ShowSales();
        }
        // Mostrar ventas (DDL)
        public DataSet ShowDDLSales()
        {
            return objSales.ShowDDLSales();
        }


        // Método para guardar una nueva venta
        public bool SaveSale(DateTime fecha, double total, string descripcion, int clienteId, int empleadoId)
        {
            return objSales.SaveSale(fecha, total, descripcion, clienteId, empleadoId);
        }

        // Método para actualizar una venta
        public bool UpdateSale(int ventId, DateTime fecha, decimal total, string descripcion, int clienteId, int empleadoId)
        {
            return objSales.UpdateSale(ventId, fecha, total, descripcion, clienteId, empleadoId);
        }

        // Método para eliminar una venta
        public bool DeleteSale(int ventId)
        {
            return objSales.DeleteSale(ventId);
        }
    }
}