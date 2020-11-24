using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PresentacionFinal
{
    public class GestorReporteDeTiemposEnPedido
    {
        Estado[] estados { get; set; }
        Estado[] estadosSeleccionados { get; set; }
        string[] estadosSelec { get; set; }
        DateTime fechaHoraActual { get; set; }
        DateTime fechaHoraDesde { get; set; }
        DateTime fechaHoraHasta { get; set; }
        public string nombreUsuarioLog { get; set; }
        public string titulo { get; set; }
        bool opcTotalizar { get; set; }
        Sector[] sectores { get; set; }
        Sector[] sectoresSeleccionados { get; set; }
        string[] sectoresSelecc { get; set; }
        int[] tiemposPerMax { get; set; }
        int[] tiemposPerMin { get; set; }
        int[] tiemposPerProm { get; set; }
        Piso[] pisos { get; set; }
        Piso[] pisosSeleccionados { get; set; }
        string[] pisosSelecc { get; set; }

        Pedido[] pedidos { get; set; }
        enum FormasVis { pdf, porPantalla, excel }
        FormasVis formaVis;

        DataTable rtdos;
        public void TomarFormaVis(IConstructor constructor)
        {
            formaVis = FormasVis.pdf;
            fechaHoraActual = DateTime.Now;
            
        }

        private void HardCore()
        {
            titulo = "Reporte de Tiempos en pedidos";
            opcTotalizar = true;
            nombreUsuarioLog = "Lucas";
            fechaHoraActual = DateTime.Now;

            string consulta = "SELECT p.id AS Pedido, p.fechaHoraPedido, s.Nombre AS Sector, m.numero AS MesaN, DATEDIFF('n', h.fechaHoraInicio, h.fechaHoraFin) AS 'Permanencia en Minutos', e.Nombre AS Estado " +
                                "FROM(((Pedido p INNER JOIN " +
                                "Mesa m ON(p.id_mesa = m.id)) " +
                                "INNER JOIN Sector s ON(m.id_seccion = s.id) ) " +
                                "INNER JOIN HistorialEstado h ON(h.id_pedido = p.id) ) " +
                                "INNER JOIN Estado e ON(e.id = h.estado) " +
                                "WHERE p.fechaHoraPedido BETWEEN @desde AND @hasta ";
            var parametros = new Dictionary<string, object>();
            parametros.Add("desde", fechaHoraDesde.Date);
            parametros.Add("hasta", fechaHoraHasta.Date);

            rtdos = new DataTable();
            rtdos = BDHelper.Instance.ConsultarSQL(consulta, parametros);

        }
        public Object GenerarReporte()
        {
            HardCore();


            IConstructor constructor = new ConstructorReportePDF();
            Director director = new Director(constructor);
            director.Construir(titulo, fechaHoraDesde, fechaHoraHasta, estadosSelec, pisosSelecc, sectoresSelecc,
                opcTotalizar, pedidos, rtdos, nombreUsuarioLog, fechaHoraActual);

            var formaVisualizacion = constructor.ObtenerProducto();
            var pdf = formaVisualizacion.VisualizarReporteGenerado();
            return pdf;
        }

        public void TomarFiltros(DateTime fechaIni, DateTime fechaFin, string[] estados, string[] sectores)
        {
            fechaHoraDesde = fechaIni;
            fechaHoraHasta = fechaFin;

            estadosSelec = estados;
            sectoresSelecc = sectores;
        }

    }
}
