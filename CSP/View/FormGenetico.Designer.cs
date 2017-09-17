namespace CSP.View
{
    partial class FormGenetico
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtProbabilidadMutacion = new System.Windows.Forms.TextBox();
            this.txtTamanhoPoblacion = new System.Windows.Forms.TextBox();
            this.txtPesoFactorCuadratura = new System.Windows.Forms.TextBox();
            this.txtPesoMinimizarRectangulo = new System.Windows.Forms.TextBox();
            this.lblProbabilidadMutacion = new System.Windows.Forms.Label();
            this.lblTamanhoPoblacion = new System.Windows.Forms.Label();
            this.lblPesoMinimizarRectangulo = new System.Windows.Forms.Label();
            this.lblPesoFactorCuadratura = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnVerResultado = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtProbabilidadMutacion
            // 
            this.txtProbabilidadMutacion.Location = new System.Drawing.Point(224, 33);
            this.txtProbabilidadMutacion.Name = "txtProbabilidadMutacion";
            this.txtProbabilidadMutacion.Size = new System.Drawing.Size(76, 20);
            this.txtProbabilidadMutacion.TabIndex = 0;
            this.txtProbabilidadMutacion.Text = "0.2";
            // 
            // txtTamanhoPoblacion
            // 
            this.txtTamanhoPoblacion.Location = new System.Drawing.Point(224, 70);
            this.txtTamanhoPoblacion.Name = "txtTamanhoPoblacion";
            this.txtTamanhoPoblacion.Size = new System.Drawing.Size(76, 20);
            this.txtTamanhoPoblacion.TabIndex = 1;
            this.txtTamanhoPoblacion.Text = "90";
            // 
            // txtPesoFactorCuadratura
            // 
            this.txtPesoFactorCuadratura.Location = new System.Drawing.Point(224, 150);
            this.txtPesoFactorCuadratura.Name = "txtPesoFactorCuadratura";
            this.txtPesoFactorCuadratura.Size = new System.Drawing.Size(76, 20);
            this.txtPesoFactorCuadratura.TabIndex = 3;
            this.txtPesoFactorCuadratura.Text = "3";
            // 
            // txtPesoMinimizarRectangulo
            // 
            this.txtPesoMinimizarRectangulo.Location = new System.Drawing.Point(224, 110);
            this.txtPesoMinimizarRectangulo.Name = "txtPesoMinimizarRectangulo";
            this.txtPesoMinimizarRectangulo.Size = new System.Drawing.Size(76, 20);
            this.txtPesoMinimizarRectangulo.TabIndex = 2;
            this.txtPesoMinimizarRectangulo.Text = "5";
            // 
            // lblProbabilidadMutacion
            // 
            this.lblProbabilidadMutacion.AutoSize = true;
            this.lblProbabilidadMutacion.Location = new System.Drawing.Point(44, 36);
            this.lblProbabilidadMutacion.Name = "lblProbabilidadMutacion";
            this.lblProbabilidadMutacion.Size = new System.Drawing.Size(129, 13);
            this.lblProbabilidadMutacion.TabIndex = 4;
            this.lblProbabilidadMutacion.Text = "Probabilidad de mutación:";
            // 
            // lblTamanhoPoblacion
            // 
            this.lblTamanhoPoblacion.AutoSize = true;
            this.lblTamanhoPoblacion.Location = new System.Drawing.Point(44, 73);
            this.lblTamanhoPoblacion.Name = "lblTamanhoPoblacion";
            this.lblTamanhoPoblacion.Size = new System.Drawing.Size(124, 13);
            this.lblTamanhoPoblacion.TabIndex = 5;
            this.lblTamanhoPoblacion.Text = "Tamaño de la población:";
            // 
            // lblPesoMinimizarRectangulo
            // 
            this.lblPesoMinimizarRectangulo.AutoSize = true;
            this.lblPesoMinimizarRectangulo.Location = new System.Drawing.Point(44, 113);
            this.lblPesoMinimizarRectangulo.Name = "lblPesoMinimizarRectangulo";
            this.lblPesoMinimizarRectangulo.Size = new System.Drawing.Size(122, 13);
            this.lblPesoMinimizarRectangulo.TabIndex = 6;
            this.lblPesoMinimizarRectangulo.Text = "Peso minizar rectángulo:";
            // 
            // lblPesoFactorCuadratura
            // 
            this.lblPesoFactorCuadratura.AutoSize = true;
            this.lblPesoFactorCuadratura.Location = new System.Drawing.Point(44, 153);
            this.lblPesoFactorCuadratura.Name = "lblPesoFactorCuadratura";
            this.lblPesoFactorCuadratura.Size = new System.Drawing.Size(118, 13);
            this.lblPesoFactorCuadratura.TabIndex = 7;
            this.lblPesoFactorCuadratura.Text = "Peso factor cuadratura:";
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(129, 192);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(111, 23);
            this.btnIniciar.TabIndex = 7;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnVerResultado
            // 
            this.btnVerResultado.Location = new System.Drawing.Point(129, 226);
            this.btnVerResultado.Name = "btnVerResultado";
            this.btnVerResultado.Size = new System.Drawing.Size(111, 23);
            this.btnVerResultado.TabIndex = 8;
            this.btnVerResultado.Text = "Ver resultado";
            this.btnVerResultado.UseVisualStyleBackColor = true;
            this.btnVerResultado.Click += new System.EventHandler(this.btnVerResultado_Click);
            // 
            // FormGenetico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 261);
            this.Controls.Add(this.btnVerResultado);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.lblPesoFactorCuadratura);
            this.Controls.Add(this.lblPesoMinimizarRectangulo);
            this.Controls.Add(this.lblTamanhoPoblacion);
            this.Controls.Add(this.lblProbabilidadMutacion);
            this.Controls.Add(this.txtPesoMinimizarRectangulo);
            this.Controls.Add(this.txtPesoFactorCuadratura);
            this.Controls.Add(this.txtTamanhoPoblacion);
            this.Controls.Add(this.txtProbabilidadMutacion);
            this.Name = "FormGenetico";
            this.Text = "Módulo Algoritmo Genético";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProbabilidadMutacion;
        private System.Windows.Forms.TextBox txtTamanhoPoblacion;
        private System.Windows.Forms.TextBox txtPesoFactorCuadratura;
        private System.Windows.Forms.TextBox txtPesoMinimizarRectangulo;
        private System.Windows.Forms.Label lblProbabilidadMutacion;
        private System.Windows.Forms.Label lblTamanhoPoblacion;
        private System.Windows.Forms.Label lblPesoMinimizarRectangulo;
        private System.Windows.Forms.Label lblPesoFactorCuadratura;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnVerResultado;
    }
}