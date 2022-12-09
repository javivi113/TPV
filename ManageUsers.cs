using Microsoft.VisualBasic;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TPV
{
    public partial class ManageUsers : Form
    {
        public MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "127.0.0.1",
            Port = 3333,
            UserID = "root",
            Password = "a",
            Database = "tpv",
        };
        public ManageUsers()
        {
            InitializeComponent();
            connectToDB();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void connectToDB()
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
            MySqlDataAdapter sqlDA= new MySqlDataAdapter("Select * from usuarios",connection);
            DataTable dtbl= new DataTable();
            sqlDA.Fill(dtbl);     
            chAdmin.DataPropertyName= "admin";
            txtPassword.DataPropertyName= "password";
            txtUsername.DataPropertyName= "username";
            dgvUsers.DataSource= dtbl;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void manageUsers(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvUsers.CurrentRow!= null)
            {
                try
                {
                    dgvUsers.Rows.Clear();
                    dgvUsers.Refresh();
                }
                catch (Exception)
                {

                }
                
                
                DataGridViewRow dgvRow = dgvUsers.CurrentRow;
                using var connection = new MySqlConnection(builder.ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = $"UPDATE usuarios SET password=?pass ,admin=?admin WHERE username=?user";
                command.Parameters.Add("?user", MySqlDbType.VarString).Value = dgvRow.Cells["txtUsername"].Value.ToString();
                command.Parameters.Add("?pass", MySqlDbType.VarString).Value = dgvRow.Cells["txtPassword"].Value.ToString();
                command.Parameters.Add("?admin", MySqlDbType.Bool).Value = Convert.ToInt32(dgvRow.Cells["chAdmin"].Value);
                command.ExecuteNonQuery();
            }
            
        }

        private void deleteSelectedUser(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                DataGridViewRow dgvRow = dgvUsers.CurrentRow;
                using var connection = new MySqlConnection(builder.ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM usuarios WHERE username=?user ";
                command.Parameters.Add("?user", MySqlDbType.VarString).Value = dgvRow.Cells["txtUsername"].Value.ToString();
                command.ExecuteNonQuery();
                dgvUsers.Rows.RemoveAt(dgvRow.Index);
            }
        }

        private void createNewUser(object sender, EventArgs e)
        {
            String username;
            do
            {
                username = Interaction.InputBox("Introduce un nuevo usuario: ", "Crear nuevo usuario");
            } while (userExist(username));
            String password;
            do
            {
                password = Interaction.InputBox("Introduce la contraseña para el usuario: ", "Crear nuevo usuario");
            } while (password.Length<=0);            
            string message = "Es administrador?";
            string caption = "Crear nuevo usuario";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            bool admin=false;
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                admin= true;
                
            }else
            {
                admin= false;
            }
            if(username!=null || password!=null) 
            {
                using var connection = new MySqlConnection(builder.ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO usuarios VALUES (?user,?pass,?admin)";
                command.Parameters.Add("?user", MySqlDbType.VarString).Value = username.Trim();
                command.Parameters.Add("?pass", MySqlDbType.VarString).Value = password;
                command.Parameters.Add("?admin", MySqlDbType.Bool).Value = admin;
                command.ExecuteNonQuery();
                connectToDB();

            }
            else
            {
                string msg = "Ha ocurrido un error. No se a podido crear el nuevo usuario.";
                string cap = "Crear nuevo usuario";
                MessageBoxButtons btn = MessageBoxButtons.CancelTryContinue;
                DialogResult res;
                result = MessageBox.Show(msg, cap, btn);
            }
        }
        public bool userExist( String user)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM usuarios WHERE username=?user";
            command.Parameters.Add("?user", MySqlDbType.VarString).Value = user.Trim();            
            Usuarios usuario = null;
            
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario = new Usuarios(reader["username"].ToString(), reader["password"].ToString(), reader.GetBoolean(2));
                    if (usuario.getUsername() == user.Trim())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void closingEvent(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1.userOpenWindow = false;
        }
    }
}
