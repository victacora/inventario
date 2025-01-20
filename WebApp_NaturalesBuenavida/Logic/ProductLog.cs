using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;

namespace Logic
{
    public class ProductLog
    {
        ProductDat objProd = new ProductDat();

        //Metodo para mostrar todos los productos
        public DataSet showProducts()
        {
            return objProd.showProducts();
        }

        //Metodo para guardar un nuevo producto
        public bool saveProducts(string _cod_producto, string _nombre, string _descripcion, int _cantidad_inventario, string _numerolote, DateTime _fecha_vencimiento,
            double _precio_venta, double _precio_compra, int _medida, int _fkcategoria, int _fkproveedor, int _fkunidadmedida, int _fkpresentacion)
        {
            return objProd.saveProducts(_cod_producto, _nombre, _descripcion, _cantidad_inventario, _numerolote, _fecha_vencimiento, _precio_venta, _precio_compra, _medida, _fkcategoria, _fkproveedor, _fkunidadmedida, _fkpresentacion);
        }
        //Metodo para actualizar un producto
        public bool updateProducts(int _prod_id, string _cod_producto, string _nombre, string _descripcion, int _cantidad_inventario, string _numerolote, DateTime _fecha_vencimiento,
            double _precio_venta, double _precio_compra, int _medida, int _fkcategoria, int _fkproveedor, int _fkunidadmedida, int _fkpresentacion)
        {
            return objProd.updateProducts(_prod_id, _cod_producto, _nombre, _descripcion, _cantidad_inventario, _numerolote, _fecha_vencimiento, _precio_venta, _precio_compra, _medida, _fkcategoria, _fkproveedor, _fkunidadmedida, _fkpresentacion);
        }

        public DataSet showProductsDDL()
        {
            return objProd.GetProductsDDL();
        }

        public bool deleteProduct(int _prodId)
        {
            return objProd.deleteProduct(_prodId);
        }
    }
}