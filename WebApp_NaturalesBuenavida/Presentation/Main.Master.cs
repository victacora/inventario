using Model;
using System;
using System.Text.Json;
using System.Web;
using System.Web.Security;

namespace Presentation
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] != null ? Session["Usuario"] as Usuario : getUsuarioFromCookie();
            if (usuario != null)
            {
                lblUser.Text = usuario.Nombres + " " + usuario.Apellidos;
            }
        }

        private Usuario getUsuarioFromCookie()
        {
            Usuario usuario = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket != null)
                {
                    usuario = JsonSerializer.Deserialize<Usuario>(ticket.UserData);
                }
            }
            return usuario;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx", true);
        }

        public string IsCurrentPage(string page)
        {
            return Request.RawUrl.Contains(page) ? "active" : string.Empty;
        }

        public string checkPrivilegios(Privilegios privilegio)
        {
            Usuario usuario = Session["Usuario"] != null ? Session["Usuario"] as Usuario : getUsuarioFromCookie();
            if (usuario == null)
            {
                return "d-none";
            }
            else
            {
                return usuario.Privilegios != null && usuario.Privilegios.Contains(((int)privilegio).ToString()) ? string.Empty : "d-none";
            }
        }

        public string checkMenu(Menu menu)
        {
            Usuario usuario = Session["Usuario"] != null ? Session["Usuario"] as Usuario : getUsuarioFromCookie();
            if (usuario == null)
            {
                return "d-none";
            }
            else if (menu.Equals(Menu.Principal))
            {
                return usuario.Privilegios != null && (usuario.Privilegios.Contains(((int)Privilegios.Dashboard).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Productos).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Devoluciones).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Ventas).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Compras).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Inventario).ToString())) ? string.Empty : "d-none";
            }
            else if (menu.Equals(Menu.Configuracion))
            {
                return usuario.Privilegios != null && (usuario.Privilegios.Contains(((int)Privilegios.Roles).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Privilegios).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.TiposDocumentos).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Categorias).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.UnidadDeMedidad).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Presentacion).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Paises).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Departamentos).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Ciudades).ToString())) ? string.Empty : "d-none";
            }
            else if (menu.Equals(Menu.Usuarios))
            {
                return usuario.Privilegios != null && (usuario.Privilegios.Contains(((int)Privilegios.Clientes).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Empleados).ToString()) ||
                    usuario.Privilegios.Contains(((int)Privilegios.Proveedores).ToString())) ? string.Empty : "d-none";
            } else
            {
                return "d-none";
            }
        }
    }
}