using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPrivileges : System.Web.UI.Page
    {
        //Crear los objetos
       private static PermitLog objPermiso = new PermitLog();

        private int _permiso_id;
        private string _permiso_name, _permiso_description;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Privilegios).ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        [WebMethod]
        public static object permisosList()
        {
            // Se obtiene un DataSet que contiene la lista de permisos desde la base de datos.
            var dataSet = objPermiso.showPermits();

            // Se crea una lista para almacenar los permisos que se van a devolver.
            var permisosList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                permisosList.Add(new
                {
                    permisoID = row["perm_id"],
                    permisoName = row["perm_nombre"],
                    permisoDescription = row["perm_descripcion"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = permisosList };
        }

        [WebMethod]
        public static AjaxResponse DeletePermiso(int id)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = objPermiso.deletePermit(id); // Llama a tu método de eliminación

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

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFPermisoID.Value = "";
            TBPermisoName.Text = "";
            TBPermisoDescription.Text = "";
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            clear();// Limpio los campos.
            LblMsg.Text = string.Empty;// Limpio el mensaje.
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _permiso_name = TBPermisoName.Text;
            _permiso_description = TBPermisoDescription.Text;

            executed = objPermiso.savePermit(_permiso_name, _permiso_description);

            if (executed)
            {
                LblMsg.Text = "El permiso se guardo exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al guardar";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFPermisoID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un permiso para actualizar.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }
            _permiso_id = Convert.ToInt32(HFPermisoID.Value);
            _permiso_name = TBPermisoName.Text;
            _permiso_description = TBPermisoDescription.Text;

            executed = objPermiso.updatePermit(_permiso_id, _permiso_name, _permiso_description);

            if (executed)
            {
                LblMsg.Text = "El permiso se actualizo exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }
    }
}