using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class BuyLog
    {
        BuyDat objBuy = new BuyDat();

        //Metodo para mostrar todas las Compras
        public DataSet showBuy()
        {
            return objBuy.showBuy();
        }

        //Metodo para guardar una nueva Compra
        public bool saveBuy(DateTime _fecha_compra, int _fkproducto_id, int _cantidad, double _precio_unitario, string _numero_factura)
        {
            return objBuy.saveBuy(_fecha_compra, _fkproducto_id, _cantidad, _precio_unitario, _numero_factura);
        }
        //Metodo para actualizar una Compra
        public bool updateBuy(int _compra_id, DateTime _fecha_compra, int _fkproducto_id, int _cantidad, double _precio_unitario, string _numero_factura)
        {
            return objBuy.updateBuy(_compra_id,_fecha_compra, _fkproducto_id, _cantidad, _precio_unitario, _numero_factura);
        }

        public bool deleteBuy(int _idBuy)
        {
            return objBuy.deleteBuy(_idBuy);
        }
    }
}