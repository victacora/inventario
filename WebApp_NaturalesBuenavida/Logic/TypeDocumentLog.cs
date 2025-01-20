using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class TypeDocumentLog
    {
        TypeDocumentDat objTypeDoc = new TypeDocumentDat();

        //Metodo para mostrar todos los tipos de documentos
        public DataSet showTypesDocument()
        {
            return objTypeDoc.showTypesDocument();
        }

        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showTypesDocumentDDL()
        {
            return objTypeDoc.showTypesDocumentDDL();
        }
        //Metodo para guardar un nuevo tipo de documento
        public bool saveTypeDocument(string _tipodoc)
        {
            return objTypeDoc.saveTypeDocument(_tipodoc);
        }
        //Metodo para actualizar un tipo de documento
        public bool updateTypeDocument(int _docId, string _tipodoc)
        {
            return objTypeDoc.updateTypeDocument(_docId, _tipodoc);
        }
        //Metodo para borrar un tipo de documento
        public bool deleteTypeDocument(int _docId)
        {
            return objTypeDoc.deleteTypeDocument(_docId);
        }
    }
}