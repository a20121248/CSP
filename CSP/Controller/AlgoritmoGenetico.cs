using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Model;
using CSP.View;

namespace CSP.Controller
{
    class AlgoritmoGenetico
    {
        public Cromosoma mejorCromosoma;

        public Graphics panel_sheet_process_window;

        public List<Rectangulo> listaPiezas;
        public List<String> chromosome;

        // Dimensiones del material en stock, considerado
        // como la suma de todo el stock disponible
        public float current_sheet_width;
        public float current_sheet_height;
        public int longitud_cromosoma;

        public Stack<Nodo> nodoPila;

        public String numero_generacion;
        public String text_fitness_of_best;
        public String text_time;

        public double campo_min_rect_factor;
        public double campo_cuadratura_factor;
        public double probMutacion;
        public int tamPoblacion;

        public Poblacion miPoblacion;

        double time_elapsed;

        public AlgoritmoGenetico(FormGenetico formGenetico)
        {
            time_elapsed = 0;

            /* Iniciar parámetros del algoritmo */
            listaPiezas = formGenetico.listaPiezas;
            probMutacion = formGenetico.probabilidadMutacion;
            tamPoblacion = formGenetico.tamanhoPoblacion;
            campo_min_rect_factor = formGenetico.pesoMinimizarRectangulo;
            campo_cuadratura_factor = formGenetico.pesoFactorCuadratura;
            longitud_cromosoma = listaPiezas.Count * 2 - 1;            
            current_sheet_width = 10000;
            current_sheet_height = 10000;

            /* Crear la poblacion inicial */
            CalcularPoblacionInicial();

            // Calular 1 generacion
            //CalculateNextGeneration();

            // Calular 1000 generaciones
            CalculateNext1000Generation();

            // Guardar el mejor cromosoma:
            mejorCromosoma = miPoblacion.Individuos[0];

            //Chromosome best = my_population.population_list[0];
            //mejorCromosoma = best;

            //Debug_prueba();

        }

        public void Debug_prueba()
        {
            Cromosoma test = new Cromosoma(new List<string>() { "3", "4", "H", "1", "2", "V", "H"});

            CalculateAndSetChromosomePhenotype(test);
            
            mejorCromosoma = test;
        }


        public void CalcularPoblacionInicial()
        {
            miPoblacion = new Poblacion(probMutacion, tamPoblacion, longitud_cromosoma);
            miPoblacion.Generacion = 0;

            foreach (Cromosoma chrom in miPoblacion.Individuos)
            {
                CalculateAndSetChromosomePhenotype(chrom);
            }

            miPoblacion.OrdenarPoblacionPorFitness();

            text_fitness_of_best = "Fitness: " + miPoblacion.Individuos[0].Fitness.ToString();

            // Tomar el mejor individuo
            Cromosoma best = miPoblacion.Individuos[0];
        }

        public void CalculateNextGeneration()
        {
            miPoblacion.CalcularUnaGeneracion();
            numero_generacion = "Generation " + miPoblacion.Generacion;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            foreach (Cromosoma chrom in miPoblacion.Individuos)
            {
                CalculateAndSetChromosomePhenotype(chrom);
            }

            miPoblacion.OrdenarPoblacionPorFitness();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            text_fitness_of_best = "Fitness: " + miPoblacion.Individuos[0].Fitness.ToString();

            // Take out the best and draw it:
            Cromosoma best = miPoblacion.Individuos[0];

            // stack initialization 
            nodoPila = new Stack<Nodo>();
            // we build the stack depending on how the chromosome specifies it
            ConstruirArbolDeUnCromosomas(best.ListaGenes);
            // pop the tree once it is built
            Nodo tree_best = nodoPila.Pop();
            // once the tree is built, now we can set the positions of the pieces depending of their relationship with other pieces (H or V)
            CalcularPosiciones(tree_best);
            CalcularYGuardarFitness(best, tree_best, listaPiezas);
        }

