<%@ Page Title="Bienvenidos" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentation.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolderLogin" runat="server">
    <div class="d-flex flex-column min-vh-100">
        <!-- Login Form -->
        <div class="row flex-grow-1 justify-content-center align-items-center">
            <div class="col-12 col-md-4">
                <div class="card shadow-lg">
                    <div class="card-body p-4">
                        <h3 class="card-title text-center mb-4">Bienvenidos</h3>
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block text-center mb-3"></asp:Label>
                        <div class="mb-3">
                            <label for="txtUsername" class="form-label">Usuario</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Ingrese su usuario"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Ingrese su contraseña"></asp:TextBox>
                        </div>
                        <div class="mb-3 form-check justify-content-end">
                            <label for="chkPersistCookie" class="form-check-label">Mantener la sesión activa</label>
                            <asp:CheckBox ID="chkPersistCookie" runat="server" CssClass="form-check-input" AutoPostBack="false" />
                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Footer Section -->
        <footer class="bg-dark text-white text-center py-3 mt-auto">
            <p>&copy; Naturales buena vida - 2025.</p>
        </footer>
    </div>
</asp:Content>
