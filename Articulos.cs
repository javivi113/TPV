using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Properties;

namespace TPV
{
    public class Articulos
    {
        public String articulo;
        public double precio;
        public int cantidad;
        public double impuestos;
        public String tipo;
        public String png;
        public Articulos(String art, double pre, int cant, double imp, String type)
        {
            articulo= art;
            precio = pre;
            cantidad=cant;
            impuestos=imp;
            tipo=type;
        }

        public String getTipo() { return tipo; }
        public String getArticulo(){return articulo;}
        public double getPrecio() { return precio;}
        public int getCantidad(){return cantidad;}
        public void cantidadMas()
        {
            cantidad++;
        }
        public void setCantidad(int cant) { cantidad = cant; }
        public double getImpuestos() { return impuestos; }
        public Image getPng()
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
        
    }
}
