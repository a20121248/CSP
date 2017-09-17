using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Model
{
    public class Nodo
    {
        private Nodo izquierdo;
        private Nodo derecho;
        private Rectangulo rect;
        private String tipoIntegracion;

        public Nodo(Rectangulo rect)
        {
            this.rect = rect;
        }

        public Rectangulo Rect { get => rect; set => rect = value; }
        public string TipoIntegracion { get => tipoIntegracion; set => tipoIntegracion = value; }
        public Nodo Izquierdo { get => izquierdo; set => izquierdo = value; }
        public Nodo Derecho { get => derecho; set => derecho = value; }

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
