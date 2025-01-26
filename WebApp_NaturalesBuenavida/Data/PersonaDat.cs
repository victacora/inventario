using Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using static Mysqlx.Crud.Order.Types;

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
                                  string direccion, string correoElectronico, int fkDocId, int fkPaisId, string usuario, string contrasena,
                                  string estado, int fkRolId, int tipo)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertPersona"; // Procedimiento almacenado para insertar persona
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_documento", MySqlDbType.VarChar).Value = identificacion;
            objInsertCmd.Parameters.Add("p_nombres", MySqlDbType.VarChar).Value = nombreRazonSocial;
            objInsertCmd.Parameters.Add("p_apellidos", MySqlDbType.VarChar).Value = apellido;
            objInsertCmd.Parameters.Add("p_direccion", MySqlDbType.VarChar).Value = direccion;
            objInsertCmd.Parameters.Add("p_telefono", MySqlDbType.VarChar).Value = telefono;
            objInsertCmd.Parameters.Add("p_email", MySqlDbType.VarChar).Value = correoElectronico;
            objInsertCmd.Parameters.Add("p_tipo_documento_id", MySqlDbType.Int32).Value = fkDocId;
            objInsertCmd.Parameters.Add("p_ciudad_id", MySqlDbType.Int32).Value = fkPaisId;
            objInsertCmd.Parameters.Add("p_usuario", MySqlDbType.VarChar).Value = usuario;
            objInsertCmd.Parameters.Add("p_contrasena", MySqlDbType.Text).Value = contrasena;
            objInsertCmd.Parameters.Add("p_estado", MySqlDbType.VarChar).Value = estado;
            objInsertCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = fkRolId;
            objInsertCmd.Parameters.Add("p_tipo", MySqlDbType.Int32).Value = tipo;

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
        public bool UpdatePersona(string identificacion, string nombreRazonSocial, string apellido, string telefono,
                                  string direccion, string correoElectronico, int fkDocId, int fkPaisId, string usuario, string contrasena,
                                  string estado, int fkRolId, int tipo, int usuID, int perId)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdatePersona"; // Procedimiento almacenado para actualizar persona
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_documento", MySqlDbType.VarChar).Value = identificacion;
            objUpdateCmd.Parameters.Add("p_nombres", MySqlDbType.VarChar).Value = nombreRazonSocial;
            objUpdateCmd.Parameters.Add("p_apellidos", MySqlDbType.VarChar).Value = apellido;
            objUpdateCmd.Parameters.Add("p_direccion", MySqlDbType.VarChar).Value = direccion;
            objUpdateCmd.Parameters.Add("p_telefono", MySqlDbType.VarChar).Value = telefono;
            objUpdateCmd.Parameters.Add("p_email", MySqlDbType.VarChar).Value = correoElectronico;
            objUpdateCmd.Parameters.Add("p_tipo_documento_id", MySqlDbType.Int32).Value = fkDocId;
            objUpdateCmd.Parameters.Add("p_ciudad_id", MySqlDbType.Int32).Value = fkPaisId;
            objUpdateCmd.Parameters.Add("p_usuario", MySqlDbType.VarChar).Value = usuario;
            objUpdateCmd.Parameters.Add("p_contrasena", MySqlDbType.Text).Value = contrasena;
            objUpdateCmd.Parameters.Add("p_estado", MySqlDbType.VarChar).Value = estado;
            objUpdateCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = fkRolId;
            objUpdateCmd.Parameters.Add("p_tipo", MySqlDbType.Int32).Value = tipo;
            objUpdateCmd.Parameters.Add("p_usu_id", MySqlDbType.Int32).Value = usuID;
            objUpdateCmd.Parameters.Add("p_pers_id", MySqlDbType.Int32).Value = perId;
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
