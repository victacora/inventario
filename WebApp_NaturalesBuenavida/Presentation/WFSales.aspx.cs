using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Services;

namespace Presentation
{
    public partial class WFSales : System.Web.UI.Page
    {
        SalesLog objSales = new SalesLog();
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Ventas).ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        [WebMethod]
        public static object ListSales()
        {
            SalesLog salesLog = new SalesLog();
            DataSet salesData = salesLog.ShowSales();

            List<object> salesList = new List<object>();
            foreach (DataRow row in salesData.Tables[0].Rows)
            {
                salesList.Add(new
                {
                    VentaID = row["Referencia"],
                    FechaVenta = Convert.ToDateTime(row["fecha"]).ToString("yyyy-MM-dd"),
                    TotalVenta = row["total"],
                    Descripción = row["descripcion"],
                    Empleado = row["nombre_empleado"]+" "+ row["apellido_empleado"],
                    IdentificacionCliente = row["identificacion_cliente"],
                    Cliente = row["nombre_cliente"]+" "+ row["apellido_cliente"],
                    EmpleadoId = row["emp_id"],
                    ClienteId = row["cli_id"],
                });
            }

            return new
            {
                draw = 1, // Esto es útil para las paginaciones
                recordsTotal = salesList.Count, // Número total de registros
                recordsFiltered = salesList.Count, // Número total de registros después de aplicar filtros (si los hay)
                data = salesList // Los datos reales de las filas
            };
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(TBDate.Text, out DateTime parsedDate))
            { 
                LblMsg.Text = "Formato de fecha invalido";
                return;
            }
            DateTime _date = DateTime.Parse(TBDate.Text);
            double total = Convert.ToDouble(TBTotal.Text);
            string description = TBDescription.Text;
            int clientId = Convert.ToInt32(DDLClient.SelectedValue);
            int employeeId = Convert.ToInt32(DDLEmployee.SelectedValue);

           executed = objSales.SaveSale(_date, total, description, clientId, employeeId);
            if(executed)
            {
                LblMsg.Text = "La Venta se guardó exiitosamente";
                LblMsg.CssClass = "text-success fw-bold";
            }
            else
            {
                LblMsg.Text = "venta no guardada.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(HFSaleID.Value, out int saleId) && DateTime.TryParse(TBDate.Text, out DateTime saleDate))
            {
                decimal total = decimal.Parse(TBTotal.Text);
                string description = TBDescription.Text;
                int clientId = int.Parse(DDLClient.SelectedValue);
                int employeeId = int.Parse(DDLEmployee.SelectedValue);

                bool success = objSales.UpdateSale(saleId, saleDate, total, description, clientId, employeeId);

                LblMsg.Text = success ? "Venta actualizada exitosamente" : "Error al actualizar la venta";
                LblMsg.CssClass = success ? "text-success fw-bold" : "text-danger fw-bold";
            }
            else
            {
                LblMsg.Text = "Por favor, seleccione una venta válida.";
                LblMsg.CssClass = "text-danger fw-bold";
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void LoadDropdowns()
        {
            ClientLog clientLog = new ClientLog();
            DataSet clients = clientLog.showClientDDL(); // Cargar clientes

            // Configura el DataSource del DropDownList
            DDLClient.DataSource = clients.Tables[0];
            DDLClient.DataValueField = "Id";  // Se define el nombre del campo
            DDLClient.DataTextField = "NombreCompleto"; // Se define el nombre del campo
            DDLClient.DataBind();
            DDLClient.Items.Insert(0, "---- Seleccione un cliente ----");

            EmployeeLog employeeLog = new EmployeeLog();
            DataSet employees = employeeLog.ShowEmployeesDDL(); // Cargar empleados

            DDLEmployee.DataSource = employees.Tables[0];
            DDLEmployee.DataValueField = "Id";  // Se define el nombre del campo
            DDLEmployee.DataTextField = "NombreCompleto"; // Se define el nombre del campo
            DDLEmployee.DataBind();
            DDLEmployee.Items.Insert(0, "---- Seleccione un empleado ----");
        }
        [WebMethod]
        public static object DeleteSale(int saleId)
        {
            SalesLog salesLog = new SalesLog();
            bool success = salesLog.DeleteSale(saleId);  // Llama al método de la capa de lógica
            return new
            {

                success = success

            };
        }

        private void ClearFields()
        {
            HFSaleID.Value = string.Empty;
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBTotal.Text = string.Empty;
            TBDescription.Text = string.Empty;
            DDLClient.SelectedIndex = 0;
            DDLEmployee.SelectedIndex = 0;
        }
    }
}
