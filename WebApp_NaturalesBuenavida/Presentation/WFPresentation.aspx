<%@ Page Title="Gestión de presentaciones" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPresentation.aspx.cs" Inherits="Presentation.WFPresentation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Lista de Presentaciones</h2>
    <div class="container mt-4 bg-white border rounded p-3">

        <asp:HiddenField ID="HFPresentationID" runat="server" />

        <div class="mt-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>

        <div class="mb-3">
            <asp:Label ID="Label3" runat="server" Text="Presentación" CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox ID="TBDescription"  CssClass="form-control" runat="server"></asp:TextBox><br />
        </div>

        <div class="d-flex flex-column flex-md-row gap-2 mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtnClear_Click" />
        </div>
    </div>


    <div class="container table-responsive mt-4 bg-white border rounded">
        <table id="presentationTable" class="table display" style="width: 100%">
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
    </div>
   

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
                            return `<button class="edit-btn" type="button" data-id="${row.PresID}">Editar</button>
                                    <button class="delete-btn" type="button" data-id="${row.PresID}">Eliminar</button>`;
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
                    if (response.d.Success) {
                        alert('Presentación eliminada correctamente.');
                        $('#presentationTable').DataTable().ajax.reload(); // Recarga la tabla para reflejar la eliminación
                    } else {
                        alert('Error al eliminar la presentacion: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Hubo un error al eliminar la presentacion.");
                }
            });
        }
    </script>
</asp:Content>
