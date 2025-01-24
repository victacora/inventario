using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

namespace Presentation
{
    public partial class WFInventory : System.Web.UI.Page
    {
        InventoryLog objInv = new InventoryLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        [WebMethod]
        public static object ListInventorys()
        {
            InventoryLog objInv = new InventoryLog();
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
        public static bool DeleteInventory(int id)
        {
            InventoryLog objInv = new InventoryLog();
            return objInv.DeleteInventory(id);
        }
    }
}
