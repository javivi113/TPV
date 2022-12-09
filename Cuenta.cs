using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV
{
    internal class Cuenta
    {
        List<Articulos> cuenta = new List<Articulos>();
        Articulos articulo;
        public static double ImporteTotal = 0;
        String TicketCuenta = "";
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "127.0.0.1",
            Port = 3333,
            UserID = "root",
            Password = "a",
            Database = "tpv",
        };
        public void actualizarCantidadProductoDB(Articulos a)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"UPDATE articulos SET cantidad=cantidad-?cant WHERE articulo=?art";            
            command.Parameters.Add("?cant", MySqlDbType.Int32).Value = a.getCantidad();            
            command.Parameters.Add("?art", MySqlDbType.Bool).Value = a.getArticulo();
            command.ExecuteNonQuery();
        }

        public void addArticulo(Articulos art)
        {
            //Articulos aux = art;
            //actualizarCantidadProductoDB(aux);
                       
            bool exists = false;
            cuenta.ForEach((a) =>
            {
                if (a.getArticulo() == art.getArticulo())
                {
                    if (a.getCantidad()+1 < art.getCantidad())
                    {

                        a.cantidadMas();
                        exists = true;
                    }
                    else
                    {
                        string msg = "No quedan mas " + a.getArticulo() + ". Reponer!!!";
                        string cap = "Falta de stock";
                        MessageBoxButtons btn = MessageBoxButtons.OK;
                        DialogResult res;
                        res = MessageBox.Show(msg, cap, btn);
                    }
                   
                }
            });
            if (!exists)
            {
                art.setCantidad(1);
                cuenta.Add(art);
            }
            ImporteTotal += art.getPrecio();


        }
        public Double getTotalCuenta()
        {
            return ImporteTotal;
        }
        public List<Articulos> getCuenta() 
        {
            return cuenta;
        }
        public void nuevaCuenta()
        {
            cuenta = new List<Articulos>();
            ImporteTotal = 0;
        }
        public String devolverTicket()
        {
            String ticket = "";
            cuenta.ForEach(arti =>
            {
                ticket += articuloCuenta(arti)+"\n";
            });
            ticket += "Total:     " + ImporteTotal;
            return ticket;
        }

        public String articuloCuenta(Articulos arti)
        {
            String ticketElem = arti.articulo + "     x" + arti.cantidad + "       " + arti.precio + "€";           
            return ticketElem;
        }
    }
}
