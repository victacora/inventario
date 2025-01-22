<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCategories.aspx.cs" Inherits="Presentation.WFCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <h4 class="mb-3">Gestión de categorias</h4>
            <p class="text-muted">Formulario para la gestión de una categoria</p>
        </div>
    </div>
    <hr />
    <div class="alert alert-warning" role="alert">
        <i class="bi bi-exclamation-triangle"></i>Campos marcados son * son obligatorios
    </div>
    <div class="row mb-3">
        <div class="col-md-12">
            <asp:LinkButton ID="BTNNuevoRegistro" runat="server" CssClass="btn btn-success" OnClick="BTNNuevoRegistro_Click" Visible="true">
                <i class="bi bi-plus-circle"></i> Nuevo Registro
            </asp:LinkButton>
        </div>
    </div>
    <div class="row justify-content-center" runat="server" id="div_Cat" visible="true">
        <div class="col-md-10">
            <div class="card border-light mb-3">
                <div class="card-header">
                    <h6 class="card-title" style="text-align: center">LISTADO DE CATEGORIAS REGISTRADAS</h6>
                </div>
                <div class="card-body text-dark">
                    <div class="table-responsive">
                        <asp:GridView ID="gvCategory" runat="server" CssClass="table table-bordered table-striped table-light table-hover table-sm"
                            Font-Size="X-Small" AutoGenerateColumns="false" OnRowCommand="gvCategory_RowCommand" DataKeyNames="id">
                            <Columns>

                                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="EDITAR" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBEdit" runat="server" CommandName="editCategory" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-outline-primary btn-sm">
                                <i class="bi bi-pencil-square"></i> Editar
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ELIMINAR" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBEliminar" runat="server" CommandName="deleteCategory" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-outline-danger btn-sm">
                                <i class="bi bi-trash"></i> Eliminar
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-warning text-center" role="alert">
                                    <h5>No se encontraron personas registradas</h5>
                                </div>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#6d88a6" Font-Bold="true" ForeColor="#495057" HorizontalAlign="Center" />
                            <RowStyle CssClass="gridview-row" />
                        </asp:GridView>

                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="container-fluid" id="div_info" runat="server" visible="false">
        <div class="row mb-3">

            <div class="col-md-3">
                <label>Descripción</label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#990000"></asp:Label>
                <asp:TextBox ID="TBDescripcion" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
            </div>


        </div>

        <%--Botones Guardar y Actualizar--%>
        <div class="row" id="div_botones" runat="server">
            <div class="col-md-12">
                <asp:LinkButton ID="BtnSave" runat="server" CssClass="btn btn-success" OnClick="BtnSave_Click" ValidationGroup="guardarP">
                <i class="fa fa-save"></i>Guardar
                </asp:LinkButton>
                <asp:LinkButton ID="BtnUpdate" runat="server" CssClass="btn btn-primary" OnClick="BtnUpdate_Click" Visible="false">
                <i class="fa fa-edit"></i>Actualizar
                </asp:LinkButton><br />
                <asp:LinkButton ID="LBRegresar" runat="server" CssClass="btn btn-secondary" OnClick="LBRegresar_Click">
                <i class="fa fa-arrow-left"></i>Regresar
                </asp:LinkButton><br />
                <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
            </div>

        </div>
    </div>
    <asp:HiddenField ID="HFCategoryId" runat="server" />
</asp:Content>
