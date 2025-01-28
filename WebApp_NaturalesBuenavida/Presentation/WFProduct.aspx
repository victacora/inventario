<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" EnableViewState="true" AutoEventWireup="true" CodeBehind="WFProduct.aspx.cs" Inherits="Presentation.WFProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Id Cliente--%>
    <asp:HiddenField ID="HFProductID" runat="server" />
    <br />

    <asp:Panel ID="PnlMultiProducts" runat="server">
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
                        <asp:TextBox ID="TBCode" runat="server"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="TBNameProduct" runat="server"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="TBQuantityP" runat="server"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="TBNumberLote" runat="server"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="TBSalePrice" runat="server"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="TBPurchasePrice" runat="server"></asp:TextBox>
                    <td>
                        <asp:TextBox ID="TBMedida" runat="server"></asp:TextBox>
                    <td>
                        <asp:DropDownList ID="DDLUnitMeasure" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLPresentation" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLCategory" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLSupplier" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnAddRow" runat="server" Text="Añadir a la fila" OnClick="btnAddRow_Click" />
                    </td>
                </tr>
            </tbody>
        </table>


        <h3>Lista productos a añadir</h3>
      
        <div>
            <asp:GridView ID="GVProduct" runat="server" AutoGenerateColumns="False" OnRowDeleting="GVProduct_RowDeleting" CssClass="table table-striped">
                <Columns>
                    <%--Columnas para mostrar las propiedades--%>
                    <asp:BoundField DataField="Codigo" HeaderText="Código"/>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Medida" HeaderText="Medida" />

                    <%--Mostrar los valores relacionados con las claves foráneas (FK)--%>
                    <asp:BoundField DataField="FkUnidadMedida" HeaderText="FkUnidadMedida" Visible="false"/>
                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
                    <asp:BoundField DataField="FkPresentacion" HeaderText="FkPresentacion" Visible="false"/>
                    <asp:BoundField DataField="Presentacion" HeaderText="Presentación" />
                    <asp:BoundField DataField="FkCategoria" HeaderText="FkCategoria" Visible="false"/>
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />

                    <%--Nuevas propiedades añadidas--%>
                    <asp:BoundField DataField="NumeroLote" HeaderText="Número Lote" />
                    <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:yyyy-MM-dd}" />

                    <%--Proveedor y su nombre--%>
                    <asp:BoundField DataField="FkProveedor" HeaderText="FkProveedor" Visible="false"/>
                    <asp:BoundField DataField="NombreProveedor" HeaderText="Proveedor" />

                    <%--Botón para eliminar una fila--%>
                    <asp:CommandField ShowDeleteButton="True" HeaderText="Acciones" ButtonType="Button" />
                </Columns>
            </asp:GridView>
        </div>

        <asp:Button ID="BtnSave" runat="server" Text="Guardar Productos" OnClick="BtnSave_Click" />
        <%--<button type="button" id="btnSaveProducts" class="btn btn-success">Guardar Productos</button>--%>
    </asp:Panel>

    <br />
    <%--Botones Guardar y Actualizar--%>
    <div>
        <%--<asp:Button ID="BtnSave1" runat="server" Text="Guardar" OnClick="BtnSave_Click" />--%>
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" /><br />
        <asp:Button ID="BtbClear" runat="server" Text="Limpiar" OnClick="BtbClear_Click" /><br />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />


    <%--Lista de Productos--%>
    <h2>Lista de Productos</h2>
    <table id="productsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ProductID</th>
                <th>Codigo</th>
                <th>Nombre</th>
                <th>Descripcion</th>
                <th>Medida</th>
                <th>fkunidadmedida</th>
                <th>UnidadMedida</th>
                <th>fkpresentacion</th>
                <th>Presentacion</th>
                <th>fkcategory</th>
                <th>Categoria</th>
                <th>Cantidad</th>
                <th>NumeroLote</th>
                <th>FechaVencimiento</th>
                <th>PrecioVenta</th>
                <th>PrecioCompra</th>
                <th>fkproveedor</th>
                <th>NombreProveedor</th>
                <th>ApellidoProveedor</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>




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
                    { "data": "NombreProveedor" },
                    { "data": "ApellidoProveedor" },
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
                    $('#productsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Producto eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el producto.");
                }
            });
        }

    </script>

</asp:Content>
