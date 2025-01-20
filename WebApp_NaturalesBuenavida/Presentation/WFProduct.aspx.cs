using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace Presentation
{
    public partial class WFProduct : System.Web.UI.Page
    {

        //private static List<TemporaryProduct> TemporaryProductList = new List<TemporaryProduct>();

        ProductLog objPro = new ProductLog();
        UnitMeasureLog objUnit = new UnitMeasureLog();
        PresentationLog objPres = new PresentationLog();
        CategoryLog objCategory = new CategoryLog();
        SupplierLog objSupplier = new SupplierLog();

        private int _id, _cantidadInventario, _medida, _fkcategoria, _fkproveedor, _fkunidadmedida, _fkpresentacion;
        private string _codigoProducto, _nombreProducto, _descripcionProducto, _numeroLote;
        private double _precioVenta, _precioCompra;
        private DateTime _date;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                //showProduct();
                showCategoryDDL();
                showSupplierDDL();
                showUnitMeasureDDL();
                showPresentationDDL();
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        public List<TemporaryProduct> TempProductList
        {
            get
            {
                if (ViewState["TempProductList"] == null)
                {
                    ViewState["TempProductList"] = new List<TemporaryProduct>();
                }
                return (List<TemporaryProduct>)ViewState["TempProductList"];
            }
            set
            {
                ViewState["TempProductList"] = value;
            }
        }


        private void AddToTemporaryList()
        {
            try
            {
                // Crear un nuevo producto temporal con los datos del formulario
                TemporaryProduct newProduct = new TemporaryProduct
                {
                    Codigo = TBCode.Text,
                    Nombre = TBNameProduct.Text,
                    Descripcion = TBDescription.Text,
                    Cantidad = Convert.ToInt32(TBQuantityP.Text),
                    PrecioVenta = Convert.ToDouble(TBSalePrice.Text),
                    PrecioCompra = Convert.ToDouble(TBPurchasePrice.Text),
                    Medida = Convert.ToInt32(TBMedida.Text), // Medida del producto
                    FkCategoria = Convert.ToInt32(DDLCategory.SelectedValue), // Categoría
                    FkProveedor = Convert.ToInt32(DDLSupplier.SelectedValue), // Proveedor
                    FkUnidadMedida = Convert.ToInt32(DDLUnitMeasure.SelectedValue), // Unidad de Medida
                    FkPresentacion = Convert.ToInt32(DDLPresentation.SelectedValue), // Presentación
                    NumeroLote = TBNumberLote.Text, // Número de lote
                                                    // Fecha de vencimiento, si existe en el formulario (puedes agregar un campo en el formulario para esto)
                    FechaVencimiento = DateTime.Parse(TBDate.Text), // Suponiendo que tienes un campo para la fecha de vencimiento
                                                                              // Asignar información del proveedor, si la tienes en el formulario
                    NombreProveedor = DDLSupplier.SelectedItem.Text, // Asumiendo que tienes el nombre del proveedor en el DDL
                    UnidadMedida = DDLUnitMeasure.SelectedItem.Text,
                    Presentacion = DDLPresentation.SelectedItem.Text,
                    Categoria = DDLCategory.SelectedItem.Text,
                };

                // Agregar el producto a la lista gestionada por ViewState
                var tempList = TempProductList;
                tempList.Add(newProduct);
                TempProductList = tempList;

                // Mostrar la lista en el GridView
                ShowTemporaryList();

                // Limpiar los campos del formulario
                clear();
            }
            catch (Exception ex)
            {
                LblMsg.Text = "Error al agregar el producto: " + ex.Message;
            }
        }


        private void ShowTemporaryList()
        {
            GVProduct.DataSource = TempProductList;
            GVProduct.DataBind();
        }

        private void clearGV()
        {
            
            GVProduct.DataSource = null;
            GVProduct.DataBind();

        }

        [WebMethod]
        public static object ListProducts()
        {
            ProductLog objPro = new ProductLog();

            // Se obtiene un DataSet que contiene la lista de clientes desde la base de datos.
            var dataSet = objPro.showProducts();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var productsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un cliente).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                productsList.Add(new
                {
                    ProductID = row["prod_id"],
                    Codigo = row["Codigo"],
                    Nombre = row["Nombre"],
                    Descripcion = row["Descripcion"],
                    Medida = row["Medida"],
                    fkunidadmedida = row["fkunidadmedida"],
                    UnidadMedida = row["UnidadMedida"],
                    fkpresentacion = row["fkpresentacion"],                  
                    Presentacion = row["Presentacion"],
                    fkcategory = row["fkcategory"],
                    Categoria = row["Categoria"],
                    Cantidad = row["Cantidad"],
                    NumeroLote = row["NumeroLote"],
                    FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"]).ToString("yyyy-MM-dd"),
                    PrecioVenta = row["PrecioVenta"],
                    PrecioCompra = row["PrecioCompra"],
                    fkproveedor = row["fkproveedor"],
                    NombreProveedor = row["NombreProveedor"],
                    ApellidoProveedor = row["ApellidoProveedor"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = productsList };
        }

        [WebMethod]
        public static bool DeleteProduct(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            ProductLog objProd = new ProductLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objProd.deleteProduct(id);
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddToTemporaryList();
        }

        //private void showProduct()
        //{
        //    DataSet objData = new DataSet();
        //    objData = objPro.showProducts();
        //    GVProduct.DataSource = objData;
        //    GVProduct.DataBind();
        //}


        private void showPresentationDDL()
        {
            DDLPresentation.DataSource = objPres.ShowPresentationsDDL();
            DDLPresentation.DataValueField = "pres_id";//Nombre de la llave primaria
            DDLPresentation.DataTextField = "pres_descripcion";
            DDLPresentation.DataBind();
            DDLPresentation.Items.Insert(0, "---- Seleccione una persona ----");
        }

        private void showUnitMeasureDDL()
        {
            DDLUnitMeasure.DataSource = objUnit.ShowDDLUnitMeasure();
            DDLUnitMeasure.DataValueField = "und_id";//Nombre de la llave primaria
            DDLUnitMeasure.DataTextField = "und_descripcion";
            DDLUnitMeasure.DataBind();
            DDLUnitMeasure.Items.Insert(0, "---- Seleccione una persona ----");
        }

        private void showCategoryDDL()
        {
            DDLCategory.DataSource = objCategory.ShowCategoriesDDL();
            DDLCategory.DataValueField = "Id";//Nombre de la llave primaria
            DDLCategory.DataTextField = "Descripcion";
            DDLCategory.DataBind();
            DDLCategory.Items.Insert(0, "---- Seleccione una persona ----");
        }
        private void showSupplierDDL()
        {
            DDLSupplier.DataSource = objSupplier.ShowSupplierDDL();
            DDLSupplier.DataValueField = "Id";//Nombre de la llave primaria
            DDLSupplier.DataTextField = "Razon social";
            DDLSupplier.DataBind();
            DDLSupplier.Items.Insert(0, "---- Seleccione una persona ----");
        }

        protected void GVProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GVProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                TempProductList.RemoveAt(index);
                ShowTemporaryList();
            }
            catch (Exception ex)
            {
                LblMsg.Text = "Error al eliminar el producto: " + ex.Message;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya productos en la lista temporal
                if (TempProductList.Count == 0)
                {
                    LblMsg.Text = "No hay productos en la lista temporal para guardar.";
                    return; // Detener si la lista está vacía
                }

                // Iterar sobre la lista de productos
                foreach (var product in TempProductList)
                {
                    // Validar la fecha para cada producto
                    if (product.FechaVencimiento == default(DateTime))
                    {
                        LblMsg.Text = "Formato de fecha inválido para el producto " + product.Codigo;
                        return; // Detener la ejecución si alguna fecha no es válida
                    }

                    // Asignar los valores de los productos de la lista temporal
                    DateTime _date = product.FechaVencimiento;
                    string _codigoProducto = product.Codigo;
                    string _nombreProducto = product.Nombre;
                    string _descripcionProducto = product.Descripcion;
                    int _cantidadInventario = product.Cantidad;
                    string _numeroLote = product.NumeroLote;
                    double _precioVenta = product.PrecioVenta;
                    double _precioCompra = product.PrecioCompra;
                    int _medida = product.Medida;

                    // Obtener los valores seleccionados de los DropDownList
                    int _fkcategoria = product.FkCategoria;
                    int _fkproveedor = product.FkProveedor;
                    int _fkunidadmedida = product.FkUnidadMedida;
                    int _fkpresentacion = product.FkPresentacion;

                    //int _fkpresentacion = Convert.ToInt32(DDLPresentation.SelectedValue);

                    // Llamar al método para guardar el producto
                    bool executed = objPro.saveProducts(_codigoProducto, _nombreProducto, _descripcionProducto,
                        _cantidadInventario, _numeroLote, _date, _precioVenta, _precioCompra, _medida,
                        _fkcategoria, _fkproveedor, _fkunidadmedida, _fkpresentacion);

                    // Verificar si el producto se guardó correctamente
                    if (!executed)
                    {
                        LblMsg.Text = "Error al guardar el producto ";
                        return; // Detener si algún producto no se guardó
                    }
                }

                // Si todos los productos fueron guardados con éxito
                LblMsg.Text = "Todos los productos fueron guardados exitosamente!";
                clear(); // Limpiar los campos
                clearGV();
                

            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                LblMsg.Text = "Error: " + ex.Message;
            }
        }
        private void clear()
        {
            HFProductID.Value = "";
            TBCode.Text = "";
            TBNameProduct.Text = "";
            TBDescription.Text = "";
            TBQuantityP.Text = "";
            TBNumberLote.Text = "";
            TBDate.Text = "";
            TBSalePrice.Text = "";
            TBPurchasePrice.Text = "";
            TBMedida.Text = "";
            DDLCategory.SelectedIndex = 0;
            DDLSupplier.SelectedIndex = 0;
            DDLUnitMeasure.SelectedIndex = 0;
            DDLPresentation.SelectedIndex = 0;
            //LblMsg.Text = "";
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un usuario para actualizar
            if (string.IsNullOrEmpty(HFProductID.Value))
            {
                LblMsg.Text = "No se ha seleccionado producto para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFProductID.Value);
            _date = DateTime.Parse(TBDate.Text);
            string formattedDate = _date.ToString("yyyy-MM-dd");
            _codigoProducto = TBCode.Text;
            _nombreProducto = TBNameProduct.Text;
            _descripcionProducto = TBDescription.Text;
            _cantidadInventario = Convert.ToInt32(TBQuantityP.Text);
            _numeroLote = TBNumberLote.Text;
            _precioVenta = Convert.ToDouble(TBSalePrice.Text);
            _precioCompra = Convert.ToDouble(TBPurchasePrice.Text);
            _medida = Convert.ToInt32(TBMedida.Text);

            _fkcategoria = Convert.ToInt32(DDLCategory.SelectedValue);
            _fkproveedor = Convert.ToInt32(DDLSupplier.SelectedValue);
            _fkunidadmedida = Convert.ToInt32(DDLUnitMeasure.SelectedValue);
            _fkpresentacion = Convert.ToInt32(DDLPresentation.SelectedValue);

            executed = objPro.updateProducts(_id,_codigoProducto, _nombreProducto, _descripcionProducto,
                _cantidadInventario, _numeroLote, _date, _precioVenta, _precioCompra, _medida,
                _fkcategoria, _fkproveedor, _fkunidadmedida, _fkpresentacion);

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
            LblMsg.Text = "";
        }
    }
}