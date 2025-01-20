using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class ClientLog
    {
        ClientDat objClient = new ClientDat();

        //Metodo para mostrar todas las Categorias
        public DataSet showClient()
        {
            return objClient.showClient();
        }

        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showClientDDL()
        {
            return objClient.showClientDDL();
        }
        //Metodo para guardar una nueva Categoria
        public bool saveClient(int _fkpersona_id)
        {
            //return objClient.saveClient(_fkpersona_id);
            // Verificar si la persona ya está registrada como cliente
            if (objClient.isPersonRegistered(_fkpersona_id))
            {
                return false; // Ya registrado, no permitir guardar
            }

            return objClient.saveClient(_fkpersona_id);
        }
        //Metodo para actualizar una Categoria
        public bool updateClient(int _cliente_id, int _fkpersona_id)
        {
            return objClient.updateClient(_cliente_id, _fkpersona_id);
        }
        public bool deleteClient(int _idCliente)
        {
            return objClient.deleteClient(_idCliente);
        }
    }
}