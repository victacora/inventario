using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class ReturnDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar las devoluciones desde la base de datos.
        public DataSet ShowReturns()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_mostrar_devoluciones"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }
        // Método para mostrar devoluciones mediante el nuevo procedimiento DDL
        public DataSet ShowDevolucionesDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDDL_mostrar_devoluciones"; // nombre del procedimiento DDL
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Método para guardar una nueva devolución
        public bool SaveReturn(DateTime fechaDevolucion, string motivo, int ventaId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_insertar_devolucion"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de la devolución.
            objSelectCmd.Parameters.Add("p_dev_fecha_devolucion", MySqlDbType.Date).Value = fechaDevolucion;
            objSelectCmd.Parameters.Add("p_dev_motivo", MySqlDbType.Text).Value = motivo;
            objSelectCmd.Parameters.Add("p_vent_id", MySqlDbType.Int32).Value = ventaId;

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

        // Método para actualizar una devolución
        public bool UpdateReturn(int devId, DateTime fechaDevolucion, string motivo, int ventaId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_actualizar_devolucion"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de la devolución.
            objSelectCmd.Parameters.Add("p_dev_id", MySqlDbType.Int32).Value = devId;
            objSelectCmd.Parameters.Add("p_dev_fecha_devolucion", MySqlDbType.Date).Value = fechaDevolucion;
            objSelectCmd.Parameters.Add("p_dev_motivo", MySqlDbType.Text).Value = motivo;
            objSelectCmd.Parameters.Add("p_vent_id", MySqlDbType.Int32).Value = ventaId;

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

        // Método para eliminar una devolución
        public bool DeleteReturn(int devId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_eliminar_devolucion"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agrega parámetro al comando para pasar el ID de la devolución.
            objSelectCmd.Parameters.Add("p_dev_id", MySqlDbType.Int32).Value = devId;

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