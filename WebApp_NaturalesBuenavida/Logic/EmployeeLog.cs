using System.Data;
using Data;

namespace Logic
{
    public class EmployeeLog
    {
        EmployeeDat objEmp = new EmployeeDat();

        // Lógica para obtener todos los empleados
        public DataSet ShowEmployees()
        {
            return objEmp.ShowEmployees();
        }

        // Lógica para guardar un nuevo empleado
        public bool AddEmployee(int personaId)
        {
            return objEmp.SaveEmployee(personaId);
        }

        // Lógica para actualizar un empleado
        public bool EditEmployee(int empId, int personaId)
        {
            return objEmp.UpdateEmployee(empId, personaId);
        }

        // Lógica para eliminar un empleado
        public bool DeleteEmployee(int empId)
        {
            return objEmp.DeleteEmployee(empId);
        }

        // Lógica para mostrar empleados con DDL
        public DataSet ShowEmployeesDDL()
        {
            return objEmp.ShowEmployeesDDL();
        }
    }
}
