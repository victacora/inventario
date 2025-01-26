using Logic;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFEmployees : System.Web.UI.Page
    {
        private static EmployeeLog objEmployee = new EmployeeLog();
        private static PersonLog objPerson = new PersonLog();
        private static UserLog objUsuario = new UserLog();
        private static TypeDocumentLog objTypeDocument = new TypeDocumentLog();
        private static PaisLog objCountry = new PaisLog();
        private static DepartamentoLog objDepartamento = new DepartamentoLog();
        private static CiudadLog objCiudad = new CiudadLog();
        private static RoleLog objRol = new RoleLog();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTipoDocumentos();
                LoadPaises();
                LoadRoles();
            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Empleados).ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        private void LoadTipoDocumentos()
        {
            DataTable paises = objTypeDocument.showTypesDocumentDDL().Tables[0];
            ddlTipoDocumento.DataSource = paises;
            ddlTipoDocumento.DataTextField = "TipoDocumento";
            ddlTipoDocumento.DataValueField = "Id";
            ddlTipoDocumento.DataBind();
            ddlTipoDocumento.Items.Insert(0, new ListItem("-- Seleccione el tipo de documento --", ""));
        }


        private void LoadRoles()
        {
            DataTable paises = objRol.showRolesDDL().Tables[0];
            ddlRol.DataSource = paises;
            ddlRol.DataTextField = "Rol";
            ddlRol.DataValueField = "Id";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem("-- Seleccione el rol --", ""));
        }

        private void LoadPaises()
        {
            DataTable paises = objCountry.ShowPaisDDL().Tables[0];
            ddlPais.DataSource = paises;
            ddlPais.DataTextField = "pais";
            ddlPais.DataValueField = "id";
            ddlPais.DataBind();
            ddlPais.Items.Insert(0, new ListItem("-- Seleccione el pais --", ""));
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            int paisId = int.Parse(ddlPais.SelectedValue);
            if (paisId > 0)
            {
                LoadDepartamentos(paisId);
            }
            else
            {
                ddlDepartamento.Items.Clear();
                ddlCiudad.Items.Clear();
            }
        }

        private void LoadDepartamentos(int paisId)
        {
            DataTable departments = objDepartamento.ShowDepartamentosDDL(paisId).Tables[0];
            ddlDepartamento.DataSource = departments;
            ddlDepartamento.DataTextField = "departamento";
            ddlDepartamento.DataValueField = "id";
            ddlDepartamento.DataBind();
            ddlDepartamento.Items.Insert(0, new ListItem("-- Seleccione el departamento --", ""));
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int departmentId = int.Parse(ddlDepartamento.SelectedValue);
            if (departmentId > 0)
            {
                LoadCiudades(departmentId);
            }
            else
            {
                ddlCiudad.Items.Clear();
            }
        }

        private void LoadCiudades(int departmentId)
        {
            DataTable cities = objCiudad.ShowCiudadesDDL(departmentId).Tables[0];
            ddlCiudad.DataSource = cities;
            ddlCiudad.DataTextField = "ciudad";
            ddlCiudad.DataValueField = "id";
            ddlCiudad.DataBind();
            ddlCiudad.Items.Insert(0, new ListItem("-- Seleccione una ciudad --", ""));
        }


        // WebMethod para listar los empleados
        [WebMethod]
        public static object ListEmployees()
        {
            EmployeeLog objEmployee = new EmployeeLog();

            // Obtener el DataSet con los datos de los empleados desde la base de datos
            var dataSet = objEmployee.ShowEmployees();

            // Crear una lista de objetos para almacenar los empleados que se devolverán
            var employeesList = new List<object>();

            // Iterar sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                employeesList.Add(new
                {
                    PersonaID = row["PersonaID"],
                    EmployeeID = row["EmpleadoID"],
                    TipoDocumentoID = row["TipoDocumentoID"],
                    TipoDocumento = row["TipoDocumento"],
                    Identification = row["Identificacion"],
                    PaisId = row["PaisId"],
                    Pais = row["Pais"],
                    DepartamentoId = row["DepartamentoId"],
                    Departamento = row["Departamento"],
                    CiudadId = row["CiudadId"],
                    Ciudad = row["Ciudad"],
                    Direccion = row["Direccion"],
                    FirstName = row["Nombre"],
                    LastName = row["Apellido"],
                    Phone = row["Telefono"],
                    Email = row["Correo"],
                    UsuId = row["usu_id"],
                    Usuario = row["usuario"],
                    Contrasena = row["contrasena"],
                    RolId = row["rol_id"],
                    Rol = row["rol"],
                    Estado = row["estado"],
                    Registro = row["registro"].ToString()
                });
            }

            // Devolver los datos en formato JSON
            return new { data = employeesList };
        }

        // WebMethod para eliminar un empleado
        [WebMethod]
        public static AjaxResponse DeleteEmployee(int idEmpleado, int idPersona, int idUsuario)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = objEmployee.DeleteEmployee(idEmpleado, idPersona, idUsuario);

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Empleado eliminado correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar el empleado.";
                }
            }
            catch (Exception ex)// En caso de error, configuro la respuesta con el mensaje de error.
            {
                response.Success = false;
                response.Message = "Ocurrió un error: " + ex.Message;
            }

            return response; 

        }


        // Método para limpiar los campos
        private void clear()
        {
            HFEmployeeID.Value = "";
            HFPersonID.Value = "";
            HFUsuID.Value = "";
            TBEmployeeId.Text = "";
            TBEmployeeName.Text = "";
            TBEmployeeLastName.Text = "";
            TBEmployeePhone.Text = "";
            TBEmployeeEmail.Text = "";
            TBDireccion.Text = "";
            TBUsuario.Text = "";
            TBContrasena.Text = "";
            ddlTipoDocumento.SelectedValue = "";
            ddlRol.SelectedValue = "";
            ddlPais.SelectedValue = "";
            ddlDepartamento.SelectedValue = "";
            ddlCiudad.SelectedValue = "";
        }

        // Método para guardar un nuevo empleado
        protected void BtnSave_Click(object sender, EventArgs e)
        {

            bool isSaved = objPerson.InsertPersona(
                TBEmployeeId.Text,
                TBEmployeeName.Text,
                TBEmployeeLastName.Text,
                TBEmployeePhone.Text,
                TBDireccion.Text,
                TBEmployeeEmail.Text,
                !ddlTipoDocumento.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlTipoDocumento.SelectedValue) : 0,
                !ddlCiudad.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlCiudad.SelectedValue) : 0,
                TBUsuario.Text,
                objUsuario.HashPassword(TBContrasena.Text),
                ddlStatus.SelectedValue,
                !ddlRol.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlRol.SelectedValue) : 0,
               (int)TipoUsuario.Empleado
            );

            if (isSaved)
            {
                LblMsg.Text = "Empleado guardado exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                clear(); // Limpiar los campos
            }
            else
            {
                LblMsg.Text = "Ocurrio un error almacenando el empleado. Verifique que el numero de documento, correo o usuario no se encuentren duplicados.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        // Método para actualizar un empleado
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            bool isUpdated = objPerson.UpdatePersona(TBEmployeeId.Text,
                TBEmployeeName.Text,
                TBEmployeeLastName.Text,
                TBEmployeePhone.Text,
                TBDireccion.Text,
                TBEmployeeEmail.Text,
                !ddlTipoDocumento.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlTipoDocumento.SelectedValue) : 0,
                !ddlCiudad.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlCiudad.SelectedValue) : 0,
                TBUsuario.Text,
                !TBContrasena.Text.Equals(HFPersonID.Value) ? objUsuario.HashPassword(TBContrasena.Text) : TBContrasena.Text,
                ddlStatus.SelectedValue,
                !ddlRol.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlRol.SelectedValue) : 0,
               (int)TipoUsuario.Empleado,
               Convert.ToInt32(HFUsuID.Value),
               Convert.ToInt32(HFPersonID.Value)
               );

            if (isUpdated)
            {
                LblMsg.Text = "Empleado actualizado exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                clear(); // Limpiar los campos
            }
            else
            {
                LblMsg.CssClass = "text-danger fw-bold";
                LblMsg.Text = "Error al actualizar el empleado. Verifique que el numero de documento, correo o usuario no se encuntren duplicados.";
            }
        }

        // Método para limpiar los campos
        protected void BtbClear_Click(object sender, EventArgs e)
        {
            clear();
        }

    }
}