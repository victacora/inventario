<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFReturns.aspx.cs" Inherits="Presentation.WFReturns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HFReturnID" runat="server" />
    <br />
    
   <!-- Fecha de devolución -->
<asp:Label ID="LabelFecha" runat="server" Text="Fecha de Devolución"></asp:Label><br />
<asp:TextBox ID="TBReturnDate" runat="server" TextMode="Date"></asp:TextBox><br />

<!-- Motivo de la devolución -->
<asp:Label ID="LabelMotivo" runat="server" Text="Motivo"></asp:Label><br />
<asp:TextBox ID="TBMotivo" runat="server"></asp:TextBox><br />

<!-- Ventas disponibles (DropDownList) -->
<asp:Label ID="LabelVenta" runat="server" Text="Venta Asociada"></asp:Label><br />
<asp:DropDownList ID="DDLVentas" runat="server"></asp:DropDownList><br />


<br />
<!-- Botones Guardar, Actualizar, Limpiar -->
<div>
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Button ID="BtnClear" runat="server" Text="Limpiar" OnClick="BtnClear_Click" />
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
</div>
<br />

    <br />

    <!-- Lista de devoluciones -->
    <h2>Lista de Devoluciones</h2>
    <table id="returnsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>DevolucionID</th>
                <th>FechaDevolucion</th>
                <th>Motivo</th>
                <th>VentaID</th>
                <th>FechaVenta</th>
                <th>DescripcionVenta</th>
                <th>EmpleadoID</th>
                <th>EmpleadoNombre</th>
                <th>ClienteID</th>
                <th>ClienteNombre</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

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
                    { "data": "VentaID" },
                    { "data": "FechaVenta" },
                    { "data": "DescripcionVenta" },
                    { "data": "IdentificacionEmpleado" },
                    { "data": "NombreEmpleado" },
                    { "data": "IdentificacionCliente" },
                    { "data": "NombreCliente" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.DevolucionID}">Editar</button>
                                    <button class="delete-btn" data-id="${row.DevolucionID}">Eliminar</button>`;
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
                            alert(response.success ? 'Devolución eliminada.' : 'Error al eliminar.');
                            $('#returnsTable').DataTable().ajax.reload();
                        },
                        error: function () {
                            alert("Error al eliminar la devolución.");
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
