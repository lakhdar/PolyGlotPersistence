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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// Application Profile  information  
    /// </summary>
    [Table("Profiles")]
    public class Profile : EntityBase
    {

        #region Properties

        /// <summary>
        /// Get or set PropertyName
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Get or set PropertyValue
        /// </summary>
        public string PropertyValue { get; set; }

        #endregion
    }

}