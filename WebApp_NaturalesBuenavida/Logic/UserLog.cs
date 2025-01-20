using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    }
}