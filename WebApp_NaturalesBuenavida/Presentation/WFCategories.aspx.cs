using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFCategories : System.Web.UI.Page
    {

        CategoryLog objCat = new CategoryLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListCategory();
            }
        }
        protected void ListCategory()
        {
            var dataset = new CategoryLog().ShowCategories();
            if (dataset != null && dataset.Tables.Count > 0)
            {
                gvCategory.DataSource = dataset.Tables[0];
                gvCategory.DataBind();
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (objCat.AddCategory(TBDescripcion.Text))
            {
                LblMsg.Text = "Categoria registrada con éxito.";
                ListCategory();
                div_Cat.Visible = true;
                div_info.Visible = false;
                div_botones.Visible = false;
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                BTNNuevoRegistro.Visible = true;
            }
            else
            {
                LblMsg.Text = "Error al registrar.";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (objCat.EditCategory(int.Parse(HFCategoryId.Value), TBDescripcion.Text))
            {
                LblMsg.Text = "Categoria actualizada.";
                ListCategory();
                div_Cat.Visible = true;
                div_info.Visible = false;
                div_botones.Visible = false;
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                BTNNuevoRegistro.Visible = true;
            }
            else
            {
                LblMsg.Text = "Error al actualizar.";
            }
        }

        protected void LBRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WFCategory.aspx");
        }

        protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "editCategory":
                    try
                    {
                        int indice = Convert.ToInt32(e.CommandArgument);
                        string id = gvCategory.DataKeys[indice].Value.ToString();
                        HFCategoryId.Value = id;
                        var dataset = new CategoryLog().ShowCategoriesId(int.Parse(id));
                        if (dataset != null && dataset.Tables.Count > 0)
                        {
                            DataRow row = dataset.Tables[0].Rows[0];
                            TBDescripcion.Text = row["Descripcion"].ToString();
                        }
                        div_Cat.Visible = false;
                        div_info.Visible = true;
                        div_botones.Visible = true;
                        BtnSave.Visible = false;
                        BtnUpdate.Visible = true;
                        BTNNuevoRegistro.Visible = false;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
                case "deleteCategory":
                    try
                    {
                        int indice = Convert.ToInt32(e.CommandArgument);
                        string id = gvCategory.DataKeys[indice].Value.ToString();
                        CategoryLog depLog = new CategoryLog();
                        bool executed = depLog.DeleteCategory(int.Parse(id));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Notify('Se ha eliminado el país con éxito.','Estado','success','false','true');", true);
                        ListCategory();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
            }
        }

        protected void BTNNuevoRegistro_Click(object sender, EventArgs e)
        {
            div_Cat.Visible = false;
            BTNNuevoRegistro.Visible = false;
            div_info.Visible = true;
        }
    }
}