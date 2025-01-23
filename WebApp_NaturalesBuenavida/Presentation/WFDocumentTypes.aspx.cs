using Logic;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentation
{
    public partial class WFDocumentTypes : System.Web.UI.Page
    {
        private static TypeDocumentLog typeDocumentLog = new TypeDocumentLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || !usuario.Rol.Equals("Administrador"))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        [WebMethod]
        public static object ListData()
        {
            var data = typeDocumentLog.showTypesDocument(); // Retorna un DataSet

            List<object> dataList = new List<object>(); // Creo una lista para almacenar los registros.


            if (data.Tables.Count > 0) // Verifico si el DataSet tiene alguna tabla.
            {
                // Itero sobre las filas de la primera tabla.
                foreach (System.Data.DataRow row in data.Tables[0].Rows)
                {
                    // Agrego cada registro a la lista de objetos.
                    dataList.Add(new
                    {
                        doc_id = row["doc_id"],
                        doc_tipo_documento = row["doc_tipo_documento"]
                    });
                }
            }

            return new // Retorno un objeto con los datos necesarios para mostrar en la tabla.
            {
                draw = 1,
                recordsTotal = dataList.Count, // Total de registros.
                recordsFiltered = dataList.Count, //Registros filtrados(en este caso, es el mismo número).
                data = dataList // Datos 
            };
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            checkFields();

            bool executed = typeDocumentLog.saveTypeDocument(TBDocumentType.Text);
            if (executed)
            {
                LblMsg.Text = "Tipo documento almacenado exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.CssClass = "text-danger fw-bold";
                LblMsg.Text = "Error al guardar el tipo documento.";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFID.Value))// Verifico que se haya seleccionado un registro para actualizar.
            {
                LblMsg.Text = "No se ha seleccionado un registro para actualizar.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            int id = int.Parse(HFID.Value);

            checkFields();

            bool executed = typeDocumentLog.updateTypeDocument(id, TBDocumentType.Text);// Llamo al método Almacenado.

            if (executed)// Verifico si la operación fue exitosa 
            {
                LblMsg.Text = "El tipo documento se actualizó exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.Text = "Error al actualizar el tipo documento.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        private void checkFields()
        {
            if (string.IsNullOrWhiteSpace(TBDocumentType.Text)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El tipod de documento no puede estar vacío.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();// Limpio los campos.
            LblMsg.Text = string.Empty;
        }

        [WebMethod]
        public static AjaxResponse Delete(int id)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = typeDocumentLog.deleteTypeDocument(id); // Llama a tu método de eliminación

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Tipo documento eliminado correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar el tipo documento.";
                }
            }
            catch (Exception ex)
            {
                // En caso de error, configuro la respuesta con el mensaje de error.
                response.Success = false;
                response.Message = "Ocurrió un error: " + ex.Message;
            }

            return response; 
        }

        private void ClearFields()
        {
            HFID.Value = string.Empty;  
            TBDocumentType.Text = string.Empty;
        }
    }
}
