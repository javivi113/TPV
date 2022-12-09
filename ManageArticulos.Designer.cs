namespace TPV
{
    partial class ManageArticulos
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
            this.dgvArticulos = new System.Windows.Forms.DataGridView();
            this.intId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dblprecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.intCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dblImpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filtrobtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.deletebtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvArticulos
            // 
            this.dgvArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArticulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.intId,
            this.txtArticulo,
            this.dblprecio,
            this.intCantidad,
            this.dblImpuesto,
            this.txtTipo});
            this.dgvArticulos.Location = new System.Drawing.Point(12, 111);
            this.dgvArticulos.Name = "dgvArticulos";
            this.dgvArticulos.RowTemplate.Height = 25;
            this.dgvArticulos.Size = new System.Drawing.Size(1158, 499);
            this.dgvArticulos.TabIndex = 0;
            this.dgvArticulos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.manageArticulos);
            // 
            // intId
            // 
            this.intId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.intId.HeaderText = "Id";
            this.intId.Name = "intId";
            this.intId.ReadOnly = true;
            this.intId.Width = 42;
            // 
            // txtArticulo
            // 
            this.txtArticulo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtArticulo.HeaderText = "Articulo";
            this.txtArticulo.Name = "txtArticulo";
            this.txtArticulo.ReadOnly = true;
            // 
            // dblprecio
            // 
            this.dblprecio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dblprecio.HeaderText = "Precio";
            this.dblprecio.Name = "dblprecio";
            // 
            // intCantidad
            // 
            this.intCantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.intCantidad.HeaderText = "Cantidad";
            this.intCantidad.Name = "intCantidad";
            // 
            // dblImpuesto
            // 
            this.dblImpuesto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dblImpuesto.HeaderText = "Impuesto";
            this.dblImpuesto.Name = "dblImpuesto";
            // 
            // txtTipo
            // 
            this.txtTipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtTipo.HeaderText = "Tipo";
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.ReadOnly = true;
            this.txtTipo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txtTipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // filtrobtn
            // 
            this.filtrobtn.Location = new System.Drawing.Point(1072, 61);
            this.filtrobtn.Name = "filtrobtn";
            this.filtrobtn.Size = new System.Drawing.Size(98, 33);
            this.filtrobtn.TabIndex = 1;
            this.filtrobtn.Text = "Buscar";
            this.filtrobtn.UseVisualStyleBackColor = true;
            this.filtrobtn.Click += new System.EventHandler(this.hacerFiltro);
            // 
            // addbtn
            // 
            this.addbtn.Location = new System.Drawing.Point(12, 12);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(105, 93);
            this.addbtn.TabIndex = 2;
            this.addbtn.Text = "Añadir";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.createNewArticulo);
            // 
            // deletebtn
            // 
            this.deletebtn.Location = new System.Drawing.Point(123, 12);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(105, 93);
            this.deletebtn.TabIndex = 3;
            this.deletebtn.Text = "Eliminar";
            this.deletebtn.UseVisualStyleBackColor = true;
            this.deletebtn.Click += new System.EventHandler(this.deleteSelectedArt);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(907, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(159, 33);
            this.textBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(907, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Buscar un articulo";
            // 
            // ManageArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 622);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.deletebtn);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.filtrobtn);
            this.Controls.Add(this.dgvArticulos);
            this.Name = "ManageArticulos";
            this.Text = "ManageArticulos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dgvArticulos;
        private Button filtrobtn;
        private Button addbtn;
        private Button deletebtn;
        private TextBox textBox1;
        private Label label1;
        private DataGridViewTextBoxColumn intId;
        private DataGridViewTextBoxColumn txtArticulo;
        private DataGridViewTextBoxColumn dblprecio;
        private DataGridViewTextBoxColumn intCantidad;
        private DataGridViewTextBoxColumn dblImpuesto;
        private DataGridViewTextBoxColumn txtTipo;
    }
}