<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRole.aspx.cs" Inherits="Presentation.WFRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Listado de Roles</h2>
    <div class="container mt-4 bg-white border rounded p-3">

        <%--Formulario--%>
        <%--Rol Id--%>
        <asp:HiddenField ID="HFRoleID" runat="server" />

        <div class="mt-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Label ID="LabelRol" runat="server" Text="Rol" CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox ID="TBRoleName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="LabelName" runat="server" Text="Descripción" CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox ID="TBRoleDescription" TextMode="MultiLine" Rows="5" Columns="40" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="d-flex flex-column flex-md-row  gap-2 mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtnClear_Click" />
        </div>
    </div>


    <div class="container mt-4 table-responsive bg-white border rounded">
        <table id="rolesTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Rol</th>
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

    <%--Tipos de roles--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#rolesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFRole.aspx/RolesList",// Se invoca el WebMethod Listar roles
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
                    { "data": "roleID" },
                    { "data": "roleName" },
                    { "data": "roleDescription" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.roleID}">Editar</button>
                             <button class="delete-btn" data-id="${row.roleID}">Eliminar</button>`;
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
            // Editar un role
            $('#rolesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#rolesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadRoleDat(rowData);
            });

            // Eliminar un permiso
            $('#rolesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del permiso
                if (confirm("¿Estás seguro de que deseas eliminar el rol?")) {
                    deleteRole(id);// Invoca a la función para eliminar el rol
                }
            });
        });

        // Cargar los datos en los TextBox para actualizar
        function loadRoleDat(rowData) {
            $('#<%= HFRoleID.ClientID %>').val(rowData.roleID);
            $('#<%= TBRoleName.ClientID %>').val(rowData.roleName);
            $('#<%= TBRoleDescription.ClientID %>').val(rowData.roleDescription);

        }

        // Función para eliminar un rol
        function deleteRole(id) {
            $.ajax({
                url: 'WFRole.aspx/deleteRole',
                type: 'POST',
                data: JSON.stringify({ id: id }),
                contentType: 'application/json',
                success: function (response) {
                    if (response.d.Success) {
                        alert('Rol eliminado correctamente.');
                        $('#dataTable').DataTable().ajax.reload();
                    } else {
                        alert('Error al eliminar el rol: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Hubo un error al eliminar el rol.");
                }
            });
        }
    </script>
</asp:Content>
