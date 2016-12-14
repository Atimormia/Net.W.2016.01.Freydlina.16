using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ShawarmaProject.WebUI.ViewModels
{
    public class RevenueViewModel
    {
        [Required]
        public string SellingPointTitle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartPeriod { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndPeriod { get; set; }
    }

    public class SallaryViewModel
    {
        [Required]
        public string SellerName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartPeriod { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndPeriod { get; set; }

        [Required]
        public decimal WorkingRate { get; set; }

        [Required]
        public decimal CookingRate { get; set; }
    }
}
