using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Grupo
    {
        public int Id { get; set; }
        public String ClaveGrupo { get; set; }
        public int ClaveMateria { get; set; }
        public int ClaveCarrera { get; set; }
        public byte Cupo { get; set; }
        public byte Horario { get; set; }
        public String Dias { get; set; }
    }
}
