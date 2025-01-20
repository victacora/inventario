using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class SupplierDat
    {
        Persistence objPer = new Persistence();

        // Método para insertar un proveedor
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

        // Método para obtener todos los proveedores
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

        // Método para actualizar un proveedor
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

        // Método para eliminar un proveedor
        public void DeleteSupplier(int provId)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = objPer.openConnection();
            cmd.CommandText = "spDeleteSupplier";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_pro_id", provId);

            cmd.ExecuteNonQuery();
            objPer.closeConnection();
        }

        // Método para obtener proveedores en formato DDL
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
