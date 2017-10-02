using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Controller;
using CSP.Model;

namespace CSP.Controller
{
    class AlgoritmoStocks
    {
        public List<Stock> listaStocks;

        public AlgoritmoStocks(List<Stock> listaStocks, Nodo arbol)
        {
            this.listaStocks = listaStocks;
            foreach (Stock stock in this.listaStocks)
            {
                stock.Arbol = null;
            }
            Algoritmo(arbol);

            Algoritmo2();
        }

        public String SePuedeInsertar(Rectangulo pieza, Stock stock)
        {
            float pieza_ancho = stock.Arbol.Rect.W;
            float pieza_alto = stock.Arbol.Rect.H;

            // Integrar horizontal
            float ancho_horizontal = pieza_ancho + pieza.W;
            float alto_horizontal = Math.Max(pieza_alto, pieza.H);

            // Integrar vertical
            float ancho_vertical = Math.Max(pieza_ancho, pieza.W);
            float alto_vertical = pieza_alto + pieza.H;

            if (ancho_horizontal <= stock.W &&
                alto_horizontal <= stock.H)
            {
                return "H";
            }
            else if (ancho_vertical <= stock.W &&
                     alto_vertical <= stock.H)
            {
                return "V";
            }
            else
            {
                return "No";
            }
        }

        public void InsertarPiezaEnUnStock(Rectangulo pieza)
        {
            foreach (Stock stock in listaStocks)
            {
                Nodo arbol = stock.Arbol;
                Nodo segundoNodo = new Nodo(new Rectangulo(pieza));
                // Si el stock está vacío, lo inserto directamente
                if (arbol == null)
                {
                    segundoNodo.Rect.X = 0;
                    segundoNodo.Rect.Y = 0;
                    stock.Arbol = segundoNodo;
                    return;
                }
                // Si tiene piezas, entonces busco el operador de
                // integración de modo que ocupe la menor área.
                else
                {
                    Rectangulo piezaIntegrada;
                    // Devuelve H o V
                    String tipoIntegracion = SePuedeInsertar(pieza, stock);
                    
                    // Integrar con operador H
                    if (tipoIntegracion == "H")
                    {
                        float anchoIntegrado = arbol.Rect.W + segundoNodo.Rect.W;
                        float altoIntegrado = Math.Max(arbol.Rect.H, segundoNodo.Rect.H);
                        piezaIntegrada = new Rectangulo(0, 0, anchoIntegrado, altoIntegrado);
                    }
                    // Integrar con operador V
                    else if (tipoIntegracion == "V")
                    {
                        float anchoIntegrado = Math.Max(arbol.Rect.W, segundoNodo.Rect.W);
                        float altoIntegrado = arbol.Rect.H + segundoNodo.Rect.H;
                        piezaIntegrada = new Rectangulo(0, 0, anchoIntegrado, altoIntegrado);
                    }
                    // No se puede integrar
                    else
                    {
                        continue;
                    }

                    // Crear un nodo que contiene el rectángulo integrado
                    Nodo nodo_rectanguloIntegrado = new Nodo(piezaIntegrada);

                    // Agrega el tipo de integración al rectángulo
                    nodo_rectanguloIntegrado.TipoIntegracion = tipoIntegracion;

                    // Agrega los hijos izquierdo y derecho al nodo, considerando que es una pila
                    nodo_rectanguloIntegrado.Izquierdo = arbol;
                    nodo_rectanguloIntegrado.Derecho = segundoNodo;

                    Utilitarios.CalcularPosiciones(nodo_rectanguloIntegrado);
                    Utilitarios.CalcularPosicionesAbsolutas(nodo_rectanguloIntegrado, 0, 0);
                    stock.Arbol = nodo_rectanguloIntegrado;

                    return;
                }
            }
        }

