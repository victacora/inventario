using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class ClientDat
    {
        Persistence objPer = new Persistence();

        //Metodo para mostrar todos los clientes
        public DataSet showClient()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCliente";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        /// <summary>
        /// Metodo para mostar unicamente del id y el nombre del cliente
        /// </summary>
        /// <returns></returns>
        public DataSet showClientDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectClienteDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        /// <summary>
        /// Metodo para guardar un cliente
        /// </summary>
        /// <param name="_fkpersona_id">Id de la persona que se convertirá en cliente. Foranea</param>
        /// <returns></returns>
        public bool saveClient(int _fkpersona_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertCliente"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_fkpersona_id", MySqlDbType.Int32).Value = _fkpersona_id;

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

        /// <summary>
        /// Metodo para actualizar un cliente
        /// </summary>
        /// <param name="_cliente_id">Id del cliente para ser actualizado</param>
        /// <param name="_fkpersona_id">Id de la persona que se convertirá en cliente. Foranea</param>
        /// <returns></returns>
        public bool updateClient(int _cliente_id, int _fkpersona_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateCliente"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_cliente_id", MySqlDbType.Int32).Value = _cliente_id;
            objSelectCmd.Parameters.Add("p_fkpersona_id", MySqlDbType.Int32).Value = _fkpersona_id;

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

        public bool deleteClient(int idCliente, int idPersona)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteCliente"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_cliente_id", MySqlDbType.Int32).Value = idCliente;
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

        public bool isPersonRegistered(int _fkpersona_id)
        {
            bool exists = false;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spCheckPersonRegisteredAsClient"; // Procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_fkpersona_id", MySqlDbType.Int32).Value = _fkpersona_id;

            try
            {
                exists = Convert.ToBoolean(objSelectCmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }

            objPer.closeConnection();
            return exists;
        }

    }
}