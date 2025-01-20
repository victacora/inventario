<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPresentation.aspx.cs" Inherits="Presentation.WFPresentation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- HiddenField para manejar ID de presentación --%>
    <asp:HiddenField ID="HFPresentationID" runat="server" />
    <br />

    <%-- Campo para ingresar la descripción de la presentación --%>
    <asp:Label ID="Label1" runat="server" Text="Descripción de la Presentación"></asp:Label><br />
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox><br />
    <br />

    <%-- Botones para Guardar, Actualizar y Limpiar --%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Button ID="BtnClear" runat="server" Text="Limpiar" OnClick="BtnClear_Click" /><br />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%-- Tabla para listar las presentaciones --%>
    <h2>Lista de Presentaciones</h2>
    <table id="presentationTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>Presentacion ID</th>
                <th>Descripción</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <%-- Script de DataTables --%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#presentationTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPresentation.aspx/ListPresentations",
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
                    { "data": "PresID" },
                    { "data": "Descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.PresID}">Editar</button>
                                    <button class="delete-btn" data-id="${row.PresID}">Eliminar</button>`;
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

            // Editar una presentación
            $('#presentationTable').on('click', '.edit-btn', function () {
                const rowData = $('#presentationTable').DataTable().row($(this).parents('tr')).data();
                loadPresentationData(rowData);
            });

            // Eliminar una presentación
            $('#presentationTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar esta presentación?")) {
                    deletePresentation(id);
                }
            });
        });

        // Cargar datos en los campos para editar
        function loadPresentationData(rowData) {
            $('#<%= HFPresentationID.ClientID %>').val(rowData.PresID);
            $('#<%= TBDescription.ClientID %>').val(rowData.Descripcion);
        }

        // Eliminar una presentación
        function deletePresentation(id) {
            $.ajax({
                type: "POST",
                url: "WFPresentation.aspx/DeletePresentation",
                data: JSON.stringify({ presId: id }),
                contentType: "application/json",
                success: function (response) {
                    alert(response.d.message);
                    $('#presentationTable').DataTable().ajax.reload();
                },
                error: function (error) {
                    console.error(error);
                }
            });
        }
    </script>
</asp:Content>
