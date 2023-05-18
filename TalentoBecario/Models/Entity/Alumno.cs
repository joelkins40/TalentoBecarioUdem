using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalentoBecario.Models.Entity
{
    public class Alumno
    {

        public string idAlumno { get; set; }
        public string nombre { get; set; }
        public string nivel { get; set; }
        public string horario { get; set; }
        public string formador { get; set; }
        public string estatus { get; set; }
        public string carrera { get; set; }
        
        public List<AreaInteres> listAreaInteres { get; set; }
        public List<AreaInteres> listHabilidades { get; set; }

    }
}