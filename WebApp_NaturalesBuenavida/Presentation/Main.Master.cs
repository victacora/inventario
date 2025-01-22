using System;
using System.Web;
using System.Web.Security;

namespace Presentation
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        public string Is(string page)
        {
            return Request.RawUrl.Contains(page) ? "active" : string.Empty;
        }
    }
}