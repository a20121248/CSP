using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using CSP.Model;

namespace CSP.View
{
    internal partial class FormPedidos : Form
    {
        public List<Rectangulo> listaPiezas;
        public List<Rectangulo> listaStocks;
        public float factorImagen;

        public FormPedidos()
        {
            InitializeComponent();
            factorImagen = 2.5f;
            listaPiezas = new List<Rectangulo>();
            listaStocks = new List<Rectangulo>();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarArchPiezas(txtRutaArchPedidos.Text);
            CargarArchStock(txtRutaArchStock.Text);
        }

        private void btnRutaArchPedidos_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de Excel|*.xls;*.xlsx;*.xlsm",
                FilterIndex = 1,
                Multiselect = false
            };

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtRutaArchPedidos.Text = openFileDialog.FileName;
            }
        }

        private void btnRutaArchStock_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de Excel|*.xls;*.xlsx;*.xlsm",
                FilterIndex = 1,
                Multiselect = false
            };

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtRutaArchStock.Text = openFileDialog.FileName;
            }
        }

        private void CargarArchPiezas(String rutaArch)
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(rutaArch);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //Borro la lista de piezas antes de leer nuevamente
            listaPiezas.Clear();
            // Recordar que en Excel empieza con 1
            for (int i = 1; i <= rowCount; ++i)
            {
                //La cabecera del excel
                if (i == 1)
                {
                    continue;
                }

                // Leer una pieza
                float ancho = float.Parse(xlRange.Cells[i, 2].Value2.ToString()) * factorImagen;
                float alto = float.Parse(xlRange.Cells[i, 3].Value2.ToString()) * factorImagen;
                Rectangulo pieza = new Rectangulo(0, 0, ancho, alto);

                // Lo agrego a la lista
                listaPiezas.Add(pieza);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private void CargarArchStock(String rutaArch)
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(rutaArch);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //Borro la lista de piezas antes de leer nuevamente
            listaStocks.Clear();
            // Recordar que en Excel empieza con 1
            for (int i = 1; i <= rowCount; ++i)
            {
                //La cabecera del excel
                if (i == 1)
                {
                    continue;
                }

                // Leer una pieza
                float ancho = float.Parse(xlRange.Cells[i, 2].Value2.ToString()) * factorImagen;
                float alto = float.Parse(xlRange.Cells[i, 3].Value2.ToString()) * factorImagen;
                Rectangulo pieza = new Rectangulo(0, 0, ancho, alto);

                // Lo agrego a la lista
                listaStocks.Add(pieza);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private void btnGenetico_Click(object sender, EventArgs e)
        {
            FormGenetico formGenetico = new FormGenetico(this);
            formGenetico.Show();
        }

        private void btnCuckooSearch_Click(object sender, EventArgs e)
        {
            FormCuckooSearch formCuckooSearch = new FormCuckooSearch();
            formCuckooSearch.Show();
        }
    }
}
