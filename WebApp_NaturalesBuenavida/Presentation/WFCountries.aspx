<%@ Page Title="Gestión de Paises" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCountries.aspx.cs" Inherits="Presentation.WFCountries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Listado de Paises</h2>
    <div class="container mt-4 bg-white border rounded p-3">

        <asp:HiddenField ID="HFID" runat="server" />

        <div class="mt-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Label ID="LabelCodigo" runat="server" Text="Código" CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox ID="TBCode" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="LabelName" runat="server" Text="Nombre" CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox ID="TBCountryName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="d-flex flex-column flex-md-row gap-2 mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtnClear_Click" />
        </div>
    </div>


    <div class="container table-responsive mt-4 bg-white border rounded">
        <table id="dataTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Codigo</th>
                    <th>Pais</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFCountries.aspx/ListData",
                    "type": "POST",
                    "contentType": "application/json",
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "id" },
                    { "data": "codigo" },
                    { "data": "pais" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" type="button" data-id="${row.id}">Editar</button>            
                                    <button class="delete-btn" type="button" data-id="${row.id}">Eliminar</button>`;
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
            $('#dataTable').on('click', '.edit-btn', function () {
                const rowData = $('#dataTable').DataTable().row($(this).parents('tr')).data();
                loadData(rowData);
            });

            // Manejo del clic en el botón de "eliminar" de la tabla
            $('#dataTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');       // Obtengo el ID de la unidad a eliminar
                if (confirm("¿Estás seguro de que deseas eliminar este pais?")) {
                    $.ajax({
                        url: 'WFCountries.aspx/Delete',
                        type: 'POST',
                        data: JSON.stringify({ id: id }),
                        contentType: 'application/json',
                        success: function (response) {
                            if (response.d.Success) {
                                alert('Pais eliminado correctamente.');
                                $('#dataTable').DataTable().ajax.reload();
                            } else {
                                alert('Error al eliminar el pais: ' + response.d.Message);
                            }
                        },
                        error: function () {
                            alert("Hubo un error al eliminar el pais.");
                        }
                    });
                }
            });

        });

        // Función para cargar los datos de la fila seleccionada en los campos de edición
        function loadData(rowData) {
            $('#<%= HFID.ClientID %>').val(rowData.id);
            $('#<%= TBCode.ClientID %>').val(rowData.codigo);
            $('#<%= TBCountryName.ClientID %>').val(rowData.pais);
        }
    </script>
</asp:Content>
