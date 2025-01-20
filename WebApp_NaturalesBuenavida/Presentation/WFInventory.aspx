<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFInventory.aspx.cs" Inherits="Presentation.WFInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- ID Inventario --%>
    <asp:HiddenField ID="HFInventoryId" runat="server" />
    <br />
    <%-- Fecha Inventario --%>
    <asp:Label ID="Label1" runat="server" Text="Fecha de Inventario"></asp:Label><br />
    <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox><br />
    <%-- Producto inventariado --%>
    <asp:Label ID="Label2" runat="server" Text="Producto"></asp:Label><br />
    <asp:DropDownList ID="DDLProduct" runat="server"></asp:DropDownList><br />
    <%-- Cantidad Inventario --%>
    <asp:Label ID="Label3" runat="server" Text="Cantidad Nueva Inventario"></asp:Label><br />
    <asp:TextBox ID="TBQuantityInv" runat="server"></asp:TextBox><br />
    <%-- Observaciones del inventario --%>
    <asp:Label ID="Label4" runat="server" Text="Observaciones"></asp:Label><br />
    <asp:TextBox ID="TBObservation" runat="server"></asp:TextBox><br />
    <%-- Empleado que realiza el inventario --%>
    <asp:Label ID="Label5" runat="server" Text="Empleado que realiza el inventario"></asp:Label><br />
    <asp:DropDownList ID="DDLEmployee" runat="server"></asp:DropDownList><br />
    <br />

    <%--Botones Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" /><br />
        <asp:Button ID="BtbClear" runat="server" Text="Limpiar" OnClick="BtbClear_Click" /><br />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de Inventarios--%>
    <h2>Lista de Inventarios</h2>
    <table id="inventorysTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>InventoryID</th>
                <th>FechaInventario</th>
                <th>Observacion</th>
                <th>CantidadActualInventario</th>
                <th>fkproducto</th>
                <th>CodigoProducto</th>
                <th>Producto</th>
                <th>Descripcion</th>
                <th>Medida</th>
                <th>fkunidadmedida</th>
                <th>UnidadMedida</th>
                <th>CantidadNueva</th>
                <th>fkpersona</th>
                <th>NombreEmpleado</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Productos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#inventorysTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFInventory.aspx/ListInventorys",// Se invoca el WebMethod Listar Compras
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "InventoryID", "visible": false },
                    { "data": "FechaInventario" },
                    { "data": "Observacion" },
                    { "data": "CantidadActualInventario" },
                    { "data": "fkproducto", "visible": false },
                    { "data": "CodigoProducto"},
                    { "data": "Producto" },
                    { "data": "Descripcion"},
                    { "data": "Medida" },
                    { "data": "fkunidadmedida", "visible": false },
                    { "data": "UnidadMedida" },
                    { "data": "CantidadNueva" },
                    { "data": "fkpersona", "visible": false },
                    { "data": "NombreEmpleado" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.InventoryID}">Editar</button>
                        <button class="delete-btn" data-id="${row.InventoryID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            // Editar un inventario
            $('#inventorysTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#inventorysTable').DataTable().row($(this).parents('tr')).data();

                //alert(JSON.stringify(rowData, null, 2));
                loadInventorysData(rowData);
            });

            //Eliminar un inventario
            $('#inventorysTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del producto
                if (confirm("¿Estás seguro de que deseas eliminar este producto?")) {
                    deleteInventory(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadInventorysData(rowData) {
            $('#<%= HFInventoryId.ClientID %>').val(rowData.InventoryID);
            $('#<%= TBDate.ClientID %>').val(rowData.FechaInventario);
            $('#<%= DDLProduct.ClientID %>').val(rowData.fkproducto);
            $('#<%= TBQuantityInv.ClientID %>').val(rowData.CantidadNueva);
            $('#<%= TBObservation.ClientID %>').val(rowData.Observacion);
            $('#<%= DDLEmployee.ClientID %>').val(rowData.fkpersona);
        }
        // Función para eliminar un inventario
        function deleteInventory(id) {
            $.ajax({
                type: "POST",
                url: "WFInventory.aspx/DeleteInventory",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#inventorysTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Inventario eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el inventario.");
                }
            });
        }
    </script>
</asp:Content>
