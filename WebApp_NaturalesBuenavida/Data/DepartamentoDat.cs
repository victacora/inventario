using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class DepartamentoDat
    {
        Persistence objPer = new Persistence();

        // Método para obtener todos los departamentos
        public DataSet ShowDepartamentos()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectDepartamento"; // Procedimiento almacenado para seleccionar departamentos
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar un nuevo departamento
        public bool InsertDepartamento(string codigo, string nombre, int fkPaisId)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertDepartamento"; // Procedimiento almacenado para insertar departamento
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_codigo", MySqlDbType.VarChar).Value = codigo;
            objInsertCmd.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = nombre;
            objInsertCmd.Parameters.Add("p_fkpais_id", MySqlDbType.Int32).Value = fkPaisId;

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

        // Método para actualizar un departamento existente
        public bool UpdateDepartamento(int id, string codigo, string nombre, int paisId)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "sp_actualizar_departamento"; // Procedimiento almacenado para actualizar departamento
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;
            objUpdateCmd.Parameters.Add("p_codigo", MySqlDbType.VarChar).Value = codigo;
            objUpdateCmd.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = nombre;
            objUpdateCmd.Parameters.Add("p_pais_id", MySqlDbType.Int32).Value = paisId;

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

        // Método para eliminar un departamento
        public bool DeleteDepartamento(int id)
        {
            bool executed = false;
            int row;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "sp_eliminar_departamento"; // Procedimiento almacenado para eliminar departamento
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
		
		// Método para obtener todos los departamentos (DDL)
        public DataSet ShowDepartamentosDDL(int paiId)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectDepartamentoDDL"; // Procedimiento almacenado para el DDL de departamentos
            objSelectCmd.Parameters.Add("p_paisId", MySqlDbType.Int32).Value = paiId;
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}
