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
    internal partial class FormGenetico : Form
    {
        public double probabilidadMutacion;
        public int tamanhoPoblacion;
        public double pesoMinimizarRectangulo;
        public double pesoFactorCuadratura;
        public List<Rectangulo> listaPiezas;
        public List<Stock> listaStocks;
        public List<String> solucion;
        public double fitness;
        public Nodo arbol_solucion;

        public FormGenetico(FormPedidos formPedidos)
        {            
            InitializeComponent();
            listaPiezas = formPedidos.listaPiezas;
            listaStocks = formPedidos.listaStocks;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            probabilidadMutacion = double.Parse(txtProbabilidadMutacion.Text);
            tamanhoPoblacion = int.Parse(txtTamanhoPoblacion.Text);
            pesoMinimizarRectangulo = double.Parse(txtPesoMinimizarRectangulo.Text);
            pesoFactorCuadratura = double.Parse(txtPesoFactorCuadratura.Text);

            AlgoritmoGenetico algGenetico = new AlgoritmoGenetico(this);
            solucion = algGenetico.mejorCromosoma.ListaGenes;
            fitness = algGenetico.mejorCromosoma.Fitness;
            arbol_solucion = algGenetico.mejorCromosoma.Tree;
        }

        private void btnVerResultado_Click(object sender, EventArgs e)
        {
            FormResultado formResultado = new FormResultado(this);
            formResultado.Show();
        }
    }
}
