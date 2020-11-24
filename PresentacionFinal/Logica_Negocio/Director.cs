using System;
using System.Data;


namespace PresentacionFinal
{
    public class Director
    {
        IConstructor constructor;
        public void Construir(string titulo, DateTime fechaDesde, DateTime fechaHasta, 
            string[] estados, string[] pisos, string[] sectores, bool totalizar,
            Pedido[] pedidos, DataTable calculosReporte, string usuario,
            DateTime fechaHoraActual)
        {
            constructor.ConstruirProducto();
            constructor.ConstruirEncabezado(titulo, fechaDesde, fechaHasta);
            constructor.ConstruirCuerpo(estados, sectores, calculosReporte);
            constructor.ConstruirPieDePagina(usuario, fechaHoraActual);

        }

        public Director(IConstructor miconstructor)
        {
            constructor = miconstructor;
        }

        
    }
}
