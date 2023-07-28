using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalentoBecario.Models.Entity
{
    public class Comunicado
        
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public int tipo { get; set; }
        public string descripcion { get; set; }
        public string visible { get; set; }
    }
}