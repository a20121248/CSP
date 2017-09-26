using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Model;

namespace CSP.Controller
{
    static class Utilitarios
    {
        public static String RotarPiezaAleatorio(String pieza, System.Random rnd)
        {
            // Si no es pieza devolvemos el operador tal cual
            int n;
            if (!Utilitarios.EsPieza(pieza, out n))
            {
                return pieza;
            }

            String rotacion = Utilitarios.ObtenerRotacion(pieza);
            // Cambiar el sentido, con 50% de probabilidad
            if (rnd.Next(1, 3) == 1)
            {
                rotacion = Utilitarios.CambiarSentido(rotacion);
            }
            return Utilitarios.ObtenerPieza(pieza) + rotacion;
        }

        // sets the positions of the pieces doing a inorder traversal to the tree (once built)
        public static void CalcularPosiciones(Nodo root)
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

        public static Nodo ConstruirArbolDeUnCromosomas(List<String> cromosoma, List<Rectangulo> listaPiezas)
        {
            Stack<Nodo> nodoPila = new Stack<Nodo>();
            for (int i = 0; i < cromosoma.Count; ++i)
            {
                int n = -1;
                // Si el gen es una pieza, entonces la apilamos
                if (Utilitarios.EsPieza(cromosoma[i], out n))
                {
                    // El segundo parámetro de la pila es el id de la pieza empezando desde 0 (es por ello que restamos 1)
                    // Por ejemplo, si es la pieza 3, entonces queremos apilar la pieza en la posición 2
                    ApilarPieza(nodoPila, n - 1, cromosoma[i].Substring(cromosoma[i].Length - 1, 1), listaPiezas);
                }
                else // Si es un operador, desapilamos los dos últimos bloques y operamos
                {
                    if (nodoPila.Count < 2)
                    {
                        int a = 1;
                    }
                    DesapilaOperadorYApilaBloque(nodoPila, cromosoma[i]);
                }
            }
            // En este momento, en la pila solo hay un elemento y sería el árbol construido
            return nodoPila.Pop();
        }

        // Apila el elemento que está en la posición "i" de la lista de piezas en la pila, como un nodo (árbol)
        // No calcula las posiciones, pues están se calcularán cuando el árbol esté completamente construido
        // El "id" se refiere a la posición de la pieza en a lista, contando a partir de 0.
        public static void ApilarPieza(Stack<Nodo> nodoPila, int id, String rotacion, List<Rectangulo> listaPiezas)
        {
            // Obten las dimensiones de la pieza
            float ancho = listaPiezas[id].ObtenerW(rotacion);
            float alto = listaPiezas[id].ObtenerH(rotacion);

            // Crea un rectángulo dummy, que representará a un bloque.
            Rectangulo piezaRectangulo = new Rectangulo(id, 0, 0, ancho, alto);

            // Crea el nodo a partir del bloque.
            Nodo node = new Nodo(piezaRectangulo);

            // Apila el nodo a la pila
            nodoPila.Push(node);
        }

        // Como recibe un operador, desapila los dos últimos nodos,
        // opera sobre ambos a partir del operador y apila el resultado, como un bloque
        public static void DesapilaOperadorYApilaBloque(Stack<Nodo> nodoPila, String op)
        {
            // Desapila los dos nodos para obtener la dimensión de sus bloques
            Nodo primerNodo = nodoPila.Pop();
            Nodo segundoNodo = nodoPila.Pop();

            Rectangulo piezaIntegrada;
            // La integración depende del tipo de operador, si es H o V
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
                throw new System.ArgumentException("El parámetro solo puede ser \"H\" o \"V\"", "op (operador)");
            }

            // Crear un nodo que contiene el rectángulo integrado
            Nodo nodo_rectanguloIntegrado = new Nodo(piezaIntegrada);

            // Agrega el tipo de integración al rectángulo
            nodo_rectanguloIntegrado.TipoIntegracion = op;

            // Agrega los hijos izquierdo y derecho al nodo, considerando que es una pila
            nodo_rectanguloIntegrado.Izquierdo = segundoNodo;
            nodo_rectanguloIntegrado.Derecho = primerNodo;

            // Finalmente agrega el árbol a la pila
            nodoPila.Push(nodo_rectanguloIntegrado);
        }

