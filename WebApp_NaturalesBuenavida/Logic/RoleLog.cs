using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class RoleLog
    {
        RoleDat objRole = new RoleDat();

        //Metodo para mostrar todos los roles
        public DataSet showRoles()
        {
            return objRole.showRoles();
        }

        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showRolesDDL()
        {
            return objRole.showRolesDDL();
        }
        //Metodo para guardar un nuevo rol
        public bool saveRole(string _role_name, string _role_description)
        {
            return objRole.saveRole(_role_name, _role_description);
        }
        //Metodo para actualizar un rol
        public bool updateRole(int _role_id, string _role_name, string _role_description)
        {
            return objRole.updateRole(_role_id, _role_name, _role_description);
        }
        //Metodo para borrar un rol
        public bool deleteRole(int _role_id)
        {
            return objRole.deleteRole(_role_id);
        }
    }
}