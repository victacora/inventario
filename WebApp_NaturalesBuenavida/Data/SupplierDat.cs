using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class SupplierDat
    {
        Persistence objPer = new Persistence();

        // M�todo para insertar un proveedor
        public void InsertSupplier(int personaId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = objPer.openConnection();
            cmd.CommandText = "spInsertSupplier";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_fkpersona_id", personaId);

            cmd.ExecuteNonQuery();
            objPer.closeConnection();
        }

        // M�todo para obtener todos los proveedores
        public DataSet GetSupplier()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = objPer.openConnection();
            cmd.CommandText = "spGetSupplier";
            cmd.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            objPer.closeConnection();
            return ds;
        }

        // M�todo para actualizar un proveedor
        public void UpdateSupplier(int provId, int personaId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = objPer.openConnection();
            cmd.CommandText = "spUpdateSupplier";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_pro_id", provId);
            cmd.Parameters.AddWithValue("p_persona_id", personaId);

            cmd.ExecuteNonQuery();
            objPer.closeConnection();
        }

        // M�todo para eliminar un proveedor
        public bool DeleteSupplier(int provId, int idPersona)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteSupplier"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_pro_id", MySqlDbType.Int32).Value = provId;
            objSelectCmd.Parameters.Add("p_per_id", MySqlDbType.Int32).Value = idPersona;
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


        // M�todo para obtener proveedores en formato DDL
        public DataSet GetSupplierDDL()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = objPer.openConnection();
            cmd.CommandText = "spGetSupplierDDL";
            cmd.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            objPer.closeConnection();
            return ds;
        }
    }
}
