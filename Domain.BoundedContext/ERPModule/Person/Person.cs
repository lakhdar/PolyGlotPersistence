namespace Domain.BoundedContext.ERPModule 
{
    using Domain.BoundedContext.Resources;
    using Domain.Core;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

     public abstract class Person
        : EntityBase
    {
        /// <summary>
        /// Get or set if this Employee is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Get or set Civility of this  Employee
        /// </summary>
        [Required]
        public Civility TitleOfCourtesy { get; set; }

        /// <summary>
        /// Get or set even is male or female   
        /// </summary>
        [Required]
        public Gender Gender { get; set; }

        /// <summary>
        /// Get or set the Given name of this customer
        /// </summary>
        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(Messages))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Messages))]
        public string FirstName { get; set; }

        /// <summary>
        /// Get or set the surname of this customer
        /// </summary>
        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(Messages))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Messages))]
        public string LastName { get; set; }

        /// <summary>
        /// Get or set the full name of this customer
        /// </summary>
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", this.LastName, this.FirstName);
            }
        }

        /// <summary>
        /// Get or set the Given name of this customer
        /// </summary>
        public byte[] Photo { get; set; }
        /// <summary>
        /// Get or set the telephone 
        /// </summary>
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "InvalidPhoneNumber", ErrorMessageResourceType = typeof(Messages))]
        public string PhoneNumber { get; set; } 

        /// <summary>
        /// Get or set the Email 
        /// </summary>

        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Messages))]
        public string Email { get; set; }

        /// <summary>
        /// Get or set the Date of birth 
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Get or set associated Address identifier
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// Get the current associated Address for this Person
        /// </summary>
        [ForeignKey("AddressId")]
         public  Address Address { get; set; }
     
    }
}
