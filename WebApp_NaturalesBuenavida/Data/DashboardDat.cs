using Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;

namespace Data
{
    public class DashboarDat
    {
        Persistence objPer = new Persistence();

        public  object getDasboardData(string fechaIni, string fechaFin)
        {
            throw new NotImplementedException();
        }

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
      
    }
}