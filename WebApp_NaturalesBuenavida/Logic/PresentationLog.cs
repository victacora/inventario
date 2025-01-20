using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class PresentationLog
    {
        PresentationDat objPresentation = new PresentationDat();

        // Método para mostrar todas las presentaciones
        public DataSet ShowPresentations()
        {
            return objPresentation.ShowPresentations();
        }
        // Método para mostrar EL DDL
        public DataSet ShowPresentationsDDL()
        {
            return objPresentation.ShowPresentationDDL ();
        }

        // Método para guardar una nueva presentación
        public bool SavePresentation(string descripcion)
        {
            return objPresentation.SavePresentation(descripcion);
        }

        // Método para actualizar una presentación
        public bool UpdatePresentation(int presId, string descripcion)
        {
            return objPresentation.UpdatePresentation(presId, descripcion);
        }

        // Método para eliminar una presentación
        public bool DeletePresentation(int presId)
        {
            return objPresentation.DeletePresentation(presId);
        }
    }
}