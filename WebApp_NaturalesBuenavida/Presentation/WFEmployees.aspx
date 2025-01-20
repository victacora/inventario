<%@ Page Title="Gestión de Unidades de Medida" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmployees.aspx.cs" Inherits="Presentation.WFEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HFUnitID" runat="server" />
    <br />
    <%-- Nombre de la Unidad --%>
    <asp:Label ID="LabelName" runat="server" Text="Nombre de la Unidad"></asp:Label><br />
    <asp:TextBox ID="TBUnitName" runat="server"></asp:TextBox><br />
    <br />
    <%-- Botones Guardar, Actualizar y Limpiar --%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Button ID="BtnClear" runat="server" Text="Limpiar" OnClick="BtnClear_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%-- Lista de Unidades --%>
    <h2>Lista de Unidades de Medida</h2>
    <!-- Título para la tabla de unidades -->
    <table id="unitsTable" class="display" style="width: 100%">
        <!-- Tabla donde mostraré las unidades de medida -->
        <thead>
            <tr>
                <!-- Defino las cabeceras de la tabla -->
                <th>ID</th>
                <th>Descripcion</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

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
                            return `<button class="edit-btn" data-id="${row.und_id}">Editar</button>            
                                    <button class="delete-btn" data-id="${row.und_id}">Eliminar</button>`;  // Defino los botones de acción con sus respectivos IDs
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
                            if (response.success) {
                                alert('Unidad eliminada correctamente.');
                                $('#unitsTable').DataTable().ajax.reload(); // Recarga la tabla para reflejar la eliminación
                            } else {
                                alert('Error al eliminar la unidad: ' + response.Message);
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
