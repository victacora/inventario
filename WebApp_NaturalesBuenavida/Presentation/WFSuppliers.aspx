<%@ Page Title="Gestión de proveedores" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFSuppliers.aspx.cs" Inherits="Presentation.WFSuppliers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <h2 class="text-center main-title">Lista de proveedores</h2>
    <div class="container mt-4 bg-white border rounded p-3">
        <%--Formulario para añadir o actualizar proveedor--%>
        <%-- Id proveedor --%>
        <asp:HiddenField ID="HFSupplierID" runat="server" />
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
                        <asp:TextBox ID="TBSupplierId" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblNombres" runat="server" CssClass="form-label fw-bold" Text="Nombres"></asp:Label>
                        <asp:TextBox ID="TBSupplierName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblApellidos" runat="server" CssClass="form-label fw-bold" Text="Apellidos"></asp:Label>
                        <asp:TextBox ID="TBSupplierLastName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="lblTelefono" runat="server" CssClass="form-label fw-bold" Text="Teléfono"></asp:Label>
                        <asp:TextBox ID="TBSupplierPhone" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 border rounded p-1">
                    <div class="mb-3">
                        <asp:Label ID="lblEmail" runat="server" CssClass="form-label fw-bold" Text="Correo Electrónico"></asp:Label>
                        <asp:TextBox ID="TBSupplierEmail" CssClass="form-control" runat="server"></asp:TextBox>
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
        <table id="suppliersTable" class="table display" style="width: 100%">
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

    <%-- Scripts para manejar la tabla de proveedores --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#suppliersTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFSuppliers.aspx/ListSuppliers", // WebMethod que lista los proveedores
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d); // Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data; // Extrae la lista de proveedores
                    }
                },
                "columns": [
                    { "data": "PersonaID", visible: false },
                    { "data": "ProveedorID" },
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
                            return `<button class="edit-btn" type="button" data-id="${row.ProveedorID}">Editar</button>
                                    <button class="delete-btn" type="button"  data-id="${row.ProveedorID}">Eliminar</button>`;
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

            // Editar un proveedor
            $('#suppliersTable').on('click', '.edit-btn', function () {
                const rowData = $('#suppliersTable').DataTable().row($(this).parents('tr')).data();
                loadSupplierData(rowData);
            });

            // Eliminar un proveedor
            $('#suppliersTable').on('click', '.delete-btn', function () {
                const rowData = $('#suppliersTable').DataTable().row($(this).parents('tr')).data();
                if (confirm("¿Estás seguro de que deseas eliminar este proveedor?")) {
                    deleteSupplier(rowData);
                }
            });
        });

        // Función para cargar los datos seleccionado
        function loadSupplierData(rowData) {
            console.log('', rowData)
            $('#<%= HFPersonID.ClientID %>').val(rowData.PersonaID);
            $('#<%= HFSupplierID.ClientID %>').val(rowData.ProveedorID);
            $('#<%= ddlTipoDocumento.ClientID %>').val(rowData.TipoDocumentoID);
            $('#<%= TBSupplierId.ClientID %>').val(rowData.Identification);
            $('#<%= TBSupplierName.ClientID %>').val(rowData.FirstName);
            $('#<%= TBSupplierLastName.ClientID %>').val(rowData.LastName);
            $('#<%= TBSupplierPhone.ClientID %>').val(rowData.Phone);
            $('#<%= TBSupplierEmail.ClientID %>').val(rowData.Email);
            $('#<%= ddlPais.ClientID %>').val(rowData.PaisId).change();
            setTimeout(function () {
                $('#<%= ddlDepartamento.ClientID %>').val(rowData.DepartamentoId).change();
            }, 500);
            setTimeout(function () {
                $('#<%= ddlCiudad.ClientID %>').val(rowData.CiudadId);
            }, 1000);
            $('#<%= TBDireccion.ClientID %>').val(rowData.Direccion);
        }

        // Función para eliminar un proveedor
        function deleteSupplier(rowData) {
            $.ajax({
                type: "POST",
                url: "WFSuppliers.aspx/DeleteSupplier", // WebMethod para eliminar un proveedor
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idProveedor: rowData.ProveedorID, idPersona: rowData.PersonaID }),
                success: function (response) {
                    if (response.d.Success) {
                        alert("Proveedor eliminado exitosamente.");
                        $('#suppliersTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    } else {
                        alert('Error al eliminar el proveedor: ' + response.d.Message);
                    }
                },
                error: function () {
                    alert("Error al eliminar el proveedor.");
                }
            });
        }
    </script>
</asp:Content>
