using Logic;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFStates : System.Web.UI.Page
    {
        private static DepartamentoLog departamentoLog = new DepartamentoLog();
        private static PaisLog paisLog = new PaisLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDDLCountries();
            }

            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || !usuario.Rol.Equals("Administrador"))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        private void PopulateDDLCountries()
        {
            var countries = paisLog.ShowPaisDDL();

            var dataList = new List<KeyValuePair<string, string>>();

            if (countries.Tables.Count > 0) 
            {
                foreach (System.Data.DataRow row in countries.Tables[0].Rows)
                {
                    dataList.Add(new KeyValuePair<string, string>(row["id"].ToString(), row["pais"].ToString()));
                }
            }

            ddlCountries.DataSource = dataList;
            ddlCountries.DataTextField = "Value"; 
            ddlCountries.DataValueField = "Key";
            ddlCountries.DataBind();
            ddlCountries.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione un pais --", ""));
        }

        [WebMethod]
        public static object ListData()
        {
            var data = departamentoLog.ShowDepartamentos(); // Retorna un DataSet

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
                        pais_id = row["pais_id"],
                        pais = row["pais"],
                        departamento = row["departamento"],
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

            int idPais = int.Parse(ddlCountries.SelectedValue);

            bool executed = departamentoLog.InsertDepartamento(TBCode.Text,TBStateName.Text, idPais);
            if (executed)
            {
                LblMsg.Text = "Departamento almacenado exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.CssClass = "text-danger fw-bold";
                LblMsg.Text = "Error al guardar el departamento.";
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

            int idPais = int.Parse(ddlCountries.SelectedValue);

            bool executed = departamentoLog.UpdateDepartamento(id, TBCode.Text, TBStateName.Text, idPais);// Llamo al método Almacenado.

            if (executed)// Verifico si la operación fue exitosa 
            {
                LblMsg.Text = "El departamento se actualizó exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.Text = "Error al actualizar el departamento.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        private void checkFileds()
        {
            if (string.IsNullOrWhiteSpace(TBCode.Text)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El codigo del departamento no puede estar vacío.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            if (string.IsNullOrWhiteSpace(TBStateName.Text)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El nombre del departamento no puede estar vacío.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            if (string.IsNullOrWhiteSpace(ddlCountries.SelectedValue)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El departamento debe estar asociado a un pais.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();// Limpio los campos.
        }

        [WebMethod]
        public static AjaxResponse Delete(int id)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = departamentoLog.DeleteDepartamento(id); // Llama a tu método de eliminación

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Departamento eliminado correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar el departamento.";
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
            TBStateName.Text = string.Empty;
            TBCode.Text = string.Empty;
            LblMsg.Text = string.Empty;  
        }
    }
}
