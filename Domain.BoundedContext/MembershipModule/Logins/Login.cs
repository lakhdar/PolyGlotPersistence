
namespace Domain.BoundedContext.MembershipModule
{
    using Domain.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// Application Login  information  
    /// </summary>
    [Table("Logins")]
    public class Login : EntityBase
    {

        #region Properties
        /// <summary>
        ///     The login provider for the login (i.e. facebook, google)
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        ///     Key representing the login for the provider
        /// </summary>
        public virtual string ProviderKey { get; set; }

        /// <summary>
        ///     Display name for the login
        /// </summary>
        public virtual string ProviderDisplayName { get; set; }

        /// <summary>
        /// Get or set User id 
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        ///     User   for the user who owns this login
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

         
        #endregion
    }

}