using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET.MVC5.Client.Models
{
    public class DepartmentViewModels
    {
    }


    public class DepartmentemViewModel
    {
        public string Id { get; set; }
        public string FacilityName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentListViewModel
    {
        public int Total { get; set; }
        public IEnumerable<DepartmentemViewModel> Departments { get; set; }
    }

    public class DepartmentCreateViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Facility Name")]
        public string FacilityId { get; set; }

        public IEnumerable<SelectListItem> FacilitiesModel { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}