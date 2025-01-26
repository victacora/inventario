<%@ Page Title="Gestión de roles" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRole.aspx.cs" Inherits="Presentation.WFRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="text-center main-title">Listado de Roles</h2>
    <div class="container mt-4 bg-white border rounded p-3">
        <div class="border rounded p-2 m-1">
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
        </div>
        <div id="permisos" class="table-responsive border rounded p-2 m-1" style="display: none">
            <table id="permisosTable" class="table display" style="width: 100%">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Permiso</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="d-flex flex-column flex-md-row  gap-2 mt-3 ms-md-2 ">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtnClear_Click" />
        </div>
    </div>
    <div class="container mt-4 table-responsive bg-white border rounded">
        <table id="rolesTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th></th>
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
                    {
                        className: 'dt-control text-start',
                        orderable: false,
                        data: null,
                        defaultContent: ''
                    },
                    { "data": "roleID" },
                    { "data": "roleName" },
                    { "data": "roleDescription" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" type="button" data-id="${row.roleID}">Editar</button>
                             <button class="delete-btn" type="button" data-id="${row.roleID}">Eliminar</button>`;
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

            $('#rolesTable').on('click', 'td.dt-control', function (e) {
                let tr = e.target.closest('tr');
                let row = $('#rolesTable').DataTable().row(tr);

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                }
                else {
                    // Open this row
                    row.child(format(row.data())).show();
                }
            });

            function format(d) {
                if (!d.privilegios || d.privilegios.length === 0) {
                    return '<div>No tiene permisos asociados</div>';
                }

                var privilegesHtml = '<table class="table display" style="width: 100%">' +
                    '<thead>' +
                    '<tr>' +
                    '<th>ID</th>' +
                    '<th>Permiso</th>' +
                    '<th>Descripcion</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody>';
                d.privilegios.forEach(function (priv) {
                    privilegesHtml += `<tr><td>${priv.permisoID}</td><td>${priv.permisoName}</td><td>${priv.permisoDescription}</td></tr>`;
                });
                privilegesHtml += '</tbody></table>';

                return privilegesHtml;
            }


            $('#permisosTable').DataTable({
                "processing": true,
                "serverSide": false,
                "deferLoading": 0,
                "ajax": {
                    "url": "WFRole.aspx/PermisosByRolList",// Se invoca el WebMethod Listar roles
                    "type": "POST",
                    "data": function (d) {
                        const rolId = $('#<%= HFRoleID.ClientID %>').val();
                        return JSON.stringify({ rolId: rolId && rolId != '' ? rolId : '0' });
                    },
                    "contentType": "application/json",
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de permisos del resultado
                    }
                },
                "columns": [
                    { "data": "permisoID" },
                    { "data": "permisoName" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return row.agregar ? `<button class="add-btn" type="button" data-id="${row.permisoID}">Agregar</button>`:`<button class="delete-btn" type="button" data-id="${row.permisoID}">Eliminar</button>`;
                        }
                    }
                ],
                "lengthMenu": [
                    [5, 10, 20, 50],
                    ["5", "10", "25", "50 "]
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

            // Agregar un permiso
            $('#permisosTable').on('click', '.add-btn', function () {
                const rowData = $('#permisosTable').DataTable().row($(this).parents('tr')).data();
                savePermitByRole(rowData.permisoID, rowData.rolId);
            });

            // Eliminar un permiso
            $('#permisosTable').on('click', '.delete-btn', function () {
                const rowData = $('#permisosTable').DataTable().row($(this).parents('tr')).data();
                if (confirm("¿Estás seguro de que deseas eliminar el permiso?")) {
                    deletePermisoByRole(rowData.permisoID, rowData.rolId);
                }
            });
        });

        // Cargar los datos en los TextBox para actualizar
        function loadRoleDat(rowData) {
            const roleId = rowData.roleID;
            $('#<%= HFRoleID.ClientID %>').val(rowData.roleID);
            $('#<%= TBRoleName.ClientID %>').val(rowData.roleName);
            $('#<%= TBRoleDescription.ClientID %>').val(rowData.roleDescription);
            if (roleId) {
                $('#permisos').show();
                // Reload table with new data for the selected role
                $('#permisosTable').DataTable().ajax.reload();
            } else {
                // Clear the table if no role is selected
                $('#permisos').hide();
                $('#permisosTable').DataTable().clear().draw();
            }
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
                        $('#rolesTable').DataTable().ajax.reload();
                    } else {
                        alert('Error al eliminar el rol: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Hubo un error al eliminar el rol.");
                }
            });
        }

        // Función para eliminar un permiso asociado a un rol
        function deletePermisoByRole(permId, rolId) {
            $.ajax({
                url: 'WFRole.aspx/DeletePermitByRole',
                type: 'POST',
                data: JSON.stringify({ rolId: rolId, permId: permId  }),
                contentType: 'application/json',
                success: function (response) {
                    if (response.d.Success) {
                        alert('Permiso eliminado correctamente.');
                        $('#permisosTable').DataTable().ajax.reload();
                    } else {
                        alert('Error al eliminar el permiso asociado al rol: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Hubo un error al eliminar el permiso asociado al rol.");
                }
            });
        }

        // Función para agregar un permiso asociado a un rol
        function savePermitByRole(permId, rolId) {
            $.ajax({
                url: 'WFRole.aspx/savePermitByRole',
                type: 'POST',
                data: JSON.stringify({ rolId: rolId, permId: permId }),
                contentType: 'application/json',
                success: function (response) {
                    if (response.d.Success) {
                        alert('Permiso agregado correctamente.');
                        $('#permisosTable').DataTable().ajax.reload();
                    } else {
                        alert('Error al agregar el permiso asociado al rol: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Hubo un error al agregar el permiso asociado al rol.");
                }
            });
        }
    </script>
</asp:Content>
