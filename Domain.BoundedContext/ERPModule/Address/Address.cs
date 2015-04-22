namespace Domain.BoundedContext.ERPModule
{
    using Domain.BoundedContext.Resources;
    using Domain.Core;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    /// <summary>
    /// Address  information  
    /// </summary>
    [Table("Addresses")]
    public class Address : EntityBase
    {

        #region Properties

        /// <summary>
        /// Get or set the city of this address 
        /// </summary>
        [Required(ErrorMessageResourceName = "AddressCityRequired", ErrorMessageResourceType = typeof(Messages))]
        public string City { get; set; }

        /// <summary>
        /// Get or set the zip code
        /// </summary>
       
        public string ZipCode { get; set; }

        /// <summary>
        /// Get or set address line 1
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Get or set address line 2
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Get or set CountryGeoId 
        /// </summary>
        [Required(ErrorMessageResourceName = "AddressCountryRequired", ErrorMessageResourceType = typeof(Messages))]
        public int CountryGeoId { get; set; }

        #endregion
    }
}
