﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Model
{
    public class Rectangulo
    {
        private int id;
        private float h;
        private float x;
        private float y;
        private float w;
        private float x_abs;
        private float y_abs;

        public Rectangulo(Rectangulo otro)
        {
        this.id = otro.id;
        this.h = otro.h;
        this.w = otro.w;
        this.x = otro.x;
        this.y = otro.y;
        this.x_abs = otro.x_abs;
        this.y_abs = otro.y_abs;
    }

        public Rectangulo(float x, float y, float w, float h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public Rectangulo(int id, float x, float y, float w, float h)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public int Id { get => id; set => id = value; }
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float W { get => w; set => w = value; }
        public float H { get => h; set => h = value; }
        public float X_abs { get => x_abs; set => x_abs = value; }
        public float Y_abs { get => y_abs; set => y_abs = value; }

        public float ObtenerW(String rotacion)
        {
            if (rotacion.Equals("R")) // sí está rotada
            {
                return h;
            }
            return w;
        }

        public float ObtenerH(String rotacion)
        {
            if (rotacion.Equals("R")) // sí está rotada
            {
                return w;
            }
            return h;
        }

        public Boolean EstaCubierto(Rectangulo otro)
        {
            return otro.w >= this.w && otro.h >= this.h;
        }

        public Boolean TieneMismoTamanho(Rectangulo otro)
        {
            return this.w == otro.w && this.h == otro.h;
        }
    }

}
