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
    public partial class WFBuy : System.Web.UI.Page
    {
        BuyLog objBuy = new BuyLog();
        ProductLog objProduct = new ProductLog();

        private int _id, _quantity, _fkProduct;
        private double _unitprice;
        private string _invoicenumber;
        private DateTime _date;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //showBuy();
                showProductDDL();

                // Al cargar la página por primera vez, establecer la fecha actual en el calendario
                //Calendar1.SelectedDate = DateTime.Now;
                // También puedes mostrar la fecha actual en el TextBox
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListBuys()
        {
            BuyLog objBuy = new BuyLog();

            // Se obtiene un DataSet que contiene la lista de clientes desde la base de datos.
            var dataSet = objBuy.showBuy();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var buysList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un cliente).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                buysList.Add(new
                {
                    CompraID = row["CompraID"],
                    FechaCompra = Convert.ToDateTime(row["FechaCompra"]).ToString("yyyy-MM-dd"),
                    TotalCompra = row["TotalCompra"],
                    NumeroFactura = row["NumeroFactura"],
                    CantidadComprada = row["CantidadComprada"],
                    PrecioUnitario = row["PrecioUnitario"],
                    fkproduct = row["fkproduct"],
                    CodigoProducto = row["CodigoProducto"],
                    NombreProducto = row["NombreProducto"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = buysList };
        }

        [WebMethod]
        public static bool DeleteBuy(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            BuyLog objBuy = new BuyLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objBuy.deleteBuy(id);
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //TBDate.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(TBDate.Text, out DateTime parsedDate))
            {
                LblMsg.Text = "Formato de fecha inválido";
                return;
            }
            _date = DateTime.Parse(TBDate.Text);
            _quantity = Convert.ToInt32(TBQuantity.Text);
            _unitprice = Convert.ToDouble(TBUnitPrice.Text);
            _fkProduct = Convert.ToInt32(DDLProduct.SelectedValue);
            _invoicenumber = TBInvoiceNumber.Text;

            bool executed = objBuy.saveBuy(_date, _fkProduct, _quantity, _unitprice, _invoicenumber);

            if (executed)
            {
                LblMsg.Text = "Compra guardada exitosamente";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un usuario para actualizar
            if (string.IsNullOrEmpty(HFBuyID.Value))
            {
                LblMsg.Text = "No se ha seleccionado la compra para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFBuyID.Value);
            _date = DateTime.Parse(TBDate.Text);
            _quantity = Convert.ToInt32(TBQuantity.Text);
            _unitprice = Convert.ToDouble(TBUnitPrice.Text);
            _fkProduct = Convert.ToInt32(DDLProduct.SelectedValue);
            _invoicenumber = TBInvoiceNumber.Text;

            executed = objBuy.updateBuy(_id, _date, _fkProduct, _quantity, _unitprice, _invoicenumber);

            if (executed)
            {
                LblMsg.Text = "La compra se actualizo exitosamente!";
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
        }

        //private void showBuy()
        //{
        //    DataSet objData = new DataSet();
        //    objData = objBuy.showBuy();
        //    GVCompras.DataSource = objData;
        //    GVCompras.DataBind();
        //}

        private void showProductDDL()
        {
            DDLProduct.DataSource = objProduct.showProductsDDL();
            DDLProduct.DataValueField = "Id";//Nombre de la llave primaria
            DDLProduct.DataTextField = "Producto";
            DDLProduct.DataBind();
            DDLProduct.Items.Insert(0, "---- Seleccione un producto ----");
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFBuyID.Value = "";
            DDLProduct.SelectedIndex = 0;
            TBDate.Text = "";
            TBQuantity.Text = "";
            TBInvoiceNumber.Text = "";
            TBUnitPrice.Text = "";
            //LblMsg.Text = "";

        }

        protected void GVCompras_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GVCompras_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}