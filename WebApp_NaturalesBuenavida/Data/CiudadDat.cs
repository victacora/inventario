using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class CiudadDat
    {
        Persistence objPer = new Persistence();

        // Método para obtener todas las ciudades
        public DataSet ShowCiudades()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCiudad"; // Procedimiento almacenado para seleccionar ciudades
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar una nueva ciudad
        public bool InsertCiudad(string codigo, string nombre, int depId)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertCiudad"; // Procedimiento almacenado para insertar ciudad
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_codigo", MySqlDbType.VarChar).Value = codigo;
            objInsertCmd.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = nombre;
            objInsertCmd.Parameters.Add("p_dep_id", MySqlDbType.Int32).Value = depId;

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

        // Método para actualizar una ciudad existente
        public bool UpdateCiudad(int id, string codigo, string nombre, int depId)
        {
            bool executed = false;
            int row;


            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateCiudad"; // Procedimiento almacenado para actualizar ciudad
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;
            objUpdateCmd.Parameters.Add("p_codigo", MySqlDbType.VarChar).Value = codigo;
            objUpdateCmd.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = nombre;
            objUpdateCmd.Parameters.Add("p_dep_id", MySqlDbType.Int32).Value = depId;

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


        // Método para eliminar una ciudad
        public bool DeleteCiudad(int id)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteCiudad"; // Procedimiento almacenado para eliminar ciudad
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;

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

        // Método para obtener todas las ciudades (DDL)
        public DataSet ShowCiudadesDDL(int idDep)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCiudadDDL"; // Procedimiento almacenado para el DDL de ciudades
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_depId", MySqlDbType.Int32).Value = idDep;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}
