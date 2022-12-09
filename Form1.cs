using Microsoft.VisualBasic;
using MySqlConnector;
using System.Drawing.Printing;
using TPV.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace TPV
{
    public partial class Form1 : Form
    {

        List<String> lTipoArticulos;
        public static bool userOpenWindow=false;
        Cuenta cuenta = new Cuenta();
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "127.0.0.1",
            Port = 3333,
            UserID = "root",
            Password = "a",
            Database = "tpv",
        };
        public Form1()
        {
            InitializeComponent();
            usuario.Text = Usuarios.username;
            if (!Usuarios.admin)
            {
                btnManageUsers.Enabled = false;
                btnEditProductos.Enabled = false;              
               
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
        private void addNewArticulo(object sender, PaintEventArgs e)
        {

        }
        
        public List<Articulos> getArticulos( String tipo)
        {
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
                
                btn.Click += new EventHandler(addCuentaEvent);
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

        private void nuevaCuenta(object sender, EventArgs e)
        {
            cuenta.nuevaCuenta();
        }

        private void vaciarCuenta(object sender, EventArgs e)
        {
            cuenta.nuevaCuenta();
        }

        void addCuentaEvent(Object sender, EventArgs e)
        {
            Button btn= sender as Button;
            String articulo =btn.Text;
            using var connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM articulos WHERE articulo=?art";
            command.Parameters.Add("?art", MySqlDbType.VarString).Value = articulo;
            Articulos articuloCuentaSelec=null;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    articuloCuentaSelec = new Articulos(reader["articulo"].ToString(), Double.Parse(reader["precio"].ToString()), Convert.ToInt32(reader["cantidad"].ToString()), Double.Parse(reader["impuestos"].ToString()), reader["tipo"].ToString()); ;
                }
            }
            añadirACuenta(articuloCuentaSelec);
        }

        private void añadirACuenta(Articulos a)
        {
            if (a.getCantidad()>0)
            {
                cuenta.addArticulo(a);
                dgvCuenta.Rows.Clear();
                cuenta.getCuenta().ForEach((art) =>
                {
                    double importe = ((art.getCantidad() * art.getPrecio()) + ((art.getCantidad() * art.getPrecio()) * (art.getImpuestos() / 100)));
                    dgvCuenta.Rows.Add(art.getArticulo(), art.getPrecio(), art.getCantidad(), art.getImpuestos(), importe);
                    Double impuTot = Double.Parse(impuestoCalcTotal.Text);
                    Double subtotal = cuenta.getTotalCuenta();
                    if (impuTot <= 0)
                    {
                        SubtotalCalTotal.Text = subtotal.ToString();
                        TotalCalTotal.Text = subtotal.ToString();
                    }
                    else
                    {
                        SubtotalCalTotal.Text = subtotal.ToString();
                        TotalCalTotal.Text = (subtotal + (subtotal * (impuTot / 100))).ToString();
                    }
                });                
            }
            else
            {
                string msg = "No quedan mas "+a.getArticulo()+". Reponer!!!";
                string cap = "Falta de stock";
                MessageBoxButtons btn = MessageBoxButtons.OK;
                DialogResult res;
                res = MessageBox.Show(msg, cap, btn);
            }

            

        }

        private void sacarTicket(object sender, EventArgs e)
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string file = "ticket.pdf";

            PrintDocument pDoc = new PrintDocument()
            {
                PrinterSettings = new PrinterSettings()
                {
                    PrinterName = "Microsoft Print to PDF",
                    PrintToFile = true,
                    PrintFileName = System.IO.Path.Combine(directory, file),
                }
            };

            pDoc.PrintPage += new PrintPageEventHandler(Print_Page);
            pDoc.Print();
        }
        public void Print_Page(object sender, PrintPageEventArgs e)
        {
            Font fnt = new Font("Courier New", 12);
            String ticketCuenta = cuenta.devolverTicket();
            e.Graphics.DrawString
              (ticketCuenta, fnt, System.Drawing.Brushes.Black, 0, 0);
        }

        private void btnEditProductos_Click(object sender, EventArgs e)
        {
            if (!userOpenWindow)
            {
                userOpenWindow = true;
                Form form = new ManageArticulos();
                form.Show();
            }
        }

        private void actionPagar(object sender, EventArgs e)
        {

        }
    }
}