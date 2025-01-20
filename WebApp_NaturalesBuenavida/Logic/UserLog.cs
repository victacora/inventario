using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Logic
{
    public class UserLog
    {
        UserDat objUser = new UserDat();

        //Metodo para mostrar todos los usuarios
        public DataSet showCategories()
        {
            return objUser.showUsers();
        }

        //Metodo para guardar un nuevo usuario
        public bool saveUser(string _user_Name, string _user_Password, string _user_State, DateTime _user_DateCreation, int _user_fkempId, int _user_fkrolId)
        {
            return objUser.saveUser(_user_Name, _user_Password, _user_State, _user_DateCreation, _user_fkempId, _user_fkrolId);
        }
        //Metodo para actualizar un usuario
        public bool updateUser(int _user_Id, string _user_Name, string _user_Password, string _user_State, DateTime _user_DateCreation, int _user_fkempId, int _user_fkrolId)
        {
            return objUser.updateUser(_user_Id, _user_Name, _user_Password, _user_State, _user_DateCreation, _user_fkempId, _user_fkrolId);
        }
        //Metodo para borrar un usuario
        public bool deleteUser(int _user_Id)
        {
            return objUser.deleteUser(_user_Id);
        }

        /// <summary>
        /// Por seguridad las contrasenas almacenas en bd no pueden ser legibles y deben estar cifradas
        /// </summary>
        /// <param name="password">Contrasena del usuario sin cifrar</param>
        /// <returns>Contrasena cifrada</returns>
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Verifica si un usuario se encuentra registrado y tiene acceso a la aplicacion usando un login y password
        /// </summary>
        /// <param name="username">Usuario</param>
        /// <param name="password">Contrasena</param>
        /// <returns>Retorna los datos del usuario registrado si es existoso en caso contrario retorna nulo</returns>
        public Usuario LoginUser(string username, string password)
        {
            string hashPassword = HashPassword(password);
            return objUser.LoginUser(username, hashPassword);
        }
    }
}