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
        private void actualizarCantidadProductoDB(Articulos a)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"UPDATE articulos SET cantidad=?cant WHERE articulo=?art";            
            command.Parameters.Add("?cant", MySqlDbType.Int32).Value = a.getCantidad()-1;            
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
        }
        public String devolverTicket()
        {
            String ticket = "";
            cuenta.ForEach(arti =>
            {
                ticket += articuloCuenta(arti)+"\n";
            });
            ticket+= "\n"+"Total a pagar: "+ImporteTotal;
            return ticket;
        }

        public String articuloCuenta(Articulos arti)
        {
            String ticketElem = "";
            String prod;
            String cant;
            String importeProd="";
            if (arti.articulo.Length >= 15)
            {
                prod = arti.articulo.Substring(0, 15);
            }
            else
            {
                prod = arti.articulo;
                while (prod.Length < 16)
                {
                    prod += " ";
                }
            }
            cant = arti.cantidad.ToString();
            while (cant.Length < 10)
            {
                cant += " ";
            }

            String auxImporteProd = (arti.cantidad * arti.precio).ToString();
            while (auxImporteProd.Length < 10)
            {
                importeProd += " ";
            }
            importeProd += auxImporteProd;

            ImporteTotal += arti.cantidad * arti.precio;
            ticketElem = prod + cant + importeProd;
            return ticketElem;
        }
    }
}
