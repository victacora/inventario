<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFClient.aspx.cs" Inherits="Presentation.WFClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:Label ID="LBId" runat="server" Text="" Visible="true"></asp:Label><br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Seleccione la persona a añadir como cliente"></asp:Label><br />
    
    <asp:DropDownList ID="DDLPerson" runat="server">
        <%--<asp:ListItem Text="Seleccione una persona" Value="0"></asp:ListItem>
    </asp:DropDownList><br />
    <br />
    <asp:GridView ID="GVClient" runat="server" OnSelectedIndexChanged="GVClient_SelectedIndexChanged" 
        OnRowDeleting="GVClient_RowDeleting">
        <Columns>
            <asp:CommandField ShowSelectButton="true"/>
            <asp:CommandField ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>--%>
    <%--Formulario--%>
    <form>
        <%--Id Cliente--%>
        <asp:HiddenField ID="HFClientID" runat="server" />
        <br />

        <%--Persona--%>
        <asp:Label ID="Label6" runat="server" Text="Seleccione la Persona"></asp:Label>
        <asp:DropDownList ID="DDLPerson" runat="server"></asp:DropDownList>
        <br />
        <br />
        <%--Identificación Persona--%>
        <asp:Label ID="Label1" runat="server" Text="Identificación Persona"></asp:Label><br />
        <asp:TextBox ID="TBIdPersona" runat="server"></asp:TextBox><br />
        <%--Nombre Persona--%>
        <asp:Label ID="Label2" runat="server" Text="Nombre de la persona"></asp:Label><br />
        <asp:TextBox ID="TBNamePerson" runat="server"></asp:TextBox><br />
        <%--Apellido Persona--%>
        <asp:Label ID="Label3" runat="server" Text="Apellido de la persona"></asp:Label><br />
        <asp:TextBox ID="TBLastNamePerson" runat="server"></asp:TextBox><br />
        <%--Telefono Persona--%>
        <asp:Label ID="Label4" runat="server" Text="Telefono de la persona"></asp:Label><br />
        <asp:TextBox ID="TBPhonePerson" runat="server"></asp:TextBox><br />
        <%--Email Persona--%>
        <asp:Label ID="Label5" runat="server" Text="Correo Electrónico de la persona"></asp:Label><br />
        <asp:TextBox ID="TBEmailPerson" runat="server"></asp:TextBox><br />
        <br />

        <%--Botones Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" /><br />
            <asp:Button ID="BtbClear" runat="server" Text="Limpiar" OnClick="BtbClear_Click" /><br />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />

    </form>

    <%--Lista de Clientes--%>
    <h2>Lista de Clientes</h2>
    <table id="clientsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Identificacion</th>
                <th>Nombre</th>
                <th>Apellido</th>
                <th>Telefono</th>
                <th>Email</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>


    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Clientes--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#clientsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFClient.aspx/ListClients",// Se invoca el WebMethod Listar Clientes
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "ClienteID" },
                    { "data": "Identificacion" },
                    { "data": "Nombre" },
                    { "data": "Apellido" },
                    { "data": "Telefono" },
                    { "data": "Email" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.ClienteID}">Editar</button>
                                 <button class="delete-btn" data-id="${row.ClienteID}">Eliminar</button>`;
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

            // Editar un cliente
            $('#clientsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#clientsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadClientData(rowData);
            });

            //Eliminar un producto
            $('#clientsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del cliente
                if (confirm("¿Estás seguro de que deseas eliminar este cliente?")) {
                    deleteClient(id);// Invoca a la función para eliminar el producto
                }
            });



        });

        $(document).ready(function () {
            $('#<%= DDLPerson.ClientID %>').change(function () {
                var personId = $(this).val();

                if (personId && personId !== "---- Seleccione una persona ----") {
                    $.ajax({
                        type: "POST",
                        url: "WFClient.aspx/GetPersonData", // Llama al WebMethod GetPersonData
                        data: JSON.stringify({ personId: personId }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var person = response.d;
                            // Llena los campos con los datos de la persona

                            $('#<%= TBIdPersona.ClientID %>').val(person.Identificacion);
                            $('#<%= TBNamePerson.ClientID %>').val(person.Nombre);
                            $('#<%= TBLastNamePerson.ClientID %>').val(person.Apellido);
                            $('#<%= TBPhonePerson.ClientID %>').val(person.Telefono);
                            $('#<%= TBEmailPerson.ClientID %>').val(person.Email);
                        },
                        error: function (error) {
                            alert("Error al obtener los datos de la persona.");
                        }
                    });
                } else {
                    // Limpia los campos si no se selecciona ninguna persona

                    $('#<%= TBIdPersona.ClientID %>').val('');
                    $('#<%= TBNamePerson.ClientID %>').val('');
                    $('#<%= TBLastNamePerson.ClientID %>').val('');
                    $('#<%= TBPhonePerson.ClientID %>').val('');
                    $('#<%= TBEmailPerson.ClientID %>').val('');
                }
            });
        });


        // Cargar los datos en los TextBox y DDL para actualizar
        function loadClientData(rowData) {
            $('#<%= HFClientID.ClientID %>').val(rowData.ClienteID);
            $('#<%= TBIdPersona.ClientID %>').val(rowData.Identificacion);
            $('#<%= TBNamePerson.ClientID %>').val(rowData.Nombre);
            $('#<%= TBLastNamePerson.ClientID %>').val(rowData.Apellido);
            $('#<%= TBPhonePerson.ClientID %>').val(rowData.Telefono);
            $('#<%= TBEmailPerson.ClientID %>').val(rowData.Email);
        }

        //Función para eliminar un cliente
        function deleteClient(id) {
            $.ajax({
                type: "POST",
                url: "WFClient.aspx/deleteClient",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#clientsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Cliente eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el cliente.");
                }
            });
        }
    </script>
</asp:Content>