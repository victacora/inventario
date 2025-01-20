using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class CategoryDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todas las categorías
        public DataSet ShowCategories()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spGetCategory"; // Procedimiento almacenado para mostrar categorías
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para crear una nueva categoría
        public bool CreateCategory(string descripcion)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spCreateCategory"; // Procedimiento almacenado para insertar categoría
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 45).Value = descripcion;

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

        // Método para actualizar una categoría
        public bool UpdateCategory(int catId, string descripcion)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateCategory"; // Procedimiento almacenado para actualizar categoría
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_cat_id", MySqlDbType.Int32).Value = catId;
            objUpdateCmd.Parameters.Add("p_descripcion", MySqlDbType.VarChar, 45).Value = descripcion;

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

        // Método para eliminar una categoría
        public bool DeleteCategory(int catId)
        {
            bool executed = false;
            int row;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteCategory"; // Procedimiento almacenado para eliminar categoría
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("p_cat_id", MySqlDbType.Int32).Value = catId;

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

        // Método para obtener categorías en formato DDL
        public DataSet ShowCategoriesDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spGetCategoryDDL"; // Procedimiento almacenado para DDL de categorías
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}
