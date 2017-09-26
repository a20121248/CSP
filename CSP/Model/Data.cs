using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Model
{
    class Data
    {
        double probabilidadMutacion;
        int tamanhoPoblacion;
        double pesoMinimizarRectangulo;
        double pesoFactorCuadratura;
        String ruta;

        public Data(String ruta)
        {
            this.ruta = ruta;
        }

        public Data(double probabilidadMutacion, int tamanhoPoblacion, double pesoMinimizarRectangulo, double pesoFactorCuadratura)
        {
            this.probabilidadMutacion = probabilidadMutacion;
            this.tamanhoPoblacion = tamanhoPoblacion;
            this.pesoMinimizarRectangulo = pesoMinimizarRectangulo;
            this.pesoFactorCuadratura = pesoFactorCuadratura;
        }

        public double ProbabilidadMutacion { get => probabilidadMutacion; set => probabilidadMutacion = value; }
        public int TamanhoPoblacion { get => tamanhoPoblacion; set => tamanhoPoblacion = value; }
        public double PesoMinimizarRectangulo { get => pesoMinimizarRectangulo; set => pesoMinimizarRectangulo = value; }
        public double PesoFactorCuadratura { get => pesoFactorCuadratura; set => pesoFactorCuadratura = value; }
    }
}
