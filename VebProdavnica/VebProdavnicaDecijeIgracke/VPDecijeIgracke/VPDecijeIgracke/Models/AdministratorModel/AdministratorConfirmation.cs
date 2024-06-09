﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VPDecijeIgracke.Models.AdministratorModel
{
    public class AdministratorConfirmation
    {
        public int AdminID { get; set; }
        public string ImeAdmin { get; set; }

        public string PrezimeAdmin { get; set; }

        public string EmailAdmin { get; set; }

        public string KorisnickoImeAdmin { get; set; }

        public string LozinkaAdmin { get; set; }

        public string AdresaAdmin { get; set; }

        public string TelefonAdmin { get; set; }
    }
}
