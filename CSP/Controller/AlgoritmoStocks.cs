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
        public List<Nodo> listaNodos;
        public List<Stock> listaStocks;

        public AlgoritmoStocks(List<Stock> listaStocks, Cromosoma cromosoma)
        {
            this.listaStocks = listaStocks;

            Algoritmo(cromosoma);
        }

        public void CrearListaBloques(Nodo raiz)
        {
            this.listaNodos = new List<Nodo>();
            if (raiz == null)
            {
                return;
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

        public void Algoritmo(Cromosoma cromosoma)
        {
            // En esta funcion creo todos los arboles posibles en un recorrido postorden
            CrearListaBloques(cromosoma.Tree);

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
