using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class PaisDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todos los países
        public DataSet ShowPais()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPais"; // Procedimiento almacenado para seleccionar países
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar un nuevo país
        public bool InsertPais(string codigo, string nombre)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spCreatePais"; // Procedimiento almacenado para insertar país
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_codigo", MySqlDbType.VarChar).Value = codigo;
            objInsertCmd.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = nombre;

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

        // Método para actualizar un país existente
        public bool UpdatePais(int id, string codigo, string nombre)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdatepais"; // Procedimiento almacenado para actualizar país
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;
            objUpdateCmd.Parameters.Add("p_codigo", MySqlDbType.VarChar).Value = codigo;
            objUpdateCmd.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = nombre;

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

        // Método para eliminar un país
        public bool DeletePais(int id)
        {
            bool executed = false;
            int row;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeletepais"; // Procedimiento almacenado para eliminar país
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

        // Método para mostrar país DDL
        public DataSet ShowPaisDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectDDL = new MySqlCommand();
            objSelectDDL.Connection = objPer.openConnection();
            objSelectDDL.CommandText = "spSelectPaisDDL"; // Procedimiento almacenado para selección DDL de país
            objSelectDDL.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectDDL;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}
