<%@ Page Title="Detalle del inventario" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFInventoryDetails.aspx.cs" Inherits="Presentation.WFInventoryDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Detalles del Inventario</h2>
    <div class="container mt-4 bg-white border rounded p-3">
        <asp:HiddenField ID="LblInventoryId" runat="server" />

        <%--<asp:Label ID="LabelInventoryId" runat="server" Text="ID Inventario: "></asp:Label><br />
    <asp:Label ID="LblInventoryId" runat="server"></asp:Label><br />--%>
        <div class="mb-3">
            <asp:Label ID="LabelFecha" runat="server"  CssClass="form-label fw-bold" Text="Fecha Inventario: "></asp:Label>
            <asp:Label ID="LblFecha" runat="server"></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Label ID="LabelProducto" runat="server"  CssClass="form-label fw-bold" Text="Producto: "></asp:Label>
            <asp:Label ID="LblProducto" runat="server"></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Label ID="LabelObservacion" runat="server"  CssClass="form-label fw-bold" Text="Observaciones: "></asp:Label>
            <asp:Label ID="LblObservacion" runat="server"></asp:Label>
        </div>
        <div class="mb-3">
            <asp:Label ID="LabelEmpleado" runat="server"  CssClass="form-label fw-bold" Text="Empleado: "></asp:Label>
            <asp:Label ID="LblEmpleado" runat="server"></asp:Label>
        </div>

        <div class="mb-3">
            <asp:Repeater ID="RepeaterProducts" runat="server">
                <HeaderTemplate>
                    <h3 class="text-center main-title">Productos Asociados</h3>
                    <table class="table" border="1">
                        <tr>
                            <th class="table-header">Nombre del Producto</th>
                            <th class="table-header">Cantidad Nueva</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("nombre_producto") %></td>
                        <td><%# Eval("cantidad_nueva") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>

            </asp:Repeater>
        </div>

        <!-- Botones Guardar, Actualizar, Limpiar -->

        <div class="d-flex flex-column flex-md-row gap-2 mt-3">
            <asp:Button ID="btnRedirigir" runat="server" Text="Regresar" CssClass="btn" OnClick="btnRedirigir_Click" />
        </div>

    </div>
</asp:Content>
