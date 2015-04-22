//===================================================================================
// 
//=================================================================================== 
// 
// 
// 
//===================================================================================
// 
// 
// 
//===================================================================================

namespace Domain.BoundedContext.MembershipModule
{
    using Domain.Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// Application Role  information  
    /// </summary>
    [Table("Roles")]
    public class Role : EntityBase
    {

        #region Properties

        /// <summary>
        /// Get or set Role Name
        /// </summary>
        [Required(ErrorMessageResourceName = "RoleNameRequired", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string Name { get; set; }

        /// <summary>
        /// Get or set User associated with this role
        /// </summary>
        public ICollection<User> Users { get; set; }
        
        /// <summary>
        ///     Navigation property for claims in the role
        /// </summary>
        public virtual ICollection<Claim> Claims { get; set; }

        #endregion

    }

}
