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
        public void addArticulo(Articulos art)
        {
            art.setCantidad(1);
            bool exists = false;
            cuenta.ForEach((a) =>
            {
                if (a == art)
                {
                    a.cantidadMas();
                    exists = true;
                }
            });
            if (!exists)
            {
                cuenta.Add(art);
            }
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
