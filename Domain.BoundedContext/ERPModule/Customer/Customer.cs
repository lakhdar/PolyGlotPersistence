namespace Domain.BoundedContext.ERPModule
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain.Core;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.BoundedContext.Resources;

    /// <summary>
    /// Aggregate root for Customer Aggregate.
    /// </summary>
    [Table("Customers")]
    public class Customer
        : EntityBase
    {


        #region Properties

        /// <summary>
        /// Get or set the Given name of this customer
        /// </summary>
        [Required(ErrorMessageResourceName = "CustomerNameRequired", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the Given name of this customer
        /// </summary>
        public byte[] Logo { get; set; }
        /// <summary>
        /// Get or set the telephone 
        /// </summary>
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "InvalidPhoneNumber", ErrorMessageResourceType = typeof(Messages))]
        public string Phone { get; set; }

        /// <summary>
        /// Get or set the Email 
        /// </summary>
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Messages))]
        public string Email { get; set; }

        /// <summary>
        /// Get or set if this customer is enabled
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Get or set if this associated address identifier
        /// </summary>

        public Guid AddressId { get; set; }

        /// <summary>
        /// Get or set the address of this customer
        /// </summary>
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        /// <summary>
        /// Get or set the Organization list 
        /// </summary>
        public virtual ICollection<Organization> Organizations { get; set; }

        #endregion




    }
}
