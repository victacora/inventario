using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class PermitLog
    {
        PermitDat objPer = new PermitDat();

        //Metodo para mostrar todas los permisos
        public DataSet showPermits()
        {
            return objPer.showPermits();
        }

        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showPermitDDL()
        {
            return objPer.showPermitDDL();
        }
        //Metodo para guardar un nuevo permiso
        public bool savePermit(string _perm_Nombre, string _perm_Descripcion)
        {
            return objPer.savePermit(_perm_Nombre, _perm_Descripcion);
        }
        //Metodo para actualizar un permiso
        public bool updatePermit(int _permId, string _permNombre, string _permDescripcion)
        {
            return objPer.updatePermit(_permId, _permNombre, _permDescripcion);
        }
        //Metodo para borrar un permiso
        public bool deletePermit(int _permId)
        {
            return objPer.deletePermit(_permId);
        }
    }
}