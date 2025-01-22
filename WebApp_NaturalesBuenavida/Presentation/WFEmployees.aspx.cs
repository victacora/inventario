using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFEmployee : System.Web.UI.Page
    {
        EmployeeLog objEmployee = new EmployeeLog();
        PersonLog objPerson = new PersonLog();

        private int _id;
        private bool executed = false;
        private int _fkPerson;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showPersonDDL();
            }
        }

        // WebMethod para listar los empleados
        [WebMethod]
        public static object ListEmployees()
        {
            EmployeeLog objEmployee = new EmployeeLog();

            // Obtener el DataSet con los datos de los empleados desde la base de datos
            var dataSet = objEmployee.ShowEmployees();

            // Crear una lista de objetos para almacenar los empleados que se devolverán
            var employeesList = new List<object>();

            // Iterar sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                employeesList.Add(new
                {
                    EmployeeID = row["EmpleadoID"],
                    Identification = row["Identificacion"],
                    FirstName = row["Nombre"],
                    LastName = row["Apellido"],
                    Phone = row["Telefono"],
                    Email = row["Correo"]
                });
            }

            // Devolver los datos en formato JSON
            return new { data = employeesList };
        }

        // WebMethod para eliminar un empleado
        [WebMethod]
        public static bool DeleteEmployee(int id)
        {
            EmployeeLog objEmployee = new EmployeeLog();

            // Llamar al método para eliminar el empleado
            return objEmployee.DeleteEmployee(id);
        }

        // Método para mostrar las personas en el DDL
        private void showPersonDDL()
        {
            DDLPerson.DataSource = objPerson.ShowPersonasDDL(); // Obtener las personas
            DDLPerson.DataValueField = "Id"; // Id de la persona
            DDLPerson.DataTextField = "NombreCompleto"; // Nombre completo de la persona
            DDLPerson.DataBind();
            DDLPerson.Items.Insert(0, "---- Seleccione una persona ----");
        }

        // Método para limpiar los campos
        private void clear()
        {
            HFEmployeeID.Value = ""; // Limpiar el campo oculto
            DDLPerson.SelectedIndex = 0; // Limpiar el DDL
            TBEmployeeId.Text = ""; // Limpiar los campos de texto
            TBEmployeeName.Text = "";
            TBEmployeeLastName.Text = "";
            TBEmployeePhone.Text = "";
            TBEmployeeEmail.Text = "";
            LblMsg.Text = ""; // Limpiar el mensaje
        }

        // Método para guardar un nuevo empleado
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkPerson = Convert.ToInt32(DDLPerson.SelectedValue); // Obtener el id de la persona seleccionada

            bool isSaved = objEmployee.AddEmployee(_fkPerson);

            if (isSaved)
            {
                LblMsg.Text = "Empleado guardado exitosamente.";
                clear(); // Limpiar los campos
            }
            else
            {
                LblMsg.Text = "Este empleado ya está registrado.";
            }
        }

        // Método para actualizar un empleado
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un empleado para actualizar
            if (string.IsNullOrEmpty(HFEmployeeID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un empleado para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFEmployeeID.Value);
            _fkPerson = Convert.ToInt32(DDLPerson.SelectedValue);

            bool isUpdated = objEmployee.EditEmployee(_id, _fkPerson);

            if (isUpdated)
            {
                LblMsg.Text = "Empleado actualizado exitosamente!";
                clear(); // Limpiar los campos
            }
            else
            {
                LblMsg.Text = "Error al actualizar el empleado.";
            }
        }

        // Método para limpiar los campos
        protected void BtbClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        // WebMethod para obtener los datos de la persona seleccionada
        [WebMethod]
        public static object GetPersonData(int personId)
        {
            PersonLog objPerson = new PersonLog();
            DataRow personData = objPerson.GetPersonById(personId).Tables[0].Rows[0];

            var person = new
            {
                Identification = personData["Identification"].ToString(),
                FirstName = personData["FirstName"].ToString(),
                LastName = personData["LastName"].ToString(),
                Phone = personData["Phone"].ToString(),
                Email = personData["Email"].ToString()
            };

            return person;
        }
    }
}