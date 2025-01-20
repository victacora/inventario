using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class UnitMeasureDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar las unidades de medida desde la base de datos.
        public DataSet ShowUnits()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_mostrar_unidad_medida"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }
        // Método para mostrar las unidades de medida mediante el nuevo procedimiento
        public DataSet ShowDDLUnitMeasure()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDDL_mostrar_unidad_medida"; // nombre del procedimiento DDL
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();

            return objData;
        }

        // Método para guardar una nueva unidad de medida
        public bool SaveUnit(string descripcion)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_insertar_unidad_medida"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de la unidad de medida.
            objSelectCmd.Parameters.Add("p_und_descripcion", MySqlDbType.VarString).Value = descripcion;

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

        // Método para actualizar una unidad de medida
        public bool UpdateUnit(int undId, string descripcion)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_actualizar_unidad_medida"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de la unidad de medida.
            objSelectCmd.Parameters.Add("p_und_id", MySqlDbType.Int32).Value = undId;
            objSelectCmd.Parameters.Add("p_und_descripcion", MySqlDbType.VarString).Value = descripcion;

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

        // Método para eliminar una unidad de medida
        public bool DeleteUnit(int undId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_eliminar_unidad_medida"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agrega parámetro al comando para pasar el ID de la unidad de medida.
            objSelectCmd.Parameters.Add("p_und_id", MySqlDbType.Int32).Value = undId;

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