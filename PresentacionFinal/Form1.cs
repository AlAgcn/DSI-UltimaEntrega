using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentacionFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Checkeo de estados
            int cant = chkListEstados.CheckedItems.Count;
            string[] estadosSelected = new string[cant];
            for (int i = 0; i < cant; i++)
            {
                estadosSelected[i] = chkListEstados.CheckedItems[i].ToString();
            }
            /*
            if (chkAbierto.Checked)
                estados.Add("Abierto");
            if (chkCerrado.Checked)
                estados.Add("Cerrado");
            if (chkFacturado.Checked)
                estados.Add("Facturado");
            if (chkCobrado.Checked)
                estados.Add("Cobrado");

            string[] estadosSeleccionados = new string[estados.Count];
            for (int i = 0; i < estados.Count;i++) { estadosSeleccionados[i] = estados[i]; }*/

            //Checkeo de sectores
            int cant2 = chkListSectores.CheckedItems.Count;
            string[] sectoresSelected = new string[cant2];
            for (int i = 0; i < cant2; i++)
            {
                sectoresSelected[i] = chkListSectores.CheckedItems[i].ToString();
            }

            GestorReporteDeTiemposEnPedido gestor = new GestorReporteDeTiemposEnPedido();
            gestor.TomarFiltros(dateTimePicker1.Value, dateTimePicker2.Value, estadosSelected, sectoresSelected);
            gestor.GenerarReporte();

            Cursor.Current = Cursors.Default;
            MessageBox.Show("PDF Creado con exito ;)", "Creacion de PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
