using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;

namespace Presentation
{
    public partial class WFPresentation : System.Web.UI.Page
    {
        private static PresentationLog objPresentationLog = new PresentationLog();

        // Variables privadas
        private int _presId;
        private string _description;
        private bool _executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }

        [WebMethod]
        public static object ListPresentations()
        {
            var dataSet = objPresentationLog.ShowPresentations();

            var presentationsList = new List<object>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                presentationsList.Add(new
                {
                    PresID = row["pres_id"],
                    Descripcion = row["pres_descripcion"]
                });
            }

            return new { data = presentationsList };
        }


        [WebMethod]
        public static object DeletePresentation(int presId)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = objPresentationLog.DeletePresentation(presId); // Llama a tu método de eliminación

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Presentción eliminada correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar la presentación.";
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _description = TBDescription.Text.Trim();

            if (!string.IsNullOrEmpty(_description))
            {
                _executed = objPresentationLog.SavePresentation(_description);

                if (_executed)
                {
                    LblMsg.Text = "Presentación guardada con éxito.";
                    LblMsg.CssClass = "text-success fw-bold";
                    ClearFields();
                }
                else
                {
                    LblMsg.Text = "Error al guardar la presentación.";
                    LblMsg.CssClass = "text-danger fw-bold";
                }
            }
            else
            {
                LblMsg.Text = "La descripción no puede estar vacía.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(HFPresentationID.Value, out _presId) && !string.IsNullOrEmpty(TBDescription.Text.Trim()))
            {
                _description = TBDescription.Text.Trim();
                _executed = objPresentationLog.UpdatePresentation(_presId, _description);

                if (_executed)
                {
                    LblMsg.Text = "Presentación actualizada con éxito.";
                    LblMsg.CssClass = "text-success";
                    ClearFields();
                }
                else
                {
                    LblMsg.Text = "Error al actualizar la presentación.";
                    LblMsg.CssClass = "text-danger fw-bold";
                }
            }
            else
            {
                LblMsg.Text = "Por favor, seleccione una presentación válida y complete la descripción.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

      
        private void ClearFields()
        {
            HFPresentationID.Value = string.Empty;
            TBDescription.Text = string.Empty;
            LblMsg.Text = string.Empty;
        }
    }
}
