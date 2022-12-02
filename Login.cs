
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
using static System.Windows.Forms.DataFormats;

namespace TPV
{       
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login(object sender, EventArgs e)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                Port = 3333,
                UserID = "root",
                Password = "a",
                Database = "tpv",
            };
            using var connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM usuarios WHERE username=?user AND password=?pass";
            command.Parameters.Add("?user", MySqlDbType.VarString).Value = textBox1.Text;
            command.Parameters.Add("?pass", MySqlDbType.VarString).Value = textBox2.Text;
            Usuarios usuario=null;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario = new Usuarios(reader["username"].ToString(), reader["password"].ToString(), reader.GetBoolean(2));
                }
            }
            if (usuario.getUsername() == textBox1.Text && usuario.getPassword() == textBox2.Text)
            {
                this.Hide();
                Form form = new Form1();
                form.Show();
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
                label1.Text = "Usuario y/o contraseña incorrecto";
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void loginKey(object sender, KeyEventArgs e)
        {
        
        }
    }
}
