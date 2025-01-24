<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="WFInventory.aspx.cs" Inherits="Presentation.WFInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Lista de Inventarios</h2>

    <table id="inventorysTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Fecha</th>
                <th>Observación</th>
                <th>Empleado</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <%-- Datatables Script --%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //const baseUrl = "https://localhost:44352/WFInventoryDetails.aspx";
            // Inicializar DataTable
            $('#inventorysTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFInventory.aspx/ListInventorys",
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);
                    },
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "InventoryID", "visible": false },
                    { "data": "FechaInventario" },
                    { "data": "Observacion" },
                    { "data": "NombreResponsable" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `
                                    <button type="button" class="view-btn" data-id="${row.InventoryID}">Ver Detalles</button>
                                    <button type="button" class="delete-btn" data-id="${row.InventoryID}">Eliminar</button>`;
                        }
                    }
                ]
            });

            // Ver detalles del inventario
            $('#inventorysTable').on('click', '.view-btn', function () {
                //alert("Evento detectado");
                //console.log("Evento de clic detectado."); // Depuración
                const rowData = $('#inventorysTable').DataTable().row($(this).parents('tr')).data();
                //alert("RowData" + rowData);
                //console.log("Datos de la fila:", rowData); // Depuración
                const inventoryId = rowData.InventoryID;
                //alert("ID: "+ inventoryId);
                if (inventoryId) {
                    alert('Redirigiendo a: ' + inventoryId );
                    
                    window.location.href = `https://localhost:44352/WFInventoryDetails.aspx?inventoryId=${inventoryId}`;
                    //window.location.href = `WFInventoryDetails.aspx?inventoryId=${inventoryId}`;
                    //document.location.assign(`WFInventoryDetails.aspx?inventoryId=${inventoryId}`);

                } else {
                    alert('ID de inventario no encontrado.');
                }
            });

            // Eliminar un inventario
            $('#inventorysTable').on('click', '.delete-btn', function () {
                const inventoryId = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este inventario?")) {
                    deleteInventory(inventoryId);
                }
            });
        });

        // Función para eliminar un inventario
        function deleteInventory(id) {
            $.ajax({
                type: "POST",
                url: "WFInventory.aspx/DeleteInventory",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function () {
                    $('#inventorysTable').DataTable().ajax.reload();
                    alert("Inventario eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el inventario.");
                }
            });
        }
    </script>
</asp:Content>
