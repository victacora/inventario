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
    public partial class WFRole : System.Web.UI.Page
    {
        //Crear los objetos
       private static RoleLog objRole = new RoleLog();

        private int _role_id;
        private string _role_name, _role_description;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || !usuario.Rol.Equals("Administrador"))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }
        [WebMethod]
        public static object RolesList()
        {
            // Se obtiene un DataSet que contiene la lista de roles desde la base de datos.
            var dataSet = objRole.showRoles();

            // Se crea una lista para almacenar los roles que se van a devolver.
            var rolesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rolesList.Add(new
                {
                    roleID = row["rol_id"],
                    roleName = row["rol_nombre"],
                    roleDescription = row["rol_descripcion"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = rolesList };
        }

        [WebMethod]
        public static AjaxResponse DeleteRole(int id)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = objRole.deleteRole(id); // Llama a tu método de eliminación

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
            HFRoleID.Value = "";
            TBRoleName.Text = "";
            TBRoleDescription.Text = "";
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            clear();// Limpio los campos.
            LblMsg.Text = string.Empty;// Limpio el mensaje.
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _role_name = TBRoleName.Text;
            _role_description = TBRoleDescription.Text;

            executed = objRole.saveRole(_role_name, _role_description);

            if (executed)
            {
                LblMsg.Text = "El rol se guardo exitosamente!";
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
            if (string.IsNullOrEmpty(HFRoleID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un role para actualizar.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }
            _role_id = Convert.ToInt32(HFRoleID.Value);
            _role_name = TBRoleName.Text;
            _role_description = TBRoleDescription.Text;

            executed = objRole.updateRole(_role_id, _role_name, _role_description);

            if (executed)
            {
                LblMsg.Text = "El rol se actualizo exitosamente!";
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