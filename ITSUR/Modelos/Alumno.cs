using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    //plain old java object (POJO)
    //plain old c# object (POCO) 
    public class Alumno
    {
        public String NoControl { get; set; }
        public String Nombre { get; set; }
        public String Apellido1 { get; set; }
        public String Apellido2 { get; set; }
        public String Telefono { get; set; }
        public DateTime FechaNac { get; set; }
        public int ClaveCarrera { get; set; }
   
    }
}
