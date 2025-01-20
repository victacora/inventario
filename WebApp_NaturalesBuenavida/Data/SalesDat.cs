using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class SalesDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar las ventas desde la base de datos.
        public DataSet ShowSales()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_mostrar_ventas"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }
        // Método para mostrar las ventas desde el procedimiento DDL
        public DataSet ShowDDLSales()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDDL_mostrar_ventas"; // nombre del procedimiento almacenado DDL
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Método para guardar una nueva venta
        public bool SaveSale(DateTime fecha, double total, string descripcion, int clienteId, int empleadoId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_insertar_venta"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de la venta.
            objSelectCmd.Parameters.Add("p_vent_fecha", MySqlDbType.Date).Value = fecha;
            objSelectCmd.Parameters.Add("p_vent_total", MySqlDbType.Double).Value = total;
            objSelectCmd.Parameters.Add("p_vent_descripcion", MySqlDbType.Text).Value = descripcion;
            objSelectCmd.Parameters.Add("p_cli_id", MySqlDbType.Int32).Value = clienteId;
            objSelectCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = empleadoId;

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

        // Método para actualizar una venta
        public bool UpdateSale(int ventId, DateTime fecha, decimal total, string descripcion, int clienteId, int empleadoId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_actualizar_venta"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de la venta.
            objSelectCmd.Parameters.Add("p_vent_id", MySqlDbType.Int32).Value = ventId;
            objSelectCmd.Parameters.Add("p_vent_fecha", MySqlDbType.Date).Value = fecha;
            objSelectCmd.Parameters.Add("p_vent_total", MySqlDbType.Decimal).Value = total;
            objSelectCmd.Parameters.Add("p_vent_descripcion", MySqlDbType.Text).Value = descripcion;
            objSelectCmd.Parameters.Add("p_cli_id", MySqlDbType.Int32).Value = clienteId;
            objSelectCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = empleadoId;

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

        // Método para eliminar una venta
        public bool DeleteSale(int ventId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_eliminar_venta"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agrega parámetro al comando para pasar el ID de la venta.
            objSelectCmd.Parameters.Add("p_vent_id", MySqlDbType.Int32).Value = ventId;

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