using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace Presentation
{
    public partial class WFInventory : System.Web.UI.Page
    {
        InventoryLog objInv = new InventoryLog();
        ProductLog objProd = new ProductLog();
        EmployeeLog objEmp = new EmployeeLog();

        private DateTime _date;
        private int _fkproducto, _CantidadNueva, _fkpersona, _id;
        private string _Observacion;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showEmployeeDDL();
                showProductDDL();
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        [WebMethod]
        public static object ListInventorys()
        {
            InventoryLog objInv = new InventoryLog();
            

            // Se obtiene un DataSet que contiene la lista de clientes desde la base de datos.
            var dataSet = objInv.ShowInventory();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var inventorysList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un cliente).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                inventorysList.Add(new
                {
                    InventoryID = row["Id"],
                    FechaInventario = Convert.ToDateTime(row["FechaInventario"]).ToString("yyyy-MM-dd"),
                    Observacion = row["Observacion"],
                    CantidadActualInventario = row["Cantidad actual inventario"],
                    fkproducto = row["fkproducto"],
                    CodigoProducto = row["CodigoProducto"],
                    Producto = row["Producto"],
                    Descripcion = row["Descripcion"],
                    Medida = row["Medida"],
                    fkunidadmedida = row["fkunidadmedida"],
                    UnidadMedida = row["UnidadMedida"],
                    CantidadNueva = row["Cantidad Nueva"],
                    fkpersona = row["fkpersona"],
                    NombreEmpleado = row["NombreEmpleado"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de inventario
            return new { data = inventorysList };
        }

        [WebMethod]
        public static bool DeleteInventory(int id)
        {
            // Crear una instancia de la clase de lógica de inventario
            InventoryLog objInv = new InventoryLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objInv.DeleteInventory(id);
        }

        private void showProductDDL()
        {
            DDLProduct.DataSource = objProd.showProductsDDL();
            DDLProduct.DataValueField = "Id";//Nombre de la llave primaria
            DDLProduct.DataTextField = "Producto";
            DDLProduct.DataBind();
            DDLProduct.Items.Insert(0, "---- Seleccione un producto ----");
        }

        private void showEmployeeDDL()
        {
            DDLEmployee.DataSource = objEmp.ShowEmployeesDDL();
            DDLEmployee.DataValueField = "Id";//Nombre de la llave primaria
            DDLEmployee.DataTextField = "NombreCompleto";
            DDLEmployee.DataBind();
            DDLEmployee.Items.Insert(0, "---- Seleccione un empleado ----");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(TBDate.Text, out DateTime parsedDate))
            {
                LblMsg.Text = "Formato de fecha inválido";
                return;
            }

            _date = DateTime.Parse(TBDate.Text);
            _CantidadNueva = Convert.ToInt32(TBQuantityInv.Text);
            _Observacion = TBObservation.Text;

            _fkpersona= Convert.ToInt32(DDLEmployee.SelectedValue);
            _fkproducto = Convert.ToInt32(DDLProduct.SelectedValue);

            executed = objInv.AddInventory(_CantidadNueva, _date, _Observacion, _fkproducto, _fkpersona);


            if (executed)
            {
                MessageBox.Show("Inventario se guardo exitosamente!");
                LblMsg.Text = "Inventario se guardó exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        private void clear()
        {
            HFInventoryId.Value = "";
            TBDate.Text = "";
            TBObservation.Text = "";
            TBQuantityInv.Text = "";
            DDLEmployee.SelectedIndex = 0;
            DDLProduct.SelectedIndex = 0;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un usuario para actualizar
            if (string.IsNullOrEmpty(HFInventoryId.Value))
            {
                LblMsg.Text = "No se ha seleccionado el inventario para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFInventoryId.Value);
            _date = DateTime.Parse(TBDate.Text);
            _CantidadNueva = Convert.ToInt32(TBQuantityInv.Text);
            _Observacion = TBObservation.Text;

            _fkpersona = Convert.ToInt32(DDLEmployee.SelectedValue);
            _fkproducto = Convert.ToInt32(DDLProduct.SelectedValue);

            executed = objInv.UpdateInventory(_id, _CantidadNueva, _date, _Observacion, _fkproducto, _fkpersona);

            if (executed)
            {
                LblMsg.Text = "El inventario se actualizo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void BtbClear_Click(object sender, EventArgs e)
        {
            clear();
            LblMsg.Text = "";
        }
    }
}