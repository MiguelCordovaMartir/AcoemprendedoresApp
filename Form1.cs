using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CooperativaApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Opciones para el ComboBox de productos
            cmbProducto.Items.Add("Cuenta de ahorro");
            cmbProducto.Items.Add("Cuenta corriente");
            cmbProducto.Items.Add("Tarjeta de débito");
            cmbProducto.Items.Add("Tarjeta de crédito");
            cmbProducto.Items.Add("Préstamo personal");
            cmbProducto.Items.Add("Préstamo agropecuario");
            cmbProducto.Items.Add("Préstamo hipotecario");

            // Seleccionar el primer elemento como predeterminado
            cmbProducto.SelectedIndex = 0;

            // Cargar los clientes en el DataGridView
            CargarClientes();
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            // Verificar si algún campo está vacío
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDUI.Text) ||
                cmbProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, llene todos los campos.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Salir si algún campo está vacío
            }

            string connectionString = "server=localhost;user=root;database=acoemprendedores;password=1202M2708i2024g;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string query = "INSERT INTO Clientes (nombre, apellido, DUI, producto) VALUES (@nombre, @apellido, @DUI, @producto)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@apellido", txtApellido.Text);
                cmd.Parameters.AddWithValue("@DUI", txtDUI.Text);
                cmd.Parameters.AddWithValue("@producto", cmbProducto.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente agregado correctamente.");

                // Llamar a CargarClientes() para actualizar el DataGridView
                CargarClientes();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void CargarClientes()
        {
            string connectionString = "server=localhost;user=root;database=acoemprendedores;password=1202M2708i2024g;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string query = "SELECT * FROM Clientes";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);

                // Asignar el DataTable al DataGridView
                dataGridViewClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
