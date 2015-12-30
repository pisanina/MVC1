using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTutorial.Models
{
    public class OgloszenieSzukajViewModel
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Opis { get; set; }
        public int SprzedajacyID { get; set; }
        public decimal CenaOd { get; set; }
        public decimal CenaDo { get; set; }
    }
}