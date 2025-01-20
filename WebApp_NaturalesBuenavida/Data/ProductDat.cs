using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class ProductDat
    {
        Persistence objPer = new Persistence();

        /// <summary>
        /// Metodo para mostrar todos los productos
        /// </summary>
        /// <returns></returns>
        public DataSet showProducts()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectProductos";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        /// <summary>
        /// Metodo para guardar un nuevo producto
        /// </summary>
        /// <param name="_cod_producto">Codigo del producto string</param>
        /// <param name="_nombre">nombre del producto string</param>
        /// <param name="_descripcion">descripcion de un producto string</param>
        /// <param name="_cantidad_inventario">cantidad actual en inventario int</param>
        /// <param name="_numerolote">numerlo de lote del producto</param>
        /// <param name="_fecha_vencimiento">fecha vencimiento del producto yyyy-mm-dd</param>
        /// <param name="_precio_venta">precio venta del producto</param>
        /// <param name="_precio_compra">precio de compra del producto</param>
        /// <param name="_medida">medida en números respecto a la cantidad, sólo número sin unidad de medida</param>
        /// <param name="_fkcategoria">id categoria del producto Foranea</param>
        /// <param name="_fkproveedor">id proveedor Foranea</param>
        /// <param name="_fkunidadmedida">id unidad de medida foranea</param>
        /// <param name="_fkpresentacion">id tipo de presentación (Caja, tetrapack, tubo, botella, etc.) Foranea</param>
        /// <returns></returns>
        public bool saveProducts(
            string _cod_producto, string _nombre, string _descripcion, int _cantidad_inventario, string _numerolote, DateTime _fecha_vencimiento,
            double _precio_venta, double _precio_compra, int _medida, int _fkcategoria, int _fkproveedor, int _fkunidadmedida, int _fkpresentacion
            )
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertProduct"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_cod_producto", MySqlDbType.VarString).Value = _cod_producto;
            objSelectCmd.Parameters.Add("p_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_descripcion", MySqlDbType.VarString).Value = _descripcion;
            objSelectCmd.Parameters.Add("p_cantidad_inventario", MySqlDbType.Int32).Value = _cantidad_inventario;
            objSelectCmd.Parameters.Add("p_numerolote", MySqlDbType.VarString).Value = _numerolote;
            objSelectCmd.Parameters.Add("p_fecha_vencimiento", MySqlDbType.DateTime).Value = _fecha_vencimiento;
            objSelectCmd.Parameters.Add("p_precio_venta", MySqlDbType.Double).Value = _precio_venta;
            objSelectCmd.Parameters.Add("p_precio_compra", MySqlDbType.Double).Value = _precio_compra;
            objSelectCmd.Parameters.Add("p_medida", MySqlDbType.Int32).Value = _medida;
            objSelectCmd.Parameters.Add("p_fkcategoria", MySqlDbType.Int32).Value = _fkcategoria;
            objSelectCmd.Parameters.Add("p_fkproveedor", MySqlDbType.Int32).Value = _fkproveedor;
            objSelectCmd.Parameters.Add("p_fkunidadmedida", MySqlDbType.Int32).Value = _fkunidadmedida;
            objSelectCmd.Parameters.Add("p_fkpresentacion", MySqlDbType.Int32).Value = _fkpresentacion;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;

        }

        /// <summary>
        /// Metodo para actualizar un producto
        /// </summary>
        /// <param name="_prod_id">Id del producto para poderlo actualizar</param>
        /// <param name="_cod_producto">Codigo del producto string</param>
        /// <param name="_nombre">nombre del producto string</param>
        /// <param name="_descripcion">descripcion de un producto string</param>
        /// <param name="_cantidad_inventario">cantidad actual en inventario int</param>
        /// <param name="_numerolote">numerlo de lote del producto</param>
        /// <param name="_fecha_vencimiento">fecha vencimiento del producto yyyy-mm-dd</param>
        /// <param name="_precio_venta">precio venta del producto</param>
        /// <param name="_precio_compra">precio de compra del producto</param>
        /// <param name="_medida">medida en números respecto a la cantidad, sólo número sin unidad de medida</param>
        /// <param name="_fkcategoria">id categoria del producto Foranea</param>
        /// <param name="_fkproveedor">id proveedor Foranea</param>
        /// <param name="_fkunidadmedida">id unidad de medida foranea</param>
        /// <param name="_fkpresentacion">id tipo de presentación (Caja, tetrapack, tubo, botella, etc.) Foranea</param>
        /// <returns></returns>
        public bool updateProducts(
            int _prod_id, string _cod_producto, string _nombre, string _descripcion, int _cantidad_inventario, string _numerolote, DateTime _fecha_vencimiento,
            double _precio_venta, double _precio_compra, int _medida, int _fkcategoria, int _fkproveedor, int _fkunidadmedida, int _fkpresentacion
            )
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateProducto"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_prod_id", MySqlDbType.Int32).Value = _prod_id;
            objSelectCmd.Parameters.Add("p_cod_producto", MySqlDbType.VarString).Value = _cod_producto;
            objSelectCmd.Parameters.Add("p_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_descripcion", MySqlDbType.VarString).Value = _descripcion;
            objSelectCmd.Parameters.Add("p_cantidad_inventario", MySqlDbType.Int32).Value = _cantidad_inventario;
            objSelectCmd.Parameters.Add("p_numerolote", MySqlDbType.VarString).Value = _numerolote;
            objSelectCmd.Parameters.Add("p_fecha_vencimiento", MySqlDbType.DateTime).Value = _fecha_vencimiento;
            objSelectCmd.Parameters.Add("p_precio_venta", MySqlDbType.Double).Value = _precio_venta;
            objSelectCmd.Parameters.Add("p_precio_compra", MySqlDbType.Double).Value = _precio_compra;
            objSelectCmd.Parameters.Add("p_medida", MySqlDbType.Int32).Value = _medida;
            objSelectCmd.Parameters.Add("p_fkcategoria", MySqlDbType.Int32).Value = _fkcategoria;
            objSelectCmd.Parameters.Add("p_fkproveedor", MySqlDbType.Int32).Value = _fkproveedor;
            objSelectCmd.Parameters.Add("p_fkunidadmedida", MySqlDbType.Int32).Value = _fkunidadmedida;
            objSelectCmd.Parameters.Add("p_fkpresentacion", MySqlDbType.Int32).Value = _fkpresentacion;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;

        }

        public DataSet GetProductsDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectProductDDL"; // Procedimiento almacenado para el DDL de productos
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public bool deleteProduct(int _prodId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteProduct"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agrega parámetro al comando para pasar el ID de la presentación.
            objSelectCmd.Parameters.Add("p_product_id", MySqlDbType.Int32).Value = _prodId;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }
    }

}