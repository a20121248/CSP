using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Model
{
    public class Nodo /*: IComparable<Nodo>*/
    {
        private int id;
        private int idPadre;
        private Nodo izquierdo;
        private Nodo derecho;
        private Rectangulo rect;
        private String tipoIntegracion;
        public float Area { get => rect.W * rect.H; }

        public Nodo()
        {
        }

        public Nodo(Rectangulo rect)
        {
            this.rect = rect;
        }

        public Rectangulo Rect { get => rect; set => rect = value; }
        public string TipoIntegracion { get => tipoIntegracion; set => tipoIntegracion = value; }
        public Nodo Izquierdo { get => izquierdo; set => izquierdo = value; }
        public Nodo Derecho { get => derecho; set => derecho = value; }
        public int Id { get => id; set => id = value; }
        public int IdPadre { get => idPadre; set => idPadre = value; }

        /*
        public int CompareTo(Nodo otro)
        {
            float areathis = this.rect.W * this.rect.H;
            float areaother = otro.rect.W * otro.rect.W;

            return areathis.CompareTo(areaother);
           
        }
        */
        /*
        public void SetTipoIntegracion(String tipo)
        {
        if (tipo != "H" || tipo != "V")
        {
        throw new System.ArgumentException("La llamado del método SetTipoIntegracion() no admite otro operadora diferente a H o V: El parámetro actualmente tiene como valor a: " + tipo);
        }
        tipoIntegracion = tipo;
        }
        */

        public bool EstaIntegrado(Nodo node)
        {
            return izquierdo == null ? false : true;
        }
    }
}
