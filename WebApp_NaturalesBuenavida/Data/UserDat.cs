using Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;

namespace Data
{
    public class UserDat
    {
        Persistence objPer = new Persistence();

        //Metodo para mostrar todos los usuarios
        public DataSet showUsers()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spGetAllUsers";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para guardar un nuevo Usuario
        public bool saveUser(string _user_Name, string _user_Password, string _user_State, DateTime _user_DateCreation, int _user_fkempId, int _user_fkrolId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertUser"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_user_Name", MySqlDbType.VarString).Value = _user_Name;
            objSelectCmd.Parameters.Add("p_user_Password", MySqlDbType.VarString).Value = _user_Password;
            objSelectCmd.Parameters.Add("p_user_State", MySqlDbType.VarString).Value = _user_State;
            objSelectCmd.Parameters.Add("p_user_DateCreation", MySqlDbType.DateTime).Value = _user_DateCreation;
            objSelectCmd.Parameters.Add("p_user_fkempId", MySqlDbType.Int32).Value = _user_fkempId;
            objSelectCmd.Parameters.Add("p_user_fkrolId", MySqlDbType.Int32).Value = _user_fkrolId;

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
        //Metodo para actualizar un Usuario
        public bool updateUser(int _user_Id, string _user_Name, string _user_Password, string _user_State, DateTime _user_DateCreation, int _user_fkempId, int _user_fkrolId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateUser"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_user_Id", MySqlDbType.Int32).Value = _user_Id;
            objSelectCmd.Parameters.Add("p_user_Name", MySqlDbType.VarString).Value = _user_Name;
            objSelectCmd.Parameters.Add("p_user_Password", MySqlDbType.VarString).Value = _user_Password;
            objSelectCmd.Parameters.Add("p_user_State", MySqlDbType.VarString).Value = _user_State;
            objSelectCmd.Parameters.Add("p_user_DateCreation", MySqlDbType.DateTime).Value = _user_DateCreation;
            objSelectCmd.Parameters.Add("p_user_fkempId", MySqlDbType.Int32).Value = _user_fkempId;
            objSelectCmd.Parameters.Add("p_user_fkrolId", MySqlDbType.Int32).Value = _user_fkrolId;

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
        //Metodo para borrar un usuario
        public bool deleteUser(int _user_Id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteUser"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_user_Id", MySqlDbType.Int32).Value = _user_Id;

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
        /// Verifica si un usuario se encuentra registrado y tiene acceso a la aplicacion usando un login y password
        /// </summary>
        /// <param name="username">Usuario</param>
        /// <param name="password">Contrasena</param>
        /// <returns>Retorna los datos del usuario registrado si es existoso en caso contrario retorna nulo</returns>
        public Usuario LoginUser(string username, string password)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "sp_login_user";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_username", MySqlDbType.String).Value = username;
            objSelectCmd.Parameters.Add("p_password", MySqlDbType.String).Value = password;
            objAdapter.SelectCommand = objSelectCmd;
            MySqlDataReader reader = objSelectCmd.ExecuteReader();
            if (reader.Read())
            {
                Usuario usuario = new Usuario();
                usuario.Usu_Id = int.Parse(reader["usu_id"].ToString());
                usuario.Rol_Id = int.Parse(reader["tbl_rol_rol_id"].ToString());
                usuario.Login = reader["usuario"].ToString();
                usuario.Correo = reader["correo"].ToString();
                usuario.Nombres = reader["nombres"].ToString();
                usuario.Apellidos = reader["apellidos"].ToString();
                usuario.Rol = reader["rol_nombre"].ToString();
                usuario.Privilegios = reader["privilegios"].ToString().Split(',').ToList();
                return usuario;
            }
            else
            {
                return null;
            }
        }
    }
}