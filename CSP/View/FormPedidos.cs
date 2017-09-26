using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSP.Model;
using CSP.Controller;

namespace CSP.View
{
    internal partial class FormPedidos : Form
    {
        public List<Rectangulo> listaPiezas;
        public List<Stock> listaStocks;

        public FormPedidos()
        {
            InitializeComponent();
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

            Data data = new Data(txtRutaArchPedidos.Text);
            Lector lector = new Lector(txtRutaArchPedidos.Text);
            FormCargando loading = new FormCargando(FormCargando.LECTOR_PEDIDOS, lector, data);
            loading.ShowDialog(this);
            this.listaPiezas = lector.listaPiezas;
            //this.listaPiezas = lector.LeerArchPiezas();
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

            Data data = new Data(txtRutaArchStock.Text);
            Lector lector = new Lector(txtRutaArchStock.Text);
            FormCargando loading = new FormCargando(FormCargando.LECTOR_STOCKS, lector, data);
            loading.ShowDialog(this);
            this.listaStocks = lector.listaStocks;

            //Lector lector = new Lector(txtRutaArchStock.Text);
            //this.listaStocks = lector.LeerArchStocks();
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
