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
        public List<Stock> listaStocks;
        public float factorImagen;

        private Excel.Application xlApp;
        private Excel.Workbook xlWorkbook;
        private Excel._Worksheet xlWorksheet;
        private Excel.Range xlRange;
        private int rowCount;
        private int colCount;

        public FormPedidos()
        {
            InitializeComponent();
            factorImagen = 2.5f;
            listaPiezas = new List<Rectangulo>();
            listaStocks = new List<Stock>();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarArchStock(txtRutaArchStock.Text);
            CargarArchPiezas(txtRutaArchPedidos.Text);            

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
            AbrirArchExcel(rutaArch);

            LeerHojaExcel("Piezas");
            listaPiezas.Clear();
            for (int i = 1; i <= rowCount; ++i)
            {
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
            CerrarArchExcel();
        }

        private void CargarArchStock(String rutaArch)
        {
            AbrirArchExcel(rutaArch);

            LeerHojaExcel("Stocks");
            listaStocks.Clear();
            for (int i = 1; i <= rowCount; ++i)
            {
                if (i == 1)
                {
                    continue;
                }
                // Leer un stock
                int id = int.Parse(xlRange.Cells[i, 1].Value2.ToString());
                float ancho = float.Parse(xlRange.Cells[i, 2].Value2.ToString()) * factorImagen;
                float alto = float.Parse(xlRange.Cells[i, 3].Value2.ToString()) * factorImagen;
                Stock stock = new Stock(id, ancho, alto);
                // Lo agrego a la lista
                listaStocks.Add(stock);
            }

            LeerHojaExcel("Defectos");
            for (int i = 1; i <= rowCount; ++i)
            {
                if (i == 1)
                {
                    continue;
                }
                // Leer un stock
                int stock_id = int.Parse(xlRange.Cells[i, 1].Value2.ToString());
                Stock stock = listaStocks.Find(obj => obj.Id == stock_id);
                // Leer defecto
                int defecto_id = int.Parse(xlRange.Cells[i, 2].Value2.ToString());
                float x = float.Parse(xlRange.Cells[i, 3].Value2.ToString()) * factorImagen;
                float y = float.Parse(xlRange.Cells[i, 4].Value2.ToString()) * factorImagen;
                float ancho = float.Parse(xlRange.Cells[i, 5].Value2.ToString()) * factorImagen;
                float alto = float.Parse(xlRange.Cells[i, 6].Value2.ToString()) * factorImagen;
                Rectangulo pieza = new Rectangulo(defecto_id, x, y, ancho, alto);
                // Lo agrego al stock
                stock.ListaDefectos.Add(pieza);
            }

            CerrarArchExcel();
        }

        private void AbrirArchExcel(String rutaArch)
        {
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(rutaArch);
        }

        private void LeerHojaExcel(String nombHoja)
        {
            xlWorksheet = xlWorkbook.Sheets[nombHoja];
            xlRange = xlWorksheet.UsedRange;
            rowCount = xlRange.Rows.Count;
            colCount = xlRange.Columns.Count;
        }

        private void CerrarArchExcel()
        {
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

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
