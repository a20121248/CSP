using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Model
{
    class Data
    {
        private double probabilidadMutacion;
        private int tamanhoPoblacion;
        private double pesoMinimizarRectangulo;
        private double pesoFactorCuadratura;
        private int cantidadElitismo;
        private int cantMaxGeneraciones;
        private List<Stock> listaStocks;
        private String ruta;

        public Data(String ruta)
        {
            this.ruta = ruta;
        }

        public Data(double probabilidadMutacion, int tamanhoPoblacion, double pesoMinimizarRectangulo, double pesoFactorCuadratura, int cantidadElitismo, int cantMaxGeneraciones, List<Stock> listaStocks)
        {
            this.probabilidadMutacion = probabilidadMutacion;
            this.tamanhoPoblacion = tamanhoPoblacion;
            this.pesoMinimizarRectangulo = pesoMinimizarRectangulo;
            this.pesoFactorCuadratura = pesoFactorCuadratura;
            this.cantidadElitismo = cantidadElitismo;
            this.cantMaxGeneraciones = cantMaxGeneraciones;
            this.listaStocks = listaStocks;
        }

        public double ProbabilidadMutacion { get => probabilidadMutacion; set => probabilidadMutacion = value; }
        public int TamanhoPoblacion { get => tamanhoPoblacion; set => tamanhoPoblacion = value; }
        public double PesoMinimizarRectangulo { get => pesoMinimizarRectangulo; set => pesoMinimizarRectangulo = value; }
        public double PesoFactorCuadratura { get => pesoFactorCuadratura; set => pesoFactorCuadratura = value; }
        public int CantidadElitismo { get => cantidadElitismo; set => cantidadElitismo = value; }
        public int CantMaxGeneraciones { get => cantMaxGeneraciones; set => cantMaxGeneraciones = value; }
        internal List<Stock> ListaStocks { get => listaStocks; set => listaStocks = value; }
    }
}
