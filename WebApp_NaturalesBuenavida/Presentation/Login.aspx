<%@ Page Title="Bienvenidos" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentation.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolderLogin" runat="server">
    <link href="resources/css/login.css" rel="stylesheet" />
    <div class="d-flex flex-column min-vh-100">
        <div class="background"></div>
        <div class="content my-auto d-flex flex-column flex-md-row justify-content-center align-items-center">
            <!-- Logo and Welcome Section -->
            <div class="text-center text-white m-4">
                <h2>Bienvenido a</h2>
                <img src="resources/image/logo-naturales-buena-vida.png" alt="Logo Naturales BuenaVida" class="login-logo">
                <h3 class="fw-bold">Naturales Buenavida S.A.S.</h3>
                <p class="fst-italic text-center">Naturales Buenavida,<br />
                    mientras comes alargas tu vida.</p>
            </div>

            <!-- Login Form -->
            <div class="login-container col-md-4">
                <h4 class="text-center mb-4">Inicio de sesión</h4>
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block text-center mb-3"></asp:Label>
                <div class="mb-3">
                    <label for="txtUsername" class="form-label">Nombre usuario</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Usuario"></asp:TextBox>
                    <!-- <a href="#" class="small d-block mt-1">¿Olvidaste tu usuario?</a> -->
                </div>
                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>
                    <!--<a href="#" class="small d-block mt-1">¿Olvidaste tu contraseña?</a> -->
                </div>
                <div class="mb-3 form-check justify-content-end">
                    <label for="chkPersistCookie" class="form-check-label">Mantener la sesión activa</label>
                    <input type="checkbox" id="chkPersistCookie" class="form-check-input" runat="server" />
                </div>
                <div class="d-grid justify-content-center">
                    <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-success" OnClick="btnLogin_Click" />
                </div>
            </div>
        </div>
        <!-- Footer -->
        <footer class="content bg-dark text-white text-center py-3 mt-5 text-break">
            Contacto: 300 111 22 33 | Email: naturalesbuenavidasas@gmail.com
        
        </footer>
    </div>
</asp:Content>
