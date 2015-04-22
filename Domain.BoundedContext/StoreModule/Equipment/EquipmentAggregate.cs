namespace Domain.BoundedContext.StoreModule
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Core;

    public class EquipmentAggregate:EntityBase
    {
 
        /// <summary>
        /// Get or Set DepartmentId
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Get or set the Department name 
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Get or set the name 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

    }
}
