using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Logic
{
    public class DashBoardLog
    {
        DashboarDat objUser = new DashboarDat();

        //Metodo para mostrar todos los usuarios
        public Object getDasboardData(string fechaIni, string fechaFin)
        {
            return objUser.getDasboardData(fechaIni, fechaFin);
        }

    }
}