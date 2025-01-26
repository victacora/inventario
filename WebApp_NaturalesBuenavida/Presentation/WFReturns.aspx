<%@ Page Title="Gestión de devoluciones" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFReturns.aspx.cs" Inherits="Presentation.WFReturns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HFReturnID" runat="server" />
    <h2 class="text-center main-title">Lista de Devoluciones</h2>
    <div class="container mt-4 bg-white border rounded p-3">

        <asp:HiddenField ID="HFID" runat="server" />

        <div class="mt-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>
        <div class="mb-3">
            <!-- Fecha de devolución -->
            <asp:Label ID="LabelFecha" runat="server" CssClass="form-label fw-bold" Text="Fecha de Devolución"></asp:Label>
            <asp:TextBox ID="TBReturnDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>
        <div class="mb-3">
            <!-- Motivo de la devolución -->
            <asp:Label ID="LabelMotivo" runat="server" CssClass="form-label fw-bold" Text="Motivo"></asp:Label>
            <asp:TextBox ID="TBMotivo" TextMode="MultiLine" Rows="5" Columns="40" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <!-- Ventas disponibles (DropDownList) -->
            <asp:Label ID="LabelVenta" runat="server" CssClass="form-label fw-bold" Text="Venta Asociada"></asp:Label>
            <asp:DropDownList ID="DDLVentas" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>

        <!-- Botones Guardar, Actualizar, Limpiar -->

        <div class="d-flex flex-column flex-md-row gap-2 mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtnClear_Click" />
        </div>
    </div>


    <div class="container table-responsive mt-4 bg-white border rounded">
        <table id="returnsTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Fecha devolucion</th>
                    <th>Motivo</th>
                    <th>VentaID</th>
                    <th>Fecha venta</th>
                    <th>Descripcion venta</th>
                    <th>Documento empleado</th>
                    <th>Empleado</th>
                    <th>Documento cliente</th>
                    <th>Cliente</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>

    <!-- Scripts -->
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Inicializar DataTable para devoluciones
            $('#returnsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFReturns.aspx/ListReturns", // Método en el servidor
                    "type": "POST",
                    "contentType": "application/json",
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "DevolucionID" },
                    { "data": "FechaDevolucion" },
                    { "data": "Motivo" },
                    { "data": "VentaID", visible: false },
                    { "data": "FechaVenta" },
                    { "data": "DescripcionVenta" },
                    { "data": "IdentificacionEmpleado" },
                    { "data": "NombreEmpleado" },
                    { "data": "IdentificacionCliente" },
                    { "data": "NombreCliente" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" type="button" data-id="${row.DevolucionID}">Editar</button>
                                    <button class="delete-btn" type="button" data-id="${row.DevolucionID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    "emptyTable": "No hay datos disponibles en la tabla",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
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

            // Lógica para editar una devolución
            $('#returnsTable').on('click', '.edit-btn', function () {
                const rowData = $('#returnsTable').DataTable().row($(this).parents('tr')).data();
                loadReturnData(rowData);
            });

            // Lógica para eliminar una devolución
            $('#returnsTable').on('click', '.delete-btn', function () {
                const returnId = $(this).data('id');
                if (confirm("¿Estás seguro de eliminar esta devolución?")) {
                    $.ajax({
                        url: 'WFReturns.aspx/DeleteReturn',
                        type: 'POST',
                        data: JSON.stringify({ returnId: returnId }),
                        contentType: 'application/json',
                        success: function (response) {
                            if (response.d.Success) {
                                alert('Devolución eliminada correctamente.');
                                $('#returnsTable').DataTable().ajax.reload(); // Recarga la tabla para reflejar la eliminación
                            } else {
                                alert('Error al eliminar la devolución: ' + response.d.Message);
                            }
                        },
                        error: function () {
                            alert("Hubo un error al eliminar la devolución.");
                        }
                    });
                }
            });
        });

        function loadReturnData(rowData) {
            console.log("Datos cargados:", rowData);
            $('#<%= HFReturnID.ClientID %>').val(rowData.DevolucionID);
            $('#<%= TBReturnDate.ClientID %>').val(rowData.FechaDevolucion);
            $('#<%= TBMotivo.ClientID %>').val(rowData.Motivo);
            $('#<%= DDLVentas.ClientID %>').val(rowData.VentaID);
        }
    </script>
</asp:Content>
