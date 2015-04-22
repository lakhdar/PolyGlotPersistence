
namespace Domain.BoundedContext.MembershipModule
{
    using Domain.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// Application Claim  information  
    /// </summary>
    [Table("Claims")]
    public class Claim : EntityBase
    {

        #region Properties

        /// <summary>
        ///     Claim type
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        public virtual string ClaimValue { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Get or set  User Id for the user who owns this claim
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