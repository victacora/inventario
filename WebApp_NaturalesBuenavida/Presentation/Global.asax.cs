using System;
using System.Web;
using System.Web.Security;

namespace Presentation
{
    public class Global : HttpApplication
    {
        protected void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    try
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        string[] roles = ticket.UserData.Split(',');
                        e.User = new System.Security.Principal.GenericPrincipal(
                            new System.Security.Principal.GenericIdentity(ticket.Name), roles);
                    }
                    catch (Exception)
                    {
                        FormsAuthentication.SignOut();
                        Response.Redirect(FormsAuthentication.LoginUrl);
                    }
                }
            }
            else
            {
                throw new HttpException("No existe una cookie asociada a la peticion");
            }
        }
    }
}