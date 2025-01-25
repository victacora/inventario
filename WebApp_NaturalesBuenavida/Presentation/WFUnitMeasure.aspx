<%@ Page Title="Gestión de Unidades de Medida" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUnitMeasure.aspx.cs" Inherits="Presentation.WFUnitMeasure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Listado de unidades de medida</h2>
    <div class="container mt-4 bg-white border rounded p-3">

        <asp:HiddenField ID="HFUnitID" runat="server" />

        <div class="mt-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>

        <div class="mb-3">
            <asp:Label ID="Label2" runat="server" Text="Nombre" CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox ID="TBUnitName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="d-flex flex-column flex-md-row gap-2 mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtnClear_Click" />
        </div>
    </div>


    <div class="container table-responsive mt-4 bg-white border rounded">
        <table id="unitsTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Descripcion</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>


    <%-- Scripts --%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#unitsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFUnitMeasure.aspx/ListUnits",
                    "type": "POST",
                    "contentType": "application/json",
                    "dataSrc": function (json) {
                        return json.d.data; // Esto debe coincidir con el formato devuelto por el servidor
                    }
                },
                "columns": [
                    { "data": "und_id" },  // Asegúrate de que esta propiedad coincida con lo que devuelve el servidor
                    { "data": "und_descripcion" }, // Asegúrate de que esta propiedad coincida con lo que devuelve el servidor
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" type="button" data-id="${row.und_id}">Editar</button>            
                                    <button class="delete-btn" type="button" data-id="${row.und_id}">Eliminar</button>`;  // Defino los botones de acción con sus respectivos IDs
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

            // Manejo de clic en botones de acción
            $('#unitsTable').on('click', '.edit-btn', function () {
                const rowData = $('#unitsTable').DataTable().row($(this).parents('tr')).data();
                loadUnitData(rowData);
            });
            // Manejo del clic en el botón de "eliminar" de la tabla
            $('#unitsTable').on('click', '.delete-btn', function () {
                const unitId = $(this).data('id');       // Obtengo el ID de la unidad a eliminar
                if (confirm("¿Estás seguro de que deseas eliminar esta unidad?")) {
                    $.ajax({
                        url: 'WFUnitMeasure.aspx/DeleteUnit', // invoca el método DeleteUnit en el servidor
                        type: 'POST',
                        data: JSON.stringify({ unitId: unitId }),  // Enviar el ID de la unidad a eliminar
                        contentType: 'application/json',
                        success: function (response) {
                            if (response.d.Success) {
                                alert('Unidad eliminada correctamente.');
                                $('#unitsTable').DataTable().ajax.reload(); // Recarga la tabla para reflejar la eliminación
                            } else {
                                alert('Error al eliminar la unidad: ' + response.d.Message);
                            }
                        },
                        error: function () {
                            alert("Hubo un error al eliminar la unidad.");
                        }
                    });
                }
            });

        });
        // Función para cargar los datos de la unidad seleccionada en los campos de edición
        function loadUnitData(rowData) {
            $('#<%= HFUnitID.ClientID %>').val(rowData.und_id);  // Coloco el ID de la unidad en el HiddenField
            $('#<%= TBUnitName.ClientID %>').val(rowData.und_descripcion); // Coloco la descripción de la unidad en el TextBox
        }
    </script>
</asp:Content>
