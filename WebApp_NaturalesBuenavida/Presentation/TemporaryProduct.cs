using System;

namespace Presentation
{

    [Serializable]
    public class TemporaryProduct
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double PrecioVenta { get; set; }
        public double PrecioCompra { get; set; }
        public int Medida { get; set; }
        public int FkCategoria { get; set; }
        public int FkProveedor { get; set; }
        public int FkUnidadMedida { get; set; }
        public int FkPresentacion { get; set; }
        public string NumeroLote { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string NombreProveedor { get; set; }
        public string UnidadMedida { get; set; }
        public string Presentacion { get; set; }
        public string Categoria { get; set; }
    }
}