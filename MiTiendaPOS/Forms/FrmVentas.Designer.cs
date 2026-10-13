namespace MiTiendaPOS.Forms
{
    partial class FrmVentas
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
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            cboUsuario = new ComboBox();
            cboCliente = new ComboBox();
            lblVenta = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            dgvDetalle = new DataGridView();
            btnQuitar = new Button();
            btnAgregar = new Button();
            nudCantidad = new NumericUpDown();
            label5 = new Label();
            cboProducto = new ComboBox();
            label4 = new Label();
            inicialMiTiendaPOSBindingSource = new BindingSource(components);
            lblTotal = new Label();
            btnNueva = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            dgvVentas = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            Fecha = new DataGridViewTextBoxColumn();
            Cliente = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            Producto = new DataGridViewTextBoxColumn();
            Cantidad = new DataGridViewTextBoxColumn();
            Precio = new DataGridViewTextBoxColumn();
            Subtotal = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)inicialMiTiendaPOSBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cboUsuario);
            groupBox1.Controls.Add(cboCliente);
            groupBox1.Controls.Add(lblVenta);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(534, 117);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Encabezado de la venta ";
            // 
            // cboUsuario
            // 
            cboUsuario.FormattingEnabled = true;
            cboUsuario.Location = new Point(404, 35);
            cboUsuario.Name = "cboUsuario";
            cboUsuario.Size = new Size(112, 28);
            cboUsuario.TabIndex = 4;
            // 
            // cboCliente
            // 
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCliente.FormattingEnabled = true;
            cboCliente.Location = new Point(112, 38);
            cboCliente.Name = "cboCliente";
            cboCliente.Size = new Size(166, 28);
            cboCliente.TabIndex = 3;
            // 
            // lblVenta
            // 
            lblVenta.AutoSize = true;
            lblVenta.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVenta.Location = new Point(24, 81);
            lblVenta.Name = "lblVenta";
            lblVenta.Size = new Size(99, 20);
            lblVenta.TabIndex = 2;
            lblVenta.Text = "Venta nueva ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(321, 38);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 1;
            label2.Text = " Usuario";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(24, 38);
            label1.Name = "label1";
            label1.Size = new Size(57, 20);
            label1.TabIndex = 0;
            label1.Text = "Cliente";
            // 
            // groupBox2
            // 
            // mostrar el historial usando dgvVentas (no el dataGridView1 legacy)
            groupBox2.Controls.Add(dgvVentas);
            groupBox2.Location = new Point(593, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(522, 452);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Historial de Ventas";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvDetalle);
            groupBox3.Controls.Add(btnQuitar);
            groupBox3.Controls.Add(btnAgregar);
            groupBox3.Controls.Add(nudCantidad);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(cboProducto);
            groupBox3.Controls.Add(label4);
            groupBox3.Location = new Point(3, 135);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(543, 271);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Detalle de productos ";
            // 
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Columns.AddRange(new DataGridViewColumn[] { Producto, Cantidad, Precio, Subtotal });
            dgvDetalle.Location = new Point(18, 85);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.Size = new Size(507, 116);
            dgvDetalle.TabIndex = 10;
            // 
            // btnQuitar
            // 
            btnQuitar.Location = new Point(18, 218);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(94, 29);
            btnQuitar.TabIndex = 9;
            btnQuitar.Text = "Quitar línea ";
            btnQuitar.UseVisualStyleBackColor = true;
            btnQuitar.Click += btnQuitar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(397, 49);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 8;
            btnAgregar.Text = "Agregar ";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(330, 52);
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(45, 27);
            nudCantidad.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(278, 57);
            label5.Name = "label5";
            label5.Size = new Size(46, 20);
            label5.TabIndex = 6;
            label5.Text = "Cant.";
            // 
            // cboProducto
            // 
            // usar DropDownList: obligar a seleccionar productos existentes (según especificación)
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProducto.FormattingEnabled = true;
            cboProducto.Location = new Point(97, 49);
            cboProducto.Name = "cboProducto";
            cboProducto.Size = new Size(166, 28);
            cboProducto.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(9, 52);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 5;
            label4.Text = " Producto";
            // 
            // inicialMiTiendaPOSBindingSource
            // 
            inicialMiTiendaPOSBindingSource.DataSource = typeof(Migrations.InicialMiTiendaPOS);
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(308, 222);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(73, 31);
            lblTotal.TabIndex = 11;
            lblTotal.Text = "Total:";
            // 
            // btnNueva
            // 
            btnNueva.Location = new Point(36, 435);
            btnNueva.Name = "btnNueva";
            btnNueva.Size = new Size(129, 29);
            btnNueva.TabIndex = 3;
            btnNueva.Text = "Nueva venta";
            btnNueva.UseVisualStyleBackColor = true;
            btnNueva.Click += btnNueva_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(191, 435);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(136, 29);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar venta";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(356, 435);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(144, 29);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar venta ";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvVentas
            // 
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Columns.AddRange(new DataGridViewColumn[] { id, Fecha, Cliente, Total });
            // ubicar dgvVentas dentro del groupBox2 para que sea el grid visible del historial
            dgvVentas.Location = new Point(22, 41);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.RowHeadersWidth = 51;
            dgvVentas.Size = new Size(494, 398);
            dgvVentas.TabIndex = 0;
            dgvVentas.CellClick += dgvVentas_CellClick;
            // 
            // id
            // 
            id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            id.HeaderText = "id";
            id.MinimumWidth = 6;
            id.Name = "id";
            id.ReadOnly = true;
            // 
            // Fecha
            // 
            Fecha.HeaderText = "Fecha";
            Fecha.MinimumWidth = 6;
            Fecha.Name = "Fecha";
            Fecha.ReadOnly = true;
            Fecha.Width = 125;
            // 
            // Cliente
            // 
            Cliente.HeaderText = "Cliente";
            Cliente.MinimumWidth = 6;
            Cliente.Name = "Cliente";
            Cliente.ReadOnly = true;
            Cliente.Width = 125;
            // 
            // Total
            // 
            Total.HeaderText = "Total";
            Total.MinimumWidth = 6;
            Total.Name = "Total";
            Total.ReadOnly = true;
            Total.Width = 125;
            // 
            // Column1
            // 
            // Column1/Column2 legacy removed
            // 
            // dataGridView1
            // 
            // eliminar el control legacy dataGridView1: ya no se usa
            // dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            // dataGridView1.Columns.AddRange(new DataGridViewColumn[] { N, F, C, To });
            // dataGridView1.Location = new Point(22, 41);
            // dataGridView1.Name = "dataGridView1";
            // dataGridView1.RowHeadersWidth = 51;
            // dataGridView1.Size = new Size(494, 398);
            // dataGridView1.TabIndex = 0;
            // 
            // Producto
            // 
            Producto.HeaderText = "Producto";
            Producto.MinimumWidth = 6;
            Producto.Name = "Producto";
            Producto.Width = 125;
            // 
            // Cantidad
            // 
            Cantidad.HeaderText = "Cantidad";
            Cantidad.MinimumWidth = 6;
            Cantidad.Name = "Cantidad";
            Cantidad.Width = 125;
            // 
            // Precio
            // 
            Precio.HeaderText = "Precio";
            Precio.MinimumWidth = 6;
            Precio.Name = "Precio";
            Precio.Width = 125;
            // 
            // Subtotal
            // 
            Subtotal.HeaderText = "Subtotal";
            Subtotal.MinimumWidth = 6;
            Subtotal.Name = "Subtotal";
            Subtotal.Width = 125;
            // 
            // Legacy columns N/F/C/To removed
            // 
            // FrmVentas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 494);
            // Patch: no-op update to trigger file change
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNueva);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FrmVentas";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registros de Ventas";
            Load += FrmVentas_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)inicialMiTiendaPOSBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label lblVenta;
        private Label label2;
        private Label label1;
        private ComboBox cboUsuario;
        private ComboBox cboCliente;
        private NumericUpDown nudCantidad;
        private Label label5;
        private ComboBox cboProducto;
        private Label label4;
        private Button btnAgregar;
        private Button btnQuitar;
        private BindingSource inicialMiTiendaPOSBindingSource;
        private DataGridView dgvDetalle;
        private Label lblTotal;
        private Button btnNueva;
        private DataGridView dgvVentas;
        private Button btnGuardar;
        private Button btnEliminar;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn Fecha;
        private DataGridViewTextBoxColumn Cliente;
        private DataGridViewTextBoxColumn Total;

        // etiquetas instructivas añadidas por el asistente
        // (se mantienen aquí en caso de auditoría; no usadas por la lógica)
        private Label lblBtnEliminar;
        private Label lblDgvVentas;
        private DataGridViewTextBoxColumn Producto;
        private DataGridViewTextBoxColumn Cantidad;
        private DataGridViewTextBoxColumn Precio;
        private DataGridViewTextBoxColumn Subtotal;
    }
}