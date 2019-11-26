using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Semana09ASPNet.Models
{
    /*
    dnicont char(8) primary key,
    nomcont varchar(255) not null,
    apecont varchar(255) not null,
    dircont varchar(255) not null,
    iddis int references DISTRITO
     */
    public class Contribuyente
    {
        [Display(Name = "Dni" , Order = 0)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingresa el DNI")]
        public string dnicont { get; set; }

        [Display(Name = "Nombre", Order = 1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingresa el Nombre")]
        public string nomcont { get; set; }

        [Display(Name = "Apellido", Order = 2)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingresa el Apellido")]
        public string apecont { get; set; }

        [Display(Name = "Dirección", Order = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingresa el Dirección")]
        public string dircont { get; set; }

        [Display(Name = "Distrito", Order = 4)]
        public int iddis { get; set; }
    }
}