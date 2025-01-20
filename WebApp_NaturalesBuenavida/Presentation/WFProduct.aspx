<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" EnableViewState="true" AutoEventWireup="true" CodeBehind="WFProduct.aspx.cs" Inherits="Presentation.WFProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Id Cliente--%>
    <asp:HiddenField ID="HFProductID" runat="server" />
    <br />
    <%--<%--Codigo producto--
        <asp:Label ID="Label2" runat="server" Text="Codigo del producto"></asp:Label><br />
        <asp:TextBox ID="TBCode" runat="server"></asp:TextBox><br />
        <%--Nombre del producto--
        <asp:Label ID="Label3" runat="server" Text="Nombre del Producto"></asp:Label><br />
        <asp:TextBox ID="TBNameProduct" runat="server"></asp:TextBox><br />
        <%--Descripcion del producto--
        <asp:Label ID="Label4" runat="server" Text="Descripción del Producto"></asp:Label><br />
        <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox><br />
        <%--Cantidad del producto inicial para el inventario--
        <asp:Label ID="Label5" runat="server" Text="Cantidad del producto para inventario"></asp:Label><br />
        <asp:TextBox ID="TBQuantityP" runat="server"></asp:TextBox><br />
        <%-- Número de Lote --
        <asp:Label ID="Label6" runat="server" Text="Número de Lote"></asp:Label><br />
        <asp:TextBox ID="TBNumberLote" runat="server"></asp:TextBox><br />
        <%-- Fecha de vencimiento del producto --
        <asp:Label ID="Label7" runat="server" Text="Fecha de Vencimiento"></asp:Label><br />
        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox><br />
        <%-- Precio de venta del producto --
        <asp:Label ID="Label8" runat="server" Text="Precio de Venta"></asp:Label><br />
        <asp:TextBox ID="TBSalePrice" runat="server"></asp:TextBox><br />
        <%-- Precio de compra del producto --
        <asp:Label ID="Label9" runat="server" Text="Precio de Compra"></asp:Label><br />
        <asp:TextBox ID="TBPurchasePrice" runat="server"></asp:TextBox><br />
        <%-- Medida numérica --
        <asp:Label ID="Label10" runat="server" Text="Dato de Medida"></asp:Label><br />
        <asp:TextBox ID="TBMedida" runat="server"></asp:TextBox><br />
        <%-- DDL Unidad de la medida --
        <asp:Label ID="Label1" runat="server" Text="Unidad de Medida"></asp:Label><br />
        <asp:DropDownList ID="DDLUnitMeasure" runat="server">
            <%--<asp:ListItem Text="Seleccione una unidad de medida" Value="0"></asp:ListItem>--
        </asp:DropDownList><br />
        <%-- DDL Presentación del producto (tetrapack, caja, bolsa, etc...) --
        <asp:Label ID="Label13" runat="server" Text="Presentación del producto"></asp:Label><br />
        <asp:DropDownList ID="DDLPresentation" runat="server">
            <%--<asp:ListItem Text="Seleccione un tipo de presentación" Value="0"></asp:ListItem>--
        </asp:DropDownList><br />
        <%-- DDL Categoria del producto --
        <asp:Label ID="Label11" runat="server" Text="Categoria del producto"></asp:Label><br />
        <asp:DropDownList ID="DDLCategory" runat="server">
            <%--<asp:ListItem Text="Seleccione una categoria" Value="0"></asp:ListItem>--
        </asp:DropDownList><br />
        <%-- DDL Proveedor --
        <asp:Label ID="Label12" runat="server" Text="Proveedor"></asp:Label><br />
        <asp:DropDownList ID="DDLSupplier" runat="server">
            <%--<asp:ListItem Text="Seleccione un proveedor" Value="0"></asp:ListItem>--
        </asp:DropDownList><br />
        <br />
        <br />--%>

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
                        <%--<button type="button" id="btnAddRow" class="btn btn-primary">Añadir Fila</button>--%>
                        <%--<button type="button" class="btn btn-danger btnRemoveRow">Eliminar</button>--%>
                    </td>
                </tr>
            </tbody>
        </table>
        <%--<asp:Button ID="btnAddRow" runat="server" Text="Añadir a la fila"/>--%>
        <br />

        <h3>Lista productos a añadir</h3>
        <%--<table id="previewTable" class="table">
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
            </tbody>
        </table>--%>
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
                            return `<button class="edit-btn" data-id="${row.ProductID}">Editar</button>
                            <button class="delete-btn" data-id="${row.ProductID}">Eliminar</button>`;
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

    <%--<script type="text/javascript">
        // Al presionar el botón Añadir Fila
        document.getElementById("btnAddRow").onclick = function () {
            var code = document.getElementById("TBCode").value;
            alert("Code:" + code);
            // Obtener los valores de los campos
            var code = document.getElementById("TBCode").value;
            var name = document.getElementById("TBNameProduct").value;
            var description = document.getElementById("TBDescription").value;
            var quantity = document.getElementById("TBQuantityP").value;
            var lote = document.getElementById("TBNumberLote").value;
            var expiryDate = document.getElementById("TBDate").value;
            var salePrice = document.getElementById("TBSalePrice").value;
            var purchasePrice = document.getElementById("TBPurchasePrice").value;
            var measure = document.getElementById("TBMedida").value;
            var unitMeasure = document.getElementById("DDLUnitMeasure").value;
            var presentation = document.getElementById("DDLPresentation").value;
            var category = document.getElementById("DDLCategory").value;
            var supplier = document.getElementById("DDLSupplier").value;

            // Crear una nueva fila en la tabla previewTable
            var table = document.getElementById("previewTable").getElementsByTagName('tbody')[0];
            var newRow = table.insertRow(table.rows.length);

            // Añadir celdas con los valores
            newRow.insertCell(0).textContent = code;
            newRow.insertCell(1).textContent = name;
            newRow.insertCell(2).textContent = description;
            newRow.insertCell(3).textContent = quantity;
            newRow.insertCell(4).textContent = lote;
            newRow.insertCell(5).textContent = expiryDate;
            newRow.insertCell(6).textContent = salePrice;
            newRow.insertCell(7).textContent = purchasePrice;
            newRow.insertCell(8).textContent = measure;
            newRow.insertCell(9).textContent = unitMeasure;
            newRow.insertCell(10).textContent = presentation;
            newRow.insertCell(11).textContent = category;
            newRow.insertCell(12).textContent = supplier;

            // Botón Eliminar para la fila
            var removeCell = newRow.insertCell(13);
            var removeButton = document.createElement("button");
            removeButton.textContent = "Eliminar";
            removeButton.className = "btn btn-danger";
            removeButton.onclick = function () {
                var row = this.parentNode.parentNode;
                row.parentNode.removeChild(row);
            };
            removeCell.appendChild(removeButton);


            // Limpiar los campos de entrada después de agregar
            document.getElementById("TBCode").value = "";
            document.getElementById("TBNameProduct").value = "";
            document.getElementById("TBDescription").value = "";
            document.getElementById("TBQuantityP").value = "";
            document.getElementById("TBNumberLote").value = "";
            document.getElementById("TBDate").value = "";
            document.getElementById("TBSalePrice").value = "";
            document.getElementById("TBPurchasePrice").value = "";
            document.getElementById("TBMedida").value = "";
            document.getElementById("DDLUnitMeasure").value = "";
            document.getElementById("DDLPresentation").value = "";
            document.getElementById("DDLCategory").value = "";
            document.getElementById("DDLSupplier").value = "";
        };

        // Guardar productos (al hacer clic en Guardar)
        document.getElementById("btnSaveProducts").onclick = function () {
            // Aquí puedes agregar lógica para guardar los productos en la base de datos
            alert("Productos guardados exitosamente.");
        };
    </script>--%>
</asp:Content>
