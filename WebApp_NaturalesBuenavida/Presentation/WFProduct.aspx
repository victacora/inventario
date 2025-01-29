<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" EnableViewState="true" AutoEventWireup="true" CodeBehind="WFProduct.aspx.cs" Inherits="Presentation.WFProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Lista de Productos--%>
    <h2 class="text-center main-title">Lista de Productos</h2>
    <%--Id Cliente--%>
    <div class="container mt-4 bg-white border rounded p-3">
        <asp:HiddenField ID="HFProductID" runat="server" />
        <asp:Label ID="LblMsg" runat="server" Text="" ></asp:Label>
        <asp:Panel ID="PnlMultiProducts" runat="server">
            <div class="table-responsive mb-3 p-2 bg-white border rounded">
                <table id="productTable" class="table">
                    <thead>
                        <tr>
                            <th>Código</th>
                            <th>Nombre</th>
                            <th>Descripción</th>
                            <th>Cantidad</th>
                            <th>Número de Lote</th>
                            <th>Fecha de Vencimiento</th>
                            <th>Precio Venta</th>
                            <th>Precio Compra</th>
                            <th>Medida</th>
                            <th>Unidad de Medida</th>
                            <th>Presentación</th>
                            <th>Categoría</th>
                            <th>Proveedor</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:TextBox ID="TBCode" runat="server" CssClass="w-100px"></asp:TextBox>
                            <td>
                                <asp:TextBox ID="TBNameProduct" runat="server" CssClass="w-100px"></asp:TextBox>
                            <td>
                                <asp:TextBox ID="TBDescription" runat="server" CssClass="w-100px"></asp:TextBox>
                            <td>
                                <asp:TextBox ID="TBQuantityP" runat="server" CssClass="w-100px"></asp:TextBox>
                            <td>
                                <asp:TextBox ID="TBNumberLote" runat="server" CssClass="w-100px"></asp:TextBox>
                            <td>
                                <asp:TextBox ID="TBDate" runat="server"  CssClass="w-100px" TextMode="Date"></asp:TextBox>
                            <td>
                                <asp:TextBox ID="TBSalePrice" runat="server" CssClass="w-100px"></asp:TextBox>
                            <td>
                                <asp:TextBox ID="TBPurchasePrice" runat="server" CssClass="w-100px"></asp:TextBox>
                            <td>
                                <asp:TextBox ID="TBMedida" runat="server" CssClass="w-100px"></asp:TextBox>
                            <td>
                                <asp:DropDownList ID="DDLUnitMeasure" runat="server" CssClass="form-control w-100px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLPresentation" runat="server" CssClass="form-control w-100px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLCategory" runat="server" CssClass="form-control w-100px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLSupplier" runat="server" CssClass="form-control w-100px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnAddRow" runat="server" Text="Añadir a la fila" CssClass="add-btn" OnClick="btnAddRow_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>


                <h3 class="text-center main-title mb-3">Lista productos a añadir</h3>
             

                <div>
                    <asp:GridView ID="GVProduct" runat="server" AutoGenerateColumns="False" OnRowDeleting="GVProduct_RowDeleting" CssClass="table table-striped">
                        <Columns>
                            <%--Columnas para mostrar las propiedades--%>
                            <asp:BoundField DataField="Codigo" HeaderText="Código" HeaderStyle-CssClass="table-header" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="table-header"/>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="table-header"/>
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad"  HeaderStyle-CssClass="table-header"  />
                            <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta" HeaderStyle-CssClass="table-header" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" HeaderStyle-CssClass="table-header" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="Medida" HeaderText="Medida" HeaderStyle-CssClass="table-header" />

                            <%--Mostrar los valores relacionados con las claves foráneas (FK)--%>
                            <asp:BoundField DataField="FkUnidadMedida" HeaderText="FkUnidadMedida" Visible="false" />
                            <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" HeaderStyle-CssClass="table-header" />
                            <asp:BoundField DataField="FkPresentacion" HeaderText="FkPresentacion" Visible="false" />
                            <asp:BoundField DataField="Presentacion" HeaderText="Presentación" HeaderStyle-CssClass="table-header" />
                            <asp:BoundField DataField="FkCategoria" HeaderText="FkCategoria" Visible="false" />
                            <asp:BoundField DataField="Categoria" HeaderText="Categoría" HeaderStyle-CssClass="table-header" />

                            <%--Nuevas propiedades añadidas--%>
                            <asp:BoundField DataField="NumeroLote" HeaderText="Número Lote" HeaderStyle-CssClass="table-header"/>
                            <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-CssClass="table-header" />

                            <%--Proveedor y su nombre--%>
                            <asp:BoundField DataField="FkProveedor" HeaderText="FkProveedor" Visible="false" />
                            <asp:BoundField DataField="NombreProveedor" HeaderText="Proveedor" HeaderStyle-CssClass="table-header"/>

                            <%--Botón para eliminar una fila--%>
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Acciones"  HeaderStyle-CssClass="table-header"  ControlStyle-CssClass="delete-btn" ButtonType="Button" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>


        <%--Botones Guardar y Actualizar--%>
        
        <div class="d-flex flex-column flex-md-row  gap-2 mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar Productos" CssClass="btn" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar"  CssClass="btn" OnClick="BtnUpdate_Click" /><br />
            <asp:Button ID="BtbClear" runat="server" Text="Limpiar"  CssClass="btn" OnClick="BtbClear_Click" /><br />
        </div>
    </div>

    <div class="container table-responsive mt-4 bg-white border rounded">

        <table id="productsTable" class="table display" style="width: 100%">
            <thead>
                <tr>
                    <th>ProductID</th>
                    <th>Codigo</th>
                    <th>Nombre</th>
                    <th>Descripcion</th>
                    <th>Medida</th>
                    <th>fkunidadmedida</th>
                    <th>Unidad medida</th>
                    <th>fkpresentacion</th>
                    <th>Presentacion</th>
                    <th>fkcategory</th>
                    <th>Categoria</th>
                    <th>Cantidad</th>
                    <th>Numero lote</th>
                    <th>Fecha vencimiento</th>
                    <th>Precio venta</th>
                    <th>Precio compra</th>
                    <th>fkproveedor</th>
                    <th>Proveedor</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Productos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#productsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFProduct.aspx/ListProducts",// Se invoca el WebMethod Listar Compras
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
                    { "data": "ProductID", "visible": false },
                    { "data": "Codigo" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    { "data": "Medida" },
                    { "data": "fkunidadmedida", "visible": false },
                    { "data": "UnidadMedida" },
                    { "data": "fkpresentacion", "visible": false },
                    { "data": "Presentacion" },
                    { "data": "fkcategory", "visible": false },
                    { "data": "Categoria" },
                    { "data": "Cantidad" },
                    { "data": "NumeroLote" },
                    { "data": "FechaVencimiento" },
                    { "data": "PrecioVenta" },
                    { "data": "PrecioCompra" },
                    { "data": "fkproveedor", "visible": false },
                    { "data": "Proveedor" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" type="button" data-id="${row.ProductID}">Editar</button>
                            <button class="delete-btn" type="button" data-id="${row.ProductID}">Eliminar</button>`;
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

            // Editar un producto
            $('#productsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#productsTable').DataTable().row($(this).parents('tr')).data();

                //alert(JSON.stringify(rowData, null, 2));
                loadProductsData(rowData);
            });

            //Eliminar un producto
            $('#productsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del producto
                if (confirm("¿Estás seguro de que deseas eliminar este producto?")) {
                    deleteProduct(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadProductsData(rowData) {
            $('#<%= HFProductID.ClientID %>').val(rowData.ProductID);
            $('#<%= TBCode.ClientID %>').val(rowData.Codigo);
            $('#<%= TBNameProduct.ClientID %>').val(rowData.Nombre);
            $('#<%= TBDescription.ClientID %>').val(rowData.Descripcion);
            $('#<%= TBQuantityP.ClientID %>').val(rowData.Cantidad);
            $('#<%= TBNumberLote.ClientID %>').val(rowData.NumeroLote);
            $('#<%= TBDate.ClientID %>').val(rowData.FechaVencimiento);
            $('#<%= TBSalePrice.ClientID %>').val(rowData.PrecioVenta);
            $('#<%= TBPurchasePrice.ClientID %>').val(rowData.PrecioCompra);
            $('#<%= TBMedida.ClientID %>').val(rowData.Medida);
            $('#<%= DDLUnitMeasure.ClientID %>').val(rowData.fkunidadmedida);
            $('#<%= DDLPresentation.ClientID %>').val(rowData.fkpresentacion);
            $('#<%= DDLCategory.ClientID %>').val(rowData.fkcategory);
            $('#<%= DDLSupplier.ClientID %>').val(rowData.fkproveedor);


        }
        // Función para eliminar un producto
        function deleteProduct(id) {
            $.ajax({
                type: "POST",
                url: "WFProduct.aspx/DeleteProduct",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    if (response.d.Success) {
                        alert('Producto eliminado correctamente.');
                        $('#productsTable').DataTable().ajax.reload(); // Recarga los datos de la tabla
                    } else {
                        alert('Error al eliminar el producto.');
                    }
                    $('#').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert(" eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el producto.");
                }
            });
        }

    </script>

</asp:Content>
