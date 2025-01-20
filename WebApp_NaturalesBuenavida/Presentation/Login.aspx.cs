using Data;
using Logic;
using System;
using System.Web.UI;

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
           
                if (usuario!=null)
                {
                    // Store user information in Session
                    Session["UserID"] = usuario.Usu_Id;
                    Session["RoleID"] = usuario.Rol_Id;
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid username or password.";
                }
            
        }
    }
}