        public void CalculateNext1000Generation()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // el código que se desse medir va aquí

            for (int i = 0; i < 1000; ++i)
            {
                miPoblacion.CalcularUnaGeneracion();
                numero_generacion = "Generation " + miPoblacion.Generacion;

                foreach (Cromosoma chrom in miPoblacion.Individuos)
                {
                    CalculateAndSetChromosomePhenotype(chrom);
                }
            }

            miPoblacion.OrdenarPoblacionPorFitness();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            text_time = "Time: " + elapsedMs + " ms.";

            text_fitness_of_best = "Fitness: " + miPoblacion.Individuos[0].Fitness.ToString();
        }

        public void CalculateAndSetChromosomePhenotype(Cromosoma crom)
        {
            // Inicialización de la pila
            nodoPila = new Stack<Nodo>();

            // Construimos una pila dependiendo de cómo está especificado el cromosoma
            ConstruirArbolDeUnCromosomas(crom.ListaGenes);

            // Desapilamos una vez que el árbol este construido
            Nodo tree = nodoPila.Pop();

            // Una vez construido el árbol, arbol podemos calcular las posiciones de las piezas
            // dependiendo de su relación con otras piezas (operadores H y V)
            CalcularPosiciones(tree);
            crom.Tree = tree;
            CalcularYGuardarFitness(crom, tree, listaPiezas);
        }

        public void ConstruirArbolDeUnCromosomas(List<String> chromosome)
        {
            for (int i = 0; i < chromosome.Count; ++i)
            {
                int n = -1;
                // Si el gen del cromosoma es una pieza, entonces necesitamos obtener esa pieza
                if (Utilities.EsNumero(chromosome[i], out n))
                {
                    // Recuerda: El segundo parámetro de la pila es el id, empezando de 0 (es por ello que restamos 1)
                    // Por ejemplo, si el cromosoma es 3, entonces queremos apilar la pieza numero 2
                    ApilaPieza(listaPiezas, n - 1);
                }
                else // not a number -> V or H
                {
                    DesapilaOperadorYApilaBloque(chromosome[i]);
                }
            }
        }

        // Apila el elemento que está en la posición "i" de la lista de piezas en la pila, como un nodo (árbol)
        // Este método recibe una lista de piezas.

        // No calcula las posiciones, pues están se calcularán cuando el árbol esté completamente construido
        // El "id" se refiere a la posición de la pieza en a lista, contando a partir de 0.
        public void ApilaPieza(List<Rectangulo> miListaPiezas, int id)
        {
            // Obten las dimensiones de la pieza
            float rect_ancho = miListaPiezas[id].W;
            float rect_alto = miListaPiezas[id].H;

            // Crea un rectángulo dummy, que representará a un bloque.
            Rectangulo piezaRectangulo = new Rectangulo(0, 0, rect_ancho, rect_alto);
            piezaRectangulo.Id = id;

            // Crea el nodo a partir del bloque.
            Nodo node = new Nodo(piezaRectangulo);

            // Apila el nodo a la pila
            nodoPila.Push(node);
        }

