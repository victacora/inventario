using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class Dashboard : Page
    {
        private static DashBoardLog dashBoardLog = new DashBoardLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Dashboard).ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }


        [WebMethod]
        public static AjaxResponse loadDashboardData(string fechaInicial, string fechaFinal)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                response.Result = dashBoardLog.getDashboardData(fechaInicial, fechaFinal);
                response.Success = true;
            }
            catch (Exception ex)// En caso de error, configuro la respuesta con el mensaje de error.
            {
                response.Success = false;
                response.Message = "Ocurrió un error: " + ex.Message;
            }

            return response;
        }
    }
}