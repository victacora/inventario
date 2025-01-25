using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class Dashboard : Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(Privilegios.Dashboard.ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }
    }
}