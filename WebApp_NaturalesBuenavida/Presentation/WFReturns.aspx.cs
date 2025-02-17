﻿using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFReturns : System.Web.UI.Page
    {
        // Instancia de los objetos que contienen la lógica de negocio
        private static ReturnLog objReturn = new ReturnLog();
        private static SalesLog objSale = new SalesLog();
        // Variables privadas para almacenar datos
        private int _id, _fkSale;
        private string _reason;
        private DateTime _returnDate;
        private bool executed = false;

        // Método que se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Cargar ventas
                ShowSalesDDL();
                // Establecer la fecha actual
                TBReturnDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                // Esto asegura que solo se carguen los valores de cliente y empleado al hacer clic en editar
                if (!string.IsNullOrEmpty(HFReturnID.Value))
                {
                    // Los valores de cliente y empleado no deben ser sobrescritos si ya están establecidos.
                    // Aquí podrías establecer valores adicionales si es necesario.
                }
            }
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null || usuario.Privilegios != null && !usuario.Privilegios.Contains(((int)Privilegios.Devoluciones).ToString()))
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }


        // Método Web que devuelve todas las devoluciones registradas en formato JSON
        [WebMethod]
        public static object ListReturns()
        {
            // Obtiene las devoluciones desde la base de datos
            var dataSet = objReturn.ShowReturns();
            var returnsList = new List<object>();

            // Itera sobre las filas del DataSet y las agrega a la lista
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                returnsList.Add(new
                {
                    DevolucionID = row["dev_id"],
                    FechaDevolucion = Convert.ToDateTime(row["dev_fecha_devolucion"]).ToString("yyyy-MM-dd"),
                    Motivo = row["dev_motivo"],
                    VentaID = row["vent_id"],
                    FechaVenta = Convert.ToDateTime(row["vent_fecha"]).ToString("yyyy-MM-dd"),
                    DescripcionVenta = row["vent_descripcion"],
                    IdentificacionEmpleado = row["identificacion_empleado"],  // Empleado ID
                    NombreEmpleado = row["nombre_empleado"],  // Empleado Nombre
                    IdentificacionCliente = row["identificacion_cliente"],  // Cliente ID
                    NombreCliente = row["nombre_cliente"],  // Cliente Nombre
                });
            }

            return new { data = returnsList };
        }


        // Método Web para eliminar una devolución
        [WebMethod]
        public static AjaxResponse DeleteReturn(int returnId)
        {
            AjaxResponse response = new AjaxResponse();
            try
            {
                // Creo un objeto de respuesta para devolver al cliente.
                bool executed = objReturn.DeleteReturn(returnId);

                if (executed) // Verifico si la eliminación fue exitosa
                {
                    response.Success = true;
                    response.Message = "Devolución eliminada correctamente.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error al eliminar la eliminacion";
                }
            }
            catch (Exception ex)// En caso de error, configuro la respuesta con el mensaje de error.
            {
                response.Success = false;
                response.Message = "Ocurrió un error: " + ex.Message;
            }

            return response;
        }

        // Método que se ejecuta cuando se hace clic en el botón de guardar
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(TBReturnDate.Text, out DateTime parsedDate))
            {
                LblMsg.Text = "Formato de fecha inválido";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            // Asigna valores a las variables privadas
            _returnDate = parsedDate;
            _reason = TBMotivo.Text;
            _fkSale = !DDLVentas.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(DDLVentas.SelectedValue) : 0;

            // Guarda la devolución y muestra un mensaje adecuado
            executed = objReturn.SaveReturn(_returnDate, _reason, _fkSale);
            LblMsg.Text = executed ? "Devolución registrada correctamente." : "Error al registrar la devolución.";
            LblMsg.CssClass = executed ? "text-success fw-bold" : "text-danger fw-bold";
            if (executed) ClearFields();
        }

        // Método que se ejecuta cuando se hace clic en el botón de actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFReturnID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una devolución para actualizar.";
                LblMsg.CssClass = "text-danger fw-bold";
                return;
            }

            // Asigna valores a las variables privadas
            _id = Convert.ToInt32(HFReturnID.Value);
            _returnDate = DateTime.Parse(TBReturnDate.Text);
            _reason = TBMotivo.Text;
            _fkSale = !DDLVentas.SelectedValue.Equals(string.Empty) ? Convert.ToInt32(DDLVentas.SelectedValue) : 0;

            // Actualiza la devolución y muestra un mensaje adecuado
            executed = objReturn.UpdateReturn(_id, _returnDate, _reason, _fkSale);
            LblMsg.Text = executed ? "Devolución actualizada correctamente." : "Error al actualizar la devolución.";
            LblMsg.CssClass = executed ? "text-success fw-bold" : "text-danger fw-bold";
            if (executed) ClearFields();
        }

        // Método que se ejecuta cuando se hace clic en el botón de limpiar
        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        // Método para cargar las ventas en el dropdown
        private void ShowSalesDDL()
        {
            DDLVentas.DataSource = objSale.ShowDDLSales();
            DDLVentas.DataValueField = "id"; // Establece el valor de cada opción (ID de la venta)
            DDLVentas.DataTextField = "descripcion"; // Establece el texto de cada opción (Descripción de la venta)
            DDLVentas.DataBind();
            DDLVentas.Items.Insert(0, new ListItem("---- Seleccione una venta ----", "")); // Añade la opción por defecto
        }

        // Método para limpiar los campos del formulario
        private void ClearFields()
        {
            HFReturnID.Value = string.Empty;
            TBReturnDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBMotivo.Text = string.Empty;
            DDLVentas.SelectedIndex = 0;
        }
    }
}
