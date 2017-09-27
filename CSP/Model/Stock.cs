using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Model;

namespace CSP.Model
{
    class Stock /* : IComparable<Stock>*/
    {
        private int id;
        private float w, h;
        private List<Rectangulo> listaDefectos;
        private float x;
        private float y;
        private Nodo arbol;

        public Stock(Stock otro)
        {
            this.id = otro.Id;
            this.w = otro.W;
            this.h = otro.H;
            this.listaDefectos = otro.ListaDefectos;
            this.arbol = otro.Arbol;
        }

        public Stock(int id, float w, float h)
        {
            this.id = id;
            this.w = w;
            this.h = h;
            this.listaDefectos = new List<Rectangulo>();
            this.arbol = null;
        }

        /*
        public int CompareTo(Stock otro)
        {
            float areathis = this.w * this.h;
            float areaother = otro.W * otro.W;

            return areathis.CompareTo(areaother);
        }
        */

        public float Area { get => w * h; }
        public int Id { get => id; set => id = value; }
        public float W { get => w; set => w = value; }
        public float H { get => h; set => h = value; }
        public List<Rectangulo> ListaDefectos { get => listaDefectos; set => listaDefectos = value; }
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public Nodo Arbol { get => arbol; set => arbol = value; }
    }
}
