using Logic;
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
    public partial class WFClient : System.Web.UI.Page
    {
        ClientLog objClient = new ClientLog();
        PersonLog objPerson = new PersonLog();

        private int _id;
        private bool executed = false;
        private int _fkPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { 
                showPersonDDL();
            }
        }

        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListClients()
        {
            ClientLog objClient = new ClientLog();

            // Se obtiene un DataSet que contiene la lista de clientes desde la base de datos.
            var dataSet = objClient.showClient();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var clientsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un cliente).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                clientsList.Add(new
                {
                    ClienteID = row["ClienteID"],
                    Identificacion = row["Identificacion"],
                    Nombre = row["Nombre"],
                    Apellido = row["Apellido"],
                    Telefono = row["Telefono"],
                    Email = row["Email"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = clientsList };
        }

        [WebMethod]
        public static bool DeleteClient(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            ClientLog objClient = new ClientLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objClient.deleteClient(id);
        }


        //private void showClient()
        //{
        //    DataSet objData = new DataSet();
        //    objData = objClient.showClient();
        //    GVClient.DataSource = objData;
        //    GVClient.DataBind();
        //}

        //Metodo para mostrar las personas en el DDL que está en cliente
        private void showPersonDDL()
        {
            DDLPerson.DataSource = objPerson.ShowPersonasDDL();
            DDLPerson.DataValueField = "Id";//Nombre de la llave primaria
            DDLPerson.DataTextField = "NombreCompleto";
            DDLPerson.DataBind();
            DDLPerson.Items.Insert(0, "---- Seleccione una persona ----");
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFClientID.Value = "";
            DDLPerson.SelectedIndex = 0;
            TBIdPersona.Text = "";
            TBNamePerson.Text = "";
            TBLastNamePerson.Text = "";
            TBPhonePerson.Text = "";
            TBEmailPerson.Text = "";
            LblMsg.Text = "";
        }

        protected void GVClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GVClient_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //_fkPerson = Convert.ToInt32(DDLPerson.SelectedValue);

            //executed = objClient.saveClient(_fkPerson);

            //if (executed)
            //{
            //    LblMsg.Text = "El producto se guardo exitosamente!";
            //    clear();//Se invoca el metodo para limpiar los campos 
            //}
            //else
            //{
            //    LblMsg.Text = "Error al guardar";
            //}

            int personId = Convert.ToInt32(DDLPerson.SelectedValue); // Asume que tienes un DropDownList de personas

            bool isSaved = objClient.saveClient(personId);

            if (isSaved)
            {
                LblMsg.Text = "Cliente guardado exitosamente.";
                clear();
            }
            else
            {
                LblMsg.Text = "Esta persona ya está registrada como cliente.";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFClientID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un cliente para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFClientID.Value);
            _fkPerson = Convert.ToInt32(DDLPerson.SelectedValue);

            executed = objClient.updateClient(_id, _fkPerson);

            if (executed)
            {
                LblMsg.Text = "El cliente se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void BtbClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        [WebMethod]
        public static object GetPersonData(int personId)
        {
            PersonLog objPerson = new PersonLog();
            DataRow personData = objPerson.GetPersonById(personId).Tables[0].Rows[0];

            var person = new
            {
                Identificacion = personData["Identificacion"].ToString(),
                Nombre = personData["Nombre"].ToString(),
                Apellido = personData["Apellido"].ToString(),
                Telefono = personData["Telefono"].ToString(),
                Email = personData["Email"].ToString()
            };

            return person;
        }
    }
}