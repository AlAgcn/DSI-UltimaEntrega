using System;
using System.Collections.Generic;
using System.Data;


namespace PresentacionFinal
{
    public class ConstructorReportePDF:IConstructor
    {
        VistaPDF vistaPDF;

        public int CalcularNumeroDePag()
        {
            throw new NotImplementedException();
        }

        public void ConstruirCuerpo(string[] estados, string[] sectores, DataTable calculoReporte)
        {
            List<int> valores;
            int max, min, prom;
            vistaPDF.IniciarFila();

            for (int i=0; i < sectores.Length; i++)
            {

                for (int k = 0; k < estados.Length; k++)
                {
                    valores = new List<int>();
                    prom = 0;

                    for (int j = 0; j < calculoReporte.Rows.Count; j++)
                    {
                        if (calculoReporte.Rows[j][5].ToString() == estados[k] && calculoReporte.Rows[j][2].ToString() == sectores[i])
                        {
                            var valor = int.Parse(calculoReporte.Rows[j][4].ToString());
                            valores.Add(valor);
                            prom = prom + valor;
                        }


                    }
                    if (valores.Count > 0)
                    {
                        valores.Sort();
                        min = valores[0];
                        max = valores[valores.Count - 1];
                        prom = prom / valores.Count;
                        vistaPDF.AgregarFila(sectores[i], estados[k], max, min, prom);
                    }
                }

            }
        }

        public void ConstruirEncabezado(string titulo, DateTime fechaDesde, DateTime fechaHasta)
        {
            //vistaPDF.reporteEncabezado = titulo + fechaDesde.ToString() + fechaHasta.ToString();
            vistaPDF.AgregarEncabezado(titulo, fechaDesde, fechaHasta);
        }

        public void ConstruirPieDePagina(string usuario, DateTime fechaHoraActual)
        {
             vistaPDF.SetPiePagina(usuario, fechaHoraActual);

        }

        public void ConstruirProducto()
        {
            vistaPDF = new VistaPDF();
        }

        public IFormasVisualizacion ObtenerProducto()
        {
            return vistaPDF;
        }
    }
}
