using Logic;
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
        RoleLog objRole = new RoleLog();

        private int _role_id;
        private string _role_name, _role_description;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        [WebMethod]
        public static object RolesList()
        {
            RoleLog objRole = new RoleLog();

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
    public static bool DeleteRole(int id)
    {
        // Crear una instancia de la clase de lógica de productos
        RoleLog objRole = new RoleLog();

        // Invocar al método para eliminar el producto y devolver el resultado
        return objRole.deleteRole(id);
    }
  
    //Metodo para limpiar los TextBox y los DDL
    private void clear()
    {
        HFRoleID.Value = "";
        TBRoleName.Text = "";
        TBRoleDescription.Text = "";
    }
    protected void BtnSave_Click(object sender, EventArgs e)
        {
            //_role_id = Convert.ToInt32(HFRoleID.Value);
            _role_name = TBRoleName.Text;
            _role_description = TBRoleDescription.Text;



            executed = objRole.saveRole( _role_name, _role_description);

            if (executed)
            {
                LblMsg.Text = "El rol se guardo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFRoleID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un role para actualizar.";
                return;
            }
            _role_id = Convert.ToInt32(HFRoleID.Value);
            _role_name = TBRoleName.Text;
            _role_description = TBRoleDescription.Text;

            executed = objRole.updateRole(_role_id, _role_name, _role_description);

            if (executed)
            {
                LblMsg.Text = "El rol se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}