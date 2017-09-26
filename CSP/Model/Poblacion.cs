using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Controller;

namespace CSP.Model
{
    public class Poblacion
    {
        private int generacion;
        private int tamanhoPoblacion;
        private double probabilidadMutacion;
        private List<Cromosoma> listaCromosomas;

        // Crea una nueva población con individuos generados aleatoriamente
        public Poblacion(int generacion, int tamanhoPoblacion, double probabilidadMutacion, int numGenes, System.Random rnd)
        {
            this.generacion = generacion;
            this.tamanhoPoblacion = tamanhoPoblacion;
            this.probabilidadMutacion = probabilidadMutacion;
            this.listaCromosomas = new List<Cromosoma>();
            for (int i = 0; i < tamanhoPoblacion; ++i)
            {
                this.listaCromosomas.Add(new Cromosoma(numGenes, rnd));
            }
        }

        // Constructor vacío. Crear una población sin cromosomas
        public Poblacion(int generacion, int tamanhoPoblacion, double probabilidadMutacion)
        {
            this.generacion = generacion;
            this.tamanhoPoblacion = tamanhoPoblacion;
            this.probabilidadMutacion = probabilidadMutacion;
            this.listaCromosomas = new List<Cromosoma>();
        }

        public int TamanhoPoblacion { get => tamanhoPoblacion; set => tamanhoPoblacion = value; }
        public double ProbabilidadMutacion { get => probabilidadMutacion; set => probabilidadMutacion = value; }
        public int Generacion { get => generacion; set => generacion = value; }
        public List<Cromosoma> ListaCromosomas { get => listaCromosomas; set => listaCromosomas = value; }

        public static int CompararFitness(Cromosoma cromosoma1, Cromosoma cromosoma2)
        {
            return cromosoma1.Fitness.CompareTo(cromosoma2.Fitness);
        }

        public void OrdenarPoblacionPorFitness()
        {
            Comparison<Cromosoma> compCromosoma = new Comparison<Cromosoma>(Poblacion.CompararFitness);
            listaCromosomas.Sort(compCromosoma);
            listaCromosomas.Reverse();
        }

        public double CalcularFitness()
        {
            double sumaDelFitness = 0;
            foreach (Cromosoma cromosoma in this.listaCromosomas)
            {
                sumaDelFitness += cromosoma.Fitness;
            }
            return sumaDelFitness;
        }
    }

}
