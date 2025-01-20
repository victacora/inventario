<%@ Page Title="Gestión de Ventas" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFSales.aspx.cs" Inherits="Presentation.WFSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form>
        <asp:HiddenField ID="HFSaleID" runat="server" />
        <br />
        <%-- Fecha de la venta --%>
        <asp:Label ID="LabelDate" runat="server" Text="Fecha de la venta"></asp:Label><br />
        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox><br />
        <br />
        <%-- Total de la venta --%>
        <asp:Label ID="LabelTotal" runat="server" Text="Total de la venta"></asp:Label><br />
        <asp:TextBox ID="TBTotal" runat="server"></asp:TextBox><br />
        <br />
        <%-- Descripción de la venta --%>
        <asp:Label ID="LabelDescription" runat="server" Text="Descripción"></asp:Label><br />
        <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox><br />
        <br />
        <%-- Cliente --%>
        <asp:Label ID="LabelClient" runat="server" Text="Cliente"></asp:Label><br />
        <asp:DropDownList ID="DDLClient" runat="server"></asp:DropDownList><br />
        <br />
        <%-- Empleado --%>
        <asp:Label ID="LabelEmployee" runat="server" Text="Empleado"></asp:Label><br />
        <asp:DropDownList ID="DDLEmployee" runat="server"></asp:DropDownList><br />
        <br />
        <%-- Botones Guardar, Actualizar y Limpiar --%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" OnClick="BtnClear_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%-- Lista de Ventas --%>
    <h2>Lista de Ventas</h2>
    <table id="salesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>VentaID</th>
                <th>FechaVenta</th>
                <th>TotalVenta</th>
                <th>Descripción</th>
                <th>NombreEmpleado</th>
                <th>ApellidoEmpleado</th>
                <th>IdentificacionCliente</th>
                <th>NombreCliente</th>
                <th>ApellidoCliente</th>                
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <%-- Scripts --%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#salesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFSales.aspx/ListSales",
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
                    { "data": "VentaID", "visible": false },
                    { "data": "FechaVenta" },
                    { "data": "TotalVenta" },
                    { "data": "Descripción" },
                    { "data": "NombreEmpleado" },
                    { "data": "ApellidoEmpleado" },
                    { "data": "IdentificacionCliente" },
                    { "data": "NombreCliente" },
                    { "data": "ApellidoCliente" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.VentaID}">Editar</button>
                                    <button class="delete-btn" data-id="${row.VentaID}">Eliminar</button>`;
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
            // Manejar clic en el botón de eliminar
            $('#salesTable').on('click', '.delete-btn', function () {
                const saleId = $(this).data('id');  // Obtiene el ID de la venta

                if (confirm("¿Estás seguro de que deseas eliminar esta venta?")) {
                    $.ajax({
                        url: 'WFSales.aspx/DeleteSale',
                        type: 'POST',
                        data: JSON.stringify({ saleId: saleId }), // Pasa el ID de la venta
                        contentType: 'application/json',
                        success: function (response) {
                            if (response.success) {
                                alert('Venta eliminada correctamente.');
                                $('#salesTable').DataTable().ajax.reload(); // Recarga los datos de la tabla
                            } else {
                                alert('Error al eliminar la venta.');
                            }
                        }
                    });
                }
            });

             // Manejar clic en el botón de Editar
            $('#salesTable').on('click', '.edit-btn', function () {
                const rowData = $('#salesTable').DataTable().row($(this).parents('tr')).data();
                loadSalesData(rowData);
            });
        });

        function loadSalesData(rowData) {
            $('#<%= HFSaleID.ClientID %>').val(rowData.VentaID);
            $('#<%= TBDate.ClientID %>').val(rowData.FechaVenta);
            $('#<%= TBTotal.ClientID %>').val(rowData.TotalVenta);
            $('#<%= TBDescription.ClientID %>').val(rowData.Descripción);
            $('#<%= DDLClient.ClientID %>').val(rowData.ClienteID);
            $('#<%= DDLEmployee.ClientID %>').val(rowData.EmpleadoID);
        }
    </script>
</asp:Content>
