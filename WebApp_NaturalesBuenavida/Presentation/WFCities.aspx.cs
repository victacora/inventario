using Logic;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentation
{
    public partial class WFCities : System.Web.UI.Page
    {
        private static DepartamentoLog departamentoLog = new DepartamentoLog();
        private static CiudadLog ciudadLog = new CiudadLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDDLStates();
            }

            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(Privilegios.Ciudades.ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        private void PopulateDDLStates()
        {
            var states = departamentoLog.ShowDepartamentosDDL();

            var dataList = new List<KeyValuePair<string, string>>();

            if (states.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in states.Tables[0].Rows)
                {
                    dataList.Add(new KeyValuePair<string, string>(row["id"].ToString(), row["departamento"].ToString()));
                }
            }

            ddlStates.DataSource = dataList;
            ddlStates.DataTextField = "Value";
            ddlStates.DataValueField = "Key";
            ddlStates.DataBind();
            ddlStates.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione un departamento --", ""));
        }

        [WebMethod]
        public static object ListData()
        {
            var data = ciudadLog.ShowCiudades(); // Retorna un DataSet

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
                        codigo = row["codigo"],
                        dep_id = row["dep_id"],
                        departamento = row["departamento"],
                        ciudad = row["ciudad"],
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
            checkFileds();

            int idDepartamento = int.Parse(ddlStates.SelectedValue);

            bool executed = ciudadLog.InsertCiudad(TBCode.Text, TBCityName.Text, idDepartamento);
            if (executed)
            {
                LblMsg.Text = "Ciudad almacenada exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.CssClass = "text-danger fw-bold";
                LblMsg.Text = "Error al guardar la ciudad.";
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

            checkFileds();

            int idDepartamento = int.Parse(ddlStates.SelectedValue);

            bool executed = ciudadLog.UpdateCiudad(id, TBCode.Text, TBCityName.Text, idDepartamento);// Llamo al método Almacenado.

            if (executed)// Verifico si la operación fue exitosa 
            {
                LblMsg.Text = "La ciudad se actualizó exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.Text = "Error al actualizar la ciudad.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        private void checkFileds()
        {
            if (string.IsNullOrWhiteSpace(TBCode.Text)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El codigo de la ciudad no puede estar vacío.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            if (string.IsNullOrWhiteSpace(TBCityName.Text)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El nombre de la ciudad no puede estar vacío.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            if (string.IsNullOrWhiteSpace(ddlStates.SelectedValue)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "La ciudad debe estar asociado a un departamento.";
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
                bool executed = ciudadLog.DeleteCiudad(id); // Llama a tu método de eliminación

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Ciudad eliminada correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar la ciudad.";
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
            TBCityName.Text = string.Empty;
            ddlStates.SelectedValue = string.Empty;
            TBCode.Text = string.Empty;
        }
    }
}