        public void Algoritmo2()
        {
            foreach (Stock stock in listaStocks)
            {
                if (stock.Arbol != null)
                {
                    // Calcular las posiciones absolutas
                    Utilitarios.CalcularPosicionesAbsolutas(stock.Arbol, 0, 0);
                    // Convertirlo a lista
                    List<Rectangulo> listaPiezas_Stock = Utilitarios.ConvertirALista(stock.Arbol);

                    foreach (Rectangulo pieza in listaPiezas_Stock)
                    {
                        // Si la pieza cae en una región con defecto
                        if (Utilitarios.SeEncuentraEnLista(pieza, stock.ListaDefectos))
                        {
                            InsertarPiezaEnUnStock(pieza);
                        }
                    }
                }
            }
        }

        public List<Nodo> CrearListaBloques(Nodo raiz)
        {
            List<Nodo> listaNodos = new List<Nodo>();
            if (raiz == null)
            {
                return null;
            }

            int id_padre = 0;
            // Crear una pila vacia y colocar los elemenots ahí
            Stack<Nodo> nodoPila = new Stack<Nodo>();
            raiz.Id = listaNodos.Count + 1; //1
            raiz.IdPadre = id_padre;        //0
            nodoPila.Push(raiz);
            while (nodoPila.Count != 0)
            {
                Nodo nodo = nodoPila.Pop();
                id_padre = nodo.Id;

                listaNodos.Add(nodo);
                if (nodo.Derecho != null)
                {
                    nodoPila.Push(nodo.Derecho);
                    nodo.Derecho.Id = listaNodos.Count + nodoPila.Count;
                    nodo.Derecho.IdPadre = id_padre;
                }
                if (nodo.Izquierdo != null)
                {
                    nodoPila.Push(nodo.Izquierdo);
                    nodo.Izquierdo.Id = listaNodos.Count + nodoPila.Count;
                    nodo.Izquierdo.IdPadre = id_padre;
                }
            }
            return listaNodos;
        }
        
        public static int CompararAreaNodo(Nodo nodo1, Nodo nodo2)
        {
            return nodo1.Area.CompareTo(nodo2.Area);
        }

        public static int CompararAreaStock(Stock stock1, Stock stock2)
        {
            return stock1.Area.CompareTo(stock2.Area);
        }

        public void EliminarSubArboles(List<Nodo> miListaNodos, Nodo nodo_padre)
        {
            if (nodo_padre == null)
            {
                return;
            }

            miListaNodos.RemoveAll(nodo => nodo.IdPadre == nodo_padre.Id);

            EliminarSubArboles(miListaNodos, nodo_padre.Izquierdo);
            EliminarSubArboles(miListaNodos, nodo_padre.Derecho);
        }

        public void Algoritmo(Nodo arbol)
        {
            // En esta funcion creo todos los arboles posibles en un recorrido postorden
            List<Nodo> listaNodos = CrearListaBloques(arbol);

            // Ahora selecciono los adecuados
            // Ordeno la lista de mayor a menor en base al area que ocupan (importante!)
            Comparison<Nodo> compNodo = new Comparison<Nodo>(AlgoritmoStocks.CompararAreaNodo);
            listaNodos.Sort(compNodo);
            listaNodos.Reverse();

            // Ordeno los stocks de mayor a menor tambien.
            Comparison<Stock> compStock = new Comparison<Stock>(AlgoritmoStocks.CompararAreaStock);
            listaStocks.Sort(compStock);
            listaStocks.Reverse();

            int i = 0;
            while (listaNodos.Count != 0)
            {
                Nodo nodo = listaNodos[0];
                if (nodo.Rect.W <= listaStocks[i].W &&
                    nodo.Rect.H <= listaStocks[i].H)
                {
                    nodo.Rect.X = 0;
                    nodo.Rect.Y = 0;
                    listaStocks[i].Arbol = nodo;
                    EliminarSubArboles(listaNodos, nodo);
                    ++i;
                }
                listaNodos.RemoveAt(0);
            }
        }
    }
}
