<%@ Page Title="Bienvenidos" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentation.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-light">
        <div class="container d-flex justify-content-center align-items-center vh-100">
            <div class="card shadow-lg" style="width: 400px;">
                <div class="card-body">
                    <h3 class="card-title text-center mb-4">Login</h3>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block text-center mb-3"></asp:Label>
                    <form runat="server">
                        <div class="mb-3">
                            <label for="txtUsername" class="form-label">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Enter your username"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter your password"></asp:TextBox>
                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
