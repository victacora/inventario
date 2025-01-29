using Model;
using MySql.Data.MySqlClient;
using Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Data
{
    public class DashboarDat
    {
        Persistence objPer = new Persistence();

        public object getDashboardData(string fechaIni, string fechaFin)
        {
            try
            {
                return new
                {
                    ventasPorAnioYMesList = getVentasPorAnioYMes(fechaIni, fechaFin),
                    comprasPorAnioYMesList = getComprasPorAnioYMes(fechaIni, fechaFin),
                    totalInventario = getTotalInventario(fechaIni, fechaFin),
                    totalVentasPorPeriodo = getTotalVentasPorPeriodo(fechaIni, fechaFin),
                    totalComprasPorPeriodo = getTotalComprasPorPeriodo(fechaIni, fechaFin),
                    cantidadVentasPorPeriodo = getCantidadVentasPorPeriodo(fechaIni, fechaFin),
                    cantidadDevolucionesPorPeriodo = getCantidadDevolucionesPorPeriodo(fechaIni, fechaFin),
                    ventasPorCategoria = getVentasPorCategoria(fechaIni, fechaFin),
                    comprasPorClientes = getComprasPorClientes(fechaIni, fechaFin),
                    stockProductosVentasTotales = getStockProductosVentasTotales(fechaIni, fechaFin),
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            return new
            {
                ventasPorAnioYMesList = new List<Object>(),
                comprasPorAnioYMesList = new List<Object>(),
                totalInventario = 0,
                totalVentasPorPeriodo = 0,
                totalComprasPorPeriodo = 0,
                cantidadVentasPorPeriodo = 0,
                cantidadDevolucionesPorPeriodo = 0,
                ventasPorCategoria = new List<Object>(),
                comprasPorClientes = new List<Object>(),
                stockProductosVentasTotales = new List<Object>(),
            }; ;

        }

        public List<object> getVentasPorAnioYMes(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectVentasPorAnioYMes";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            var resultList = new List<object>();
            foreach (DataRow row in objData.Tables[0].Rows)
            {
                resultList.Add(new
                {
                    year = row["year"],
                    month = row["month"],
                    total = row["total"]
                });
            }
            return resultList;
        }

        public List<object> getComprasPorAnioYMes(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectComprasPorAnioYMes";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            var resultList = new List<object>();
            foreach (DataRow row in objData.Tables[0].Rows)
            {
                resultList.Add(new
                {
                    year = row["year"],
                    month = row["month"],
                    total = row["total"]
                });
            }
            return resultList;
        }

        public object getTotalInventario(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectTotalInventario";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            if (objData.Tables[0].Rows.Count > 0)
            {

                return objData.Tables[0].Rows[0]["total_inventario"];
            }
            return 0;
        }

        public object getTotalVentasPorPeriodo(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectTotalVentasPorPeriodo";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            if (objData.Tables[0].Rows.Count > 0)
            {

                return objData.Tables[0].Rows[0]["total_ventas"];
            }
            return 0;
        }

        public object getTotalComprasPorPeriodo(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectTotalComprasPorPeriodo";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            if (objData.Tables[0].Rows.Count > 0)
            {

                return objData.Tables[0].Rows[0]["total_compras"];
            }
            return 0;
        }

        public object getCantidadVentasPorPeriodo(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCantidadVentasPorPeriodo";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            if (objData.Tables[0].Rows.Count > 0)
            {

                return objData.Tables[0].Rows[0]["total_ventas"];
            }
            return 0;
        }

        public object getCantidadDevolucionesPorPeriodo(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCantidadDevolucionesPorPeriodo";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            if (objData.Tables[0].Rows.Count > 0)
            {

                return objData.Tables[0].Rows[0]["total_devoluciones"];
            }
            return 0;
        }

        public List<Categoria> getVentasPorCategoria(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectVentasPorCategoria";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objSelectCmd.Parameters.Add("p_numero_grupos", MySqlDbType.VarChar).Value = 10;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            var resultList = new List<Categoria>();
            double valorTotal = 0;
            foreach (DataRow row in objData.Tables[0].Rows)
            {
                resultList.Add(new
                Categoria
                {
                    name = row["cat_descripcion"].ToString(),
                    y = double.Parse(row["total"].ToString())
                });
                valorTotal += int.Parse(row["total"].ToString());
            }
            foreach (var result in resultList)
            {
                result.y = (result.y / (double)valorTotal) * 100;
            }

            return resultList;
        }

        public List<object> getComprasPorClientes(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectComprasPorClientes";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objSelectCmd.Parameters.Add("p_numero_grupos", MySqlDbType.VarChar).Value = 10;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            var resultList = new List<object>();
            foreach (DataRow row in objData.Tables[0].Rows)
            {
                resultList.Add(new
                {
                    cliente = row["cliente"],
                    total = row["total"]
                });
            }
            return resultList;
        }

        public List<object> getStockProductosVentasTotales(String fechaIni, String fechaFin)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectStockProductosVentasTotales";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_ini", MySqlDbType.VarChar).Value = fechaIni;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.VarChar).Value = fechaFin;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            var resultList = new List<object>();
            foreach (DataRow row in objData.Tables[0].Rows)
            {
                resultList.Add(new
                {
                    producto = row["prod_nombre"],
                    cantidad = row["prod_cantidad_inventario"],
                    promedio = row["promedio"],
                    precio = row["prod_precio_compra"],
                    valor = row["valor_existencias"]
                });
            }
            return resultList;
        }

    }
}