        // Como recibe un operador, desapila los dos últimos nodos,
        // opera sobre ambos a partir del operador y apila el resultado, como un bloque
        public void DesapilaOperadorYApilaBloque(String op)
        {
            // Desapila los dos nodos para obtener la dimensión de sus bloques
            Nodo primerNodo = nodoPila.Pop();
            Nodo segundoNodo = nodoPila.Pop();

            Rectangulo piezaIntegrada;

            // The procedure depends on the type of operator given
            if (op == "H")
            {
                float anchoIntegrado = primerNodo.Rect.W + segundoNodo.Rect.W;
                float altoIntegrado = Math.Max(primerNodo.Rect.H, segundoNodo.Rect.H);
                piezaIntegrada = new Rectangulo(0, 0, anchoIntegrado, altoIntegrado);
            }
            else if (op == "V")
            {
                float anchoIntegrado = Math.Max(primerNodo.Rect.W, segundoNodo.Rect.W);
                float altoIntegrado = primerNodo.Rect.H + segundoNodo.Rect.H;
                piezaIntegrada = new Rectangulo(0, 0, anchoIntegrado, altoIntegrado);
            }
            else
            {
                throw new System.ArgumentException("El parámetr solo puede ser \"H\" o \"V\"", "op (operador)");
            }

            // Create a rectangle node with the integrated rectangle
            Nodo integrated_rectangle_node = new Nodo(piezaIntegrada);

            // Set how has been made to set positions afterwards
            integrated_rectangle_node.TipoIntegracion = op;

            // Make the childs be the rectangles it has been made of
            integrated_rectangle_node.Izquierdo = segundoNodo;
            integrated_rectangle_node.Derecho = primerNodo;

            nodoPila.Push(integrated_rectangle_node);
        }

        // sets the positions of the pieces doing a inorder traversal to the tree (once built)
        public void CalcularPosiciones(Nodo root)
        {
            // BASE CASE: if this node is a leaf, do not do anything
            if (root.Izquierdo == null)
            {
                return;
            }
            // GENERAL CASE: if this node has childs, set its positions depending on this node's integration type
            // Note: Since the positions are always relative to parent, they always start in (0, 0) and it is the parent which changes their actual position
            // the traversal does not matter in this case (?)
            else
            {
                // Recursive calls first for a post-order traversal
                CalcularPosiciones(root.Izquierdo);
                CalcularPosiciones(root.Derecho);

                // Then, do the actions
                root.Izquierdo.Rect.X = 0;
                root.Izquierdo.Rect.Y = 0;

                if (root.TipoIntegracion == "V")
                {
                    // if it is V integrated, the second (right child) piece is put under the first (left child) one
                    root.Derecho.Rect.X = 0;
                    root.Derecho.Rect.Y = 0 + root.Izquierdo.Rect.H;
                }
                else if (root.TipoIntegracion == "H")
                {
                    // if it is H integrated, the second piece is put to the right of the first one
                    root.Derecho.Rect.X = 0 + root.Izquierdo.Rect.W;
                    root.Derecho.Rect.Y = 0;
                }
            }
        }

        // Área beneficiada
        public void CalcularYGuardarFitness(Cromosoma chr, Nodo tree, List<Rectangulo> miListaPiezas)
        {
            double pesoFactorMinimizacion = campo_min_rect_factor;
            double pesoFactorCuadratura = campo_cuadratura_factor;

            // Obtener el área de todas las piezas            
            double w, h;
            double areaDeLasPiezas = 0;
            foreach (Rectangulo piezas in miListaPiezas)
            {
                w = piezas.W;
                h = piezas.H;
                areaDeLasPiezas += w * h;
            }

            // Obtener el área de todo el rectángulo
            double areaOcupadaPiezas_ancho = tree.Rect.W;
            double areaOcupadaPiezas_alto = tree.Rect.H;
            double areaOcupadaPiezas = areaOcupadaPiezas_ancho * areaOcupadaPiezas_alto;

            // Obtener la proporción
            double proporcion = areaDeLasPiezas / areaOcupadaPiezas;

            // Calcular que tan "cuadrada" es la pieza integrada.
            // Es decir, el mínimo valor entre W y H, dividido por el del otro (el máximo)
            double square_factor = Math.Min(areaOcupadaPiezas_ancho, areaOcupadaPiezas_alto) / Math.Max(areaOcupadaPiezas_ancho, areaOcupadaPiezas_alto);

            double fitness = (proporcion * pesoFactorMinimizacion) + (square_factor * pesoFactorCuadratura);

            chr.Fitness = fitness;
            //fitness = fitness * Math.Pow(10, 6);
        }
    }
}
