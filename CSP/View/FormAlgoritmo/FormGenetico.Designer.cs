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
            this.btnVerStockInfinito = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidadElitismo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantMaxGeneraciones = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipoCruce = new System.Windows.Forms.ComboBox();
            this.cmbTipoSeleccion = new System.Windows.Forms.ComboBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtProbabilidadMutacion
            // 
            this.txtProbabilidadMutacion.Location = new System.Drawing.Point(211, 81);
            this.txtProbabilidadMutacion.Name = "txtProbabilidadMutacion";
            this.txtProbabilidadMutacion.Size = new System.Drawing.Size(76, 20);
            this.txtProbabilidadMutacion.TabIndex = 0;
            this.txtProbabilidadMutacion.Text = "0.2";
            // 
            // txtTamanhoPoblacion
            // 
            this.txtTamanhoPoblacion.Location = new System.Drawing.Point(211, 118);
            this.txtTamanhoPoblacion.Name = "txtTamanhoPoblacion";
            this.txtTamanhoPoblacion.Size = new System.Drawing.Size(76, 20);
            this.txtTamanhoPoblacion.TabIndex = 1;
            this.txtTamanhoPoblacion.Text = "90";
            // 
            // txtPesoFactorCuadratura
            // 
            this.txtPesoFactorCuadratura.Location = new System.Drawing.Point(211, 198);
            this.txtPesoFactorCuadratura.Name = "txtPesoFactorCuadratura";
            this.txtPesoFactorCuadratura.Size = new System.Drawing.Size(76, 20);
            this.txtPesoFactorCuadratura.TabIndex = 3;
            this.txtPesoFactorCuadratura.Text = "3";
            // 
            // txtPesoMinimizarRectangulo
            // 
            this.txtPesoMinimizarRectangulo.Location = new System.Drawing.Point(211, 158);
            this.txtPesoMinimizarRectangulo.Name = "txtPesoMinimizarRectangulo";
            this.txtPesoMinimizarRectangulo.Size = new System.Drawing.Size(76, 20);
            this.txtPesoMinimizarRectangulo.TabIndex = 2;
            this.txtPesoMinimizarRectangulo.Text = "5";
            // 
            // lblProbabilidadMutacion
            // 
            this.lblProbabilidadMutacion.AutoSize = true;
            this.lblProbabilidadMutacion.Location = new System.Drawing.Point(44, 84);
            this.lblProbabilidadMutacion.Name = "lblProbabilidadMutacion";
            this.lblProbabilidadMutacion.Size = new System.Drawing.Size(129, 13);
            this.lblProbabilidadMutacion.TabIndex = 4;
            this.lblProbabilidadMutacion.Text = "Probabilidad de mutación:";
            // 
            // lblTamanhoPoblacion
            // 
            this.lblTamanhoPoblacion.AutoSize = true;
            this.lblTamanhoPoblacion.Location = new System.Drawing.Point(49, 121);
            this.lblTamanhoPoblacion.Name = "lblTamanhoPoblacion";
            this.lblTamanhoPoblacion.Size = new System.Drawing.Size(124, 13);
            this.lblTamanhoPoblacion.TabIndex = 5;
            this.lblTamanhoPoblacion.Text = "Tamaño de la población:";
            // 
            // lblPesoMinimizarRectangulo
            // 
            this.lblPesoMinimizarRectangulo.AutoSize = true;
            this.lblPesoMinimizarRectangulo.Location = new System.Drawing.Point(51, 161);
            this.lblPesoMinimizarRectangulo.Name = "lblPesoMinimizarRectangulo";
            this.lblPesoMinimizarRectangulo.Size = new System.Drawing.Size(122, 13);
            this.lblPesoMinimizarRectangulo.TabIndex = 6;
            this.lblPesoMinimizarRectangulo.Text = "Peso minizar rectángulo:";
            // 
            // lblPesoFactorCuadratura
            // 
            this.lblPesoFactorCuadratura.AutoSize = true;
            this.lblPesoFactorCuadratura.Location = new System.Drawing.Point(55, 201);
            this.lblPesoFactorCuadratura.Name = "lblPesoFactorCuadratura";
            this.lblPesoFactorCuadratura.Size = new System.Drawing.Size(118, 13);
            this.lblPesoFactorCuadratura.TabIndex = 7;
            this.lblPesoFactorCuadratura.Text = "Peso factor cuadratura:";
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(179)))), ((int)(((byte)(148)))));
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciar.ForeColor = System.Drawing.Color.White;
            this.btnIniciar.Location = new System.Drawing.Point(201, 246);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(111, 25);
            this.btnIniciar.TabIndex = 7;
            this.btnIniciar.Text = "INICIAR";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnVerResultado
            // 
            this.btnVerResultado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(179)))), ((int)(((byte)(148)))));
            this.btnVerResultado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerResultado.ForeColor = System.Drawing.Color.White;
            this.btnVerResultado.Location = new System.Drawing.Point(381, 246);
            this.btnVerResultado.Name = "btnVerResultado";
            this.btnVerResultado.Size = new System.Drawing.Size(111, 25);
            this.btnVerResultado.TabIndex = 8;
            this.btnVerResultado.Text = "RESULTADO";
            this.btnVerResultado.UseVisualStyleBackColor = false;
            this.btnVerResultado.Click += new System.EventHandler(this.btnVerResultado_Click);
            // 
            // btnVerStockInfinito
            // 
            this.btnVerStockInfinito.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(179)))), ((int)(((byte)(148)))));
            this.btnVerStockInfinito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerStockInfinito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerStockInfinito.ForeColor = System.Drawing.Color.White;
            this.btnVerStockInfinito.Location = new System.Drawing.Point(564, 246);
            this.btnVerStockInfinito.Name = "btnVerStockInfinito";
            this.btnVerStockInfinito.Size = new System.Drawing.Size(61, 23);
            this.btnVerStockInfinito.TabIndex = 9;
            this.btnVerStockInfinito.Text = "VER";
            this.btnVerStockInfinito.UseVisualStyleBackColor = false;
            this.btnVerStockInfinito.Click += new System.EventHandler(this.btnVerStockInfinito_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(366, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Cantidad de elitismo:";
            // 
            // txtCantidadElitismo
            // 
            this.txtCantidadElitismo.Location = new System.Drawing.Point(476, 81);
            this.txtCantidadElitismo.Name = "txtCantidadElitismo";
            this.txtCantidadElitismo.Size = new System.Drawing.Size(76, 20);
            this.txtCantidadElitismo.TabIndex = 11;
            this.txtCantidadElitismo.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Máxima de generaciones:";
            // 
            // txtCantMaxGeneraciones
            // 
            this.txtCantMaxGeneraciones.Location = new System.Drawing.Point(476, 118);
            this.txtCantMaxGeneraciones.Name = "txtCantMaxGeneraciones";
            this.txtCantMaxGeneraciones.Size = new System.Drawing.Size(76, 20);
            this.txtCantMaxGeneraciones.TabIndex = 13;
            this.txtCantMaxGeneraciones.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Tipo de cruce:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(361, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Método de selección:";
            // 
            // cmbTipoCruce
            // 
            this.cmbTipoCruce.Enabled = false;
            this.cmbTipoCruce.FormattingEnabled = true;
            this.cmbTipoCruce.Items.AddRange(new object[] {
            "Partial-mapped Crossover"});
            this.cmbTipoCruce.Location = new System.Drawing.Point(476, 158);
            this.cmbTipoCruce.Name = "cmbTipoCruce";
            this.cmbTipoCruce.Size = new System.Drawing.Size(149, 21);
            this.cmbTipoCruce.TabIndex = 16;
            // 
            // cmbTipoSeleccion
            // 
            this.cmbTipoSeleccion.DisplayMember = "1";
            this.cmbTipoSeleccion.Enabled = false;
            this.cmbTipoSeleccion.FormattingEnabled = true;
            this.cmbTipoSeleccion.Items.AddRange(new object[] {
            "Roulette Wheel"});
            this.cmbTipoSeleccion.Location = new System.Drawing.Point(476, 198);
            this.cmbTipoSeleccion.Name = "cmbTipoSeleccion";
            this.cmbTipoSeleccion.Size = new System.Drawing.Size(149, 21);
            this.cmbTipoSeleccion.TabIndex = 17;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Navy;
            this.lblTitulo.Location = new System.Drawing.Point(164, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(378, 25);
            this.lblTitulo.TabIndex = 18;
            this.lblTitulo.Text = "Parámetros del Algoritmo Genético";
            // 
            // FormGenetico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(668, 290);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.cmbTipoSeleccion);
            this.Controls.Add(this.cmbTipoCruce);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCantMaxGeneraciones);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCantidadElitismo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVerStockInfinito);
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
        private System.Windows.Forms.Button btnVerStockInfinito;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidadElitismo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantMaxGeneraciones;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipoCruce;
        private System.Windows.Forms.ComboBox cmbTipoSeleccion;
        private System.Windows.Forms.Label lblTitulo;
    }
}