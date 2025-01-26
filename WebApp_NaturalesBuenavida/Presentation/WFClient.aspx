<%@ Page Title="Gestión de clientes" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFClient.aspx.cs" Inherits="Presentation.WFClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <h2 class="text-center main-title">Lista de Clientes</h2>
    <div class="container mt-4 bg-white border rounded p-3">
        <%--Formulario para añadir o actualizar cliente--%>
        <%-- Id cliente --%>
        <asp:HiddenField ID="HFClientID" runat="server" />
        <asp:HiddenField ID="HFPersonID" runat="server" />
        <div class="my-3 text-center">
            <asp:Label ID="LblMsg" runat="server" Text="" CssClass=""></asp:Label>
        </div>
        <div class="d-flex flex-column">
            <div class="d-flex flex-column flex-md-row gap-1 p-1">
                <div class="col-md-6 border rounded p-1">
                    <div class="mb-3">
                        <asp:Label ID="lblTipoDocumento" runat="server" CssClass="form-label fw-bold" Text="Tipo documento"></asp:Label>
                        <asp:DropDownList ID="ddlTipoDocumento" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblIdentificacion" runat="server" CssClass="form-label fw-bold" Text="Identificación"></asp:Label>
                        <asp:TextBox ID="TBClientId" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblNombres" runat="server" CssClass="form-label fw-bold" Text="Nombres"></asp:Label>
                        <asp:TextBox ID="TBClientName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblApellidos" runat="server" CssClass="form-label fw-bold" Text="Apellidos"></asp:Label>
                        <asp:TextBox ID="TBClientLastName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblTelefono" runat="server" CssClass="form-label fw-bold" Text="Teléfono"></asp:Label>
                        <asp:TextBox ID="TBClientPhone" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 border rounded p-1">
                    <div class="mb-3">
                        <asp:Label ID="lblEmail" runat="server" CssClass="form-label fw-bold" Text="Correo Electrónico"></asp:Label>
                        <asp:TextBox ID="TBClientEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <div class="mb-3">
                                <asp:Label ID="lblPais" runat="server" CssClass="form-label fw-bold" Text="Pais"></asp:Label>
                                <asp:DropDownList ID="ddlPais" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblDep" runat="server" CssClass="form-label fw-bold" Text="Departamento"></asp:Label>
                                <asp:DropDownList ID="ddlDepartamento" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblCiudad" runat="server" CssClass="form-label fw-bold" Text="Ciudad"></asp:Label>
                                <asp:DropDownList ID="ddlCiudad" CssClass="form-select" runat="server">
                                </asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="mb-3">
                        <asp:Label ID="lblDireccion" runat="server" CssClass="form-label fw-bold" Text="Direccion"></asp:Label>
                        <asp:TextBox ID="TBDireccion" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <%-- Botones Guardar y Actualizar --%>
            <div class="d-flex flex-column flex-md-row  gap-2 mt-3">
                <asp:Button ID="BtnSave" CssClass="btn" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
                <asp:Button ID="BtbClear" runat="server" Text="Limpiar" CssClass="btn" OnClick="BtbClear_Click" /><br />
            </div>
        </div>
    </div>

    <div class="container mt-4 table-responsive bg-white border rounded">
        <table id="clientsTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>Person ID</th>
                    <th>ID</th>
                    <th>Tipo documento ID</th>
                    <th>Tipo documento</th>
                    <th>Identificación</th>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Teléfono</th>
                    <th>Email</th>
                    <th>Pais Id</th>
                    <th>Pais</th>
                    <th>Departamento Id</th>
                    <th>Departamento</th>
                    <th>Ciudad Id</th>
                    <th>Ciudad</th>
                    <th>Direccion</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <%-- Librerías de DataTables --%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%-- Scripts para manejar la tabla de clientes --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#clientsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFClient.aspx/ListClients", // WebMethod que lista los clientes
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d); // Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data; // Extrae la lista de clientes
                    }
                },
                "columns": [
                    { "data": "PersonaID", visible: false },
                    { "data": "ClienteID" },
                    { "data": "TipoDocumentoID", visible: false },
                    { "data": "TipoDocumento" },
                    { "data": "Identification" },
                    { "data": "FirstName" },
                    { "data": "LastName" },
                    { "data": "Phone" },
                    { "data": "Email" },
                    { "data": "PaisId", visible: false },
                    { "data": "Pais" },
                    { "data": "DepartamentoId", visible: false },
                    { "data": "Departamento" },
                    { "data": "CiudadId", visible: false },
                    { "data": "Ciudad" },
                    { "data": "Direccion", visible: false },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" type="button" data-id="${row.ClientID}">Editar</button>
                                    <button class="delete-btn" type="button"  data-id="${row.ClientID}">Eliminar</button>`;
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
                const rowData = $('#clientsTable').DataTable().row($(this).parents('tr')).data();
                loadClientData(rowData);
            });

            // Eliminar un cliente
            $('#clientsTable').on('click', '.delete-btn', function () {
                const rowData = $('#clientsTable').DataTable().row($(this).parents('tr')).data();
                if (confirm("¿Estás seguro de que deseas eliminar este cliente?")) {
                    deleteClient(rowData);
                }
            });
        });

        // Función para cargar los datos seleccionado
        function loadClientData(rowData) {
            console.log('', rowData)
            $('#<%= HFPersonID.ClientID %>').val(rowData.PersonaID);
            $('#<%= HFClientID.ClientID %>').val(rowData.ClienteID);
            $('#<%= ddlTipoDocumento.ClientID %>').val(rowData.TipoDocumentoID);
            $('#<%= TBClientId.ClientID %>').val(rowData.Identification);
            $('#<%= TBClientName.ClientID %>').val(rowData.FirstName);
            $('#<%= TBClientLastName.ClientID %>').val(rowData.LastName);
            $('#<%= TBClientPhone.ClientID %>').val(rowData.Phone);
            $('#<%= TBClientEmail.ClientID %>').val(rowData.Email);
            $('#<%= ddlPais.ClientID %>').val(rowData.PaisId).change();
            setTimeout(function () {
                $('#<%= ddlDepartamento.ClientID %>').val(rowData.DepartamentoId).change();
            }, 500);
            setTimeout(function () {
                $('#<%= ddlCiudad.ClientID %>').val(rowData.CiudadId);
            }, 1000);
            $('#<%= TBDireccion.ClientID %>').val(rowData.Direccion);
        }

        // Función para eliminar un cliente
        function deleteClient(rowData) {
            $.ajax({
                type: "POST",
                url: "WFClient.aspx/DeleteClient", // WebMethod para eliminar un cliente
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idCliente: rowData.ClienteID, idPersona: rowData.PersonaID }),
                success: function (response) {
                    if (response.d.Success) {
                        alert("Cliente eliminado exitosamente.");
                        $('#clientsTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    } else {
                        alert('Error al eliminar el cliente: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Error al eliminar el cliente.");
                }
            });
        }
    </script>
</asp:Content>
