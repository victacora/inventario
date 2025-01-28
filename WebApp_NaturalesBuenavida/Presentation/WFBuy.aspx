<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFBuy.aspx.cs" Inherits="Presentation.WFBuy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Lista de Compras</h2>
    <div class="container mt-4 bg-white border rounded p-3">

        <asp:HiddenField ID="HFBuyID" runat="server" />

        <div class="mt-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Label ID="LabelDate" runat="server" CssClass="form-label fw-bold" Text="Fecha de la compra"></asp:Label>
            <asp:TextBox ID="TBDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="Label7" runat="server"  CssClass="form-label fw-bold" Text="Seleccione el producto"></asp:Label>
            <asp:DropDownList ID="DDLProduct" CssClass="form-select" runat="server"></asp:DropDownList>
        </div>
        <div class="mb-3">
            <asp:Label ID="Label8" runat="server" Text="Cantidad comprada"  CssClass="form-label fw-bold" ></asp:Label>
            <asp:TextBox ID="TBQuantity" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="Label9" runat="server" Text="Precio unitario"  CssClass="form-label fw-bold" ></asp:Label>
            <asp:TextBox ID="TBUnitPrice" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="Label10" runat="server" Text="Número de Factura"   CssClass="form-label fw-bold" ></asp:Label>
            <asp:TextBox ID="TBInvoiceNumber" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <!-- Botones Guardar, Actualizar, Limpiar -->

        <div class="d-flex flex-column flex-md-row gap-2 mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtnClear_Click" />
        </div>
    </div>


    <div class="container table-responsive mt-4 bg-white border rounded">
        <table id="buysTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>CompraID</th>
                    <th>Fecha</th>
                    <th>Total</th>
                    <th>Numero factura</th>
                    <th>Cantidad</th>
                    <th>Precio unitario</th>
                    <th>fkproduct</th>
                    <th>Codigo producto</th>
                    <th>Nombre producto</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>

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
                            return `<button class="edit-btn"   type="button"  data-id="${row.CompraID}">Editar</button>
                         <button class="delete-btn"   type="button" data-id="${row.CompraID}">Eliminar</button>`;
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
                    if (response.d.Success) {
                        alert('Compra eliminada correctamente.');
                        $('#buysTable').DataTable().ajax.reload(); // Recarga la tabla para reflejar la eliminación
                    } else {
                        alert('Error al eliminar la compra: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Hubo un error al eliminar la compra.");
                }
            });
        }
    </script>
</asp:Content>
