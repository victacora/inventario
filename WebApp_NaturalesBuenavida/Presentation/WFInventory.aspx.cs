using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

namespace Presentation
{
    public partial class WFInventory : System.Web.UI.Page
    {
        private static InventoryLog objInv = new InventoryLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Inventario).ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        [WebMethod]
        public static object ListInventorys()
        {
            var dataSet = objInv.ShowInventorySummary();

            // Crear una lista para almacenar los inventarios
            var inventoryList = new List<object>();

            // Iterar sobre cada fila del DataSet y agregar los datos a la lista
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                inventoryList.Add(new
                {
                    InventoryID = row["Id"],
                    FechaInventario = Convert.ToDateTime(row["FechaInventario"]).ToString("yyyy-MM-dd"),
                    Observacion = row["Observacion"],
                    NombreResponsable = row["NombreResponsable"]
                });
            }

            // Retornar la lista en formato JSON
            return new { data = inventoryList };
        }

        [WebMethod]
        public static AjaxResponse DeleteInventory(int id)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = objInv.DeleteInventory(id);

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Inventario eliminado correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar el inventario.";
                }
            }
            catch (Exception ex)// En caso de error, configuro la respuesta con el mensaje de error.
            {
                response.Success = false;
                response.Message = "Ocurrió un error: " + ex.Message;
            }

            return response;
        }
    }
}
