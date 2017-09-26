using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Model;
using CSP.View;
using System.ComponentModel;

namespace CSP.Controller
{
    class AlgoritmoGenetico
    {
        public Cromosoma mejorCromosoma;
        private double pesoFactorMinimizacion;
        private double pesoFactorCuadratura;
        private BackgroundWorker worker;

        public AlgoritmoGenetico(int numMaxGeneraciones, List<Rectangulo> listaPiezas, double probabilidadMutacion, int tamanhoPoblacion, double pesoFactorMinimizacion, double pesoFactorCuadratura, int cantElitismo, BackgroundWorker worker)
        {
            // Iniciar parámetros del algoritmo
            System.Random rnd = new System.Random();
            this.worker = worker;
            this.pesoFactorMinimizacion = pesoFactorMinimizacion;
            this.pesoFactorCuadratura = pesoFactorCuadratura;

            // Crear la poblacion inicial
            Poblacion poblacion = CalcularPoblacionInicial(tamanhoPoblacion, probabilidadMutacion, listaPiezas, rnd);
            this.mejorCromosoma = poblacion.ListaCromosomas[0];
            // Calcular hasta un maximo de generaciones (o agregar condicion de parada de Nilton)
            for (int i = 0; i < numMaxGeneraciones; ++i)
            {
                // Calculo la nueva poblacion a partir de la anterior
                poblacion = CalcularSiguienteGeneracion(poblacion, cantElitismo, probabilidadMutacion, tamanhoPoblacion, rnd);
                
                // Luego, calculo sus posiciones y area. Esto es netamente cálculo, nada del algoritmo u aleatorio
                foreach (Cromosoma cromosoma in poblacion.ListaCromosomas)
                {
                    CalcularCromosoma(cromosoma, listaPiezas);
                }
                poblacion.OrdenarPoblacionPorFitness();

                // Codigo para indicar el porcentaje de avance en la barra de cargando
                int progreso = (int)(i * 1.0 / (numMaxGeneraciones - 1) * 100);
                worker.ReportProgress(progreso);
            }
            this.mejorCromosoma = poblacion.ListaCromosomas[0];

            //Debug_prueba(listaPiezas);
        }
        
        public Poblacion CalcularPoblacionInicial(int tamanhoPoblacion, double probabilidadMutacion, List<Rectangulo> listaPiezas, System.Random rnd)
        {
            // Creo una lista de "tamPoblacion" cromosomas aleatorios.
            // El constructor de Cromosoma dentro de la clase Poblacion se encarga de crearlo aleatoriamente
            int numGenes = listaPiezas.Count * 2 - 1;
            Poblacion poblacion = new Poblacion(1, tamanhoPoblacion, probabilidadMutacion, numGenes, rnd);
            
            // Completar la información de cada cromosoma (posición, fitness) y ordenarlo
            foreach (Cromosoma cromosoma in poblacion.ListaCromosomas)
            {
                CalcularCromosoma(cromosoma, listaPiezas);
            }
            poblacion.OrdenarPoblacionPorFitness();
            return poblacion;
        }
        
        public List<Cromosoma> SeleccionarProgenitores(double sumaDelFitness, Poblacion actualPoblacion, System.Random rnd)
        {
            List<Cromosoma> listaProgenitores = new List<Cromosoma>();

            // Generar un número aleatorio en el intervalo [0, SumaFitness]
            double random = Utilitarios.ObtenerAleatorioEntre(0, sumaDelFitness, rnd);
            double s = 0;
            // Recorre todas las poblaciones cuya suma de fitness sea [0 - suma_fitness].
            // Cuando la suma sea mayor que random, para y devuelve el cromosoma donde estas 
            foreach (Cromosoma cromosoma in actualPoblacion.ListaCromosomas)
            {
                s += cromosoma.Fitness;
                if (s > random)
                {
                    listaProgenitores.Add(cromosoma); // progenitor 1
                    break;
                }
            }

            // Hacer lo mismo para el progenitor2
            random = Utilitarios.ObtenerAleatorioEntre(0, sumaDelFitness, rnd);
            s = 0;
            // Anda a través de todas las poblaciones cuya suma de fitness sea [0 - suma_fitness].
            // Cuando la suma sea mayor que random, para y devuelve el cromosoma donde estas 
            foreach (Cromosoma cromosoma in actualPoblacion.ListaCromosomas)
            {
                s += cromosoma.Fitness;
                if (s > random)
                {
                    listaProgenitores.Add(cromosoma); // progenitor 2
                    break;
                }
            }

            return listaProgenitores;
        }

