<%@ Page Title="Gestión de permisos" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPrivileges.aspx.cs" Inherits="Presentation.WFPrivileges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Listado de Permisos</h2>
    <div class="container mt-4 bg-white border rounded p-3">

        <%--Formulario--%>
        <%--Rol Id--%>
        <asp:HiddenField ID="HFPermisoID" runat="server" />

        <div class="mt-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Label ID="LabelPermiso" runat="server" Text="Permiso" CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox ID="TBPermisoName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="LabelName" runat="server" Text="Descripción" CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox ID="TBPermisoDescription" TextMode="MultiLine" Rows="5" Columns="40" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="d-flex flex-column flex-md-row  gap-2 mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtnClear_Click" />
        </div>
    </div>


    <div class="container mt-4 table-responsive bg-white border rounded">
        <table id="permisosTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Permiso</th>
                    <th>Descripcion</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>

    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Tipos de permisos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#permisosTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPrivileges.aspx/permisosList",// Se invoca el WebMethod Listar permisos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de permisos del resultado
                    }
                },
                "columns": [
                    { "data": "permisoID" },
                    { "data": "permisoName" },
                    { "data": "permisoDescription" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" type="button" data-id="${row.permisoID}">Editar</button>`;
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
            // Editar un permiso
            $('#permisosTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#permisosTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadPermisoDat(rowData);
            });

        });

        // Cargar los datos en los TextBox para actualizar
        function loadPermisoDat(rowData) {
            $('#<%= HFPermisoID.ClientID %>').val(rowData.permisoID);
            $('#<%= TBPermisoName.ClientID %>').val(rowData.permisoName);
            $('#<%= TBPermisoDescription.ClientID %>').val(rowData.permisoDescription);

        }
    </script>
</asp:Content>
