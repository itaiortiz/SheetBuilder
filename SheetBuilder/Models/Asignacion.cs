using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheetBuilder.Models
{
    public class Asignacion
    {
        public string Asignado { get; set; }
        public string Ayudante { get; set; }
        public bool Lectura { get; set; }
        public bool PrimeraConversacion { get; set; }
        public bool PrimeraRevisita { get; set; }
        public bool SegundaRevisita { get; set; }
        public bool TerceraRevisita { get; set; }
        public bool CursoBiblico { get; set; }
        public bool Discurso { get; set; }
        public bool Otro { get; set; }
        public string Leccion { get; set; }
        public bool SalaPrincipal { get; set; }
        public bool SalaAux1 { get; set; }
        public bool SalaAux2 { get; set; }
        public string Fecha { get; set; }
    }

}