        private void SepararPiezasYOperadores(Cromosoma progenitor, out List<String> listaPiezas, out List<String> listaOperadores)
        {
            listaPiezas = new List<String>();
            listaOperadores = new List<String>();

            int n;
            foreach (String gen in progenitor.ListaGenes)
            {
                // Si es una pieza
                if (Utilitarios.EsPieza(gen, out n))
                {
                    // Agrega dicho gen a la lista de piezas
                    listaPiezas.Add(gen);
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

        public void CorregirPiezas(List<String> progenitor1_listaPiezas, List<String> progenitor2_listaPiezas, Dictionary<string, List<string>> mapping_relaciones, int primerCorte, int segundoCorte, int posIni, int posFin)
        {
            String aux;
            for (int i = posIni; i <= posFin; ++i) // segmento
            {
                for (int j = primerCorte; j <= segundoCorte; ++j) // segmento interno
                {
                    if (Utilitarios.CompararPiezas(progenitor1_listaPiezas[i], progenitor1_listaPiezas[j])) // significa que el elemento interno debe ser intercambiado por un elemento de su mapping_relaciones, que no está en la cadena
                    {
                        // Para cada elemento de la relación
                        foreach (string el in mapping_relaciones[Utilitarios.ObtenerPieza(progenitor1_listaPiezas[j])])
                        {
                            // Si el elemento no está en la lista de piezas, entonces intercambiar
                            //Comparison<String> compPiezas = new Comparison<String>(Utilities.CompararPiezas);
                            //if (!progenitor1_listaPiezas.Contains(el, StringComparer.OrdinalIgnoreCase))
                            //if (!progenitor1_listaPiezas.Contains(el))
                            bool contiene = progenitor1_listaPiezas.Any(x => Utilitarios.ObtenerPieza(x) == Utilitarios.ObtenerPieza(el));
                            if (!contiene)
                            {
                                // in "el" we have the element that can be swapped with the "repeated number in 1", we must find it in outter 2 to make the swap
                                for (int k = 0; k < primerCorte; ++k)
                                {
                                    if (Utilitarios.CompararPiezas(el, progenitor2_listaPiezas[k]))
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
                                    if (Utilitarios.CompararPiezas(el, progenitor2_listaPiezas[l]))
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

        public List<Cromosoma> Cruce(Cromosoma progenitor1, Cromosoma progenitor2, System.Random rnd)
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
            foreach (string genPieza in progenitor1_listaPiezas)
            {
                mapping_relaciones.Add(genPieza.Substring(0, genPieza.Length - 1), new List<string>());
            }


            // Ahora, escoger dos número que delimitarán el segmento aleatorio de la lista
            // de piezas que serán intercambiados entre los progenitores para formar un hijo
            // el segundo numero debe ser mayor al primero!!
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
                //mapping_relaciones[progenitor1_listaPiezas[i]].Add(progenitor2_listaPiezas[i]);
                //mapping_relaciones[progenitor2_listaPiezas[i]].Add(progenitor1_listaPiezas[i]);
                mapping_relaciones[progenitor1_listaPiezas[i].Substring(0, progenitor1_listaPiezas[i].Length - 1)].Add(progenitor2_listaPiezas[i]);
                mapping_relaciones[progenitor2_listaPiezas[i].Substring(0, progenitor2_listaPiezas[i].Length - 1)].Add(progenitor1_listaPiezas[i]);
            }
            Utilitarios.unfold_relationships(mapping_relaciones);

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

        public bool Mutacion(Cromosoma cromosoma, System.Random rnd)
        {
            List<String> listaGenes = cromosoma.ListaGenes;

            // Escoger 2 diferentes posiciones de genes aleatorios
            int posGen1 = rnd.Next(0, listaGenes.Count - 1);
            int posGen2 = rnd.Next(posGen1 + 1, listaGenes.Count);

            // Ahora, revisamos que son
            // Si ambos genes son piezas, podemos intercambiarlos sin problemas
            if (Utilitarios.EsPieza(listaGenes[posGen1]) && Utilitarios.EsPieza(listaGenes[posGen2]))
            {

                String aux = listaGenes[posGen1];
                listaGenes[posGen1] = listaGenes[posGen2];
                listaGenes[posGen2] = aux;
            }
            else if (!Utilitarios.EsPieza(listaGenes[posGen1]))
            {
                String aux = listaGenes[posGen1];
                listaGenes[posGen1] = listaGenes[posGen2];
                listaGenes[posGen2] = aux;
            }
            // Si el gen1 es pieza y el gen2 es un operador
            else if (Utilitarios.EsPieza(listaGenes[posGen1]) && !Utilitarios.EsPieza(listaGenes[posGen2]))
            {
                int cantOperadores = 0;
                int cantPiezas = 0;

                // Debemos verificar que cada posición entre los genes p1 y p2 verifica que No <= Np - 3
                for (int i = 0; i < listaGenes.Count; ++i)
                {
                    if (Utilitarios.EsPieza(listaGenes[i]))
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
                String aux = listaGenes[posGen1];
                listaGenes[posGen1] = listaGenes[posGen2];
                listaGenes[posGen2] = aux;
            }

            listaGenes[posGen1] = Utilitarios.RotarPiezaAleatorio(listaGenes[posGen1], rnd);
            listaGenes[posGen2] = Utilitarios.RotarPiezaAleatorio(listaGenes[posGen2], rnd);

            cromosoma.ListaGenes = listaGenes;

            return true;
        }

        public Poblacion CalcularSiguienteGeneracion(Poblacion actualPoblacion, int cantElitismo, double probabilidadMutacion, int tamanhoPoblacion, System.Random rnd)
        {
            // Creo una poblacion vacia
            Poblacion nuevaPoblacion = new Poblacion(actualPoblacion.Generacion + 1, tamanhoPoblacion, probabilidadMutacion);

            // Elitismo: Guardo los mejores cromosomas de la población actual
            for (int i = 0; i < cantElitismo; ++i)
            {
                nuevaPoblacion.ListaCromosomas.Add(actualPoblacion.ListaCromosomas[i]);
            }

            // Calculo el fitness de la poblacion actual (para ayudar al método de selección)
            double sumaDelFitness = actualPoblacion.CalcularFitness();
            while (nuevaPoblacion.ListaCromosomas.Count < tamanhoPoblacion)
            {
                //  Seleccionar 2 progenitores con el Método de la ruleta
                List<Cromosoma> listaProgenitores = SeleccionarProgenitores(sumaDelFitness, actualPoblacion, rnd);

                // Una vez que tengamos ambos progenitores seleccionados, los cruzamos, obtenemos dos descendientes y los agregamos a la población
                List<Cromosoma> listaDescendientes = Cruce(listaProgenitores[0], listaProgenitores[1], rnd);

                // Agrego los hijos a la poblacion que se está creando
                nuevaPoblacion.ListaCromosomas.Add(listaDescendientes[0]);
                nuevaPoblacion.ListaCromosomas.Add(listaDescendientes[1]);
            }

            // Mutacion. No olvidarse que esto no afecta a los mejores individuos
            for (int i = cantElitismo+1; i < nuevaPoblacion.ListaCromosomas.Count; ++i)
            {
                if (rnd.NextDouble() < probabilidadMutacion)
                {
                    while (!Mutacion(nuevaPoblacion.ListaCromosomas[i], rnd)) ;
                }
            }

            return nuevaPoblacion;
        }

        public Cromosoma CrearCromosoma(List<String> lista, List<Rectangulo> miListaPiezas)
        {
            Cromosoma test = new Cromosoma(lista);
            CalcularCromosoma(test, miListaPiezas);
            return test;
        }

        public void CalcularCromosoma(Cromosoma cromosoma, List<Rectangulo> listaPiezas)
        {
            // Construimos el árbol a partir de la notación postfija del cromosoma
            Nodo tree = Utilitarios.ConstruirArbolDeUnCromosomas(cromosoma.ListaGenes, listaPiezas);

            // Una vez construido el árbol, calculamos las posiciones relativas (dependiendo del bloque al que pertenenece)
            Utilitarios.CalcularPosiciones(tree);
            cromosoma.Tree = tree;

            cromosoma.Fitness = Utilitarios.CalcularFitness(tree, listaPiezas, pesoFactorMinimizacion, pesoFactorCuadratura);
        }

        public void Debug_prueba(List<Rectangulo> milistaPiezas)
        {
            //Cromosoma test = new Cromosoma(new List<string>() { "3", "4", "H", "1", "2", "V", "H"});
            Cromosoma test = new Cromosoma(new List<string>() { "7N", "3R", "H", "4R", "H", "8N", "V", "6N", "V", "10N", "2R", "H", "5R", "V", "9R", "V", "1R", "H", "H" });

            CalcularCromosoma(test, milistaPiezas);

            mejorCromosoma = test;
        }

    }
}
