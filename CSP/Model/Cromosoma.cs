using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Controller;

namespace CSP.Model
{
    public class Cromosoma
    {
        private List<String> listaGenes;
        private Nodo tree;
        private double fitness;

        // Constructores

        // Void constructor
        public Cromosoma()
        {

        }

        public Nodo Tree { get => tree; set => tree = value; }
        public double Fitness { get => fitness; set => fitness = value; }
        public List<string> ListaGenes { get => listaGenes; set => listaGenes = value; }

        // Defined constructor
        public Cromosoma(List<String> chromosome)
        {
            listaGenes = chromosome;
        }

        // Introduce a un cromosoma, represetando por List<String>
        // un gen tipo Pieza de una lista de piezas disponibles
        public void IntroducirPiezaAleatoria(List<String> miCromosoma, List<String> miListaPiezasDisponibles, System.Random rnd_obj)
        {
            // Selecciona una pieza
            int nroPieza = rnd_obj.Next(0, miListaPiezasDisponibles.Count);
            String genPieza = miListaPiezasDisponibles[nroPieza];

            // Agrega la pieza al cromosoma
            miCromosoma.Add(genPieza);

            // Remueve la pieza de la lista de piezas para que no sea anhadida en un futuro
            miListaPiezasDisponibles.Remove(genPieza);
        }

        // Introduce al final del cromosoma un operadora genetico
        public void IntroducirOperadorAleatorio(List<String> miCromosoma, System.Random objRandom)
        {
            // Seleccionar aleatoriamente un operador
            int eleccionOperador = objRandom.Next(1, 3);
            if (eleccionOperador == 1)
            {
                miCromosoma.Add("H");
            }
            if (eleccionOperador == 2)
            {
                miCromosoma.Add("V");
            }
        }

        // Random constructor (toma la longitud como argumento)
        // Crear un cromosoma válido aleatorio
        public Cromosoma(int longitudCromosoma)
        {
            List<String> cromosoma = new List<String>();

            // Dada una longitud (número de genes en el cromosoma genes),
            // conocemos exactamente el número de piezas, pues [ Ng = Np + No = 2Np - 1 ] -> Np = (Ng + 1) / 2
            int ng = longitudCromosoma; // Número de genes en el cromosoma
            int np = (ng + 1) / 2;      // Número de piezas
            int no = ng - np;           // Número de operadores

            // Generamos una lista que contendrá a las piezas
            List<String> listaPiezas = new List<String>();

            System.Random rnd = new System.Random();

            // Llenamos las piezas con numeros de pieza
            for (int i = 1; i <= np; ++i)
            {
                listaPiezas.Add(i.ToString());
            }

            // Las primeras dos piezas siempre serán parte número
            IntroducirPiezaAleatoria(cromosoma, listaPiezas, rnd);
            IntroducirPiezaAleatoria(cromosoma, listaPiezas, rnd);
            IntroducirOperadorAleatorio(cromosoma, rnd);

            // A partir de ahora, necesitamos completar el cromosoma pero satisfaciendo las restrcciones

            // Para los siguientes genes del cromosoma
            for (int i = cromosoma.Count; i < ng; ++i)
            {
                // Revisar si el No >= Np+1 (solo en la izquierda)
                int actual_np = 0;
                int actual_no = 0;
                int n;
                for (int j = 0; j < cromosoma.Count; ++j)
                {
                    if (Utilities.EsNumero(cromosoma[j], out n)) {
                        ++actual_np;
                    }
                    else
                    {
                        ++actual_no;
                    }
                }

                // Una vez contados, decidimos:
                if (no == np + 1)
                {
                    // Límite de la restricción, necesitamos poner una pieza
                    IntroducirPiezaAleatoria(cromosoma, listaPiezas, rnd);
                }
                // Si la restricción no está en el límite, entonces tenemos con un 50% de probabilidad una pieza o un operadora, si aun hay piezas
                else
                {
                    // Si no hay mas piezas en la lista, entonces introducimos un operador.
                    if (listaPiezas.Count == 0)
                    {
                        IntroducirOperadorAleatorio(cromosoma, rnd);
                    }
                    else
                    {
                        // 50% de probabilidad de pieza u operador
                        int numeroAleatorio = rnd.Next(1, 3);

                        // introducir pieza
                        if (numeroAleatorio == 1)
                        {
                            IntroducirPiezaAleatoria(cromosoma, listaPiezas, rnd);
                        }
                        // introducir operador
                        if (numeroAleatorio == 2)
                        {
                            IntroducirOperadorAleatorio(cromosoma, rnd);
                            // El cromosoma construido no puede debe ser válido
                            try
                            {
                                Stack<System.Object> testPila = new Stack<System.Object>();
                                ConstruirPilaDeCromosomaSimple(cromosoma, testPila);
                            }
                            catch (Exception)
                            {
                                cromosoma.RemoveAt(cromosoma.Count - 1);
                                IntroducirPiezaAleatoria(cromosoma, listaPiezas, rnd);
                            }
                        }
                    }
                }
            }
            listaGenes = cromosoma;
        }

        // Operaciones de pila para el proceso de construcción,
        // necesarios para saber si un cromosoma es válido.
        public void ApilarSimple(Stack<System.Object> my_stack)
        {
            System.Object o = new System.Object();
            my_stack.Push(o);
        }

        public void DesapilarYApilarSimple(Stack<System.Object> miPila)
        {
            // Desapilar 
            System.Object first_node = miPila.Pop();
            System.Object second_node = miPila.Pop();

            System.Object o = new System.Object();
            miPila.Push(o);
        }

        public void ConstruirPilaDeCromosomaSimple(List<String> cromosoma, Stack<System.Object> miPila)
        {
            for (int i = 0; i < cromosoma.Count; ++i)
            {
                int n = -1;
                // Si el gen del cromosoma es un número (tipo pieza),
                // entonces necesitamos obtener ese numero
                if (Utilities.EsNumero(cromosoma[i], out n))
                {
                    ApilarSimple(miPila);
                }
                else // not a number -> V or H
                {
                    DesapilarYApilarSimple(miPila);
                }
            }
        }
    }

}
