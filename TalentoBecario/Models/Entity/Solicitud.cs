using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalentoBecario.Models.Entity
{
    public class Solicitud
        
    {
        public int id { get; set; }
        public Alumno alumno { get; set; }
        public Proyecto proyecto { get; set; }
        public string comentario { get; set; }
        public string estatus { get; set; }
    }
}