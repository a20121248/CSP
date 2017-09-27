namespace CSP.View
{
    partial class FormPedidos
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
            this.btnRutaArchPedidos = new System.Windows.Forms.Button();
            this.txtRutaArchPedidos = new System.Windows.Forms.TextBox();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRutaArchStock = new System.Windows.Forms.Button();
            this.txtRutaArchStock = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnGenetico = new System.Windows.Forms.Button();
            this.btnCuckooSearch = new System.Windows.Forms.Button();
            this.gbDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRutaArchPedidos
            // 
            this.btnRutaArchPedidos.Location = new System.Drawing.Point(428, 19);
            this.btnRutaArchPedidos.Name = "btnRutaArchPedidos";
            this.btnRutaArchPedidos.Size = new System.Drawing.Size(38, 20);
            this.btnRutaArchPedidos.TabIndex = 0;
            this.btnRutaArchPedidos.Text = "...";
            this.btnRutaArchPedidos.UseVisualStyleBackColor = true;
            this.btnRutaArchPedidos.Click += new System.EventHandler(this.btnRutaArchPedidos_Click);
            // 
            // txtRutaArchPedidos
            // 
            this.txtRutaArchPedidos.Location = new System.Drawing.Point(84, 19);
            this.txtRutaArchPedidos.Name = "txtRutaArchPedidos";
            this.txtRutaArchPedidos.Size = new System.Drawing.Size(333, 20);
            this.txtRutaArchPedidos.TabIndex = 1;
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.btnRutaArchStock);
            this.gbDatos.Controls.Add(this.txtRutaArchStock);
            this.gbDatos.Controls.Add(this.label1);
            this.gbDatos.Controls.Add(this.btnRutaArchPedidos);
            this.gbDatos.Controls.Add(this.txtRutaArchPedidos);
            this.gbDatos.Location = new System.Drawing.Point(12, 49);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(494, 83);
            this.gbDatos.TabIndex = 2;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Stock:";
            // 
            // btnRutaArchStock
            // 
            this.btnRutaArchStock.Location = new System.Drawing.Point(428, 45);
            this.btnRutaArchStock.Name = "btnRutaArchStock";
            this.btnRutaArchStock.Size = new System.Drawing.Size(38, 20);
            this.btnRutaArchStock.TabIndex = 3;
            this.btnRutaArchStock.Text = "...";
            this.btnRutaArchStock.UseVisualStyleBackColor = true;
            this.btnRutaArchStock.Click += new System.EventHandler(this.btnRutaArchStock_Click);
            // 
            // txtRutaArchStock
            // 
            this.txtRutaArchStock.Location = new System.Drawing.Point(84, 45);
            this.txtRutaArchStock.Name = "txtRutaArchStock";
            this.txtRutaArchStock.Size = new System.Drawing.Size(333, 20);
            this.txtRutaArchStock.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pedidos:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitulo.Location = new System.Drawing.Point(110, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(322, 25);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "Optimización de cortes en 2D";
            // 
            // btnGenetico
            // 
            this.btnGenetico.BackColor = System.Drawing.Color.Navy;
            this.btnGenetico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenetico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenetico.ForeColor = System.Drawing.Color.White;
            this.btnGenetico.Location = new System.Drawing.Point(91, 158);
            this.btnGenetico.Name = "btnGenetico";
            this.btnGenetico.Size = new System.Drawing.Size(132, 41);
            this.btnGenetico.TabIndex = 4;
            this.btnGenetico.Text = "ALGORITMO\r\nGENÉTICO";
            this.btnGenetico.UseVisualStyleBackColor = false;
            this.btnGenetico.Click += new System.EventHandler(this.btnGenetico_Click);
            // 
            // btnCuckooSearch
            // 
            this.btnCuckooSearch.BackColor = System.Drawing.Color.Teal;
            this.btnCuckooSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCuckooSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCuckooSearch.ForeColor = System.Drawing.Color.White;
            this.btnCuckooSearch.Location = new System.Drawing.Point(300, 158);
            this.btnCuckooSearch.Name = "btnCuckooSearch";
            this.btnCuckooSearch.Size = new System.Drawing.Size(132, 41);
            this.btnCuckooSearch.TabIndex = 5;
            this.btnCuckooSearch.Text = "ALGORITMO\r\nCUCKOO SEARCH";
            this.btnCuckooSearch.UseVisualStyleBackColor = false;
            this.btnCuckooSearch.Click += new System.EventHandler(this.btnCuckooSearch_Click);
            // 
            // FormPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(518, 228);
            this.Controls.Add(this.btnCuckooSearch);
            this.Controls.Add(this.btnGenetico);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.gbDatos);
            this.Name = "FormPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo Pedidos";
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRutaArchPedidos;
        private System.Windows.Forms.TextBox txtRutaArchPedidos;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRutaArchStock;
        private System.Windows.Forms.TextBox txtRutaArchStock;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnGenetico;
        private System.Windows.Forms.Button btnCuckooSearch;
    }
}