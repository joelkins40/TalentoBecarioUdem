using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalentoBecario.Models.Entity
{
    public class Formador 
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Direccion { get; set; }
        public string Vicerreptoria { get; set; }
        public List<AreaInteres> listAreaInteres { get; set; }
        public List<AreaInteres> listHabilidades { get; set; }
    }
}