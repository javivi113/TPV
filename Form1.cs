using MySqlConnector;
using TPV.Properties;

namespace TPV
{
    public partial class Form1 : Form
    {
        Boolean admin = false;
        List<String> lTipoArticulos;
        public static bool userOpenWindow=false;
        public Form1()
        {
            InitializeComponent();
            usuario.Text = Usuarios.username;
            if (!Usuarios.admin)
            {
                btnManageUsers.Enabled = false;
                admin = true;
            }
            lTipoArticulos= getTipoArticulos();
            colocarTipoArticulos();
            
        }
        public Image GetImageTipo(String tipo)
        {
            switch (tipo)
                {
                    case "Cafes":
                        return Resources.cupfee;
                    case "Cerveza":
                        return Resources.cerveza;
                    case "Licores":
                        return Resources.licores;
                    case "Picoteo":
                        return Resources.picoteo;
                    case "Vino":
                        return Resources.vino;
                    case "Refrescos":
                        return Resources.refrescos;
                    default:
                        return Resources.varios;
                }
        }
        public void colocarTipoArticulos()
        {

            lTipoArticulos.ForEach(a =>
            {
                Button btn = new Button();
                btn.BackgroundImage = GetImageTipo(a);
                btn.Font = new Font("Segoe UI",13, FontStyle.Bold);
                btn.ForeColor= Color.Red;
                btn.Text=a;
                btn.TextAlign=ContentAlignment.BottomCenter;
                Size size = new Size(
                    110,110);
                btn.Size= size;
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.Click += new EventHandler(listarProductosTipo);
                flowLayoutPanelTipos.Controls.Add(btn);
            });
            

            
        }
        public List<Articulos> getArticulos( String tipo)
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
            command.CommandText = $"SELECT * FROM articulos WHERE tipo=?tipo";
            command.Parameters.Add("?tipo", MySqlDbType.VarString).Value = tipo;
            
            List<Articulos> lArticulos = new List<Articulos>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Articulos artics = new Articulos(reader["articulo"].ToString(), Double.Parse(reader["precio"].ToString()), Convert.ToInt32(reader["cantidad"].ToString()), Double.Parse(reader["impuestos"].ToString()), reader["tipo"].ToString());
                    lArticulos.Add(artics);
                }
            }
            return lArticulos;
        }
        public List<String> getTipoArticulos()
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
            command.CommandText = $"SELECT DISTINCT(tipo) FROM articulos";
            String tipos = null;
            List<String> lArticulos = new List<String>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tipos= reader["tipo"].ToString();
                    lArticulos.Add(tipos);
                }
            }
            return lArticulos;
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        void listarProductosTipo(Object sender, EventArgs e)
        {
            fbProductos.Controls.Clear();
            var btn = sender as Button;
            List<Articulos> lArticulos = getArticulos(btn.Text);
            int i= 0;
            lArticulos.ForEach(a =>
            {
                i++;
                Button btn = new Button();
                btn.BackgroundImage = a.getPng();
                btn.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                btn.ForeColor = Color.Red;
                btn.Text = a.getArticulo();
                btn.TextAlign = ContentAlignment.BottomCenter;
                Size size = new Size(
                    125, 125);
                btn.Size = size;
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                fbProductos.Controls.Add(btn);
            });

        }

        private void gestionUsuarios(object sender, EventArgs e)
        {
            if (!userOpenWindow)
            {
                userOpenWindow= true;
                Form form = new ManageUsers();
                form.Show();
            }
            
        }
    }
}