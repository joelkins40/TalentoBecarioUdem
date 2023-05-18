using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalentoBecario.Models.Entity
{
    public class Formador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<AreaInteres> listAreaInteres { get; set; }
        public List<AreaInteres> listHabilidades { get; set; }
    }
}