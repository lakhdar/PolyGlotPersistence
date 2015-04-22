using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET.MVC5.Client.Models
{
    public class EquipmentViewModels
    {
    }
    public class EquipmentItemViewModel
    {
        public string Id { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string EquipmentModel { get; set; }
        public string CreationDate { get; set; }
        public string Description { get; set; } 
    }


    public class EquipmentListViewModel
    {
        public int Total { get; set; }
        public IEnumerable<EquipmentItemViewModel> Equipment { get; set; } 
    }

    public class EquipmentCreateViewModel
    {

        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Equipment Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string EquipmentModel { get; set; }

         [Required]
         [Display(Name = "Department Name")]
        public string DepartmentId { get; set; }
        public IEnumerable<SelectListItem> FacilitiesModel  { get; set; } 

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }


     
}