using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class InventoryDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todos los registros de inventario
        public DataSet ShowInventory()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectInventario"; // Procedimiento almacenado para mostrar inventario
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet ShowInventorySummary()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectInventariosResumen"; // Procedimiento almacenado para resumen de inventarios
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para obtener los detalles de un inventario
        public DataSet ShowInventoryDetails(int inventoryId)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectInventarioDetalle"; // Procedimiento almacenado para detalles de inventario
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_id_inventario", MySqlDbType.Int32).Value = inventoryId;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar un nuevo registro de inventario
        public bool InsertInventory(int cantidad, DateTime fecha, string observacion, int fkProductoId, int fkEmpleadoId)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertInventario"; // Procedimiento almacenado para insertar inventario
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_inv_cantidad", MySqlDbType.Int32).Value = cantidad;
            objInsertCmd.Parameters.Add("p_inv_fecha", MySqlDbType.Date).Value = fecha;
            objInsertCmd.Parameters.Add("p_inv_observacion", MySqlDbType.Text).Value = observacion;
            objInsertCmd.Parameters.Add("p_prod_id", MySqlDbType.Int32).Value = fkProductoId;
            objInsertCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = fkEmpleadoId;

            try
            {
                row = objInsertCmd.ExecuteNonQuery();
                executed = row == 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para actualizar un registro de inventario
        public bool UpdateInventory(int invId, int cantidad, DateTime fecha, string observacion, int fkProducto, int fkEmpleado)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateInventario"; // Procedimiento almacenado para actualizar inventario
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_inv_id", MySqlDbType.Int32).Value = invId;
            objUpdateCmd.Parameters.Add("p_nueva_cantidad", MySqlDbType.Int32).Value = cantidad;
            objUpdateCmd.Parameters.Add("p_inv_fecha", MySqlDbType.Date).Value = fecha;
            objUpdateCmd.Parameters.Add("p_inv_observacion", MySqlDbType.Text).Value = observacion;
            objUpdateCmd.Parameters.Add("p_prod_id", MySqlDbType.Int32).Value = fkProducto;
            objUpdateCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = fkEmpleado;

            try
            {
                row = objUpdateCmd.ExecuteNonQuery();
                executed = row == 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para eliminar un registro de inventario
        public bool DeleteInventory(int invId)
        {
            bool executed = false;
            int row;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteInventory"; // Procedimiento almacenado para eliminar inventario
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("p_inv_id", MySqlDbType.Int32).Value = invId;

            try
            {
                row = objDeleteCmd.ExecuteNonQuery();
                executed = row == 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para obtener inventario en formato DDL
        public DataSet ShowInventoryDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spGetInventoryDDL"; // Procedimiento almacenado para DDL de inventario
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}
