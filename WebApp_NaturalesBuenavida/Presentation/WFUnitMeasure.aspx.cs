using Logic;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentation
{
    public partial class WFUnitMeasure : System.Web.UI.Page
    {
        // Instancio un objeto de la capa lógica para interactuar con los datos de las unidades de medida.

        private static UnitMeasureLog unitMeasureLog = new UnitMeasureLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifico si no es una recarga de la página (no es un postback).
            if (!IsPostBack)
            {
                // aqui Inicialización si es necesario (ejemplo: cargar listas, valores predeterminados, etc.)
            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(Privilegios.UnidadDeMedidad.ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        [WebMethod]
        // Método estático que se invoca desde JavaScript para obtener las unidades de medida.

        public static object ListUnits()
        {
            // Llamo al método ShowUnits para obtener los datos de las unidades de medida.
            var unitData = unitMeasureLog.ShowUnits(); // Retorna un DataSet

            List<object> unitList = new List<object>(); // Creo una lista para almacenar las unidades de medida.


            if (unitData.Tables.Count > 0) // Verifico si el DataSet tiene alguna tabla.
            {
                // Itero sobre las filas de la primera tabla (las unidades de medida).
                foreach (System.Data.DataRow row in unitData.Tables[0].Rows)
                {  
                    // Agrego cada unidad a la lista de objetos.
                    unitList.Add(new
                    {
                        und_id = row["und_id"],
                        und_descripcion = row["und_descripcion"]
                    });
                }
            }

            return new // Retorno un objeto con los datos necesarios para mostrar en la tabla.
            {
                draw = 1,
                recordsTotal = unitList.Count, // Total de registros.
                recordsFiltered = unitList.Count, //Registros filtrados(en este caso, es el mismo número).

                data = unitList // Datos 
            };
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // Obtengo el nombre de la unidad desde el TextBox.
            string unitName = TBUnitName.Text;

            bool executed = unitMeasureLog.SaveUnit(unitName);// Llamo al método SaveUnit para guardar la unidad de medida.

            if (executed)// Verifico si la operación fue exitosa y muestro un mensaje.
            {
                LblMsg.Text = "Unidad de medida guardada exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.Text = "Error al guardar la unidad de medida.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFUnitID.Value))// Verifico que se haya seleccionado una unidad para actualizar.
            {
                LblMsg.Text = "No se ha seleccionado la unidad para actualizar.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            int unitId = Convert.ToInt32(HFUnitID.Value);// Obtengo el ID de la unidad a actualizar desde el HiddenField.

            string unitName = TBUnitName.Text;// Obtengo el nuevo nombre de la unidad desde el TextBox.

            if (string.IsNullOrWhiteSpace(unitName)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El nombre de la unidad no puede estar vacío.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            bool executed = unitMeasureLog.UpdateUnit(unitId, unitName);// Llamo al método Almacenado.

            if (executed)// Verifico si la operación fue exitosa 
            {
                LblMsg.Text = "La unidad de medida se actualizó exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.Text = "Error al actualizar la unidad de medida.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            LblMsg.Text=string.Empty;// Limpio el mensaje.
            ClearFields();// Limpio los campos.
        }

        [WebMethod]
        public static AjaxResponse DeleteUnit(int unitId)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = unitMeasureLog.DeleteUnit(unitId); // Llama a tu método de eliminación

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Unidad eliminada correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar la unidad.";
                }
            }
            catch (Exception ex)// En caso de error, configuro la respuesta con el mensaje de error.
            {
                response.Success = false;
                response.Message = "Ocurrió un error: " + ex.Message;
            }

            return response; 
        }

        private void ClearFields()
        {
            HFUnitID.Value = string.Empty;  // Limpio el HiddenField con el ID de la unidad.
            TBUnitName.Text = string.Empty; // Limpio el TextBox con el nombre de la unidad.
        }
    }
}
