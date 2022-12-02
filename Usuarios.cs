using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV
{
    public class Usuarios
    {
        public static String username;
        public static String password;
        public static Boolean admin;

        public Usuarios(String u, String p, Boolean a)
        {
            username = u;
            password = p;
            admin = a;
        }
        public String getUsername(){return username;}
        public void setUsername(String u) { username = u;}
        public String getPassword() { return password;}
        public void setPassword(String p){password = p;}
        public String getAdmin() 
        {
            if (!admin){return "Empleado";}
            return "Administrador";
                
        }
        public void setAdmin(Boolean a){admin = a;}
    }
}
