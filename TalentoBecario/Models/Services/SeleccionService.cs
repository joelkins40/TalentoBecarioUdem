using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TalentoBecario.Models.Entity;

namespace TalentoBecario.Models.Services
{
    public class SeleccionService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["CONNSQL"].ConnectionString;

     

    }
}