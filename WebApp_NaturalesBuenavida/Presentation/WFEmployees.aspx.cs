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


        private int _id;
        private bool executed = false;
        private int _fkPerson;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTipoDocumentos();
                LoadPaises();
                LoadRoles();
            }
        }

        private void LoadTipoDocumentos()
        {
            DataTable paises = objTypeDocument.showTypesDocumentDDL().Tables[0];
            ddlTipoDocumento.DataSource = paises;
            ddlTipoDocumento.DataTextField = "TipoDocumento";
            ddlTipoDocumento.DataValueField = "Id";
            ddlTipoDocumento.DataBind();
            ddlTipoDocumento.Items.Insert(0, new ListItem("-- Seleccione el tipo de documento --", "0"));
        }


        private void LoadRoles()
        {
            DataTable paises = objRol.showRolesDDL().Tables[0];
            ddlRol.DataSource = paises;
            ddlRol.DataTextField = "Rol";
            ddlRol.DataValueField = "Id";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem("-- Seleccione el rol --", "0"));
        }

        private void LoadPaises()
        {
            DataTable paises = objCountry.ShowPaisDDL().Tables[0];
            ddlPais.DataSource = paises;
            ddlPais.DataTextField = "pais";
            ddlPais.DataValueField = "id";
            ddlPais.DataBind();
            ddlPais.Items.Insert(0, new ListItem("-- Seleccione el pais --", "0"));
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
            DataTable departments =objDepartamento.ShowDepartamentosDDL(paisId).Tables[0];
            ddlDepartamento.DataSource = departments;
            ddlDepartamento.DataTextField = "departamento";
            ddlDepartamento.DataValueField = "id";
            ddlDepartamento.DataBind();
            ddlDepartamento.Items.Insert(0, new ListItem("-- Seleccione el departamento --", "0"));
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
            ddlCiudad.Items.Insert(0, new ListItem("-- Seleccione una ciudad --", "0"));
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
                    TipoDocumentoID= row["TipoDocumentoID"],
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
        public static bool DeleteEmployee(int id)
        {
            EmployeeLog objEmployee = new EmployeeLog();

            // Llamar al método para eliminar el empleado
            return objEmployee.DeleteEmployee(id);
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
        }

        // Método para guardar un nuevo empleado
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkPerson = Convert.ToInt32(HFEmployeeID.Value); // Obtener el id de la persona seleccionada

            bool isSaved = objEmployee.AddEmployee(_fkPerson);

            if (isSaved)
            {
                LblMsg.Text = "Empleado guardado exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                clear(); // Limpiar los campos
            }
            else
            {
                LblMsg.Text = "Este empleado ya está registrado.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        // Método para actualizar un empleado
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un empleado para actualizar
            if (string.IsNullOrEmpty(HFEmployeeID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un empleado para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFEmployeeID.Value);

            bool isUpdated = objEmployee.EditEmployee(_id, -1);

            if (isUpdated)
            {
                LblMsg.Text = "Empleado actualizado exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                clear(); // Limpiar los campos
            }
            else
            {
                LblMsg.CssClass = "text-danger fw-bold";
                LblMsg.Text = "Error al actualizar el empleado.";
            }
        }

        // Método para limpiar los campos
        protected void BtbClear_Click(object sender, EventArgs e)
        {
            clear();
        }

    }
}