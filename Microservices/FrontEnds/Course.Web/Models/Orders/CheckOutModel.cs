using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Models.Orders
{
    public class CheckOutModel
    {
        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "District")]
        public string District { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Address")]
        public string Line { get; set; }

        [Display(Name = "Card First Name and Last Name")]
        public string CardName { get; set; }

        [Display(Name = "Cart Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration Date (Month/Year)")]
        public string Expiration { get; set; }

        [Display(Name = "CVV/CVC2 number")]
        public string CVV { get; set; }
    }
}
