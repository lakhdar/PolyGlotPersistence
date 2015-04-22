using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET.MVC5.Client.Models
{
    public class FacilityViewModels
    {
    }

    public class FacilityItelViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }


    public class FacilityListViewModel
    {
        public int Total { get; set; }
        public IEnumerable<FacilityItelViewModel> Facilities { get; set; }
    }

    public class FacilityCreateViewModel
    {

        public string Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Facility Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}