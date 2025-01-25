using Logic;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFCategories : System.Web.UI.Page
    {
        private static CategoryLog objCat = new CategoryLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }

            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Categorias).ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }


        [WebMethod]
        public static object ListData()
        {
            var data = objCat.ShowCategories(); // Retorna un DataSet

            List<object> dataList = new List<object>(); // Creo una lista para almacenar los registros.


            if (data.Tables.Count > 0) // Verifico si el DataSet tiene alguna tabla.
            {
                // Itero sobre las filas de la primera tabla.
                foreach (System.Data.DataRow row in data.Tables[0].Rows)
                {
                    // Agrego cada registro a la lista de objetos.
                    dataList.Add(new
                    {
                        id = row["Id"],
                        categoria = row["Descripcion"],
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

            bool executed =objCat.AddCategory(TBCategory.Text);
            if (executed)
            {
                LblMsg.Text = "Categoria almacenada exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.CssClass = "text-danger fw-bold";
                LblMsg.Text = "Error al guardar l categoria.";
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

            bool executed = objCat.EditCategory(id, TBCategory.Text);// Llamo al método Almacenado.

            if (executed)// Verifico si la operación fue exitosa 
            {
                LblMsg.Text = "La categoria se actualizó exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                ClearFields();
            }
            else
            {
                LblMsg.Text = "Error al actualizar la categoria.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        private void checkFileds()
        {
            if (string.IsNullOrWhiteSpace(TBCategory.Text)) // Verifico que el nombre no esté vacío.
            {
                LblMsg.Text = "El nombre de la categoria no puede estar vacío.";
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
                bool executed = objCat.DeleteCategory(id); // Llama a tu método de eliminación

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
            TBCategory.Text = string.Empty;
        }
    }
}
