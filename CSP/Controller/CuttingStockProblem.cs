using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Controller;
using CSP.Model;
using System.ComponentModel;

namespace CSP.Controller
{
    class CuttingStockProblem
    {
        private List<Rectangulo> listaPiezas;
        public List<Stock> listaStocks;

        public AlgoritmoGenetico algoritmoGenetico;
        public AlgoritmoCuckooSearch algoritmoCuckooSearch;
        public AlgoritmoStocks algoritmoStocks;
        public AlgoritmoDefectos algoritmoDefectos;

        public Cromosoma cromosoma;

        public CuttingStockProblem(List<Rectangulo> listaPiezas, List<Stock> listaStocks)
        {
            this.listaPiezas = listaPiezas;
            this.listaStocks = listaStocks;
        }

        

        public void IniciarAlgoritmoGenetico(double probabilidadMutacion, int tamanhoPoblacion, double pesoMinimizarRectangulo, double pesoFactorCuadratura, BackgroundWorker worker)
        {
            this.algoritmoGenetico = new AlgoritmoGenetico(1000, listaPiezas, probabilidadMutacion, tamanhoPoblacion, pesoMinimizarRectangulo, pesoFactorCuadratura, 1, worker);
            this.cromosoma = this.algoritmoGenetico.mejorCromosoma;
            //solucion = algGenetico.mejorCromosoma.ListaGenes;
            //fitness = algGenetico.mejorCromosoma.Fitness;
            //arbol_solucion = algGenetico.mejorCromosoma.Tree;
        }

        public void IniciarAlgoritmoCuckooSearch()
        {
            this.algoritmoCuckooSearch = new AlgoritmoCuckooSearch();
        }

        public void IniciarAlgoritmoStocks()
        {
            this.algoritmoStocks = new AlgoritmoStocks(this.listaStocks, this.algoritmoGenetico.mejorCromosoma);
            this.listaStocks = this.algoritmoStocks.listaStocks;
        }

        public void IniciarAlgoritmoDefectos()
        {
            this.algoritmoDefectos = new AlgoritmoDefectos();
        }
    }
}
