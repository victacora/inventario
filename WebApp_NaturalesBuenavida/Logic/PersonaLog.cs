using System.Data;
using Data;

namespace Logic
{
    public class PersonaLog
    {
        PersonaDat objPer = new PersonaDat();

        // Lógica para mostrar todas las personas
        public DataSet ShowPersonas()
        {
            return objPer.ShowPersonas();
        }

        // Lógica para insertar una nueva persona
        public bool InsertPersona(string identificacion, string nombreRazonSocial, string apellido, string telefono,
                                  string direccion, string correoElectronico, int fkDocId, int fkPaisId, string usuario, string contrasena,
                                  string estado, int fkRolId, int tipo)
        {
            return objPer.InsertPersona(identificacion,  nombreRazonSocial,  apellido,  telefono,
                                   direccion,  correoElectronico,  fkDocId,  fkPaisId,  usuario,  contrasena,
                                   estado,  fkRolId,  tipo);
        }

        // Lógica para actualizar una persona existente
        public bool UpdatePersona(string identificacion, string nombreRazonSocial, string apellido, string telefono,
                                  string direccion, string correoElectronico, int fkDocId, int fkPaisId, string usuario, string contrasena,
                                  string estado, int fkRolId, int tipo, int usuID, int perId)
        {
            return objPer.UpdatePersona( identificacion,  nombreRazonSocial,  apellido,  telefono,
                                   direccion,  correoElectronico,  fkDocId,  fkPaisId,  usuario,  contrasena,
                                   estado,  fkRolId,  tipo,  usuID,  perId);
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
    }
}
