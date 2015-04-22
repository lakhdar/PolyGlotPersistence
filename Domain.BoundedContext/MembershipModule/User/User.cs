namespace Domain.BoundedContext.MembershipModule
{
    using Domain.BoundedContext.ERPModule;
    using Domain.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Application User  information  
    /// </summary>
    [Table("Users")]
    public class User : Person
    {

        #region Properties

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// True if the email is confirmed, default is false
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Get or set User's login
        /// </summary>
        [Required(ErrorMessageResourceName = "UserLoginRequired", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string UserName { get; set; }

        /// <summary>
        /// Get or Set Normalized User Name
        /// </summary>
        public virtual string NormalizedUserName { get; set; }

        /// <summary>
        /// The salted/hashed form of the user password
        /// </summary>
        //[Required(ErrorMessageResourceName = "UserPasswordRequired", ErrorMessageResourceType = typeof(Resources.Messages))]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// A random value that should change whenever a users credentials change (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }


        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTimeOffset LockoutEnd { get; set; }


        /// <summary>
        /// Get or set the last activity date 
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Get or set associated Profile identifier
        /// </summary>
        public Guid? ProfileId { get; set; }

        /// <summary>
        /// Get the current associated Profile for this User
        /// </summary>
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }

        /// <summary>
        /// Get or set User's  roles
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        ///     Claims for the user
        /// </summary>
        public virtual ICollection<Claim> Claims { get; set; }

        /// <summary>
        ///     Associated logins for the user
        /// </summary>
        public virtual ICollection<Login> Logins { get; set; }

        


        #endregion


        public User()
            : base()
        {
            this.LastActivityDate = DateTime.UtcNow;
        }
    }
}

