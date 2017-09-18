using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP.Model;

namespace CSP.Model
{
    class Stock
    {
        private int id;
        private float w, h;
        private List<Rectangulo> listaDefectos;

        public Stock(int id, float w, float h)
        {
            this.id = id;
            this.w = w;
            this.h = h;
            this.listaDefectos = new List<Rectangulo>();
        }

        public int Id { get => id; set => id = value; }
        public float W { get => w; set => w = value; }
        public float H { get => h; set => h = value; }
        public List<Rectangulo> ListaDefectos { get => listaDefectos; set => listaDefectos = value; }
    }
}
