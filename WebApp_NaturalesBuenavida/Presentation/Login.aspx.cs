using Model;
using Logic;
using System;
using System.Web.Security;
using System.Web;
using System.Web.UI;
using System.Data;

namespace Presentation
{
    public partial class Login : Page
    {
        UserLog objUser = new UserLog();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = objUser.LoginUser(txtUsername.Text, txtPassword.Text);

            if (usuario != null && usuario.Rol_Id > 0)
            {
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket(1, txtUsername.Text, DateTime.Now,
                DateTime.Now.AddMinutes(30), chkPersistCookie.Checked, usuario.Rol);
                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                if (chkPersistCookie.Checked)
                    ck.Expires = tkt.Expiration;
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);

                string strRedirect;
                strRedirect = Request["ReturnUrl"];
                if (strRedirect == null)
                    strRedirect = "dashboard.aspx";
                Response.Redirect(strRedirect, true);
            }
            else
            {
                lblMessage.Text = "Usuario o contraseña inválida.";
            }
            
        }
    }
}