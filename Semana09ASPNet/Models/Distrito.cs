using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Semana09ASPNet.Models
{
    public class Distrito
    {
        [Display(Name="ID Distrito", Order = 0)]
        public int iddis { get; set; }


        [Display(Name = "Distrito", Order = 1)]
        public string nomdis { get; set; }
    }
}