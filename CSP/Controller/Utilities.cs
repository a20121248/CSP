using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Controller
{
    static class Utilities
    {
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
                    foreach (string second_el in myDic[element])
                    {
                        if (!entry.Value.Contains(second_el) && second_el != entry.Key)
                        {
                            not_processed.Add(second_el);
                            entry.Value.Add(second_el);
                        }
                    }

                }

            }
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

        public static void ImprimirLista(List<string> myList)
        {
            string list = "";
            for (int i = 0; i < myList.Count; i++)
            {
                list += myList[i];
                list += " ";
            }
            //Debug.Log(list);
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

        public static void ImprimirLista_Antiguo(List<string> miLista)
        {
            for (int i = 0; i < miLista.Count; i++)
            {
                //Debug.Log("ELEMENT Number " + i + ": " + miLista[i]);
            }
        }

        public static bool EsNumero(String str, out int n)
        {
            return int.TryParse(str, out n);
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
    }
}
