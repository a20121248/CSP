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

        private AlgoritmoGenetico algoritmoGenetico;
        private AlgoritmoCuckooSearch algoritmoCuckooSearch;
        private AlgoritmoStocks algoritmoStocks;
        private AlgoritmoDefectos algoritmoDefectos;

        public Nodo arbolSolucion;

        public CuttingStockProblem(List<Rectangulo> listaPiezas, List<Stock> listaStocks)
        {
            this.listaPiezas = listaPiezas;
            this.listaStocks = listaStocks;
        }

        public void IniciarAlgoritmoGenetico(double probabilidadMutacion, int tamanhoPoblacion,   double pesoMinimizarRectangulo,
                                             double pesoFactorCuadratura, int numMaxGeneraciones, int cantElitismo,
                                             List<Stock> listaStocks,     BackgroundWorker worker)
        {
            this.algoritmoGenetico = new AlgoritmoGenetico(numMaxGeneraciones, listaPiezas,             probabilidadMutacion,
                                                           tamanhoPoblacion,   pesoMinimizarRectangulo, pesoFactorCuadratura,
                                                           cantElitismo,       listaStocks,             worker);
            this.arbolSolucion = this.algoritmoGenetico.mejorCromosoma.Arbol;
        }

        public void IniciarAlgoritmoCuckooSearch()
        {
            this.algoritmoCuckooSearch = new AlgoritmoCuckooSearch();
        }

        public void IniciarAlgoritmoStocks()
        {
            this.algoritmoStocks = new AlgoritmoStocks(this.listaStocks, arbolSolucion);
            this.listaStocks = this.algoritmoStocks.listaStocks;
        }

        public void IniciarAlgoritmoDefectos()
        {
            this.algoritmoDefectos = new AlgoritmoDefectos();
        }
    }
}
