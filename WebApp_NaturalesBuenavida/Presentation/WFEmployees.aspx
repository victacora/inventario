<%@ Page Title="Empleados" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmployee.aspx.cs" Inherits="Presentation.WFEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Formulario para añadir o actualizar empleado--%>
    <form>
        <%-- Id Empleado --%>
        <asp:HiddenField ID="HFEmployeeID" runat="server" />
        <br />

        <%-- Selección de Persona --%>
        <asp:Label ID="Label6" runat="server" Text="Seleccione la Persona"></asp:Label>
        <asp:DropDownList ID="DDLPerson" runat="server"></asp:DropDownList>
        <br />
        <br />

        <%-- Datos del Empleado --%>
        <asp:Label ID="Label1" runat="server" Text="Identificación del Empleado"></asp:Label><br />
        <asp:TextBox ID="TBEmployeeId" runat="server"></asp:TextBox><br />

        <asp:Label ID="Label2" runat="server" Text="Nombre del Empleado"></asp:Label><br />
        <asp:TextBox ID="TBEmployeeName" runat="server"></asp:TextBox><br />

        <asp:Label ID="Label3" runat="server" Text="Apellido del Empleado"></asp:Label><br />
        <asp:TextBox ID="TBEmployeeLastName" runat="server"></asp:TextBox><br />

        <asp:Label ID="Label4" runat="server" Text="Teléfono del Empleado"></asp:Label><br />
        <asp:TextBox ID="TBEmployeePhone" runat="server"></asp:TextBox><br />

        <asp:Label ID="Label5" runat="server" Text="Correo Electrónico del Empleado"></asp:Label><br />
        <asp:TextBox ID="TBEmployeeEmail" runat="server"></asp:TextBox><br />
        <br />


        <%-- Botones Guardar y Actualizar --%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtbClear" runat="server" Text="Limpiar" OnClick="BtbClear_Click" /><br />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%-- Lista de Empleados --%>
    <h2>Lista de Empleados</h2>
    <table id="employeesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Identificación</th>
                <th>Nombre</th>
                <th>Apellido</th>
                <th>Teléfono</th>
                <th>Email</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <%-- Librerías de DataTables --%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%-- Scripts para manejar la tabla de empleados --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#employeesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEmployee.aspx/ListEmployees", // WebMethod que lista los empleados
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
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.EmployeeID}">Informacion</button>
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

        // Función para cargar los datos del empleado seleccionado
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
                url: "WFEmployee.aspx/DeleteEmployee", // WebMethod para eliminar un empleado
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#employeesTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    alert("Empleado eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el empleado.");
                }
            });
        }
    </script>

</asp:Content>
