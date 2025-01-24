<%@ Page Title="Empleados" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmployees.aspx.cs" Inherits="Presentation.WFEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Lista de Empleados</h2>
    <div class="container mt-4 bg-white border rounded p-3">
        <%--Formulario para añadir o actualizar empleado--%>
        <%-- Id Empleado --%>
        <asp:HiddenField ID="HFEmployeeID" runat="server" />

        <div class="mt-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>

        <%-- Selección de Persona --%>

        <div class="mb-3">
            <asp:Label ID="Label6" runat="server" CssClass="form-label fw-bold" Text="Seleccione la Persona"></asp:Label>
            <asp:DropDownList ID="DDLPerson" CssClass="form-select" runat="server"></asp:DropDownList>
        </div>

        <%-- Datos --%>
        <div class="mb-3">
            <asp:Label ID="Label1" runat="server" CssClass="form-label fw-bold" Text="Identificación"></asp:Label>
            <asp:TextBox ID="TBEmployeeId" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="Label2" runat="server" CssClass="form-label fw-bold" Text="Nombres"></asp:Label>
            <asp:TextBox ID="TBEmployeeName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="Label3" runat="server" CssClass="form-label fw-bold" Text="Apellidos"></asp:Label>
            <asp:TextBox ID="TBEmployeeLastName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="Label4" runat="server" CssClass="form-label fw-bold" Text="Teléfono"></asp:Label>
            <asp:TextBox ID="TBEmployeePhone" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="Label5" runat="server" CssClass="form-label fw-bold" Text="Correo Electrónico"></asp:Label>
            <asp:TextBox ID="TBEmployeeEmail" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

       <div class="mb-3">
           <label for="txtPassword" class="form-label">Contraseña</label>
           <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
           <!--<a href="#" class="small d-block mt-1">¿Olvidaste tu contraseña?</a> -->
       </div>

        <div class="mb-3">
            <asp:Label ID="Label8" runat="server" CssClass="form-label fw-bold" Text="Rol"></asp:Label>
            <asp:DropDownList ID="ddlRol" CssClass="form-select" runat="server">
            </asp:DropDownList>
        </div>

        <div class="mb-3">
            <asp:Label ID="Label7" runat="server" CssClass="form-label fw-bold" Text="Estado"></asp:Label>
            <asp:DropDownList ID="ddlStatus" CssClass="form-select" runat="server">
                <asp:ListItem Text="Activo" Value="Activo"></asp:ListItem>
                <asp:ListItem Text="Inactivo" Value="Inactivo"></asp:ListItem>
            </asp:DropDownList>
        </div>


        <%-- Botones Guardar y Actualizar --%>
        <div class="d-flex flex-column flex-md-row  gap-2 mt-3">
            <asp:Button ID="BtnSave" CssClass="btn" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtbClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtbClear_Click" /><br />
        </div>
    </div>


    <div class="container mt-4 table-responsive bg-white border rounded">
        <table id="employeesTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Identificación</th>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Teléfono</th>
                    <th>Email</th>
                    <th>Usuario id</th>
                    <th>Usuario</th>
                    <th>Fecha registro</th>
                    <th>Rol id</th>
                    <th>Rol</th>
                    <th>Estado</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <%-- Librerías de DataTables --%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%-- Scripts para manejar la tabla de empleados --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#employeesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEmployees.aspx/ListEmployees", // WebMethod que lista los empleados
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d); // Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data; // Extrae la lista de empleados
                    }
                },
                "columns": [
                    { "data": "EmployeeID" },
                    { "data": "Identification" },
                    { "data": "FirstName" },
                    { "data": "LastName" },
                    { "data": "Phone" },
                    { "data": "Email" },
                    { "data": "UsuId", visible: false },
                    { "data": "Usuario" },
                    { "data": "Registro" },
                    { "data": "RolId", visible: false },
                    { "data": "Rol" },
                    { "data": "Estado" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.EmployeeID}">Editar</button>
                                    <button class="delete-btn" data-id="${row.EmployeeID}">Eliminar</button>`;
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

            // Editar un empleado
            $('#employeesTable').on('click', '.edit-btn', function () {
                const rowData = $('#employeesTable').DataTable().row($(this).parents('tr')).data();
                loadEmployeeData(rowData);
            });

            // Eliminar un empleado
            $('#employeesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este empleado?")) {
                    deleteEmployee(id);
                }
            });
        });

        // Función para cargar los datos seleccionado
        function loadEmployeeData(rowData) {
            $('#<%= HFEmployeeID.ClientID %>').val(rowData.EmployeeID);
            $('#<%= TBEmployeeId.ClientID %>').val(rowData.Identification);
            $('#<%= TBEmployeeName.ClientID %>').val(rowData.FirstName);
            $('#<%= TBEmployeeLastName.ClientID %>').val(rowData.LastName);
            $('#<%= TBEmployeePhone.ClientID %>').val(rowData.Phone);
            $('#<%= TBEmployeeEmail.ClientID %>').val(rowData.Email);
        }

        // Función para eliminar un empleado
        function deleteEmployee(id) {
            $.ajax({
                type: "POST",
                url: "WFEmployees.aspx/DeleteEmployee", // WebMethod para eliminar un empleado
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    if (response.d.Success) {
                        alert("Empleado eliminado exitosamente.");
                        $('#employeesTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    } else {
                        alert('Error al eliminar el empleado: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Error al eliminar el empleado.");
                }
            });
        }
    </script>

</asp:Content>
