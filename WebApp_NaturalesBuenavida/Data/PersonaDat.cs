using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class PersonaDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todas las personas
        public DataSet ShowPersonas()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPersona"; // Procedimiento almacenado para seleccionar personas
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar una nueva persona
        public bool InsertPersona(string identificacion, string nombreRazonSocial, string apellido, string telefono, 
                                  string direccion, string correoElectronico, int fkDocId, int fkPaisId)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertPersona"; // Procedimiento almacenado para insertar persona
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_identificacion", MySqlDbType.VarChar).Value = identificacion;
            objInsertCmd.Parameters.Add("p_nombre_razonsocial", MySqlDbType.VarChar).Value = nombreRazonSocial;
            objInsertCmd.Parameters.Add("p_apellido", MySqlDbType.VarChar).Value = apellido;
            objInsertCmd.Parameters.Add("p_telefono", MySqlDbType.VarChar).Value = telefono;
            objInsertCmd.Parameters.Add("p_direccion", MySqlDbType.VarChar).Value = direccion;
            objInsertCmd.Parameters.Add("p_correo_electronico", MySqlDbType.VarChar).Value = correoElectronico;
            objInsertCmd.Parameters.Add("p_fkdoc_id", MySqlDbType.Int32).Value = fkDocId;
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

        // Método para actualizar una persona existente
        public bool UpdatePersona(int id, string identificacion, string nombreRazonSocial, string apellido, 
                                  string telefono, string direccion, string correoElectronico, int docId, int paisId)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "sp_actualizar_persona"; // Procedimiento almacenado para actualizar persona
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;
            objUpdateCmd.Parameters.Add("p_identificacion", MySqlDbType.VarChar).Value = identificacion;
            objUpdateCmd.Parameters.Add("p_nombre_razonsocial", MySqlDbType.VarChar).Value = nombreRazonSocial;
            objUpdateCmd.Parameters.Add("p_apellido", MySqlDbType.VarChar).Value = apellido;
            objUpdateCmd.Parameters.Add("p_telefono", MySqlDbType.VarChar).Value = telefono;
            objUpdateCmd.Parameters.Add("p_direccion", MySqlDbType.VarChar).Value = direccion;
            objUpdateCmd.Parameters.Add("p_correo_electronico", MySqlDbType.VarChar).Value = correoElectronico;
            objUpdateCmd.Parameters.Add("p_doc_id", MySqlDbType.Int32).Value = docId;
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

        // Método para eliminar una persona
        public bool DeletePersona(int id)
        {
            bool executed = false;
            int row;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "sp_eliminar_persona"; // Procedimiento almacenado para eliminar persona
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
		
		// Método para obtener todas las personas (DDL)
        public DataSet GetPersonasDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPersonaDDL"; // Procedimiento almacenado para el DDL de personas
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet GetPersonById(int personId)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPersonaById"; // Procedimiento almacenado que selecciona una persona por ID
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_persona_id", MySqlDbType.Int32).Value = personId;

            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

    }
}
