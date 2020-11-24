using System;
using System.Data;


namespace PresentacionFinal
{
    public interface IConstructor
    {
        public abstract int CalcularNumeroDePag();
        public abstract void ConstruirCuerpo(string[] estados, string[] sectores, DataTable calculoReporte);
        public abstract void ConstruirEncabezado(string titulo, DateTime fechaDesde, DateTime fechaHasta);

        public abstract void ConstruirPieDePagina(string usuario, DateTime fechaHoraActual);

        public abstract void ConstruirProducto();

        public abstract IFormasVisualizacion ObtenerProducto();
    }
}
