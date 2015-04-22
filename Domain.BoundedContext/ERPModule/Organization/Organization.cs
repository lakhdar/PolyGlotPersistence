namespace Domain.BoundedContext.ERPModule
{
    using Domain.BoundedContext.Resources;
    using Domain.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Aggregate root for Organization Aggregate.
    /// </summary>
    [Table("Organizations")] 
    public class Organization
        : EntityBase
    {

        #region Properties


        /// <summary>
        /// Get or set the Given name of this customer
        /// </summary>
        [Required(ErrorMessageResourceName = "OrganizationNameRequired", ErrorMessageResourceType = typeof(Messages))]
        [MaxLength(50, ErrorMessageResourceName = "OrganizationNameMaxLength", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the Address identifier 
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// Get or set the address  
        /// </summary>
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        /// <summary>
        /// Get or set associated Customer identifier
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Get the current Customer 
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Get or set the Departments list 
        /// </summary>
        public virtual ICollection<Department> Departments { get; set; }

        #endregion

    }
}
