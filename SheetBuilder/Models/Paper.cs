using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SheetBuilder.Models;

namespace SheetBuilder.Models
{
    public class Paper
    {
        public List<Asignacion> Asignaciones { get; set; }
        public string Fecha { get; set; }

        public Paper()
        {
            this.Asignaciones = new List<Asignacion>();
        }
    }
}
