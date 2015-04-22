namespace Domain.BoundedContext.ERPModule
{
    using Domain.Core;
    using System;

   public class DepartmentAggregate : EntityBase
    {

        public string Name { get; set; }

        /// <summary>
        /// Get or set associated Organization identifier
        /// </summary>
        public Guid FacilityId { get; set; }

        /// <summary>
        /// Get or set associated Organization Name
        /// </summary>
        public string FacilityName { get; set; }


        /// <summary>
        /// Get or set associated Organization Description
        /// </summary>
        public string Description { get; set; }
    }
}
