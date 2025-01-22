<%@ Page Title="Bienvenidos" Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="Presentation.AccessDenied" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Acceso denegado</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <div class="container d-flex justify-content-center align-items-center vh-100">
        <div class="text-center">
            <div class="alert alert-danger" role="alert">
                <h1 class="display-4">Acceso denegado</h1>
                <p class="lead">Usted no tiene permiso para acceder a esta pagina.</p>
            </div>
            <p class="text-muted">Por favor contacte al administrador si usted cree que es un error.</p>
            <a href="Login.aspx" class="btn btn-primary btn-lg mt-3">
                <i class="bi bi-arrow-left me-2"></i>Regresar
            </a>
        </div>
    </div>

    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">
</body>
</html>