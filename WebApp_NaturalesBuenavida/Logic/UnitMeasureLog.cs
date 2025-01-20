using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class UnitMeasureLog
    {
        UnitMeasureDat objUnitMeasure = new UnitMeasureDat();

        // Método para mostrar todas las unidades de medida
        public DataSet ShowUnits()
        {
            return objUnitMeasure.ShowUnits();
        }
        // Método para mostrar las unidades de medida en un DropDownList
        public DataSet ShowDDLUnitMeasure()
        {
            return objUnitMeasure.ShowDDLUnitMeasure();
        }

        // Método para guardar una nueva unidad de medida
        public bool SaveUnit(string descripcion)
        {
            return objUnitMeasure.SaveUnit(descripcion);
        }

        // Método para actualizar una unidad de medida
        public bool UpdateUnit(int undId, string descripcion)
        {
            return objUnitMeasure.UpdateUnit(undId, descripcion);
        }

        // Método para eliminar una unidad de medida
        public bool DeleteUnit(int undId)
        {
            return objUnitMeasure.DeleteUnit(undId);
        }
    }
}