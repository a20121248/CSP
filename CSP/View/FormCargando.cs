using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSP.Controller;
using CSP.Model;

namespace CSP.View
{
    internal partial class FormCargando : Form
    {
        private String tipo;
        private CuttingStockProblem csp;
        private Data data;
        private Lector lector;

        public const String LECTOR_PEDIDOS = "LectorPedidos";
        public const String LECTOR_STOCKS = "LectorStock";
        public const String ALGORITMO_GENETICO = "Genetico";
        public const String ALGOTIMO_CUCKOO_SEARCH = "CuckooSearch";

        public FormCargando(String tipo, Lector lector, Data data)
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;

            this.tipo = tipo;
            this.lector = lector;
            this.data = data;

            backgroundWorker1.RunWorkerAsync();
        }

        public FormCargando(String tipo, CuttingStockProblem csp, Data data)
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;

            this.tipo = tipo;
            this.csp = csp;
            this.data = data;

            backgroundWorker1.RunWorkerAsync();
        }

        public void UpdateProgress(int progress)
        {
            progressBar1.Value = progress;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (tipo)
            {
                case LECTOR_PEDIDOS:
                    lector.worker = backgroundWorker1;
                    lector.LeerArchPiezas();
                    break;
                case LECTOR_STOCKS:
                    lector.worker = backgroundWorker1;
                    lector.LeerArchStocks();
                    break;
                case ALGORITMO_GENETICO:
                    csp.IniciarAlgoritmoGenetico(data.ProbabilidadMutacion, data.TamanhoPoblacion, data.PesoMinimizarRectangulo, data.PesoFactorCuadratura, backgroundWorker1);
                    break;
                case ALGOTIMO_CUCKOO_SEARCH:
                    break;
                default:
                    break;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }
}
