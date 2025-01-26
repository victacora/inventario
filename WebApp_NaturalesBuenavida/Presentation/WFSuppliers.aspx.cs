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
    public partial class WFSuppliers : System.Web.UI.Page
    {
        private static SupplierLog objSupplier = new SupplierLog();
        private static PersonLog objPerson = new PersonLog();
        private static TypeDocumentLog objTypeDocument = new TypeDocumentLog();
        private static PaisLog objCountry = new PaisLog();
        private static DepartamentoLog objDepartamento = new DepartamentoLog();
        private static CiudadLog objCiudad = new CiudadLog();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTipoDocumentos();
                LoadPaises();
            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Proveedores).ToString()))
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
        public static object ListSuppliers()
        {
            SupplierLog objSupplier = new SupplierLog();

            // Obtener el DataSet con los datos de los empleados desde la base de datos
            var dataSet = objSupplier.ShowSupplier();

            // Crear una lista de objetos para almacenar los empleados que se devolverán
            var clientsList = new List<object>();

            // Iterar sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                clientsList.Add(new
                {
                    ProveedorID = row["ProveedorID"],
                    PersonaID = row["PersonaID"],
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
                    Email = row["Correo"]
                });
            }

            // Devolver los datos en formato JSON
            return new { data = clientsList };
        }

        // WebMethod para eliminar un cliente
        [WebMethod]
        public static AjaxResponse DeleteSupplier(int idProveedor, int idPersona)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = objSupplier.DeleteSupplier(idProveedor, idPersona);

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Proveedor eliminado correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar el proveedor.";
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
            HFSupplierID.Value = "";
            HFPersonID.Value = "";
            TBSupplierId.Text = "";
            TBSupplierName.Text = "";
            TBSupplierLastName.Text = "";
            TBSupplierPhone.Text = "";
            TBSupplierEmail.Text = "";
            TBDireccion.Text = "";
            ddlTipoDocumento.SelectedValue = "";
            ddlPais.SelectedValue = "";
            ddlDepartamento.SelectedValue = "";
            ddlCiudad.SelectedValue = "";
        }

        // Método para guardar un nuevo cliente
        protected void BtnSave_Click(object sender, EventArgs e)
        {

            bool isSaved = objPerson.InsertPersona(
                TBSupplierId.Text,
                TBSupplierName.Text,
                TBSupplierLastName.Text,
                TBSupplierPhone.Text,
                TBDireccion.Text,
                TBSupplierEmail.Text,
                !ddlTipoDocumento.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlTipoDocumento.SelectedValue) : 0,
                !ddlCiudad.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlCiudad.SelectedValue) : 0,
                "",
                "",
                "",
                0,
               (int)TipoUsuario.Proveedor
            );

            if (isSaved)
            {
                LblMsg.Text = "Proveedor guardado exitosamente.";
                LblMsg.CssClass = "text-success fw-bold";
                clear(); // Limpiar los campos
            }
            else
            {
                LblMsg.Text = "Ocurrio un error almacenando el cliente. Verifique que el numero de documento o correo no se encuentren duplicados.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        // Método para actualizar un cliente
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            bool isUpdated = objPerson.UpdatePersona(TBSupplierId.Text,
                TBSupplierName.Text,
                TBSupplierLastName.Text,
                TBSupplierPhone.Text,
                TBDireccion.Text,
                TBSupplierEmail.Text,
                !ddlTipoDocumento.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlTipoDocumento.SelectedValue) : 0,
                !ddlCiudad.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(ddlCiudad.SelectedValue) : 0,
               "",
               "",
                "",
                0,
               (int)TipoUsuario.Proveedor,
                0,
               Convert.ToInt32(HFPersonID.Value)
               );

            if (isUpdated)
            {
                LblMsg.Text = "Proveedor actualizado exitosamente!";
                LblMsg.CssClass = "text-success fw-bold";
                clear(); // Limpiar los campos
            }
            else
            {
                LblMsg.CssClass = "text-danger fw-bold";
                LblMsg.Text = "Error al actualizar el cliente. Verifique que el numero de documento o correo no se encuentren duplicados.";
            }
        }

        // Método para limpiar los campos
        protected void BtbClear_Click(object sender, EventArgs e)
        {
            clear();
        }

    }
}