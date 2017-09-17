namespace CSP.View
{
    partial class FormResultado
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMejorSolucion = new System.Windows.Forms.TextBox();
            this.txtFitness = new System.Windows.Forms.TextBox();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.picCanvasPiezas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvasPiezas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mejor solución:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fitness:";
            // 
            // txtMejorSolucion
            // 
            this.txtMejorSolucion.Enabled = false;
            this.txtMejorSolucion.Location = new System.Drawing.Point(179, 10);
            this.txtMejorSolucion.Name = "txtMejorSolucion";
            this.txtMejorSolucion.Size = new System.Drawing.Size(247, 20);
            this.txtMejorSolucion.TabIndex = 3;
            // 
            // txtFitness
            // 
            this.txtFitness.Enabled = false;
            this.txtFitness.Location = new System.Drawing.Point(179, 34);
            this.txtFitness.Name = "txtFitness";
            this.txtFitness.Size = new System.Drawing.Size(102, 20);
            this.txtFitness.TabIndex = 4;
            // 
            // picCanvas
            // 
            this.picCanvas.Location = new System.Drawing.Point(12, 69);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(712, 357);
            this.picCanvas.TabIndex = 5;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvas_Paint);
            // 
            // picCanvasPiezas
            // 
            this.picCanvasPiezas.Location = new System.Drawing.Point(730, 69);
            this.picCanvasPiezas.Name = "picCanvasPiezas";
            this.picCanvasPiezas.Size = new System.Drawing.Size(144, 357);
            this.picCanvasPiezas.TabIndex = 6;
            this.picCanvasPiezas.TabStop = false;
            this.picCanvasPiezas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvasPiezas_Paint);
            // 
            // FormResultado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(886, 438);
            this.Controls.Add(this.picCanvasPiezas);
            this.Controls.Add(this.picCanvas);
            this.Controls.Add(this.txtFitness);
            this.Controls.Add(this.txtMejorSolucion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormResultado";
            this.Text = "Resultados";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvasPiezas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMejorSolucion;
        private System.Windows.Forms.TextBox txtFitness;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.PictureBox picCanvasPiezas;
    }
}