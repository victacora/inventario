using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class PersonLog
    {
            PersonaDat objPer = new PersonaDat();

            // Lógica para mostrar todas las personas
            public DataSet ShowPersonas()
            {
                return objPer.ShowPersonas();
            }

            // Lógica para insertar una nueva persona
            public bool InsertPersona(string identificacion, string nombreRazonSocial, string apellido, string telefono,
                                   string direccion, string correoElectronico, int fkDocId, int fkPaisId)
            {
                return objPer.InsertPersona(identificacion, nombreRazonSocial, apellido, telefono, direccion,
                                                 correoElectronico, fkDocId, fkPaisId);
            }

            // Lógica para actualizar una persona existente
            public bool UpdatePersona(int id, string identificacion, string nombreRazonSocial, string apellido,
                                    string telefono, string direccion, string correoElectronico, int docId, int paisId)
            {
                return objPer.UpdatePersona(id, identificacion, nombreRazonSocial, apellido, telefono,
                                                 direccion, correoElectronico, docId, paisId);
            }

            // Lógica para eliminar una persona
            public bool DeletePersona(int id)
            {
                return objPer.DeletePersona(id);
            }

            // Lógica para obtener las personas para DDL
            public DataSet ShowPersonasDDL()
            {
                return objPer.GetPersonasDDL();
            }

        public DataSet GetPersonById(int personId)
        {
            return objPer.GetPersonById(personId);
        }


    }
}
