<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFBuy.aspx.cs" Inherits="Presentation.WFBuy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:HiddenField ID="HFBuyID" runat="server" />
        <br />
        <%--Fecha de la compra--%>
        <asp:Label ID="Label6" runat="server" Text="Fecha de la compra"></asp:Label><br />
        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox><br />
        <%--<asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>--%>
        <br />
        <%--DDL para seleccionar un producto a la compra--%>
        <asp:Label ID="Label7" runat="server" Text="Seleccione el producto"></asp:Label><br />
        <asp:DropDownList ID="DDLProduct" runat="server"></asp:DropDownList><br />
        <%--Cantidad de productos comprados--%>
        <asp:Label ID="Label8" runat="server" Text="Cantidad comprada"></asp:Label><br />
        <asp:TextBox ID="TBQuantity" runat="server"></asp:TextBox><br />
        <%--Precio unitario del producto comprado--%>
        <asp:Label ID="Label9" runat="server" Text="Precio unitario"></asp:Label><br />
        <asp:TextBox ID="TBUnitPrice" runat="server"></asp:TextBox><br />
        <%--Número de la factura de compra--%>
        <asp:Label ID="Label10" runat="server" Text="Número de Factura:"></asp:Label><br />
        <asp:TextBox ID="TBInvoiceNumber" runat="server"></asp:TextBox>
        <br />
        <br />
        <%--Botones Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" /><br />
            <asp:Button ID="BtbClear" runat="server" Text="Limpiar" OnClick="BtbClear_Click" /><br />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />

    <%--Lista de Compras--%>
    <h2>Lista de Compras</h2>
    <table id="buysTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>CompraID</th>
                <th>FechaCompra</th>
                <th>TotalCompra</th>
                <th>NumeroFactura</th>
                <th>CantidadComprada</th>
                <th>PrecioUnitario</th>
                <th>fkproduct</th>
                <th>CodigoProducto</th>
                <th>NombreProducto</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Compras--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#buysTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFBuy.aspx/ListBuys",// Se invoca el WebMethod Listar Compras
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
                    { "data": "CompraID", "visible": false },
                    { "data": "FechaCompra" },
                    { "data": "TotalCompra" },
                    { "data": "NumeroFactura" },
                    { "data": "CantidadComprada" },
                    { "data": "PrecioUnitario" },
                    { "data": "fkproduct", "visible": false },
                    { "data": "CodigoProducto" },
                    { "data": "NombreProducto" },

                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.CompraID}">Editar</button>
                         <button class="delete-btn" data-id="${row.CompraID}">Eliminar</button>`;
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

            // Editar un cliente
            $('#buysTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#buysTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadBuysData(rowData);
            });

            //Eliminar un producto
            $('#buysTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del cliente
                if (confirm("¿Estás seguro de que deseas eliminar esta compra?")) {
                    deleteBuy(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadBuysData(rowData) {
            $('#<%= HFBuyID.ClientID %>').val(rowData.CompraID);
            $('#<%= TBDate.ClientID %>').val(rowData.FechaCompra);
            $('#<%= DDLProduct.ClientID %>').val(rowData.fkproduct);
            $('#<%= TBQuantity.ClientID %>').val(rowData.CantidadComprada);
            $('#<%= TBUnitPrice.ClientID %>').val(rowData.PrecioUnitario);
            $('#<%= TBInvoiceNumber.ClientID %>').val(rowData.NumeroFactura);
        }

        // Función para eliminar una compra
        function deleteBuy(id) {
            $.ajax({
                type: "POST",
                url: "WFBuy.aspx/DeleteBuy",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#buysTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Compra eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la compra.");
                }
            });
        }
    </script>
</asp:Content>
