using Logic;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentation
{
    public partial class WFCountries : System.Web.UI.Page
    {
        private static PaisLog paisLog = new PaisLog();

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
            var data = paisLog.ShowPais(); // Retorna un DataSet

            List<object> dataList = new List<object>(); // Creo una lista para almacenar los registros.


            if (data.Tables.Count > 0) // Verifico si el DataSet tiene alguna tabla.
            {
                // Itero sobre las filas de la primera tabla.
                foreach (System.Data.DataRow row in data.Tables[0].Rows)
                {
                    // Agrego cada registro a la lista de objetos.
                    dataList.Add(new
                    {
                        id = row["id"],
                        codigo= row["codigo"],
                        pais = row["pais"]
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

            bool executed = paisLog.InsertPais(TBCode.Text,TBCountryName.Text);
            if (executed)
            {
                LblMsg.Text = "Pais almacenado exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.CssClass = "text-danger fw-bold";
                LblMsg.Text = "Error al guardar el pais.";
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

            bool executed = paisLog.UpdatePais(id, TBCode.Text, TBCountryName.Text);// Llamo al método Almacenado.

            if (executed)// Verifico si la operación fue exitosa 
            {
                LblMsg.Text = "El pais se actualizó exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.Text = "Error al actualizar el pais.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        private void checkFields()
        {
            if (string.IsNullOrWhiteSpace(TBCode.Text)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El codigo del pais no puede estar vacío.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            if (string.IsNullOrWhiteSpace(TBCountryName.Text)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El nombre del pais no puede estar vacío.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();// Limpio los campos.
            LblMsg.Text = string.Empty;// Limpio el mensaje.
        }

        [WebMethod]
        public static AjaxResponse Delete(int id)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = paisLog.DeletePais(id); // Llama a tu método de eliminación

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Pais eliminado correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar el pais.";
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
            TBCountryName.Text = string.Empty;
            TBCode.Text = string.Empty;
        }
    }
}
