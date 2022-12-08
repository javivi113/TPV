using Microsoft.VisualBasic;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPV.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TPV
{
    public partial class ManageArticulos : Form
    {
        public MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "127.0.0.1",
            Port = 3333,
            UserID = "root",
            Password = "a",
            Database = "tpv",
        };
        public ManageArticulos()
        {
            InitializeComponent();
            connectToDB();
        }
        public void connectToDB()
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
            MySqlDataAdapter sqlDA = new MySqlDataAdapter("Select * from articulos", connection);
            DataTable dtbl = new DataTable();
            sqlDA.Fill(dtbl);
            txtArticulo.DataPropertyName = "articulo";
            dblprecio.DataPropertyName = "precio";
            intCantidad.DataPropertyName = "cantidad";
            dblImpuesto.DataPropertyName = "impuesto";
            txtTipo.DataPropertyName = "tipo";
           
            dgvArticulos.DataSource = dtbl;
        }
        public void filtrar(String filtro)
        {
            if (filtro.Length!=0)
            {
                using var connection = new MySqlConnection(builder.ConnectionString);
                connection.Open();

                MySqlDataAdapter sqlDA = new MySqlDataAdapter("Select * from articulos where articulo=" + filtro, connection);
                DataTable dtbl = new DataTable();
                sqlDA.Fill(dtbl);
                txtArticulo.DataPropertyName = "articulo";
                dblprecio.DataPropertyName = "precio";
                intCantidad.DataPropertyName = "cantidad";
                dblImpuesto.DataPropertyName = "impuesto";
                txtTipo.DataPropertyName = "tipo";

                dgvArticulos.DataSource = dtbl;
            }
            
        }
        public bool articsExist(String art)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM articulos WHERE articulo=?art";
            command.Parameters.Add("?art", MySqlDbType.VarString).Value = art.Trim();
            Articulos articulos = null;

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    articulos = new Articulos(reader["articulos"].ToString(), reader.GetDouble("precio"), reader.GetInt32("cantidad"), reader.GetDouble("impuesto"), reader["tipo"].ToString());
                    if (articulos.getArticulo() == art.Trim())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool matchOptionType(String tipo)
        {
            switch (tipo)
            {
                case "cafes":
                    return false;
                case "cerveza":
                    return false;
                case "licores":
                    return false;
                case "picoteo":
                    return false;
                case "vino":
                    return false;
                case "refrescos":
                    return false;
                case "varios":
                    return false;
                default:
                    return true;

            }
        }
        private void createNewArticulo(object sender, EventArgs e)
        {
            String articulo;
            do
            {
                articulo = Interaction.InputBox("Introduce un nuevo nombre de articulo: ", "Crear nuevo articulo");
            } while (articsExist(articulo));
            double precio=0;
            bool seguir = true;
            while (seguir)
            {
                try
                {
                    precio = Double.Parse(Interaction.InputBox("Introduce el precio para el articulo: ", "Crear nuevo articulo"));
                    seguir = false;
                }
                catch (Exception ex)
                {

                }
            }
            int cantidad=0;
            seguir = true;
            while (seguir)
            {
                try
                {
                    cantidad = Convert.ToInt32(Interaction.InputBox("Introduce el impuesto para el articulo: ", "Crear nuevo articulo"));
                    if (cantidad > 0)
                    {
                        seguir = false;
                    }
                    
                }
                catch (Exception ex)
                {

                }
            }
            double impuesto=0;
            seguir = true;
            while (seguir)
            {
                try
                {
                    impuesto = Double.Parse(Interaction.InputBox("Introduce el impuesto para el articulo: ", "Crear nuevo articulo"));
                    seguir = false;
                }
                catch (Exception ex)
                {

                }
            }
            String tipo;
            do
            {
                tipo = Interaction.InputBox("Introduce el tipo de articulo:\n-Cerveza\n-Licores\n-Picoteo\n-Vino\n-Varios\n-Cafes ", "Crear nuevo articulo");
            } while (matchOptionType(tipo));
            try
            {
                using var connection = new MySqlConnection(builder.ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO articulos VALUES (?articulo,?precio,?cant,?impu,?tipo)";
                command.Parameters.Add("?articulo", MySqlDbType.VarString).Value = articulo.Trim();
                command.Parameters.Add("?precio", MySqlDbType.Double).Value = precio;
                command.Parameters.Add("?cant", MySqlDbType.Int32).Value = cantidad;
                command.Parameters.Add("?impu", MySqlDbType.Double).Value = impuesto;
                command.Parameters.Add("?tipo", MySqlDbType.VarString).Value = tipo;
                command.ExecuteNonQuery();
                connectToDB();
            }
            catch
            {
                string msg = "Ha ocurrido un error. No se a podido crear el nuevo articulo.";
                string cap = "Crear nuevo articulo";
                MessageBoxButtons btn = MessageBoxButtons.CancelTryContinue;
                DialogResult res;
                res = MessageBox.Show(msg, cap, btn);
            }
        }
        private void deleteSelectedArt(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                DataGridViewRow dgvRow = dgvArticulos.CurrentRow;
                using var connection = new MySqlConnection(builder.ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM articulos WHERE ariculo=?art ";
                command.Parameters.Add("?art", MySqlDbType.VarString).Value = dgvRow.Cells["txtarticulo"].Value.ToString();
                command.ExecuteNonQuery();
                dgvArticulos.Rows.RemoveAt(dgvRow.Index);
            }
        }
        private void hacerFiltro(object sender, EventArgs e)
        {
            filtrar(textBox1.Text);
        }
    }
}
