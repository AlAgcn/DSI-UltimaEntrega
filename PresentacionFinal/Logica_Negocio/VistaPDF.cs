using System;
using System.Collections.Generic;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PresentacionFinal
{
    public class VistaPDF:IFormasVisualizacion
    {
        public string[][] reporteCuerpo { get; set;}
        public Dictionary<string, object> cuerpoReporteAux;
        public string reporteEncabezado { get; set; }
        public string reportePie { get; set; }

        PdfWriter writer;
        PdfDocument pdf;
        Document document;
        Table table;
        public VistaPDF() 
        {
            writer = new PdfWriter("C:\\Users\\Ali CN\\Desktop\\demo.pdf");
            pdf = new PdfDocument(writer);
            document = new Document(pdf);
        }
        public void IniciarFila()
        {// Linea Separadora
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            table = new Table(5, false);
            Cell cell11 = new Cell()
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Estado"));
            Cell cell12 = new Cell()
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Sector"));
            Cell cell13 = new Cell()
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("TiempoMax"));
            Cell cell14 = new Cell()
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("TiempoMin"));
            Cell cell15 = new Cell()
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("TiempoProm"));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell13);
            table.AddCell(cell14);
            table.AddCell(cell15);
            //document.Add(table);
        }
        public void AgregarEncabezado(string titulo, DateTime fechaDesde, DateTime fechaHasta)
        {
            Paragraph header = new Paragraph(titulo + " " + fechaDesde.ToShortDateString() +  " - "+ fechaHasta.ToShortDateString())
                                .SetTextAlignment(TextAlignment.JUSTIFIED)
                                .SetFontSize(20);
            document.Add(header);
        }

        public void AgregarFila(string sectores, string estados, Object calculosMax, Object calculosMin, Object calculosProm)
        {


            //table = new Table(5, false);
            Cell cellx1 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(estados));
            Cell cellx2 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(sectores));
            Cell cellx3 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(calculosMax.ToString()));
            Cell cellx4 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(calculosMin.ToString()));
            Cell cellx5 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(calculosProm.ToString()));
            table.AddCell(cellx1); table.AddCell(cellx2); table.AddCell(cellx3); table.AddCell(cellx4); table.AddCell(cellx5);

            //document.Add(table);
        }


        public List<Cell> SetFila(string sectores, string estados, Object calculosMax, Object calculosMin, Object calculosProm)
        {
            //reporteCuerpo.Aggregate(sectores, estados, calculosReporte);
            //reporteCuerpo.Append(sectores, estados, calculosReporte);
            List<Cell> fila = new List<Cell>();
            table = new Table(5, false);
            Cell cellx1 = new Cell(1,1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(estados));
            Cell cellx2 = new Cell(1,1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(sectores));
            Cell cellx3 = new Cell(1,1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(calculosMax.ToString()));
            Cell cellx4 = new Cell(1,1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(calculosMin.ToString()));
            Cell cellx5 = new Cell(1,1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(calculosProm.ToString()));
            table.AddCell(cellx1); table.AddCell(cellx2); table.AddCell(cellx3); table.AddCell(cellx4); table.AddCell(cellx5);
            return fila;
        }
        public void SetFirmaPagina(string usuario, DateTime fechaHoraActual)
        {
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(usuario + " " + fechaHoraActual.ToString())),
                   0, 0, i, TextAlignment.LEFT,
                   VerticalAlignment.BOTTOM, 0);

            }
        }

        public void SetNumeroPagina() 
        {
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format("page" + i + " of " + n)),
                   559, 0, i, TextAlignment.RIGHT,
                   VerticalAlignment.BOTTOM, 0);
            }
        }
        public void SetPiePagina(string usuario, DateTime fechaHoraActual)
        {
            document.Add(table);
            SetFirmaPagina(usuario, fechaHoraActual);
            SetNumeroPagina();
            pdf.Close();
        }


        Object IFormasVisualizacion.VisualizarReporteGenerado()
        {
            return pdf;
        }
    }
}
