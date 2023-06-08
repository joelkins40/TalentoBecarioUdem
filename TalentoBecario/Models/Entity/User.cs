using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalentoBecario.Models.Entity
{
    public class User
    {
        public string Id { get; set; }
        public string Pidm { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string Program { get; set; }
        public string Gender { get; set; }
        public string StudentType { get; set; }
        public string IsEmployee { get; set; }
    }
}