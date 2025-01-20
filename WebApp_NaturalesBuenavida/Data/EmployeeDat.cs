using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class EmployeeDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todos los empleados
        public DataSet ShowEmployees()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spGetEmployee"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar un nuevo empleado
        public bool SaveEmployee(int personaId)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertEmployee"; // Nombre del procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_fkpersona_id", MySqlDbType.Int32).Value = personaId; // Parámetro

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

        // Método para actualizar un empleado
        public bool UpdateEmployee(int empId, int personaId)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateEmployee"; // Nombre del procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = empId; // Parámetro 1
            objUpdateCmd.Parameters.Add("p_persona_id", MySqlDbType.Int32).Value = personaId; // Parámetro 2

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

        // Método para eliminar un empleado
        public bool DeleteEmployee(int empId)
        {
            bool executed = false;
            int row;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteEmployee"; // Nombre del procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = empId; // Parámetro

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

        // Método para mostrar empleados con DDL
        public DataSet ShowEmployeesDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spGetEmployeeDDL"; // Procedimiento almacenado para DDL
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}
