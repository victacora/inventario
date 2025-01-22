using System;
using System.Data;
using Data;

namespace Logic
{
    public class CategoryLog
    {
        CategoryDat objCat = new CategoryDat();

        // Lógica para obtener todas las categorías
        public DataSet ShowCategories()
        {
            return objCat.ShowCategories();
        }

        // Lógica para crear una nueva categoría
        public bool AddCategory(string descripcion)
        {
            return objCat.CreateCategory(descripcion);
        }

        // Lógica para actualizar una categoría
        public bool EditCategory(int catId, string descripcion)
        {
            return objCat.UpdateCategory(catId, descripcion);
        }

        // Lógica para eliminar una categoría
        public bool DeleteCategory(int catId)
        {
            return objCat.DeleteCategory(catId);
        }

        // Lógica para obtener las categorías en formato DDL
        public DataSet ShowCategoriesDDL()
        {
            return objCat.ShowCategoriesDDL();
        }

        public DataSet ShowCategoriesId(int id)
        {
            return objCat.ShowCategoriesId(id);
        }
    }
}