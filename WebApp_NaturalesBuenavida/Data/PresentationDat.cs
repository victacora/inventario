using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class PresentationDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar las presentaciones desde la base de datos.
        public DataSet ShowPresentations()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_mostrar_presentacion"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }
        // Método para mostrar presentaciones mediante el nuevo procedimiento DDL
        public DataSet ShowPresentationDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDDL_mostrar_presentacion"; // nombre del procedimiento DDL
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Método para guardar una nueva presentación
        public bool SavePresentation(string descripcion)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_insertar_presentacion"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de la presentación.
            objSelectCmd.Parameters.Add("p_pres_descripcion", MySqlDbType.VarString).Value = descripcion;

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

        // Método para actualizar una presentación
        public bool UpdatePresentation(int presId, string descripcion)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_actualizar_presentacion"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de la presentación.
            objSelectCmd.Parameters.Add("p_pres_id", MySqlDbType.Int32).Value = presId;
            objSelectCmd.Parameters.Add("p_pres_descripcion", MySqlDbType.VarString).Value = descripcion;

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

        // Método para eliminar una presentación
        public bool DeletePresentation(int presId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_eliminar_presentacion"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agrega parámetro al comando para pasar el ID de la presentación.
            objSelectCmd.Parameters.Add("p_pres_id", MySqlDbType.Int32).Value = presId;

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