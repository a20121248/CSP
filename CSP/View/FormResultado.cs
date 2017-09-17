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
    internal partial class FormResultado : Form
    {
        public Nodo arbol_solucion;
        public List<Rectangulo> listaPiezas;
        public List<Pen> listaPencils;
        public List<Brush> listaBrushes;

        private void InicializarColores()
        {
            listaBrushes = new List<Brush>();
            Random rnd = new Random();
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            for (int i = 0; i < listaPiezas.Count; ++i)
            {
                int random = rnd.Next(properties.Length);
                Brush brush = Brushes.Transparent;
                brush = (Brush)properties[random].GetValue(null, null);
                listaBrushes.Add(brush);
            }
        }

        public FormResultado(FormGenetico formGenetico)
        {
            InitializeComponent();
            txtMejorSolucion.Text = Utilities.ConvertirListaACadena(formGenetico.solucion);
            txtFitness.Text = string.Format("{0:0.00}", formGenetico.fitness);
            arbol_solucion = formGenetico.arbol_solucion;
            listaPiezas = formGenetico.listaPiezas;
            InicializarColores();
        }

        private void DibujarPiezas(Graphics gr, Pen pen, Nodo arbol, float x, float y)
        {
            Rectangulo pieza = arbol.Rect;
            int pieza_id = pieza.Id;
            // Si es una hoja, entonces dibujar la pieza y detener la recursion
            if (arbol.Izquierdo == null && arbol.Derecho == null)
            {
                float x_abs_pieza = arbol.Rect.X + x;
                float y_abs_pieza = arbol.Rect.Y + y;
                float ancho_pieza = listaPiezas[pieza_id].W;
                float alto_pieza = listaPiezas[pieza_id].H;

                Rectangle rect = new Rectangle((int)x_abs_pieza, (int)y_abs_pieza, (int)ancho_pieza, (int)alto_pieza);
                gr.FillRectangle(listaBrushes[pieza_id], rect);
                //gr.DrawRectangle(listaPencils[pieza_id], x_abs_pieza, y_abs_pieza, ancho_pieza, alto_pieza);
                return;
            }
            // Si es un arbol, llamar de forma recursiva a sus hijos
            else
            {
                float x_abs_bloque = arbol.Rect.X + x;
                float y_abs_bloque = arbol.Rect.Y + y;
                DibujarPiezas(gr, pen, arbol.Izquierdo, x_abs_bloque, y_abs_bloque);
                DibujarPiezas(gr, pen, arbol.Derecho, x_abs_bloque, y_abs_bloque);
            }
        }
        /*
        private void Dibujar_Piezas(Graphics gr, Pen pen, Rectangle_Node arbol)
        {
            // Si es una hoja, entonces dibujar la pieza y detener la recursion
            if (arbol.left == null && arbol.right == null)
            {
                Rectangulo pieza = arbol.rect;
                int pieza_id = pieza.get_id();
                gr.DrawRectangle(listaPencils[pieza_id], pieza.x, pieza.y, listaPiezas[pieza_id].w, listaPiezas[pieza_id].h);
                return;
            }
            // Si es un arbol, llamar de forma recursiva a sus hijos
            else
            {
                Dibujar_Piezas(gr, pen, arbol.left);
                Dibujar_Piezas(gr, pen, arbol.right);
            }
        }
        */
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(picCanvas.BackColor);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Brush red = new SolidBrush(Color.Red);
            Pen redPen = new Pen(red, 1);
            DibujarPiezas(e.Graphics, redPen, arbol_solucion, arbol_solucion.Rect.X, arbol_solucion.Rect.Y);
        }

        private void picCanvasPiezas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(picCanvas.BackColor);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int ancho = 40;
            int alto = 15;
            int offset = 30;
            int x = 0;
            int y = 0;
            for (int i = 0; i < listaPiezas.Count; ++i)
            {
                Rectangle rect = new Rectangle(x, y, ancho, alto);
                e.Graphics.FillRectangle(listaBrushes[i], rect);
                e.Graphics.DrawString(string.Format("Pieza #{0}", i + 1),
                                      this.Font, Brushes.Black, new Point(x + ancho + 10, y));
                y = y + offset;
            }
        }
    }
}
