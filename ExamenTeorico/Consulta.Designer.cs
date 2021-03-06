
namespace ExamenTeorico
{
    partial class Consulta
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
            this.cbxListado = new System.Windows.Forms.ComboBox();
            this.dgvShow = new System.Windows.Forms.DataGridView();
            this.dgv4Cols = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Main = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv4Cols)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxListado
            // 
            this.cbxListado.FormattingEnabled = true;
            this.cbxListado.Location = new System.Drawing.Point(53, 111);
            this.cbxListado.Name = "cbxListado";
            this.cbxListado.Size = new System.Drawing.Size(789, 21);
            this.cbxListado.TabIndex = 0;
            this.cbxListado.DropDown += new System.EventHandler(this.cbxListado_DropDown);
            this.cbxListado.SelectedIndexChanged += new System.EventHandler(this.cbxListado_SelectedIndexChanged);
            this.cbxListado.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbxListado_MouseClick);
            // 
            // dgvShow
            // 
            this.dgvShow.AllowUserToAddRows = false;
            this.dgvShow.AllowUserToDeleteRows = false;
            this.dgvShow.AllowUserToOrderColumns = true;
            this.dgvShow.AllowUserToResizeColumns = false;
            this.dgvShow.AllowUserToResizeRows = false;
            this.dgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShow.Location = new System.Drawing.Point(53, 154);
            this.dgvShow.Name = "dgvShow";
            this.dgvShow.RowHeadersVisible = false;
            this.dgvShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShow.Size = new System.Drawing.Size(789, 137);
            this.dgvShow.TabIndex = 1;
            // 
            // dgv4Cols
            // 
            this.dgv4Cols.AllowUserToAddRows = false;
            this.dgv4Cols.AllowUserToDeleteRows = false;
            this.dgv4Cols.AllowUserToResizeColumns = false;
            this.dgv4Cols.AllowUserToResizeRows = false;
            this.dgv4Cols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv4Cols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Nombre,
            this.Main,
            this.Edad,
            this.Editar,
            this.Eliminar});
            this.dgv4Cols.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv4Cols.Location = new System.Drawing.Point(53, 154);
            this.dgv4Cols.Name = "dgv4Cols";
            this.dgv4Cols.ReadOnly = true;
            this.dgv4Cols.RowHeadersVisible = false;
            this.dgv4Cols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv4Cols.Size = new System.Drawing.Size(789, 137);
            this.dgv4Cols.TabIndex = 76;
            this.dgv4Cols.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv4Cols_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Código";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Main
            // 
            this.Main.HeaderText = "Apellidos";
            this.Main.Name = "Main";
            this.Main.ReadOnly = true;
            // 
            // Edad
            // 
            this.Edad.HeaderText = "Edad";
            this.Edad.Name = "Edad";
            this.Edad.ReadOnly = true;
            // 
            // Editar
            // 
            this.Editar.HeaderText = "Editar";
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            // 
            // Consulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 547);
            this.Controls.Add(this.dgv4Cols);
            this.Controls.Add(this.dgvShow);
            this.Controls.Add(this.cbxListado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Consulta";
            this.Text = "Consulta";
            this.Load += new System.EventHandler(this.Consulta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv4Cols)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxListado;
        private System.Windows.Forms.DataGridView dgvShow;
        private System.Windows.Forms.DataGridView dgv4Cols;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Main;
        private System.Windows.Forms.DataGridViewTextBoxColumn Edad;
        private System.Windows.Forms.DataGridViewButtonColumn Editar;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
    }
}