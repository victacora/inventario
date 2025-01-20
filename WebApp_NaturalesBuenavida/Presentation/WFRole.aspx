<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRole.aspx.cs" Inherits="Presentation.WFRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Formulario--%>
    <%--Rol Id--%>
    <asp:HiddenField ID="HFRoleID" runat="server" />
    <br />
    <%--Nombre del Role--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el rol"></asp:Label>
    <asp:TextBox ID="TBRoleName" runat="server"></asp:TextBox>
    <br />
    <%--Descripción de rol--%>
    <asp:Label ID="Label2" runat="server" Text="Descripción del rol"></asp:Label><br />
    <asp:TextBox ID="TBRoleDescription" runat="server"></asp:TextBox><br />
    <%--Botones Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de roles--%>
    <h2>Lista de roles</h2>
    <table id="rolesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>RoleName</th>
                <th>RoleDescription</th>

            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

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

        // Función para eliminar un permiso
        function deleteRole(id) {
            $.ajax({
                type: "POST",
                url: "WFRole.aspx/deleteRole",// Se invoca el WebMethod Eliminar un rol
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#rolesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Rol eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el rol.");
                }
            });
        }
    </script>
</asp:Content>
