using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalentoBecario.Models.Entity
{
    public class Proyecto
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string estatus { get; set; }
        public List<AreaInteres> listAreaInteres { get; set; }
        public List<Habilidad> listHabilidades { get; set; }

    }
}