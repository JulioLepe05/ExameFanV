
namespace ExamenTeorico
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btnConsulta;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.Button button5;
            System.Windows.Forms.Button btnNuevo;
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelPrincipal = new System.Windows.Forms.FlowLayoutPanel();
            btnConsulta = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            btnNuevo = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConsulta
            // 
            btnConsulta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            btnConsulta.Cursor = System.Windows.Forms.Cursors.Hand;
            btnConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnConsulta.ForeColor = System.Drawing.Color.White;
            btnConsulta.Image = ((System.Drawing.Image)(resources.GetObject("btnConsulta.Image")));
            btnConsulta.Location = new System.Drawing.Point(100, 277);
            btnConsulta.Margin = new System.Windows.Forms.Padding(100, 3, 3, 3);
            btnConsulta.Name = "btnConsulta";
            btnConsulta.Size = new System.Drawing.Size(139, 125);
            btnConsulta.TabIndex = 39;
            btnConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnConsulta.UseVisualStyleBackColor = false;
            btnConsulta.Click += new System.EventHandler(this.btnConsulta_Click);
            // 
            // button5
            // 
            button5.AllowDrop = true;
            button5.AutoEllipsis = true;
            button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            button5.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button5.ForeColor = System.Drawing.Color.White;
            button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            button5.Location = new System.Drawing.Point(3, 408);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(139, 132);
            button5.TabIndex = 41;
            button5.TabStop = false;
            button5.UseMnemonic = false;
            button5.UseVisualStyleBackColor = false;
            button5.UseWaitCursor = true;
            button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            this.flowLayoutPanel1.Controls.Add(button5);
            this.flowLayoutPanel1.Controls.Add(btnConsulta);
            this.flowLayoutPanel1.Controls.Add(btnNuevo);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(325, 543);
            this.flowLayoutPanel1.TabIndex = 42;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(3, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "--------------------------------------------------------------";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 83);
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.panelPrincipal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelPrincipal.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.panelPrincipal.Location = new System.Drawing.Point(331, 1);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(944, 542);
            this.panelPrincipal.TabIndex = 43;
            this.panelPrincipal.UseWaitCursor = true;
            // 
            // btnNuevo
            // 
            btnNuevo.AllowDrop = true;
            btnNuevo.AutoEllipsis = true;
            btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(54)))));
            btnNuevo.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnNuevo.ForeColor = System.Drawing.Color.White;
            btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            btnNuevo.Location = new System.Drawing.Point(100, 131);
            btnNuevo.Margin = new System.Windows.Forms.Padding(100, 3, 3, 3);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(139, 140);
            btnNuevo.TabIndex = 40;
            btnNuevo.TabStop = false;
            btnNuevo.UseMnemonic = false;
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.UseWaitCursor = true;
            btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 543);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.FlowLayoutPanel panelPrincipal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

