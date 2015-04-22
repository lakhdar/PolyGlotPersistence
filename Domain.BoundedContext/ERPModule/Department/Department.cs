namespace Domain.BoundedContext.ERPModule
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain.Core;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.BoundedContext.Resources;

    /// <summary>
    /// Aggregate root for Department Aggregate.
    /// </summary>
    [Table("Departments")]
    public class Department
        : EntityBase
    {

        #region Properties

        /// <summary>
        /// Get or set the Given name of this customer
        /// </summary>
        [Required(ErrorMessageResourceName = "DepartmentNameRequired", ErrorMessageResourceType = typeof(Messages))]
        [MaxLength(50, ErrorMessageResourceName = "DepartmentNameMaxLength", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        /// <summary>
        /// Get or set associated Organization identifier
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Get the current Organization 
        /// </summary>
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Get or set the Employees list 
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// Get or set the Position list 
        /// </summary>
        public virtual ICollection<Position> Positions { get; set; } 

        #endregion

    }
}