        public static void unfold_relationships(Dictionary<string, List<string>> myDic)
        {
            foreach (KeyValuePair<string, List<string>> entry in myDic)
            {
                // entry.Value is the 2nd List
                List<string> not_processed = new List<string>();

                // copy list in entry.Value to not_processed
                foreach (string el in entry.Value)
                {
                    not_processed.Add(el);
                }

                // Now, in not_processed we have the first list, we now need to iterate over it until it is clear

                while (not_processed.Count != 0)
                {
                    // Take and remove
                    string element = not_processed[0];
                    not_processed.RemoveAt(0);

                    // add to not processed and the entry.Value the elements of the list VALUE (peek dict value) if they are not in the 2nd List
                    foreach (string second_el in myDic[element.Substring(0,element.Length-1)])
                    {
                        if (!entry.Value.Contains(second_el) && second_el.Substring(0, second_el.Length - 1) != entry.Key)
                        {
                            not_processed.Add(second_el);
                            entry.Value.Add(second_el);
                        }
                    }

                }

            }
        }

        public static double CalcularFitness(Nodo tree, List<Rectangulo> listaPiezas, double pesoFactorMinimizacion, double pesoFactorCuadratura)
        {
            // Obtener el área de todas las piezas
            double anchoPieza, altoPieza, areaTotalPiezas = 0;
            foreach (Rectangulo piezas in listaPiezas)
            {
                anchoPieza = piezas.W;
                altoPieza = piezas.H;
                areaTotalPiezas += anchoPieza * altoPieza;
            }

            // Obtener el área ocupada por todas las piezas
            double anchoOcupadoTodasPiezas = tree.Rect.W;
            double altoOcupadoTodasPiezas = tree.Rect.H;
            double areaOcupadaPiezas = anchoOcupadoTodasPiezas * altoOcupadoTodasPiezas;

            // Obtener la proporción
            double proporcion = areaTotalPiezas / areaOcupadaPiezas;

            // Calcular que tan "cuadrada" es la pieza integrada.
            // Es decir, el mínimo valor entre W y H, dividido por el del otro (el máximo)
            double proporcion_cuadrado = Math.Min(anchoOcupadoTodasPiezas, altoOcupadoTodasPiezas) / Math.Max(anchoOcupadoTodasPiezas, altoOcupadoTodasPiezas);

            double fitness = (proporcion * pesoFactorMinimizacion) + (proporcion_cuadrado * pesoFactorCuadratura);

            return fitness;
        }

        public static void ImprimirDiccionario(Dictionary<string, List<string>> miDic)
        {
            foreach (KeyValuePair<string, List<string>> entry in miDic)
            {
                //Debug.Log("KEY: " + entry.Key);
                foreach (string result in entry.Value)
                {
                    //Debug.Log("     VALUE: " + result);
                }
            }
        }

        public static String ImprimirLista(List<String> listaStr)
        {
            String str = "";
            for (int i = 0; i < listaStr.Count; ++i)
            {
                str += listaStr[i];
                str += " ";
            }
            return str;
        }

        public static string ConvertirListaACadena(List<string> miLista)
        {
            string lista = "";
            for (int i = 0; i < miLista.Count; i++)
            {
                lista += miLista[i];
                lista += " ";
            }
            return lista;
        }

        public static bool EsNumero(String str, out int n)
        {
            return int.TryParse(str, out n);
        }

        public static bool EsPieza(String str)
        {
            if (str.Length > 1)
            {
                return true;
            }
            return false;
        }

        public static bool EsPieza(String str, out int n)
        {
            if (str.Length >= 2)
            {
                return int.TryParse(str.Substring(0, str.Length - 1), out n);
            }
            return int.TryParse(str, out n);
        }

        public static String ObtenerPieza(String str)
        {
            return str.Substring(0, str.Length-1);
        }

        public static String ObtenerRotacion(String str)
        {
            return str.Substring(str.Length - 1, 1);
        }

        public static String CambiarSentido(String str)
        {
            if (str == "R")
            {
                return "N";
            }
            return "R";
        }

        public static bool CompararPiezas(String pieza1, String pieza2)
        {
            return Utilitarios.ObtenerPieza(pieza1).Equals(Utilitarios.ObtenerPieza(pieza2));
        }

        public static bool EsNumero(String str)
        {
            int n;
            return int.TryParse(str, out n);
        }

        public static double ObtenerAleatorioEntre(double minValue, double maxValue, System.Random rnd)
        {
            return rnd.NextDouble() * (maxValue - minValue) + minValue;
        }

        // Operaciones de pila para el proceso de construcción
        public static void ApilarSimple(Stack<System.Object> pila)
        {
            System.Object o = new System.Object();
            pila.Push(o);
        }

        public static void DesapilarYApilarSimple(Stack<System.Object> pila)
        {
            // Desapilar dos piezas
            System.Object primerNodo = pila.Pop();
            System.Object segundoNodo = pila.Pop();
            // Apilar una pieza (ficticia)
            System.Object o = new System.Object();
            pila.Push(o);
        }
    }
}
