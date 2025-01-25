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
                return usuario.Privilegios != null && usuario.Privilegios.Contains(privilegio.ToString()) ? string.Empty : "d-none";
            }
        }
    }
}