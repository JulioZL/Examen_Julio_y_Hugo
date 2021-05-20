using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServerException:Exception
    {
        public ServerException():base("No se pudo establecer comunicación con el servidor, contacta al administrador") { 
            
        }
        /* Sería equivalente a esto en java
        public ServerException(){
            super("No se pudo establecer comunicación con el servidor, contacta al administrador");
        }
        
         */
    }
}
