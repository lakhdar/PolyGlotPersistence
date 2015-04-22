namespace Domain.BoundedContext.ERPModule
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain.Core;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.BoundedContext.Resources;

    /// <summary>
    /// Aggregate root for Employee Aggregate.
    /// </summary>
    [Table("Employees")]
    public class Employee
        : Person
    {

        #region Properties

        /// <summary>
        /// Get or set the Date of Creation 
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Get or set the Postion identifier 
        /// </summary>
        public Guid PostionId { get; set; }

        /// <summary>
        /// Get or set the Postion
        /// </summary>
        [Required(ErrorMessageResourceName = "PositionRequired",ErrorMessageResourceType=typeof (Messages))]
        [ForeignKey("PostionId")]
        public Position Postion { get; set; }
        
        /// <summary>
        /// Get or set associated Department identifier
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Get the current Department for this employee
        /// </summary>
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        #endregion

    }
}
