namespace Domain.BoundedContext.ERPModule
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain.Core;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.BoundedContext.Resources;

    /// <summary>
    /// Aggregate root for Position Aggregate.
    /// </summary>
    [Table("Positions")]
    public class Position
        : EntityBase
    {

        #region Members

        #endregion

        #region Properties


        /// <summary>
        /// Get or set the Given name of this customer
        /// </summary>
        [Required(ErrorMessageResourceName = "PositionTitleRequired", ErrorMessageResourceType = typeof(Messages))]
        [MaxLength(50, ErrorMessageResourceName = "PositionTitleMaxLength", ErrorMessageResourceType = typeof(Messages))]
        public string Title { get; set; }


        #endregion

    }
}
