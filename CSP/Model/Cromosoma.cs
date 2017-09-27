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
        private Nodo arbol;
        private double fitness;

        public String Cadena { get => Utilitarios.ImprimirLista(listaGenes);}

        // Constructores

        // Void constructor
        public Cromosoma()
        {

        }

        // Defined constructor
        public Cromosoma(List<String> chromosome)
        {
            listaGenes = chromosome;
        }

        // Crear un cromosoma válido aleatorio
        public Cromosoma(int numGenes, System.Random rnd)
        {
            List<String> listaGenes = new List<String>();

            int numPiezas = (numGenes + 1) / 2;       // Número de piezas
            int numOperadores = numGenes - numPiezas; // Número de operadores

            // Llenamos la lista de piezas con numeros de pieza
            List<String> listaPiezasDisponibles = new List<String>();
            for (int i = 1; i <= numPiezas; ++i)
            {
                listaPiezasDisponibles.Add(i.ToString());
            }

            // Las primeras dos piezas siempre serán piezas, seguidos de un operador
            IntroducirPiezaAleatoria(listaGenes, listaPiezasDisponibles, rnd);
            IntroducirPiezaAleatoria(listaGenes, listaPiezasDisponibles, rnd);
            IntroducirOperadorAleatorio(listaGenes, rnd);

            // A partir de ahora, necesitamos completar el cromosoma pero satisfaciendo restricciones.
            // Entonces, para los siguientes genes del cromosoma
            for (int i = listaGenes.Count; i < numGenes; ++i)
            {
                // Revisar si el No >= Np+1 (solo en la izquierda)
                int actual_np = 0;
                int actual_no = 0;
                int n;
                for (int j = 0; j < listaGenes.Count; ++j)
                {
                    if (Utilitarios.EsPieza(listaGenes[j], out n)) {
                        ++actual_np;
                    }
                    else
                    {
                        ++actual_no;
                    }
                }

                // Si estamos en el límite de la restricción, necesitamos poner una pieza
                if (numOperadores == numPiezas + 1)
                {                    
                    IntroducirPiezaAleatoria(listaGenes, listaPiezasDisponibles, rnd);
                }
                // Si la restricción no está en el límite, se puede introducir una pieza u operador
                else
                {
                    // Si no hay más piezas en la lista, entonces introducimos un operador
                    if (listaPiezasDisponibles.Count == 0)
                    {
                        IntroducirOperadorAleatorio(listaGenes, rnd);
                    }
                    // Si hay piezas en la lista, entonces hay 50% de probabilidad de introducir una pieza u operador
                    else
                    {
                        int numeroAleatorio = rnd.Next(1, 3);
                        // Introducir pieza
                        if (numeroAleatorio == 1)
                        {
                            IntroducirPiezaAleatoria(listaGenes, listaPiezasDisponibles, rnd);
                        }
                        // Introducir operador
                        if (numeroAleatorio == 2)
                        {
                            IntroducirOperadorAleatorio(listaGenes, rnd);
                            // Verificamos que el operador introduce genera un cromosoma postfijo
                            try
                            {
                                Stack<System.Object> testPila = new Stack<System.Object>();
                                ConstruirPilaDeCromosomaSimple(listaGenes, testPila);
                            }
                            // De lo contrario, removemos el operador introducido e introducimos una pieza
                            catch (Exception)
                            {
                                listaGenes.RemoveAt(listaGenes.Count - 1);
                                IntroducirPiezaAleatoria(listaGenes, listaPiezasDisponibles, rnd);
                            }
                        }
                    }
                }
            }
            this.listaGenes = listaGenes;
        }

        public List<string> ListaGenes { get => listaGenes; set => listaGenes = value; }
        public Nodo Arbol { get => arbol; set => arbol = value; }
        public double Fitness { get => fitness; set => fitness = value; }

        // Introduce a un cromosoma una pieza aleatoria de una lista de piezas disponibles
        public void IntroducirPiezaAleatoria(List<String> listaGenes, List<String> listaPiezasDisponibles, System.Random rnd)
        {
            String genPieza = "";
            // Seleccionar aleatoriamente una pieza
            int nroPieza = rnd.Next(0, listaPiezasDisponibles.Count);
            genPieza += listaPiezasDisponibles[nroPieza];
            // Seleccionar aleatoriamente si la pieza rota o no
            int eleccionRotacion = rnd.Next(1, 3);
            if (eleccionRotacion == 1) // La pieza no rota 90°
            {
                genPieza += "N";
            }
            else if (eleccionRotacion == 2) // La pieza rota 90°
            {
                genPieza += "R";
            }

            // Agrega la pieza al cromosoma
            listaGenes.Add(genPieza);

            // Remueve la pieza de la lista de piezas para que no sea anhadida posteriormente
            listaPiezasDisponibles.Remove(listaPiezasDisponibles[nroPieza]);
        }

        // Introduce a un cromosoma un operador aleatorio
        public void IntroducirOperadorAleatorio(List<String> listaGenes, System.Random rnd)
        {
            // Seleccionar aleatoriamente un operador H o V
            int eleccionOperador = rnd.Next(1, 3);
            if (eleccionOperador == 1) // Operador H
            {
                listaGenes.Add("H");
            }
            else if (eleccionOperador == 2) // Operador V
            {
                listaGenes.Add("V");
            }
        }

        // Necesarios para saber si un cromosoma es válido.
        public void ConstruirPilaDeCromosomaSimple(List<String> cromosoma, Stack<System.Object> pila)
        {
            for (int i = 0; i < cromosoma.Count; ++i)
            {
                int n = -1;
                // Si es una pieza, lo apilo
                if (Utilitarios.EsPieza(cromosoma[i], out n))
                {
                    Utilitarios.ApilarSimple(pila);
                }
                // Si es un operador, desapilo dos piezas y las opero
                else
                {
                    Utilitarios.DesapilarYApilarSimple(pila);
                }
            }
        }


    }

}
