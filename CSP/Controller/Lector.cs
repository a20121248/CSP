using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Model;
using CSP.Controller;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;

namespace CSP.Controller
{
    class Lector
    {
        public List<Rectangulo> listaPiezas;
        public List<Stock> listaStocks;
        public BackgroundWorker worker;

        private String rutaArch;
        public float factorImagen;

        private Excel.Application xlApp;
        private Excel.Workbook xlWorkbook;
        private Excel._Worksheet xlWorksheet;
        private Excel.Range xlRange;
        private int rowCount;
        private int colCount;

        public Lector(String rutaArch)
        {
            this.rutaArch = rutaArch;
            this.factorImagen = 2.5f;
        }

        public void LeerArchPiezas()
        {
            listaPiezas = new List<Rectangulo>();
            AbrirArchExcel(rutaArch);

            LeerHojaExcel("Piezas");
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

                int progreso = (int)((i - 1) * 1.0 / (rowCount - 1) * 100);
                worker.ReportProgress(progreso);
            }

            CerrarArchExcel();
        }

        public void LeerArchStocks()
        {
            listaStocks = new List<Stock>();
            AbrirArchExcel(rutaArch);

            LeerHojaExcel("Stocks");
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

                int progreso = (int)((i - 1) * 1.0 / (rowCount - 1) * 100);
                worker.ReportProgress(progreso);
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

                int progreso = (int)((i - 1) * 1.0 / (rowCount - 1) * 100);
                worker.ReportProgress(progreso);
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

    }
}
