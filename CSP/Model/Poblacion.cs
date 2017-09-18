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
        private List<Cromosoma> individuos;

        // Crea una nueva población con individuos generados aleatoriamente
        public Poblacion(int tamanhoPoblacion, double probabilidadMutacion, int longitudCromosoma)
        {
            this.generacion = 0;
            this.tamanhoPoblacion = tamanhoPoblacion;
            this.probabilidadMutacion = probabilidadMutacion;
            this.individuos = new List<Cromosoma>();
            for (int i = 0; i < tamanhoPoblacion; ++i)
            {
                this.individuos.Add(new Cromosoma(longitudCromosoma));
            }
        }

        // Constructor vacío. Crear una población sin cromosomas
        public Poblacion(double probMutation, int tamPoblacion)
        {
            this.probabilidadMutacion = probMutation;
            this.tamanhoPoblacion = tamPoblacion;
        }

        public int TamanhoPoblacion { get => tamanhoPoblacion; set => tamanhoPoblacion = value; }
        public double ProbabilidadMutacion { get => probabilidadMutacion; set => probabilidadMutacion = value; }
        public int Generacion { get => generacion; set => generacion = value; }
        public List<Cromosoma> Individuos { get => individuos; set => individuos = value; }

        private void SepararPiezasYOperadores(Cromosoma progenitor, out List<String> listaPiezas, out List<String> listaOperadores)
        {
            listaPiezas = new List<String>();
            listaOperadores = new List<String>();

            int n;
            foreach (String gen in progenitor.ListaGenes)
            {
                // Si es un número
                if (Utilities.EsNumero(gen, out n))
                {
                    // Agrega dicha parte a la lista de piezas
                    listaPiezas.Add(n.ToString());
                    // Agregar un espacio (cadena vacía) a la lista de operadores
                    listaOperadores.Add("");
                }
                else
                {
                    // Agregar un operador a la lista de operadores
                    listaOperadores.Add(gen);
                }
            }
        }

        public Cromosoma UnirOperadoresYPiezas(List<String> listaOperadores, List<String> listaPiezas)
        {
            Cromosoma descendiente = new Cromosoma();
            for (int i = 0; i < listaOperadores.Count; ++i)
            {
                if (listaOperadores[i] == "") // si el espacio es de una pieza
                {
                    listaOperadores[i] = listaPiezas[0];
                    listaPiezas.RemoveAt(0);
                }
            }
            descendiente.ListaGenes = listaOperadores;
            return descendiente;
        }

        public void CorregirPiezas(List<String> progenitor1_listaPiezas, List<String> progenitor2_listaPiezas, Dictionary<string, List<string>> mapping_relaciones, int primerCorte, int segundoCorte, int posIni, int posFin)
        {
            String aux;
            for (int i = posIni; i <= posFin; ++i) // segmento
            {
                for (int j = primerCorte; j <= segundoCorte; ++j) // segmento interno
                {
                    if (progenitor1_listaPiezas[i] == progenitor1_listaPiezas[j]) // significa que el elemento interno debe ser intercambiado por un elemento de su mapping_relaciones, que no está en la cadena
                    {
                        // Para cada elemento de la relación
                        foreach (string el in mapping_relaciones[progenitor1_listaPiezas[j]])
                        {
                            // Si el elemento no está en la lista de piezas, entonces intercambiar
                            if (!progenitor1_listaPiezas.Contains(el))
                            {
                                // in "el" we have the element that can be swapped with the "repeated number in 1", we must find it in outter 2 to make the swap
                                for (int k = 0; k < primerCorte; ++k)
                                {
                                    if (el == progenitor2_listaPiezas[k])
                                    {
                                        // Save the element of 1 to aux
                                        aux = progenitor1_listaPiezas[i];

                                        progenitor1_listaPiezas[i] = el;
                                        progenitor2_listaPiezas[k] = aux;
                                    }
                                }

                                // Revisar también el lado derecho de la selección
                                for (int l = segundoCorte + 1; l < progenitor2_listaPiezas.Count; ++l)
                                {
                                    if (el == progenitor2_listaPiezas[l])
                                    {
                                        // Save the element of 1 to aux
                                        aux = progenitor1_listaPiezas[i];

                                        progenitor1_listaPiezas[i] = el;
                                        progenitor2_listaPiezas[l] = aux;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<Cromosoma> Crossover(Cromosoma progenitor1, Cromosoma progenitor2)
        {
            String aux;

            // Lista de piezas de cada progenitor (no tendrán espacios y solo representan un orden entre las piezas de dicho progenitor)
            // Lista de operadores de cada progenitor (tendrá espacios en los que colocaremos las piezas)            

            // Separar la parte de piezas de la parte de operadores para el progenitor1
            List<String> progenitor1_listaPiezas, progenitor1_listaOperadores;
            SepararPiezasYOperadores(progenitor1, out progenitor1_listaPiezas, out progenitor1_listaOperadores);
            // Separar la parte de piezas de la parte de operadores para el progenitor2
            List<String> progenitor2_listaPiezas, progenitor2_listaOperadores;
            SepararPiezasYOperadores(progenitor2, out progenitor2_listaPiezas, out progenitor2_listaOperadores);

            
            // Se crea un diccionario vacio cuyas claves son cadenas y los valores son listas de cadenas,
            // solo iterando la lista de piezas
            Dictionary<string, List<string>> mapping_relaciones = new Dictionary<string, List<string>>();

            // Inicializar el diccionario con listas vacias en los valores de los keys
            foreach (string el in progenitor1_listaPiezas)
            {
                mapping_relaciones.Add(el, new List<string>());
            }


            // Ahora, escoger dos número que delimitarán el segmento aleatorio de la lista
            // de piezas que serán intercambiados entre los progenitores para formar un hijo
            // el segundo numero debe ser mayor al primero!!
            System.Random rnd = new System.Random();
            int primerCorte = rnd.Next(0, progenitor1_listaPiezas.Count - 2);
            int segundoCorte = rnd.Next(primerCorte, progenitor1_listaPiezas.Count - 1);

            // -----------------------------------------------------------------------------------
            // Crossover para los operadores: Debe ser ordenado
            // -----------------------------------------------------------------------------------
            // Determinar qué operadores del primer progenitor serán intercambiados con los operadores en el segundo
            List<bool> paraIntercambiar = new List<bool>();
            int cantPiezas = 0; // Esto contará las piezas
            for (int i = 0; i < progenitor1_listaOperadores.Count; ++i)
            {
                if (progenitor1_listaOperadores[i] == "")
                {
                    ++cantPiezas;
                }

                if (cantPiezas >= primerCorte && cantPiezas < segundoCorte && progenitor1_listaOperadores[i] != "")
                {
                    paraIntercambiar.Add(true);
                }
                else
                {
                    paraIntercambiar.Add(false);
                }
            }

            int cantOperadoresEncontrados;
            for (int i = 0; i < progenitor1_listaOperadores.Count; ++i)
            {
                if (paraIntercambiar[i])
                {
                    // Cuenta el número de operadores que tiene el gen hasta aquí (variable i)
                    cantOperadoresEncontrados = 0;
                    for (int j = 0; j <= i; ++j)
                    {
                        if (progenitor1_listaOperadores[j] != "")
                        {
                            ++cantOperadoresEncontrados;
                        }
                    }

                    // Una vez contados, obtenemos el operador que será intercambiado en el segundo progenitor
                    for (int j = 0; j < progenitor2_listaOperadores.Count; ++j)
                    {
                        // Primero, revisar si es un operador, si lo es, restamos 1 de la cantidad de operadores encontrados
                        if (progenitor2_listaOperadores[j] != "")
                        {
                            --cantOperadoresEncontrados;
                        }
                        if (cantOperadoresEncontrados == 0)
                        {
                            // intercambiar
                            aux = progenitor1_listaOperadores[i];
                            progenitor1_listaOperadores[i] = progenitor2_listaOperadores[j];
                            progenitor2_listaOperadores[j] = aux;
                            break;
                        }
                    }
                }
            }

            // -----------------------------------------------------------------------------------
            // Crossover para las piezas
            // -----------------------------------------------------------------------------------
            // Intercambia un subgrupo entre los progenitores
            for (int i = primerCorte; i <= segundoCorte; ++i)
            {
                // Guardar la pieza del 1er progenitor antes de removerlo
                aux = progenitor1_listaPiezas[i];
                progenitor1_listaPiezas.RemoveAt(i);

                // Insertar la pieza del 2do progenitor en la posición removida del 1er progenitor
                progenitor1_listaPiezas.Insert(i, progenitor2_listaPiezas[i]);

                // Remueve la pieza del 2do progenitor
                progenitor2_listaPiezas.RemoveAt(i);

                // Insertar la pieza del 1er progenitor en la posición removida del 2do progenitor
                progenitor2_listaPiezas.Insert(i, aux);
            }

            // Una vez intercambios, se deben corregir para evitar soluciones no validas
            // Determinar las relaciones de ambos progenitores usando el diccionario mapping_relaciones
            for (int i = primerCorte; i <= segundoCorte; ++i)
            {
                mapping_relaciones[progenitor1_listaPiezas[i]].Add(progenitor2_listaPiezas[i]);
                mapping_relaciones[progenitor2_listaPiezas[i]].Add(progenitor1_listaPiezas[i]);
            }
            Utilities.unfold_relationships(mapping_relaciones);

            // Las piezas desde [0; primerCorte-1] y del [segundoCorte+1; tamListaPiezas-1] van a cambiar* si están en el rango [primerCorte, segundoCorte],
            // con el fin de no tener piezas repetidas en un mismo cromosoma
            // * Serán cambiadas por un número (pieza) de su mapping_relaciones que no esté en toda la lista de genes de piezas
            // Segmento izquierdo
            CorregirPiezas(progenitor1_listaPiezas, progenitor2_listaPiezas, mapping_relaciones, primerCorte, segundoCorte, 0, primerCorte - 1);
            // Segmento derecho
            CorregirPiezas(progenitor1_listaPiezas, progenitor2_listaPiezas, mapping_relaciones, primerCorte, segundoCorte, segundoCorte + 1, progenitor1_listaPiezas.Count - 1);

            // -----------------------------------------------------------------------------------------
            // Unir la lista de operadores con la lista de piezas para fomar los nuevos descendientes
            // -----------------------------------------------------------------------------------------
            // Insertar la lista de piezas en los espacios de la lista de operadores del progenitor 1
            Cromosoma descendiente1 = UnirOperadoresYPiezas(progenitor1_listaOperadores, progenitor1_listaPiezas);
            // Insertar la lista de piezas en los espacios de la lista de operadores del progenitor 2
            Cromosoma descendiente2 = UnirOperadoresYPiezas(progenitor2_listaOperadores, progenitor2_listaPiezas);

            List<Cromosoma> descendientes = new List<Cromosoma>();
            descendientes.Add(descendiente1);
            descendientes.Add(descendiente2);

            return descendientes;
        }

        public static bool Mutacion(Cromosoma cromosoma)
        {
            List<string> listaGenes = cromosoma.ListaGenes;
            System.Random rnd = new System.Random();

            // Escoger 2 diferentes posiciones de genes aleatorios
            int posGen1 = rnd.Next(0, listaGenes.Count);
            int posGen2 = rnd.Next(0, listaGenes.Count);
            while (posGen1 == posGen2)
            {
                posGen2 = rnd.Next(0, listaGenes.Count);
            }
            // Si el primero número no es el izquierda, entonces intercambiamos para que sí lo sea
            if (posGen1 > posGen2)
            {
                int aux = posGen1;
                posGen1 = posGen2;
                posGen2 = aux;
            }

            // Ahora, revisamos que son
            // Si ambos genes son piezas, podemos intercambios sin problemas
            if (Utilities.EsNumero(listaGenes[posGen1]) && Utilities.EsNumero(listaGenes[posGen2]))
            {
                string aux = listaGenes[posGen1];
                listaGenes[posGen1] = listaGenes[posGen2];
                listaGenes[posGen2] = aux;
            }
            else if (!Utilities.EsNumero(listaGenes[posGen1]))
            {
                string aux = listaGenes[posGen1];
                listaGenes[posGen1] = listaGenes[posGen2];
                listaGenes[posGen2] = aux;
            }

            // Si el gen1 es pieza y el gen2 es un operador
            else if (Utilities.EsNumero(listaGenes[posGen1]) && !Utilities.EsNumero(listaGenes[posGen2]))
            {
                int cantOperadores = 0;
                int cantPiezas = 0;

                // Debemos verificar que cada posición entre los genes p1 y p2 verifica que No <= Np - 3
                for (int i = 0; i < listaGenes.Count; ++i)
                {
                    if (Utilities.EsNumero(listaGenes[i]))
                    {
                        ++cantPiezas;
                    }
                    else
                    {
                        ++cantOperadores;
                    }

                    if (i >= posGen1 && i <= posGen2)
                    {
                        if (!(cantOperadores <= cantPiezas - 3))
                        {
                            return false;
                        }
                    }
                }
                // Si se cumple la condición, entonces el intrecambio es posible
                string aux = listaGenes[posGen1];
                listaGenes[posGen1] = listaGenes[posGen2];
                listaGenes[posGen2] = aux;
            }

            cromosoma.ListaGenes = listaGenes;

            return true;
        }

        public static int CompararFitness(Cromosoma chr1, Cromosoma chr2)
        {
            return chr1.Fitness.CompareTo(chr2.Fitness);
        }

        public void OrdenarPoblacionPorFitness()
        {
            Comparison<Cromosoma> compCromosoma = new Comparison<Cromosoma>(Poblacion.CompararFitness);
            individuos.Sort(compCromosoma);
            individuos.Reverse();
        }

        public void CalcularUnaGeneracion()
        {
            double sumaDelFitness = 0;
            System.Random nature = new System.Random();

            Poblacion sobrevivientes = new Poblacion(probabilidadMutacion, tamanhoPoblacion);
            ++generacion;

            Cromosoma progenitor1 = new Cromosoma();
            Cromosoma progenitor2 = new Cromosoma();
            Cromosoma descendiente1;
            Cromosoma descendiente2;
            List<Cromosoma> listaDescendientes;

            // Es elitista para los primero n cromosomas, en este caso el primero
            for (int i = 0; i < 1; ++i)
            {
                sobrevivientes.individuos.Add(individuos[i]);
            }

            //  Calcular la suma de todo el fitness de los cromosomas de la población
            foreach (Cromosoma chr in individuos)
            {
                sumaDelFitness += chr.Fitness;
            }

            while (sobrevivientes.individuos.Count < tamanhoPoblacion)
            {
                // Generar un número aleatorio en el intervalo [0, SumaFitness]
                double random = Utilities.ObtenerAleatorioEntre(0, sumaDelFitness, nature);
                double s = 0;
                // Anda a través de todas las poblaciones cuya suma de fitness sea [0 - suma_fitness].
                // Cuando la suma sea mayor que random, para y devuelve el cromosoma donde estas 
                foreach (Cromosoma chr in individuos)
                {
                    s += chr.Fitness;
                    if (s > random)
                    {
                        progenitor1 = chr;
                        break;
                    }
                }

                // Hacer lo mismo para el progenitor2
                random = Utilities.ObtenerAleatorioEntre(0, sumaDelFitness, nature);
                s = 0;

                // Anda a través de todas las poblaciones cuya suma de fitness sea [0 - suma_fitness].
                // Cuando la suma sea mayor que random, para y devuelve el cromosoma donde estas 
                foreach (Cromosoma chr in individuos)
                {
                    s += chr.Fitness;
                    if (s > random)
                    {
                        progenitor2 = chr;
                        break;
                    }
                }

                // Una vez que tengamos ambos progenitores seleccionados, los cruzamos y obtenemos dos descendientes
                listaDescendientes = Crossover(progenitor1, progenitor2);
                descendiente1 = listaDescendientes[0];
                descendiente2 = listaDescendientes[1];

                //sobrevivientes.poblacion.Add(progenitor1);
                //sobrevivientes.poblacion.Add(progenitor2);
                sobrevivientes.individuos.Add(descendiente1);
                sobrevivientes.individuos.Add(descendiente2);
            }

            for (int i = 2; i < sobrevivientes.individuos.Count; ++i)
            {
                if (nature.NextDouble() < probabilidadMutacion)
                {
                    while (!Mutacion(sobrevivientes.individuos[i]));
                    //mutation(survivors.population_list[i]);
                }
            }

            individuos = sobrevivientes.individuos;
        }
    }

}
