using MiTiendaPOS.Models;
using MiTiendaPOS.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace MiTiendaPOS.Forms
{
    public partial class FrmCategorias : Form
    {
        private readonly CategoriaService _servicio = new();
        private int _idSeleccionado = 0; // para registro nuevo

        public FrmCategorias()
        {
            InitializeComponent();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            LimpiarFormulario();
        }

        private void CargarCategorias()
        {
            dgvCategorias.DataSource = _servicio.Listar(txtBuscar.Text)
                .Select(c => new { c.Id, c.Nombre })
                .ToList();
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            txtNombre.Clear();
            btnEliminar.Enabled = false;
            dgvCategorias.ClearSelection();
            txtNombre.Focus();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvCategorias.Rows[e.RowIndex];
            int id = Convert.ToInt32(fila.Cells["Id"].Value);
            Categoria? categoria = _servicio.ObtenerPorId(id);
            if (categoria == null) return;

            _idSeleccionado = categoria.Id;
            txtNombre.Text = categoria.Nombre;
            btnEliminar.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var categoria = new Categoria
                {
                    Id = _idSeleccionado,
                    Nombre = txtNombre.Text
                };

                if (_idSeleccionado == 0)
                    _servicio.Crear(categoria);
                else
                    _servicio.Actualizar(categoria);

                MessageBox.Show("Categoría guardada con éxito.", "MiTiendaPOS",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarCategorias();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0) return;
            var resultado = MessageBox.Show(
                "¿Está seguro de eliminar la categoría seleccionada?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _servicio.Eliminar(_idSeleccionado);
                    MessageBox.Show("Categoría eliminada correctamente.", "MiTiendaPOS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarCategorias();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "No se pudo eliminar",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

