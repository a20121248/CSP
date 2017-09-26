using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSP.Controller;
using CSP.Model;
using System.Reflection;

namespace CSP.View
{
    public partial class FormResultadoStockInfinito : Form
    {
        private Cromosoma cromosoma;
        public List<Pen> listaPencils;
        public List<Brush> listaBrushes;
        public Pen penRojo, penNegro;

        public FormResultadoStockInfinito(Cromosoma cromosoma)
        {
            InitializeComponent();
            InicializarColores();
            this.cromosoma = cromosoma;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(splitContainer1.Panel1.BackColor);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Nodo arbol_solucion = listaArboles[i++];
            Nodo arbol_solucion = this.cromosoma.Tree;
            if (arbol_solucion != null)
            {
                DibujarPiezas(e.Graphics, arbol_solucion,
                              20 + arbol_solucion.Rect.X,
                              20 + arbol_solucion.Rect.Y);
            }
        }

        private void InicializarColores()
        {
            penRojo = new Pen(new SolidBrush(Color.Red), 1);
            penNegro = new Pen(new SolidBrush(Color.Black), 1);

            Random rnd = new Random();
            int random;

            listaBrushes = new List<Brush>();
            listaPencils = new List<Pen>();

            Type brushesType = typeof(Brushes);
            Type pencilsType = typeof(Pens);
            PropertyInfo[] brushesProperties = brushesType.GetProperties();
            PropertyInfo[] pencilsProperties = pencilsType.GetProperties();
            //for (int i = 0; i < listaPiezas.Count; ++i)
            for (int i = 0; i < 20; ++i)
            {
                random = rnd.Next(brushesProperties.Length);
                Brush brush = Brushes.Transparent;
                brush = (Brush)brushesProperties[random].GetValue(null, null);
                listaBrushes.Add(brush);

                random = rnd.Next(pencilsProperties.Length);
                Pen pencil;
                pencil = (Pen)pencilsProperties[random].GetValue(null);
                listaPencils.Add(pencil);
            }
        }

        private void DibujarPiezas(Graphics gr, Nodo arbol, float x, float y)
        {
            Rectangulo pieza = arbol.Rect;
            int pieza_id = pieza.Id;
            // Si es una hoja, entonces dibujar la pieza y detener la recursion
            if (arbol.Izquierdo == null && arbol.Derecho == null)
            {
                float x_abs_pieza = arbol.Rect.X + x;
                float y_abs_pieza = arbol.Rect.Y + y;
                float ancho_pieza = arbol.Rect.W;
                float alto_pieza = arbol.Rect.H;
                //float ancho_pieza = listaPiezas[pieza_id].W;
                //float alto_pieza = listaPiezas[pieza_id].H;

                Rectangle rect = new Rectangle((int)x_abs_pieza, (int)y_abs_pieza, (int)ancho_pieza, (int)alto_pieza);
                gr.FillRectangle(listaBrushes[pieza_id], rect);
                gr.DrawRectangle(penNegro, x_abs_pieza, y_abs_pieza, ancho_pieza, alto_pieza);
                gr.DrawString(string.Format("{0}", pieza_id + 1),
                                     new Font("Arial", 12, FontStyle.Bold), Brushes.Red, x_abs_pieza, y_abs_pieza);
                return;
            }
            // Si es un arbol, llamar de forma recursiva a sus hijos
            else
            {
                float x_abs_bloque = arbol.Rect.X + x;
                float y_abs_bloque = arbol.Rect.Y + y;
                DibujarPiezas(gr, arbol.Izquierdo, x_abs_bloque, y_abs_bloque);
                DibujarPiezas(gr, arbol.Derecho, x_abs_bloque, y_abs_bloque);
            }
        }
    }
}
