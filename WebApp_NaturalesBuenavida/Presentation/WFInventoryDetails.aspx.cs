using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFInventoryDetails : System.Web.UI.Page
    {

        InventoryLog objInv = new InventoryLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["inventoryId"] != null)
                {
                    int inventoryId;
                    if (int.TryParse(Request.QueryString["inventoryId"], out inventoryId))
                    {
                        LoadInventoryDetails(inventoryId);
                    }
                    else
                    {
                        // Manejo de error
                        Response.Write("El parámetro InventoryId no es válido.");
                    }
                }
                else
                {
                    // Manejo de error
                    Response.Write("No se proporcionó el parámetro InventoryId.");
                }
            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Inventario).ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        private void LoadInventoryDetails(int inventoryId)
        {
            // Llamar al método de la capa lógica para obtener los detalles del inventario
            DataSet inventoryDetails = objInv.ShowInventoryDetails(inventoryId);

            // Verificar si hay datos y asignarlos a los controles de la página
            if (inventoryDetails != null && inventoryDetails.Tables.Count > 0 && inventoryDetails.Tables[0].Rows.Count > 0)
            {
                DataRow row = inventoryDetails.Tables[0].Rows[0];
                LblInventoryId.Value = row["id_inventario"].ToString();
                LblFecha.Text = Convert.ToDateTime(row["fecha"]).ToString("yyyy-MM-dd");
                LblObservacion.Text = row["observacion"].ToString();
                LblEmpleado.Text = row["responsable"].ToString();
            }

            // Cargar productos asociados al inventario
            if (inventoryDetails.Tables.Count > 1 && inventoryDetails.Tables[1].Rows.Count > 0)
            {

                LblProducto.Text = "Hay productos asociados a este inventario.";
                RepeaterProducts.DataSource = inventoryDetails.Tables[1];
                RepeaterProducts.DataBind();
            }
            else
            {
                // Mostrar un mensaje si no hay productos asociados
                RepeaterProducts.DataSource = null;
                RepeaterProducts.DataBind();
                LblProducto.Text = "No hay productos asociados a este inventario.";
            }
        }

        protected void btnRedirigir_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFInventory.aspx");
        }
    }
}